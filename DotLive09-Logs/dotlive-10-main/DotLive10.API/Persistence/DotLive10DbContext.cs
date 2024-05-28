using System;
using DotLive10.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotLive10.API.Persistence
{
	public class DotLive10DbContext : DbContext
	{
		public DotLive10DbContext(DbContextOptions<DotLive10DbContext> options) : base(options)
		{
		}

		public DbSet<Product> Products { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.Entity<Product>(e => e.HasKey(p => p.Id));
		}
	}
}

