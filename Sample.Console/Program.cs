﻿using Prometheus;

// This sample demonstrates how to integrate prometheus-net into a console app (e.g. a worker service).
// 
// NuGet packages required:
// * prometheus-net.AspNetCore

// Start the metrics server on your preferred port number.
using var server = new KestrelMetricServer(port: 1234);
server.Start();

// Generate some sample data from fake business logic.
var recordsProcessed = Metrics.CreateCounter("sample_records_processed_total", "Total number of records processed.");

_ = Task.Run(async delegate
{
    while (true)
    {
        // Pretend to process a record approximately every second, just for changing sample data.
        recordsProcessed.Inc();

        await Task.Delay(TimeSpan.FromSeconds(1));
    }
});

// Metrics published in this sample:
// * built-in process metrics giving basic information about the .NET runtime.
// * the sample counter defined above.
Console.WriteLine("Open http://localhost:1234/metrics in a web browser.");
Console.WriteLine("Press enter to exit.");
Console.ReadLine();