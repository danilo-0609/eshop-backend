using Microsoft.Extensions.Options;
using Quartz;

namespace Shopping.Infrastructure.Jobs;

internal sealed class ExpireOrdersJobSetup : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        JobKey jobKey = new JobKey(nameof(ExpireOrdersJob));

        options.AddJob<ExpireOrdersJob>(jobBuilder => jobBuilder.WithIdentity(jobKey))
            .AddTrigger(
                trigger =>
                    trigger.ForJob(jobKey)
                     .WithSimpleSchedule(
                           schedule =>
                            schedule.WithIntervalInSeconds(10)
                            .RepeatForever()));
    }
}

