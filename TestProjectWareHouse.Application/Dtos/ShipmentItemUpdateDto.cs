namespace TestProjectWareHouse.Application.Dtos;

public class ShipmentItemUpdateDto
{
    public long Id { get; set; }
    public long ResourceId { get; set; }
    public long MeasurementId { get; set; }
    public long Quantity { get; set; }
}
