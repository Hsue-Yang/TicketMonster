using System;
using System.Collections.Generic;

namespace TicketMonster.ApplicationCore.Entities;

public partial class Venue : BaseEntity
{
    /// <summary>
    /// 1
    /// </summary>
 

    /// <summary>
    /// 台北小巨蛋
    /// </summary>
    public string VenueName { get; set; }

    /// <summary>
    /// 台北市
    /// </summary>
    public string Location { get; set; }

    /// <summary>
    /// 2000
    /// </summary>
    public string Capacity { get; set; }

    /// <summary>
    /// 113.2456754
    /// </summary>
    public decimal Latitude { get; set; }

    /// <summary>
    /// 48.123468
    /// </summary>
    public decimal Longitude { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime ModifyTime { get; set; }

    public string CreateBy { get; set; }

    public string LastEditBy { get; set; }

    /// <summary>
    /// Svg
    /// </summary>
    public string Pic { get; set; }

    public int SectionNum { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<SeatNum> SeatNums { get; set; } = new List<SeatNum>();

    public virtual ICollection<SeatSection> SeatSections { get; set; } = new List<SeatSection>();

    public virtual ICollection<VenueArea> VenueAreas { get; set; } = new List<VenueArea>();
}
