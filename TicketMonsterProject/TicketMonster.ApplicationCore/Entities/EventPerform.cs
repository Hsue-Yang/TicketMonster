using System;
using System.Collections.Generic;

namespace TicketMonster.ApplicationCore.Entities;

public partial class EventPerform : BaseEntity
{
    /// <summary>
    /// 1
    /// </summary>


    /// <summary>
    /// 1 同個活動多個演出者
    /// </summary>
    public int EventId { get; set; }

    /// <summary>
    /// 3
    /// </summary>
    public int PerfomerId { get; set; }

    /// <summary>
    /// 判斷主客隊
    /// </summary>
    public int IsPrimary { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime ModifyTime { get; set; }

    public string CreateBy { get; set; }

    public string LastEditBy { get; set; }

    public virtual Event Event { get; set; }

    public virtual Performer Perfomer { get; set; }
}
