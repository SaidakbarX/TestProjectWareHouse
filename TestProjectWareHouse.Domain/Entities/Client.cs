namespace TestProjectWareHouse.Domain.Entities;

public class Client
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public bool IsArchived { get; set; }

    public ICollection<ShipmentDocument> ShipmentDocuments { get; set; }
}

