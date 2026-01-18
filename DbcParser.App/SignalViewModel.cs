using DbcParser.Models;
using System.Linq;

namespace DbcParser.App;

public class SignalViewModel
{
    public string Name { get; }
    public int StartBit { get; }
    public int Length { get; }
    public string LittleEndian { get; }
    public string IsSigned { get; }
    public double Scale { get; }
    public double Offset { get; }
    public double Min { get; }
    public double Max { get; }
    public string MetricUnit { get; }
    public string ValueTable { get; }

    public SignalViewModel(Signal signal)
    {
        Name = signal.Name;
        StartBit = signal.StartBit;
        Length = signal.Length;
        LittleEndian = signal.LittleEndian ? "Yes" : "No";
        IsSigned = signal.Signed ? "Yes" : "No";
        Scale = signal.Scale;
        Offset = signal.Offset;
        Min = signal.Min;
        Max = signal.Max;
        MetricUnit = signal.MetricUnit;
        
        if (signal.Attributes.Count > 0 && signal.Attributes != null)
        {
            ValueTable = string.Join(", ", signal.Attributes.Select(kvp => $"{kvp.Key}={kvp.Value}"));
        }
        
        else
        {
            ValueTable = "";
        }
    }
}