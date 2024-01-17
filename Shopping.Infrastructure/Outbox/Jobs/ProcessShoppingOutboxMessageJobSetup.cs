using Microsoft.Extensions.Options;
using Quartz;

namespace Shopping.Infrastructure.Outbox.JobOutbox;

internal class ProcessShoppingOutboxMessageJobSetup : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        JobKey jobKey = new JobKey(nameof(ProcessShoppingOutboxMessageJob));

        options.AddJob<ProcessShoppingOutboxMessageJob>(jobBuilder => jobBuilder.WithIdentity(jobKey))
            .AddTrigger(
                trigger =>
                    trigger.ForJob(jobKey)
                     .WithSimpleSchedule(
                           schedule =>
                            schedule.WithIntervalInSeconds(10)
                            .RepeatForever()));
    }
}
