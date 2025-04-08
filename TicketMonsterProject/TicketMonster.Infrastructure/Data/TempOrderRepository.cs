using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMonster.ApplicationCore.Entities;
using TicketMonster.ApplicationCore.Interfaces;

namespace TicketMonster.Infrastructure.Data
{
    public class TempOrderRepository : EfRepository<TempOrder> ,CreateTempOrderRepository
    {

        public TempOrderRepository(TicketMonsterContext dbContext) : base(dbContext)
        {
        }

        public void CreateTempOrderAndDetail(TempOrder temporder, IEnumerable<TempOrderDetail> temporderdetails)
        {
            using (var transaction = DbContext.Database.BeginTransaction())
            {
                try
                {
                    Add(temporder);
                    DbContext.SaveChanges();
                    var detailEntites = temporderdetails.ToList();
                    foreach (var entity in detailEntites)
                    {
                        entity.OrderId = temporder.Id;
                    }
                    DbContext.TempOrderDetails.AddRange(detailEntites);
                    DbContext.SaveChanges();
                    transaction.Commit();
                }
                catch{
                    transaction.Rollback();
                }
            }
        }       
    }
}
