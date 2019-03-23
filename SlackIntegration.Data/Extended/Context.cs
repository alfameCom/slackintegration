using Microsoft.EntityFrameworkCore;
using System;

// ReSharper disable once CheckNamespace
namespace SlackIntegration.Data
{
    public partial class Context
    {
        public static Context Create(string connectionString, bool tracking = false)
        {
            var dbOptions = new DbContextOptionsBuilder<Context>()
                .UseSqlServer(connectionString,
                    options => options.CommandTimeout((int)TimeSpan.FromMinutes(1).TotalSeconds))
                .Options;

            var ctx = new Context(dbOptions);

            ctx.ChangeTracker.QueryTrackingBehavior = tracking
                ? QueryTrackingBehavior.TrackAll
                : QueryTrackingBehavior.NoTracking;

            return ctx;
        }
    }
}