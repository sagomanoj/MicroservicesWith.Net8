using Azure.Messaging.ServiceBus;
using Mango.Services.EmailAPI.Message;
using Mango.Services.EmailAPI.Models.Dto;
using Mango.Services.EmailAPI.Services;
using Newtonsoft.Json;
using System.Text;

namespace Mango.Services.EmailAPI.Messaging
{
    public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    {
        private readonly EmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly string? serviceBusConnectionString;
        private readonly string emailCartQueue;
        private readonly string registerUserQueue;
        private readonly string orderCreateTopic;
        private readonly string orderCreated_EmailSubscription;
        private readonly ServiceBusProcessor _emailCartProcesor;
        private readonly ServiceBusProcessor _registerUserProcesor;
        private readonly ServiceBusProcessor _orderCreatedEmailSubscriptionProcesor;

        public AzureServiceBusConsumer(IConfiguration configuration, EmailService emailService)
        {
            _emailService = emailService;
            _configuration = configuration;
            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            emailCartQueue = _configuration.GetValue<string>("TopicAndQueueNames:EmailShoppingCartQueue");
            registerUserQueue = _configuration.GetValue<string>("TopicAndQueueNames:RegisterUserQueue");
            orderCreateTopic = _configuration.GetValue<string>("TopicAndQueueNames:OrderCreatedTopic");
            orderCreated_EmailSubscription = _configuration.GetValue<string>("TopicAndQueueNames:OrderCreated_Rewards_Subscription");

            var client = new ServiceBusClient(serviceBusConnectionString);

            _emailCartProcesor = client.CreateProcessor(emailCartQueue);
            _registerUserProcesor = client.CreateProcessor(registerUserQueue);
            _orderCreatedEmailSubscriptionProcesor = client.CreateProcessor(orderCreateTopic, orderCreated_EmailSubscription);

        }

        public async Task Start()
        {
            _emailCartProcesor.ProcessMessageAsync += onProcessMessage;
            _emailCartProcesor.ProcessErrorAsync += onError;
            await _emailCartProcesor.StartProcessingAsync();

            _registerUserProcesor.ProcessMessageAsync += OnRegisterUserProcessMessage;
            _registerUserProcesor.ProcessErrorAsync += onError;
            await _registerUserProcesor.StartProcessingAsync();

            _orderCreatedEmailSubscriptionProcesor.ProcessMessageAsync += OnOrderCreatedProcessMessage;
            _orderCreatedEmailSubscriptionProcesor.ProcessErrorAsync += onError;
            await _orderCreatedEmailSubscriptionProcesor.StartProcessingAsync();

        }

        private async Task OnOrderCreatedProcessMessage(ProcessMessageEventArgs args)
        {
            //this is where you will receive message
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            RewardsMessage objMessage = JsonConvert.DeserializeObject<RewardsMessage>(body);
            try
            {
                //TODO - try to log email
                await _emailService.LogOrderPlaced(objMessage);
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task OnRegisterUserProcessMessage(ProcessMessageEventArgs args)
        {
            var message = args.Message;
            var body = Encoding.UTF8.GetString(message.Body);

            string email = JsonConvert.DeserializeObject<string>(body);
            try
            {
                //TODO - try to log email
                await _emailService.RegisterUserEmailAndLog(email);
                await args.CompleteMessageAsync(args.Message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
      
        private async Task onProcessMessage(ProcessMessageEventArgs arg)
        {
            //receiving the message from queue.
            var message = arg.Message;
            var body = Encoding.UTF8.GetString(message.Body);
            
            try
            {
                CartDto cartDto = JsonConvert.DeserializeObject<CartDto>(body);
               await arg.CompleteMessageAsync(message);

                //Log into DB
                _emailService.EmailCartAndLog(cartDto);

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
            await _emailCartProcesor.StopProcessingAsync();
            await _registerUserProcesor.StopProcessingAsync();
            await _emailCartProcesor.DisposeAsync();
            await _registerUserProcesor.DisposeAsync();
            await _orderCreatedEmailSubscriptionProcesor.StopProcessingAsync();
            await _orderCreatedEmailSubscriptionProcesor.DisposeAsync();
        }
    }
}
