using TestProjectWareHouse.Domain.Entities;

namespace TestProjectWareHouse.Application.Dtos;

public class ShipmentDocumentUpdateDto
{
    public long Id { get; set; }
    public string Number { get; set; }
    public DateTime Date { get; set; }
    public long ClientId { get; set; }
    public ShipmentStatus Status { get; set; }
    public List<ShipmentItemUpdateDto> Items { get; set; }
}
