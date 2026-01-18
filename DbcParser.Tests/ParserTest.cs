using DbcParser.Parsers;

namespace DbcParser.Tests;

public class ParserTests
{
    [Fact]
    public void ParseExampleDbcData()
    {
        var dbcPath = Path.Combine(AppContext.BaseDirectory, "TestData", "kia_ev6.dbc");
        var dbcContent = File.ReadAllText(dbcPath);
        var parser = new Parser();
        var network = parser.Parse(dbcContent);

        // Test messages
        Assert.Equal(35, network.Messages.Count);
        
        var accelerator = network.Messages.First(m => m.Id == 53);
        Assert.Equal("ACCELERATOR", accelerator.Name);
        Assert.Equal(32, accelerator.Length);
        Assert.Equal("XXX", accelerator.Transmitter);
        Assert.Equal(4, accelerator.Signals.Count);

        // Test signal
        var gear = accelerator.Signals.First(s => s.Name == "GEAR");
        Assert.Equal(192, gear.StartBit);
        Assert.Equal(3, gear.Length);
        Assert.Equal(1.0, gear.Scale);
        Assert.True(gear.LittleEndian);
        Assert.False(gear.Signed);
        
        // Test attributes
        Assert.NotNull(gear.Attributes);
        Assert.Equal(4, gear.Attributes.Count);
        Assert.Equal("P", gear.Attributes[0]);
        Assert.Equal("D", gear.Attributes[5]);
        Assert.Equal("N", gear.Attributes[6]);
        Assert.Equal("R", gear.Attributes[7]);
    }
}