using MicroSolutions.Data.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MicroSolutions.DAL
{
	public class MicroSolutionsContext : DbContext
    {
        public MicroSolutionsContext() : base("MicroSolutionsContext")
        {
        }

		public DbSet<Vendors> Vendors { get; set; }
		public DbSet<Customer> Customer { get; set; }
		public DbSet<ExpirationPeriod> ExpirationPeriod { get; set; }
		public DbSet<ItemType> ItemType { get; set; }
		public DbSet<EmailSettings> EmailSettings { get; set; }
		public DbSet<Invoice> Invoice { get; set; }
		public DbSet<PartsForInvoice> PartsForInvoice { get; set; }

		public DbSet<Supplier> Supplier { get; set; }

		public DbSet<Notification> Notification { get; set; }

		public DbSet<webpages_Roles> webpages_Roles { get; set; }
		public DbSet<webpages_UsersInRoles> webpages_UsersInRoles { get; set; }
		public DbSet<UserProfile> UserProfile { get; set; }
		public DbSet<webpages_Membership> webpages_Membership { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}
