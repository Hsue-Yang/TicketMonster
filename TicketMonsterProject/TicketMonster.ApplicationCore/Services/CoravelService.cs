using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coravel.Invocable;
using TicketMonster.ApplicationCore.Entities;
using TicketMonster.ApplicationCore.Interfaces;

namespace TicketMonster.ApplicationCore.Services
{
    public class CoravelService
    {
        public class ReserveCheck : IInvocable
        {
            private readonly IRepository<SeatNum> _SeatNumRepo;

            public ReserveCheck(IRepository<SeatNum> seatNum)
            {
                _SeatNumRepo = seatNum;
            }

            public async Task Invoke()
            {
                await IfOrderStatusEqualThenDelete(true, 10);
            }

            public async Task IfOrderStatusEqualThenDelete(bool IsOrdered, int DeleteTime)
            {
                var time = DateTime.Now;

                var seatnumlist = await _SeatNumRepo.ListAsync(x => x.IsOrdered == IsOrdered && time > x.RetainTime.AddMinutes(DeleteTime));
                if (seatnumlist.Count > 0)
                {
                    foreach (var item in seatnumlist)
                    {
                        item.IsOrdered = false;
                        item.RetainTime = DateTime.Now.AddYears(54);
                        await _SeatNumRepo.UpdateAsync(item);
                    }
                }
            }
        }
    }
}
