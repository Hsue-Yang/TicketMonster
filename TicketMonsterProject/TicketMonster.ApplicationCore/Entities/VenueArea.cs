using System;
using System.Collections.Generic;

namespace TicketMonster.ApplicationCore.Entities;

public partial class VenueArea : BaseEntity
{
    /// <summary>
    /// 1
    /// </summary>
  

    /// <summary>
    /// 1
    /// </summary>
    public int VenuesId { get; set; }

    /// <summary>
    /// A
    /// </summary>
    public string SeatArea { get; set; }

    /// <summary>
    /// 001
    /// </summary>
    public string SeatsCount { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime ModifyTime { get; set; }

    public string CreateBy { get; set; }

    public string LastEditBy { get; set; }

    public virtual Venue Venues { get; set; }
}
