﻿using System.Collections.Concurrent;

namespace AdventureWorks.ServiceAPI.Services;

public class ApplicationService
{
    public ConcurrentBag<string> Data { get; } = []; // new ConcurrentBag<string>();
}

public class ApplicationRefresh : IHostedService, IDisposable
{
    private Timer? _timer;
    private readonly ApplicationService _appData;

    public ApplicationRefresh(ApplicationService appData)
    {
        _appData = appData;
    }
    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(AddToCache, null, TimeSpan.Zero, TimeSpan.FromSeconds(2));
        return Task.CompletedTask;
    }

    private void AddToCache(object? state)
    {
        _appData.Data.Add($"Message added at: {DateTime.Now.ToLongTimeString()}.");
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }
    public void Dispose() => _timer?.Dispose();
}