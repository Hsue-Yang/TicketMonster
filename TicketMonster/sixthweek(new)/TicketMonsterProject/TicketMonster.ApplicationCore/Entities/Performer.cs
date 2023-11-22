using System;
using System.Collections.Generic;

namespace TicketMonster.ApplicationCore.Entities;

public partial class Performer : BaseEntity
{
    /// <summary>
    /// 1
    /// </summary>


    /// <summary>
    /// BlackPink
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Kpop
    /// </summary>
    public string About { get; set; }

    /// <summary>
    /// 1
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// 2
    /// </summary>
    public int SubCategoryId { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime ModifyTime { get; set; }

    public string CreateBy { get; set; }

    public string LastEditBy { get; set; }

    public virtual Category Category { get; set; }

    public virtual ICollection<EventPerform> EventPerforms { get; set; } = new List<EventPerform>();

    public virtual ICollection<PerformerPic> PerformerPics { get; set; } = new List<PerformerPic>();

    public virtual SubCategory SubCategory { get; set; }
}
