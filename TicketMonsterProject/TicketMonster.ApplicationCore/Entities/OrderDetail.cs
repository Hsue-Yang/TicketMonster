namespace TicketMonster.ApplicationCore.Entities;

public partial class OrderDetail : BaseEntity
{
    public int OrderId { get; set; }

    public string EventSeat { get; set; }

    public decimal Price { get; set; }

    public string Barcode { get; set; }

    public DateTime? ScannedTime { get; set; }

    public bool IsUsed { get; set; }

    public virtual Order Order { get; set; }
}