using Moq;
using TicketMonster.ApplicationCore.Entities;
using TicketMonster.ApplicationCore.Interfaces;
using TicketMonster.ApplicationCore.Services;

[TestFixture]
public class CountOrderTotalMoneyServiceTests
{
    [Test]
    public void CalculateOrderDetails_ReturnsCorrectValues()
    {
        // Arrange 
        var mockSeatSectionRepo = new Mock<IRepository<SeatSection>>();
        var countOrderTotalMoneyService = new CountOrderTotalMoneyService(mockSeatSectionRepo.Object);
        decimal ticketUnitPrice = 600.0m;
        int ticketCount = 3;

        // Act
        var (OrderTotalMoney, Tax, OrderProcessingFee, ServiceFee) = countOrderTotalMoneyService.CalculateOrderDetails(ticketUnitPrice, ticketCount);

        // Assert       
        Assert.That(OrderTotalMoney, Is.EqualTo(2106.0m)); // 預期的訂單總額
        Assert.That(Tax, Is.EqualTo(90.0m)); // 預期的稅額
        Assert.That(OrderProcessingFee, Is.EqualTo(36.0m)); // 預期的訂單處理費
        Assert.That(ServiceFee, Is.EqualTo(180.0m)); // 預期的服務費
    }
}
