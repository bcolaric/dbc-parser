namespace DbcParser.Models;

public class Network
{
    public List<Message> messages { get; } = new();
    
    public Message? GetMessageById(int id)
    {
        foreach (var msg in messages)
        {
            if (msg.id == id)
            {
                return msg;
            }
        }

        return null;
    }

}