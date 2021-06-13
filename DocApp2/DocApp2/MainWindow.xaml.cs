
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DocApp2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataTable dt = new DataTable();
        HujjatContext _db = new HujjatContext();
        public MainWindow()
        {
            InitializeComponent();
            dt.Columns.Add("Id");
            dt.Columns.Add("Matni");
            dt.Columns.Add("Tuliq ismi");
            dt.Columns.Add("Viloyat Nomi");
            dt.Columns.Add("Hujjat turi");

        }

        private void Create_btn_Click(object sender, RoutedEventArgs e)
        {
            Create c = new Create();
            c.ShowDialog();

            
        }

        private void Exprort_btn_Click(object sender, RoutedEventArgs e)
        {
            Update update = new Update();
            update.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using(HujjatContext db=new HujjatContext())
            {
                var hujjat = db.Hujjats.Include(i => i.HujjatTuri)
                    .Include(j => j.Viloyat).ToList();

               
                //datatable ni tuldirayapmiz. 
                foreach( Hujjat i in hujjat)
                {
                    dt.Rows.Add(i.Id, i.Matni, i.TuliqIsmi, i.Viloyat?.ViloyatNomi, i.HujjatTuri?.HujjatNomi);
                }


                mydatagrid.DataContext = dt;





            }

        }
    }
}
