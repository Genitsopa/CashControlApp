using System;
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
        public object Leagues { get; internal set; }
        public object Teams { get; internal set; }
    }
}

