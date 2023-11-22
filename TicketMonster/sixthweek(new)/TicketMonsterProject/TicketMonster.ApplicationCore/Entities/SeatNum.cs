using System;
using System.Collections.Generic;

namespace TicketMonster.ApplicationCore.Entities;

public partial class SeatNum : BaseEntity
{
   

    public int? VenueId { get; set; }

    public int SeatSectionId { get; set; }

    public string SeatNum1 { get; set; }

    public DateTime RetainTime { get; set; }

    public bool IsOrdered { get; set; }

    public virtual SeatSection SeatSection { get; set; }

    public virtual Venue Venue { get; set; }
}
