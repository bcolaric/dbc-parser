using DbcParser.Parsers;

namespace DbcParser.Tests;

public class ParserTest
{
    // [Fact]
    // public void Parses_single_message_with_single_signal()
    // {
    //     var dbc = """
    //               BO_ 200 EngineData: 8 Vector__XXX
    //                SG_ EngineSpeed : 16|16@1+ (0.125,0) [0|8000] "rpm" Vector__XXX
    //               """;
    //
    //     var parser = new Parser();
    //     var network = parser.Parse(dbc);
    //
    //     Assert.Single(network.messages);
    //
    //     var message = network.messages[0];
    //     Assert.Equal(200, message.Id);
    //     Assert.Equal("EngineData", message.Name);
    //     Assert.Single(message.Signals);
    //
    //     var signal = message.Signals[0];
    //     Assert.Equal("EngineSpeed", signal.Name);
    //     Assert.Equal(16, signal.StartBit);
    //     Assert.Equal(16, signal.Length);
    // }
}