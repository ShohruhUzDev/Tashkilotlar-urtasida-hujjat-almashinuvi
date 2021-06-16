using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DocApp2
{
    /// <summary>
    /// Interaction logic for Create.xaml
    /// </summary>
    public partial class Create : Window
    {
        DataTable dt = new DataTable();
        HujjatContext _db = new HujjatContext();
        public Create()
        {
            InitializeComponent();
            dt.Columns.Add("NO");
            dt.Columns.Add("Matni");
        //   dt.Columns.Add("Shaxs");
            dt.Columns.Add("Tuliq ismi");
            dt.Columns.Add("Viloyat Nomi");
            dt.Columns.Add("Hujjat turi");
        }

        private void Save_btn_Click(object sender, RoutedEventArgs e)
        {



            using(HujjatContext db=new HujjatContext())
            {
                
                var viloyat = db.Viloyats.FirstOrDefault(k => k.ViloyatNomi == Viloyat_cbx.SelectedItem.ToString());
                var hujjatturi = db.HujatTuris.FirstOrDefault(j => j.HujjatNomi == Hujjatturi_cbx.SelectedItem.ToString());

                if(viloyat!=null&&hujjatturi!=null)
                {
                    if(Matni_txt.Text!=""&&Ism_Fam_txt.Text!=""&&Viloyat_cbx.SelectedIndex!=-1&&Hujjatturi_cbx.SelectedIndex!=-1)
                    {
                        Hujjat hujjatlar = new Hujjat()
                        {
                            Matni = Matni_txt.Text,
                            TuliqIsmi = Ism_Fam_txt.Text,
                            Viloyat = viloyat,
                            HujjatTuri = hujjatturi
                        };

                        db.Hujjats.Add(hujjatlar);
                        db.SaveChanges();
                        MessageBox.Show("Saqlandi");
                    }
                    else
                    {
                        MessageBox.Show("Malumot tuliq kiritilmadi");
                    }
                   
                }
                else
                {
                    MessageBox.Show("Bu elementlar topilmadi");
                }

                var hujjat = db.Hujjats.Include(i => i.HujjatTuri)
                     .Include(j => j.Viloyat).ToList();
                dt.Clear();

                //datatable ni tuldirayapmiz. 
                foreach (Hujjat i in hujjat)
                {
                    dt.Rows.Add(i.Id, i.Matni, i.TuliqIsmi, i.Viloyat?.ViloyatNomi, i.HujjatTuri?.HujjatNomi);
                }
                MainWindow mn = new MainWindow();
               

                mn.Show();
                this.Hide();

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var hujjatturi = _db.HujatTuris.ToList();
            List<string> nomi = new List<string>();
         foreach(HujjatTuri i in hujjatturi)
            {
                nomi.Add(i.HujjatNomi);
            }

            Hujjatturi_cbx.ItemsSource = nomi;



            var viloyatnomi = _db.Viloyats.ToList();
            List<string> viloyatn= new List<string>();
            foreach (Viloyat i in viloyatnomi)
            {
                viloyatn.Add(i.ViloyatNomi);
            }

            Viloyat_cbx.ItemsSource = viloyatn;

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            _db.Dispose();
           
            this.Hide();
        }
    }
}
