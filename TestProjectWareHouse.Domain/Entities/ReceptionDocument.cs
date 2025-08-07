namespace TestProjectWareHouse.Domain.Entities;

public class ReceptionDocument
{
    public long Id { get; set; }
    public string Number { get; set; }
    public DateTime Date { get; set; }

    public ICollection<ReceptionItem> Items { get; set; }
}

