using System;
using System.Collections.Generic;

namespace TicketMonster.ApplicationCore.Entities;

public partial class TempOrder : BaseEntity
{
  

    public int CustomerId { get; set; }

    public string EventName { get; set; }

    public DateTime EventDate { get; set; }

    public string EventPic { get; set; }

    public string VenueName { get; set; }

    public string VenueLocation { get; set; }

    public string VenueSvg { get; set; }

    public string MerchantTradeNo { get; set; }

    public decimal BillingAmount { get; set; }
}
