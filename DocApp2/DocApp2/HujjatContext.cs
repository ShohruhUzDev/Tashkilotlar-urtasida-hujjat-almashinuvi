using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocApp2
{
   public  class HujjatContext:DbContext
    {
       public  DbSet<Hujjat> Hujjats { get; set; }
      public   DbSet<HujjatTuri> HujatTuris { get; set; }
      public  DbSet<Viloyat> Viloyats { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb; Database=Hujjatdb; Trusted_Connection=true");
        }
    }
}
