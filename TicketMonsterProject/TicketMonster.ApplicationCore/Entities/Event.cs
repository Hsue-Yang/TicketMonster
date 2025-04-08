using System;
using System.Collections.Generic;

namespace TicketMonster.ApplicationCore.Entities;

public partial class Event : BaseEntity
{
    /// <summary>
    /// 1
    /// </summary>


    /// <summary>
    /// BlackPink
    /// </summary>
    public string EventName { get; set; }

    /// <summary>
    /// 1
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// 1
    /// </summary>
    public int VenueId { get; set; }

    /// <summary>
    /// 2023/12/07
    /// </summary>
    public DateTime EventDate { get; set; }

    /// <summary>
    /// 12(hours)
    /// </summary>
    public decimal TotalTime { get; set; }

    /// <summary>
    /// true
    /// </summary>
    public bool IsDeleted { get; set; }

    /// <summary>
    /// 1
    /// </summary>
    public int SubCategoryId { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime ModifyTime { get; set; }

    public string CreateBy { get; set; }

    public string LastEditBy { get; set; }

    public virtual ICollection<EventPerform> EventPerforms { get; set; } = new List<EventPerform>();

    public virtual ICollection<EventSeat> EventSeats { get; set; } = new List<EventSeat>();

    public virtual ICollection<EventsPic> EventsPics { get; set; } = new List<EventsPic>();

    public virtual ICollection<SeatSection> SeatSections { get; set; } = new List<SeatSection>();

    public virtual SubCategory SubCategory { get; set; }

    public virtual Venue Venue { get; set; }
}
