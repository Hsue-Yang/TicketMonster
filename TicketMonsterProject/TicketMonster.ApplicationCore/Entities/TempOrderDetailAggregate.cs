using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketMonster.ApplicationCore.Entities;

namespace TicketMonster.ApplicationCore.Entities
{
    public partial class TempOrderDetail : BaseEntity
    {
        public TempOrderDetail()
        {
            //EF Core 需要
        }

        public TempOrderDetail(string eventseat,decimal price,string barcode)
        {      
            EventSeat=eventseat;
            Price=price;
            Barcode=barcode;
        }
    }
}

