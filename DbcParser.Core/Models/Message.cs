namespace DbcParser.Models;

public class Message
{
    public int id { get; init; }
    public string name { get; init; } = string.Empty;
    public int length { get; init; }
    public List<Signal> signals { get; } = new();
    
    public Signal? GetSignal(string name)
    {
        foreach (var sig in signals)
        {
            if (sig.name == name)
            {
                return sig;
            }
        }

        return null;
    }
}