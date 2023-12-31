﻿using System.Threading.Channels;

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
            Console.WriteLine("One");
            while (!stoppingToken.IsCancellationRequested)
            {
                await foreach (var item in _channel.Reader.ReadAllAsync())
                {
                    Console.WriteLine($"Message Read 1 =>>>: {item}");
                }
            }
        }
    }
}
