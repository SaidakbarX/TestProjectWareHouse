namespace TestProjectWareHouse.Domain.Entities;

public class Balance
{
    public long Id { get; set; }
    public long ResourceId { get; set; }
    public Resource Resource { get; set; }

    public long MeasurementId { get; set; }
    public Measurement Measurement { get; set; }

    public long Quantity { get; set; }
}

