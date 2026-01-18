namespace DbcParser.Models;

public class Network
{
    public List<Message> Messages { get; set; } = new();
    public List<string> Nodes { get; set; } = new();
}