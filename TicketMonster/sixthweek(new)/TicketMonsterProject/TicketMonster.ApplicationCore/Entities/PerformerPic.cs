using System;
using System.Collections.Generic;

namespace TicketMonster.ApplicationCore.Entities;

public partial class PerformerPic : BaseEntity
{
  

    public int PerfomerId { get; set; }

    public string Pic { get; set; }

    public int Sort { get; set; }

    public DateTime CreateTime { get; set; }

    public DateTime ModifyTime { get; set; }

    public string CreateBy { get; set; }

    public string LastEditBy { get; set; }

    public virtual Performer Perfomer { get; set; }
}
