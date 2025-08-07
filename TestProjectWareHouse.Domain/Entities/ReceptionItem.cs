namespace TestProjectWareHouse.Domain.Entities;

public class ReceptionItem
{
    public long Id { get; set; }

    public long ReceptionDocumentId { get; set; }
    public ReceptionDocument ReceptionDocument { get; set; }

    public long ResourceId { get; set; }
    public Resource Resource { get; set; }

    public long MeasurementId { get; set; }
    public Measurement Measurement { get; set; }

    public long Quantity { get; set; }
}
