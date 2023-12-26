// See https://aka.ms/new-console-template for more information
using System.Threading.Channels;

Console.WriteLine("Start App");


var channel = Channel.CreateBounded<string>(new BoundedChannelOptions(2)
{
    Capacity = 2,
    FullMode = BoundedChannelFullMode.Wait,  //رفتار پرودیوسر وقتی میبینه کیو پر هست
    SingleReader = false,// میتونه ان تا ساب اسکریبر مسیج رو بخونه
    SingleWriter = false, //

}, (string item) => { // وقتی اینجا معلوم میکنیم که استرینگه دیگر لازم نیست اول متد کریت باندد مشخص کنیم که جنس پیام های داخل صف از استرینگه
    Console.WriteLine($"Droped Item : {item}");
});
var writer = Task.Run(async () =>
{
    for (int i = 0; i < 50; i++)
    {
        await channel.Writer.WriteAsync(i.ToString());
        Console.WriteLine($"Insert Message To QUEUE : {i}");
    }
});

var reader = Task.Run(async () =>
{
    await foreach (var item in channel.Reader.ReadAllAsync())
    {
        Console.WriteLine($"Message Readddddddddddd: {item}");
    }
});

await Task.WhenAll(writer, reader);

Console.WriteLine("End App");
