using System;
using System.Collections.Generic;

namespace TicketMonster.ApplicationCore.Entities;

public partial class SubCategory : BaseEntity
{
    /// <summary>
    /// 1
    /// </summary>

    /// <summary>
    /// Basketball
    /// </summary>
    public string SubCategoryName { get; set; }

    /// <summary>
    /// 1
    /// </summary>
    public int CatagoryId { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime ModifyTime { get; set; }

    public string CreateBy { get; set; }

    public string LastEditBy { get; set; }

    public string Pic { get; set; }

    public virtual Category Catagory { get; set; }

    public virtual ICollection<Event> Events { get; set; } = new List<Event>();

    public virtual ICollection<Performer> Performers { get; set; } = new List<Performer>();
}
