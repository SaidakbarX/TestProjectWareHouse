namespace TestProjectWareHouse.Application.Dtos;

public class ReceptionDocumentDto
{
    public long Id { get; set; }
    public string Number { get; set; }
    public DateTime Date { get; set; }

    public List<ReceptionItemDto> Items { get; set; }
}
