using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMonster.ApplicationCore.Entities;

namespace TicketMonster.ApplicationCore.Interfaces
{   
    public interface CreateTempOrderRepository : IRepository<TempOrder>
    {
        void CreateTempOrderAndDetail(TempOrder temporder, IEnumerable<TempOrderDetail> temporderDetails);      
    }
}
