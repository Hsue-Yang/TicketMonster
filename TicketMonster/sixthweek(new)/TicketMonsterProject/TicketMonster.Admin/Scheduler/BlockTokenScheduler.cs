﻿using TicketMonster.ApplicationCore.Entities;
using TicketMonster.ApplicationCore.Interfaces;
using Coravel.Invocable;

namespace TicketMonster.Admin.Scheduler
{
    public class BlockTokenScheduler : IInvocable
    {
        private readonly IRepository<BlockToken> _blockTokenRepo;
        private readonly DateTimeOffset _currentTime;
        private readonly ILogger<BlockTokenScheduler> _logger;

        public BlockTokenScheduler(IRepository<BlockToken> blockTokenRepo, ILogger<BlockTokenScheduler> logger)
        {
            _blockTokenRepo = blockTokenRepo;
            _logger = logger;
            _currentTime = DateTimeOffset.UtcNow;
        }

        public Task Invoke()
        {
            RemoveExpiredToken();
            return Task.CompletedTask;
        }

        private void RemoveExpiredToken()
        {
            var tokens = _blockTokenRepo.Where(x => x.ExpireTime < _currentTime).ToList();
            try
            {
                if (!tokens.Any())
                {
                    return;
                }

                _blockTokenRepo.DeleteRange(tokens);
                _logger.LogInformation("Remove blockTokens success!");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }
    }
}
