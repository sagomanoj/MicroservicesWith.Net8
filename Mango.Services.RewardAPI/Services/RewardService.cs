using Mango.Services.RewardAPI.Data;
using Mango.Services.RewardAPI.Message;
using Mango.Services.RewardAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Mango.Services.RewardAPI.Services
{
    public class RewardService : IRewardService
    {
        private readonly DbContextOptions<AppDBContext> _dbContextOptions;

        public RewardService(DbContextOptions<AppDBContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }

        public async Task UpdateRewards(RewardsMessage rewardsMessage)
        {
            try
            {
                Rewards rewards = new()
                {
                    OrderId = rewardsMessage.OrderId,
                    RewardsActivity = rewardsMessage.RewardsActivity,
                    UserId = rewardsMessage.UserId,
                    RewardsDate = DateTime.Now
                };

                await using var db = new AppDBContext(_dbContextOptions);
                db.Rewards.Add(rewards);
                await db.SaveChangesAsync();
            }
            catch(Exception ex) { }
        }
    }
}
