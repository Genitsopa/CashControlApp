using System;
using CashControl.Context;
using Microsoft.EntityFrameworkCore;

namespace CashControlBack.Models
{
	public class ApplicationDb:DbContext
	{
		public ApplicationDb(DbContextOptions<ApplicationDb> options) : base(options)
		{

		}

        public DbSet<Transaction> Transactions { get; set; }
		public DbSet<Category> Categories { get; set; }

        public DbSet<Team> Team { get; set; }

        public DbSet<Player> Player { get; set; }

		public DbSet<Chef> Chef { get; set; }

		public DbSet<Recipe> Recipe { get; set; }

    }
}

