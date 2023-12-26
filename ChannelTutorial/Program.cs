using ChannelTutorial;
using System.Threading.Channels;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {

        services.AddHostedService<ReaderOneBackgroundService>();
        services.AddSingleton(Channel.CreateBounded<string>(new BoundedChannelOptions(2)
        {
            Capacity = 2,
            FullMode = BoundedChannelFullMode.Wait,  //رفتار پرودیوسر وقتی میبینه کیو پر هست
            SingleReader = false,// میتونه ان تا ساب اسکریبر مسیج رو بخونه
            SingleWriter = false, //

        }, (string item) =>
        { // وقتی اینجا معلوم میکنیم که استرینگه دیگر لازم نیست اول متد کریت باندد مشخص کنیم که جنس پیام های داخل صف از استرینگه
            Console.WriteLine($"Droped Item : {item}");
        }));
    }).Build();


host.Run();
