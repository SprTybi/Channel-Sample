using ChannelTutorial;
using System.Threading.Channels;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {

        services.AddHostedService<ReaderOneBackgroundService>();
        services.AddSingleton(Channel.CreateBounded<string>(new BoundedChannelOptions(2)
        {
            //Capacity = 2, // ظرفیت صف . در اینجا تعریف میکنیم یا در خط بالا در کانستراکتور اپشن مقدار دهس میکنیم
            FullMode = BoundedChannelFullMode.Wait,  //رفتار پرودیوسر وقتی میبینه کیو پر هست
            //SingleReader = false,// میتونه ان تا ساب اسکریبر مسیج رو بخونه
            //SingleWriter = false, //

        }, (string item) =>
        { // وقتی اینجا معلوم میکنیم که استرینگه دیگر لازم نیست اول متد کریت باندد مشخص کنیم که جنس پیام های داخل صف از استرینگه
            Console.WriteLine($"Droped Item : {item}");
        }));
    }).Build();

var channel = host.Services.GetService<Channel<string>>();

await Task.Run(async () => 
{
    for (int i = 0; i < 100; i++)
    {
        await channel.Writer.WriteAsync(i.ToString());
        Console.WriteLine($"Send toooooo Queue{i}");
    }
});


host.Run();
