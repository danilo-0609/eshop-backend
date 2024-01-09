using Microsoft.Extensions.Options;
using Quartz;

namespace UserAccess.Infrastructure.Outbox.BackgroundJobs;

internal sealed class ProcessUserAccessOutboxMessageJobSetup : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        var jobKey = new JobKey(nameof(ProcessUserAccessOutboxMessageJob));

        options
        .AddJob<ProcessUserAccessOutboxMessageJob>(jobBuilder => jobBuilder.WithIdentity(jobKey))
        .AddTrigger(
            trigger =>
                trigger.ForJob(jobKey)
                    .WithSimpleSchedule(
                        schedule =>
                            schedule.WithIntervalInSeconds(5)
                                .RepeatForever()));
    }
}
