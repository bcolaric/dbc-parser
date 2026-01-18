namespace DbcParser.Models;

public class Message
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Length { get; set; }
    public string Transmitter { get; set; } = "";
    public List<Signal> Signals { get; set; } = new();
}