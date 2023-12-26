// See https://aka.ms/new-console-template for more information
using System.Threading.Channels;

Console.WriteLine("Start App");


var channel = Channel.CreateBounded<string>(20);
var writer = Task.Run(async () =>
{
    for (int i = 0; i < 50; i++)
    {
        await channel.Writer.WriteAsync(i.ToString());
        Console.WriteLine($"Send Message : {i}");
    }
});

var reader = Task.Run(async () =>
{
    await foreach (var item in channel.Reader.ReadAllAsync())
    {
        Console.WriteLine($"Message Reqad {item}");
    }
});

await Task.WhenAll(writer, reader);

Console.WriteLine("End App");
