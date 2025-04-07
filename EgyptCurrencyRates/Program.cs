using EgyptCurrencyRates.Jobs;
using EgyptCurrencyRates.Services;
using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;

namespace EgyptCurrencyRates
{
    public class Program
    {
        public static IScheduler _Scheduler;
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            _Scheduler = ConfigurationQuartiz();
            builder.Services.AddSingleton(provider => _Scheduler);

            // Add Quartz services
            builder.Services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionScopedJobFactory();

                var ratesJobKey = new JobKey("RatesJob");
                q.AddJob<RatesJob>(opts => opts.WithIdentity(ratesJobKey));
                q.AddTrigger(opts => opts
                    .ForJob(ratesJobKey)
                    .WithIdentity("ratesJob-trigger")
                    //.WithCronSchedule("0 0/55 * * * ?")
                    .WithCronSchedule("0 55 * * * ?")
                    .StartNow()); // run Every 55 minutes

                var goldJobKey = new JobKey("GoldJob");
                q.AddJob<GoldJob>(opts => opts.WithIdentity(goldJobKey));
                q.AddTrigger(opts => opts
                    .ForJob(goldJobKey)
                    .WithIdentity("goldJob-trigger")
                    //.WithCronSchedule("0 0/45 * * * ?")
                    .WithCronSchedule("0 45 * * * ?")
                    .StartNow()); // run Every 45 minutes

                var twitterGoldPostJobKey = new JobKey("twitterGoldPostJob");
                q.AddJob<twitterGoldPostJob>(opts => opts.WithIdentity(twitterGoldPostJobKey));
                q.AddTrigger(opts => opts
                    .ForJob(twitterGoldPostJobKey)
                    .WithIdentity("twitterGoldPostJob-trigger")
                    .WithCronSchedule("0 0 8-23,0 ? * *")
                    .StartNow());

                var ratesCleanKey = new JobKey("RatesCleanJob");
                q.AddJob<CleanDatabase>(opts => opts.WithIdentity(ratesCleanKey));
                q.AddTrigger(opts => opts
                    .ForJob(ratesCleanKey)
                    .WithIdentity("ratesCleanJob-trigger")
                    .WithCronSchedule("0 0 0 * * ?")
                    .StartNow()); // run Every day at midnight - 12am
            });


            builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

            // Register TelegramBotService
            builder.Services.AddSingleton<TelegramBotService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        public async Task StopAsync()
        {
            if (!_Scheduler.IsShutdown)
            {
                await _Scheduler.Shutdown();
            }
        }

        private static IScheduler ConfigurationQuartiz()
        {
            NameValueCollection prop = new NameValueCollection()
            {
                {"quartz.serializer.type","binary"}
            };
            StdSchedulerFactory factory = new StdSchedulerFactory(prop);
            var schecduler = factory.GetScheduler().Result;
            //schecduler.Start().Wait();
            schecduler.Shutdown();
            return schecduler;
        }
    }
}