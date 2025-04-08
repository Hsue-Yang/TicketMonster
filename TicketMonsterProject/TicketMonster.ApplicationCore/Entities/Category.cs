using System;
using System.Collections.Generic;

namespace TicketMonster.ApplicationCore.Entities;

public partial class Category : BaseEntity
{
    /// <summary>
    /// 1
    /// </summary>
 

    /// <summary>
    /// Sports
    /// </summary>
    public string CategoryName { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime ModifyTime { get; set; }

    public string CreateBy { get; set; }

    public string LastEditBy { get; set; }

    public string Pic { get; set; }

    public virtual ICollection<Performer> Performers { get; set; } = new List<Performer>();

    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
