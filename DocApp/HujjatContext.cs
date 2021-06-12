using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocApp
{
   public  class HujjatContext:DbContext
    {
        DbSet<Hujjat> Hujjats { get; set; }
        DbSet<HujatTuri> HujatTuris { get; set; }
        DbSet<Viloyat> Viloyats { get; set; }
        public HujjatContext()
        {
            Database.Delete();
            Database.Create();
        }
       
    }
}
