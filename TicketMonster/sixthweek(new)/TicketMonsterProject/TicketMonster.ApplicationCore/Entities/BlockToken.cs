using System;
using System.Collections.Generic;

namespace TicketMonster.ApplicationCore.Entities;

public partial class BlockToken :BaseEntity
{
    public string Token { get; set; }



    public DateTimeOffset ExpireTime { get; set; }
}
