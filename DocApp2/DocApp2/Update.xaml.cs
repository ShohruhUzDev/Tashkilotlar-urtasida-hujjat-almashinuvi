using System;
using System.Collections.Generic;
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
    /// Interaction logic for Update.xaml
    /// </summary>
    public partial class Update : Window
    {
        int id;
        HujjatContext hujjatContext = new HujjatContext();
        public Update(int id)
        {
            InitializeComponent();
            this.id = id;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var viloyatlar = hujjatContext.Viloyats.ToList();
            List<string> nomlari = new List<string>();
            foreach(var i in viloyatlar)
            {
                nomlari.Add(i.ViloyatNomi);
            }
            viloyat_cbx.ItemsSource = nomlari;

            var hujjatlar = hujjatContext.HujatTuris.ToList();
            List<string> nomlari2 = new List<string>();
            foreach (var i in hujjatlar)
            {
                nomlari2.Add(i.HujjatNomi);
            }
            hujjat_cbx.ItemsSource = nomlari2;

            using (HujjatContext h = new HujjatContext())
            {
                var hujjat = h.Hujjats.FirstOrDefault(i => i.Id == id);
               

                if (hujjat != null )
                {
                 editmatn_txt.Text=hujjat.Matni.ToString();
                  edittuliq_txt.Text=hujjat.TuliqIsmi;
                    

                }

                else
                {
                    MessageBox.Show("Bu hujjat topilmadi");
                }
            }
        }

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {
            using (HujjatContext h = new HujjatContext())
            {
                var hujjat = h.Hujjats.FirstOrDefault(i => i.Id == id);
                var viloyat = hujjatContext.Viloyats.FirstOrDefault(j => j.ViloyatNomi == viloyat_cbx.SelectedItem.ToString());

                var hujjatturi = hujjatContext.HujatTuris.FirstOrDefault(j => j.HujjatNomi == hujjat_cbx.SelectedItem.ToString());

                if(editmatn_txt.Text!=""&&edittuliq_txt!=null&&hujjat_cbx.SelectedIndex!=-1&&viloyat_cbx.SelectedIndex!=-1)
                {
                    if (hujjat != null && viloyat != null && hujjatturi != null)
                    {
                        hujjat.Matni = editmatn_txt.Text;
                        hujjat.TuliqIsmi = edittuliq_txt.Text;
                        hujjat.Viloyat = viloyat;
                        hujjat.HujjatTuri = hujjatturi;

                        h.Hujjats.Update(hujjat);
                        h.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("Bu hujjat topilmadi");
                    }
                }
                else
                {
                    MessageBox.Show("Malumot tuliq kiritilmadi qaytadan kiriting");
                }
                
            }
            MainWindow main = new MainWindow();
            main.Show();
            this.Hide();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Hide();
        }
    }
}
