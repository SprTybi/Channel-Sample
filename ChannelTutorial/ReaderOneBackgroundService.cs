using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace ChannelTutorial
{
    public class ReaderOneBackgroundService : BackgroundService
    {
        private readonly Channel<string> _channel;
        public ReaderOneBackgroundService(Channel<string> channel)
        {
            _channel = channel;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await foreach (var item in _channel.Reader.ReadAllAsync())
                {
                    Console.WriteLine($"Message Readddddddddddd: {item}");
                }
            }
        }
    }
}
