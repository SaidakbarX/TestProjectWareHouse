namespace TestProjectWareHouse.Application.Dtos;

public class ReceptionDocumentCreateDto
{
    public string Number { get; set; }
    public DateTime Date { get; set; }
    public List<ReceptionItemCreateDto> Items { get; set; }
}
