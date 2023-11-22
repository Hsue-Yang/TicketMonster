using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TicketMonster.ApplicationCore.Entities
{
    public partial class TempOrder : BaseEntity
    {
        public TempOrder()
        {
            //EF Core 需要
        }

        public TempOrder(int customerid, string eventname, DateTime eventdate, string eventpic, string venuename, string venuelocation, decimal latitude, decimal longtitude, ICollection<TempOrderDetail> details)
        {
            CustomerId = customerid;
            EventName = eventname;
            EventDate = eventdate;
            EventPic = eventpic;
            VenueName = venuename;
            VenueLocation= venuelocation;
            //Latitude = latitude;
            //Longitude = longtitude;
        }
    }
}



