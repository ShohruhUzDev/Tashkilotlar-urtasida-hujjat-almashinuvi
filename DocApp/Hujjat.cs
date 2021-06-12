using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocApp
{
   public  class Hujjat
    {
        public int Id { get; set; }
        public string  Matni { get; set; }
        public string  TuliqIsmi { get; set; }
        public int? ViloyatId { get; set; }
        public Viloyat Viloyat { get; set; }
        public int? HujjarTuriId { get; set; }
        public HujatTuri HujatTuri { get; set; }


    }
    public class Viloyat
    {
        public int Id { get; set; }
        public string  ViloyatNomi { get; set; }
        List<Hujjat> Hujjatlar { get; set; }
    }
    public class HujatTuri
    {
        public int Id{ get; set; }
        public string  HujjatNomi { get; set; }
        List<Hujjat> Hujjatlar { get; set; }
    }
}
