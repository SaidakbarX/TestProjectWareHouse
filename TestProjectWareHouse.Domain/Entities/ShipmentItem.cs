namespace TestProjectWareHouse.Domain.Entities;

public class ShipmentItem
{
    public long Id { get; set; }

    public long ShipmentDocumentId { get; set; }
    public ShipmentDocument ShipmentDocument { get; set; }

    public long ResourceId { get; set; }
    public Resource Resource { get; set; }

    public long MeasurementId { get; set; }
    public Measurement Measurement { get; set; }

    public long Quantity { get; set; }
}

