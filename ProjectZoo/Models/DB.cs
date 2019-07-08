namespace ProjectZoo.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DB : DbContext
    {
        public DB()
            : base("name=DB")
        {
        }

        public virtual DbSet<ZooAnimal> ZooAnimals { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
