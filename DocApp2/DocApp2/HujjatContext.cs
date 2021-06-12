using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocApp
{
   public  class HujjatContext:DbContext
    {
        DbSet<Hujjat> Hujjats { get; set; }
        DbSet<HujjatTuri> HujatTuris { get; set; }
        DbSet<Viloyat> Viloyats { get; set; }
        //public HujjatContext()
        //{
        //    Database.EnsureDeleted();
        //    Database.EnsureCreated();
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=Hujjatdb; Trusted_Connection=true");
        }
    }
}
