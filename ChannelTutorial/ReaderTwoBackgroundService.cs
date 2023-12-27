using System.Threading.Channels;

namespace ChannelTutorial
{
    public class ReaderTwoBackgroundService : BackgroundService
    {
        private readonly Channel<string> _channel;
        public ReaderTwoBackgroundService(Channel<string> channel)
        {
            _channel = channel;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Two");
            while (!stoppingToken.IsCancellationRequested)
            {
                await foreach (var item in _channel.Reader.ReadAllAsync())
                {
                    Console.WriteLine($"Message Read 2 =>>>: {item}");
                }
            }
        }
    }
}
