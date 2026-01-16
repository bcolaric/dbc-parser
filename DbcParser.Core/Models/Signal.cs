namespace DbcParser.Models;

// Reference: https://docs.openvehicles.com/en/latest/components/vehicle_dbc/docs/dbc-primer.html

public class Signal
{
    public string name { get; init; } = string.Empty;
    public int start_bit { get; init; } 
    public int length { get; init; }
    public bool little_endian { get; init; } // little or big
    public bool signed { get; init; } // signed or unsigned
    public double scale { get; init; }
    public double offset { get; init; }
    public double min { get; init; }
    public double max { get; init; }
    public string metric_unit { get; init; } = string.Empty;

    public Dictionary<string, string> attributes { get; } = new(); // (key, value) -> (name, value)
}