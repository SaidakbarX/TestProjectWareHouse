using TestProjectWareHouse.Domain.Entities;

namespace TestProjectWareHouse.Application.Dtos;

public class ShipmentDocumentDto
{
    public long Id { get; set; }
    public string Number { get; set; }
    public DateTime Date { get; set; }
    public long ClientId { get; set; }
    public string ClientName { get; set; }
    public ShipmentStatus Status { get; set; }
    public List<ShipmentItemDto> Items { get; set; }
}
