using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMonster.ApplicationCore.Entities;
using TicketMonster.ApplicationCore.Interfaces;

namespace TicketMonster.ApplicationCore.Services
{
    public class TempOrderService
    {
        private readonly IRepository<TempOrder> _temporderRepo;
        private readonly IRepository<TempOrderDetail> _temporderDetailRepo;        

        public TempOrderService(IRepository<TempOrder> temporderRepo,IRepository<TempOrderDetail> temporderDetailRepo)
        {
            _temporderRepo = temporderRepo;
            _temporderDetailRepo = temporderDetailRepo;
        }

        public async Task<TempOrder> GetDataFromTempOrderAsync(string MerchantTradeNo)
        {
            return await _temporderRepo.FirstOrDefaultAsync(x => x.MerchantTradeNo == MerchantTradeNo);     
        }

        public async Task<List<TempOrderDetail>> GetDataFromTempOrderDetailAsync(int OrderId)
        {
            return await _temporderDetailRepo.ListAsync(x => x.OrderId==OrderId);
        }
    }
}
