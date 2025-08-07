namespace TestProjectWareHouse.Domain.Entities;

public class Resource
{
    public long Id { get; set; }
    public string Name { get; set; } 
    public bool IsArchived { get; set; }

    public ICollection<Balance> Balances { get; set; }
    public ICollection<ReceptionItem> ReceptionItems { get; set; }
    public ICollection<ShipmentItem> ShipmentItems { get; set; }
}
