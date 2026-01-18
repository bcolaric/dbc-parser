using System.Globalization;
using System.Text.RegularExpressions;
using DbcParser.Models;

namespace DbcParser.Parsers;

public class Parser
{
    private static readonly Regex MessageRegex = new Regex(
        @"^BO_\s+(\d+)\s+(\S+):\s+(\d+)",
        RegexOptions.Compiled
    );

    private static readonly Regex SignalRegex = new Regex(
        @"^\s*SG_\s+(\S+)\s*:\s*(\d+)\|(\d+)@([01])([+-])\s*\(([^,]+),([^)]+)\)\s*\[([^|]+)\|([^\]]+)\]\s*""([^""]*)""",
        RegexOptions.Compiled
    );

    private static readonly Regex AttributeValRegex = new Regex(
        @"^VAL_\s+(\d+)\s+(\S+)\s+(.+);",
        RegexOptions.Compiled
    );

    private static readonly Regex ValuePairRegex = new Regex(
        @"(\d+)\s+""([^""]*)""",
        RegexOptions.Compiled
    );

    public Network Parse(string dbcContent)
    {
        var network = new Network();
        Message? currentMessage = null;
        var messagesById = new Dictionary<int, Message>();

        var lines = dbcContent.Split('\n');

        foreach (var l in lines)
        {
            var line = l.Trim();

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }
            
            // Messages
            var msgMatch = MessageRegex.Match(line);

            if (msgMatch.Success)
            {
                currentMessage = ParseMessage(msgMatch);
                network.Messages.Add(currentMessage);
                messagesById[currentMessage.Id] = currentMessage;
                continue;
            }
            
            // Signals
            if (line.StartsWith("SG_ ") && currentMessage != null)
            {
                var sigMatch = SignalRegex.Match(line);

                if (sigMatch.Success)
                {
                    currentMessage.Signals.Add(ParseSignal(sigMatch));
                }

                continue;
            }
            
            // Attributes of signal
            var atbMatch = AttributeValRegex.Match(line);

            if (atbMatch.Success)
            {
                ParseAttributes(atbMatch, messagesById);
                continue;
            }
        }
        
        return network;
    }

    private static Message ParseMessage(Match match)
    {
        var msg = new Message
        {
            Id = int.Parse(match.Groups[1].Value),
            Name = match.Groups[2].Value.TrimEnd(':'),
            Length = int.Parse(match.Groups[3].Value),
        };

        return msg;
    }
    
    private static Signal ParseSignal(Match match)
    {
        var sig = new Signal
        {
            Name = match.Groups[1].Value,
            StartBit = int.Parse(match.Groups[2].Value),
            Length = int.Parse(match.Groups[3].Value),
            LittleEndian = match.Groups[4].Value == "1",
            Signed = match.Groups[5].Value == "-",
            Scale = double.Parse(match.Groups[6].Value, CultureInfo.InvariantCulture),
            Offset = double.Parse(match.Groups[7].Value, CultureInfo.InvariantCulture),
            Min = double.Parse(match.Groups[8].Value, CultureInfo.InvariantCulture),
            Max = double.Parse(match.Groups[9].Value, CultureInfo.InvariantCulture),
            MetricUnit = match.Groups[10].Value
        };

        return sig;
    }
    
    private static void ParseAttributes(Match match, Dictionary<int, Message> messages)
    {
        var msgId = int.Parse(match.Groups[1].Value);
        var signalName = match.Groups[2].Value;
        var valuesStr = match.Groups[3].Value;

        if (!messages.TryGetValue(msgId, out var message))
            return;

        var signal = message.Signals.FirstOrDefault(s => s.Name == signalName);
        if (signal == null)
            return;

        var attributes = new Dictionary<int, string>();
        var valueMatches = ValuePairRegex.Matches(valuesStr);

        foreach (Match valueMatch in valueMatches)
        {
            var value = int.Parse(valueMatch.Groups[1].Value);
            var description = valueMatch.Groups[2].Value;
            attributes[value] = description;
        }

        signal.Attributes = attributes;
    }
}
