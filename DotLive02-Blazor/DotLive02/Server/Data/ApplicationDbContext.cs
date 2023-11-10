using DotLive02.Server.Entities;
using DotLive02.Server.Models;
using Duende.IdentityServer.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DotLive02.Server.Data;
public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
{
    public ApplicationDbContext(
        DbContextOptions options,
        IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
    {
    }

    public DbSet<Job> Jobs { get; set; }
    public DbSet<JobApplication> JobApplications { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ApplicationUser>(e =>
        {
            e.HasMany(u => u.JobApplications)
                .WithOne()
                .HasForeignKey(ja => ja.UserId);
        });

        builder.Entity<Job>(e =>
        {
            e.HasKey(j => j.Id);

            e.HasMany(j => j.JobApllications)
                .WithOne(ja => ja.Job)
                .HasForeignKey(ja => ja.UserId);
        });

        builder.Entity<JobApplication>(e =>
        {
            e.HasKey(ja => ja.Id);
        });

        base.OnModelCreating(builder);
    }
}
