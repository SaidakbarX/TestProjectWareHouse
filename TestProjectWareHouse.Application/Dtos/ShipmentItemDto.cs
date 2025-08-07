namespace TestProjectWareHouse.Application.Dtos;

public class ShipmentItemDto
{
    public long Id { get; set; }
    public long ResourceId { get; set; }
    public string ResourceName { get; set; }

    public long MeasurementId { get; set; }
    public string MeasurementName { get; set; }

    public long Quantity { get; set; }
}
