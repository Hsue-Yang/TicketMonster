using System;
using System.Collections.Generic;

namespace TicketMonster.ApplicationCore.Entities;

public partial class EventsPic : BaseEntity
{
    /// <summary>
    /// 1
    /// </summary>
  

    /// <summary>
    /// 1
    /// </summary>
    public int EventId { get; set; }

    /// <summary>
    /// svg || url
    /// </summary>
    public string Pic { get; set; }

    /// <summary>
    /// 照片順序
    /// </summary>
    public int Sort { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime ModifyTime { get; set; }

    public string CreateBy { get; set; }

    public string LastEditBy { get; set; }

    public virtual Event Event { get; set; }
}
