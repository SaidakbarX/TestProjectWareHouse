using TestProjectWareHouse.Domain.Entities;

namespace TestProjectWareHouse.Application.Dtos;

public class ShipmentDocumentCreateDto
{
    public string Number { get; set; }
    public DateTime Date { get; set; }
    public long ClientId { get; set; }
    public ShipmentStatus Status { get; set; }
    public List<ShipmentItemCreateDto> Items { get; set; }
}
