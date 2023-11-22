using System;
using System.Collections.Generic;

namespace TicketMonster.ApplicationCore.Entities;

public partial class SeatSection : BaseEntity
{
 

    public int EventId { get; set; }

    public int VenueId { get; set; }

    public string SectionName { get; set; }

    public decimal SectionPrice { get; set; }

    public int SectionCapacity { get; set; }

    public virtual Event Event { get; set; }

    public virtual ICollection<SeatNum> SeatNums { get; set; } = new List<SeatNum>();

    public virtual Venue Venue { get; set; }
}
