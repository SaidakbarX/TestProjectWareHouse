namespace TestProjectWareHouse.Domain.Entities;

public class ShipmentDocument
{
    public long Id { get; set; }
    public string Number { get; set; } 
    public DateTime Date { get; set; }

    public long ClientId { get; set; }
    public Client Client { get; set; }

    public ShipmentStatus Status { get; set; } 

    public ICollection<ShipmentItem> Items { get; set; }
}

