using System;
using System.Collections.Generic;

namespace TicketMonster.ApplicationCore.Entities;

public partial class EventSeat : BaseEntity
{
    /// <summary>
    /// 1
    /// </summary>
  

    /// <summary>
    /// 2
    /// </summary>
    public int EventId { get; set; }

    /// <summary>
    /// 001
    /// </summary>
    public string SeatRowBegin { get; set; }

    /// <summary>
    /// 100
    /// </summary>
    public string SeatRowEnd { get; set; }

    /// <summary>
    /// 區塊名稱
    /// </summary>
    public string SeatArea { get; set; }

    /// <summary>
    /// 78.54
    /// </summary>
    public decimal SeatPrice { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime ModifyTime { get; set; }

    public string CreateBy { get; set; }

    public string LastEditBy { get; set; }

    public string SeatList { get; set; }

    public virtual Event Event { get; set; }
}
