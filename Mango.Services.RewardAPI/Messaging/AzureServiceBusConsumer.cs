using Azure.Messaging.ServiceBus;
using Mango.Services.RewardAPI.Message;
using Mango.Services.RewardAPI.Messaging;
using Mango.Services.RewardAPI.Services;
using Newtonsoft.Json;
using System.Text;

namespace Mango.Services.RewardAPI.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly RewardService _rewardsService;
        private readonly IConfiguration _configuration;
        private readonly string? serviceBusConnectionString;
        private readonly string orderCreatedTopic;
        private readonly string orderCreated_UpdateRewardsSubscription;
        private readonly ServiceBusProcessor _rewardsProcesor;

        public AzureServiceBusConsumer(IConfiguration configuration, RewardService rewardsService)
        {
            _rewardsService = rewardsService;
            _configuration = configuration;
            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            orderCreatedTopic = _configuration.GetValue<string>("TopicAndQueueNames:OrderCreatedTopic");
            orderCreated_UpdateRewardsSubscription = _configuration.GetValue<string>("TopicAndQueueNames:OrderCreated_Rewards_Subscription");


            var client = new ServiceBusClient(serviceBusConnectionString);

            _rewardsProcesor = client.CreateProcessor(orderCreatedTopic, orderCreated_UpdateRewardsSubscription);


        }

        public async Task Start()
        {
            _rewardsProcesor.ProcessMessageAsync += onProcessMessage;
            _rewardsProcesor.ProcessErrorAsync += onError;
            await _rewardsProcesor.StartProcessingAsync();
        }

      
      
        private async Task onProcessMessage(ProcessMessageEventArgs arg)
        {
            //receiving the message from queue.
            var message = arg.Message;
            var body = Encoding.UTF8.GetString(message.Body);
            
            try
            {
                RewardsMessage rewardMessage = JsonConvert.DeserializeObject<RewardsMessage>(body);
               await arg.CompleteMessageAsync(message);

                //Log into DB
                _rewardsService.UpdateRewards(rewardMessage);

            }
            catch(Exception ex) {
                throw;
            }

        }

        private Task onError(ProcessErrorEventArgs arg)
        {
            Console.WriteLine(arg.Exception.ToString());
            return Task.CompletedTask;
        }

        public async Task Stop()
        {
            await _rewardsProcesor.StopProcessingAsync();
            await _rewardsProcesor.DisposeAsync();
        }
    }
}
