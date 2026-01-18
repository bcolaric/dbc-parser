namespace DbcParser.Models;

// Reference: https://docs.openvehicles.com/en/latest/components/vehicle_dbc/docs/dbc-primer.html

public class Signal
{
    public string Name { get; set; } = "";
    public int StartBit { get; set; } 
    public int Length { get; set; }
    public bool LittleEndian { get; set; } // little or big
    public bool Signed { get; set; } // signed or unsigned
    public double Scale { get; set; }
    public double Offset { get; set; }
    public double Min { get; set; }
    public double Max { get; set; }
    public string MetricUnit { get; set; } = "";

    public Dictionary<int, string>? Attributes { get; set; } = new(); // (key, value) -> (name, value)
}