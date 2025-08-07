namespace TestProjectWareHouse.Application.Dtos;

public class ReceptionDocumentUpdateDto
{
    public long Id { get; set; }
    public string Number { get; set; }
    public DateTime Date { get; set; }
    public List<ReceptionItemUpdateDto> Items { get; set; }
}
