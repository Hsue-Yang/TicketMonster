namespace TicketMonster.ApplicationCore.Entities;

public partial class Order : BaseEntity
{
    public int CustomerId { get; set; }

    public string EventName { get; set; }

    public DateTime EventDate { get; set; }

    public string EventPic { get; set; }

    public string VenueName { get; set; }

    public string VenueLocation { get; set; }

    public string VenueSvg { get; set; }

    public decimal BillingAmount { get; set; }

    public DateTime OrderDate { get; set; }

    public virtual Customer Customer { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
