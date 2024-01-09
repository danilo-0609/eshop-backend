using Microsoft.Extensions.Options;
using Quartz;

namespace Catalog.Infrastructure.Outbox.BackgroundJobs;

internal sealed class ProcessCatalogOutboxMessageJobSetup : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        var jobKey = new JobKey(nameof(ProcessCatalogOutboxMessageJob));

        options.AddJob<ProcessCatalogOutboxMessageJob>(jobBuilder => jobBuilder.WithIdentity(jobKey))
                .AddTrigger(
                    trigger =>
                        trigger.ForJob(jobKey)
                            .WithSimpleSchedule(
                                schedule =>
                                    schedule.WithIntervalInSeconds(10)
                                    .RepeatForever()));
    }
}
