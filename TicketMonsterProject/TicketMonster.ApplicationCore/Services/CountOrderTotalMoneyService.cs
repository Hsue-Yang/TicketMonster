using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMonster.ApplicationCore.Entities;
using TicketMonster.ApplicationCore.Interfaces;

namespace TicketMonster.ApplicationCore.Services
{
    public class CountOrderTotalMoneyService
    {
        private readonly IRepository<SeatSection> _seatSectionRepo;

        public CountOrderTotalMoneyService(IRepository<SeatSection> seatSectionRepo)
        {
            _seatSectionRepo = seatSectionRepo;
        }

        public (int OrderTotalMoney, decimal Tax,decimal OrderProcessingFee,decimal ServiceFee) CalculateOrderDetails(decimal ticketUnitPrice, int ticketCount)
        {
            decimal orderOriginMoney = ticketUnitPrice * ticketCount;
            decimal serviceFee = orderOriginMoney * 0.1m;
            decimal orderProcessingFee = orderOriginMoney * 0.02m;
            decimal tax = orderOriginMoney * 0.05m;
            int orderTotalMoney = (int)(orderOriginMoney + serviceFee + orderProcessingFee + tax);
            return (orderTotalMoney, tax, orderProcessingFee, serviceFee); 
        }

        public decimal GetTicketPrice(int eventId,string sectionName)
        {
            var seatSection = _seatSectionRepo.FirstOrDefault(x => x.SectionName == sectionName && x.EventId == eventId);
            var ticketPrice = seatSection.SectionPrice;
            return ticketPrice;
        }
    }
}
