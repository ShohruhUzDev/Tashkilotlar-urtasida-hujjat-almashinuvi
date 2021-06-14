
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
      public  DataTable dt = new DataTable();
        HujjatContext _db = new HujjatContext();
        public MainWindow()
        {
            InitializeComponent();
            dt.Columns.Add("NO");
            dt.Columns.Add("Matni");
            dt.Columns.Add("Tuliq_ismi");
            dt.Columns.Add("Viloyat_Nomi");
            dt.Columns.Add("Hujjat_turi");
         //   dt.Columns.Add("Action");
         //   dt.Columns.Add("Action");

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
            var hujjatturi = _db.HujatTuris.ToList();
            List<string> nomi = new List<string>();
            foreach (HujjatTuri i in hujjatturi)
            {
                nomi.Add(i.HujjatNomi);
            }

            Hujjat_cbx.ItemsSource = nomi;



            var viloyatnomi = _db.Viloyats.ToList();
            List<string> viloyatn = new List<string>();
            foreach (Viloyat i in viloyatnomi)
            {
                viloyatn.Add(i.ViloyatNomi);
            }

            Viloyat_cbx.ItemsSource = viloyatn;



            using (HujjatContext db=new HujjatContext())
            {
                var hujjat = db.Hujjats.Include(i => i.HujjatTuri)
                    .Include(j => j.Viloyat).ToList();

               


                //datatable ni tuldirayapmiz. 
                foreach ( Hujjat i in hujjat)
                {
                    dt.Rows.Add(i.Id, i.Matni, i.TuliqIsmi, i.Viloyat?.ViloyatNomi, i.HujjatTuri?.HujjatNomi);
                }


                //for (int i = 0; i < dt.Columns.Count; i++)
                //{
                //    for (int j = 0; j < dt.Rows.Count; j++)
                //    {
                //        Button MyControl1 = new Button();
                //        MyControl1.Content = "Delete";
                //        MyControl1.Name = "Delete_btn" ;

                //        dt.Columns.Add(MyControl1, j);
                //        Grid.SetRow(MyControl1, i);
                //        dt.Rows.Add(MyControl1);

                //        count++;
                //    }

                //}

                // dt.Rows.Add( btn);
                //  mydatagrid.ItemsSource = dt.DefaultView;
                _db.Hujjats.Load();
                var hujjat1 = db.Hujjats.Include(i => i.HujjatTuri)
                   .Include(j => j.Viloyat).ToList();

                mydatagrid.ItemsSource = dt.DefaultView;
                //string colProperty = "Name";

                //DataGridTextColumn col = new DataGridTextColumn();
                //col.Binding = new Binding(colProperty);
                //var spHeader = new StackPanel() { Orientation = Orientation.Vertical };
                //spHeader.Children.Add(new TextBlock(new Run(colProperty)));
                //var button = new Button();
                //button.Click += Button_Filter_Click;
                //button.Content = "Filter";
                //spHeader.Children.Add(button);
                //col.Header = spHeader;

                //mydatagrid.Columns.Add(col);
                
                //DataGridTemplateColumn colBtn = new DataGridTemplateColumn();
                //DataTemplate DttBtn = new DataTemplate();
                //FrameworkElementFactory btn1 = new FrameworkElementFactory(typeof(Button));
                //FrameworkElementFactory text = new FrameworkElementFactory(typeof(TextBlock));
                //FrameworkElementFactory panel = new FrameworkElementFactory(typeof(StackPanel));
                //// FrameworkElementFactory img = new FrameworkElementFactory(typeof(Image));

                ////img.SetValue(Image.SourceProperty, (BitmapImage)FindResource("Img"));
                ////img.SetValue(Image.HeightProperty, Convert.ToDouble(16));
                ////img.SetValue(Image.WidthProperty, Convert.ToDouble(16));
                //Button bt = new Button();
                //bt.Height = 50;
                //bt.Width = 50;
                //bt.Content = "Delete";
                //text.Text = "Delete";
               
                //panel.AppendChild(text);
               
                ////panel.AppendChild(img);
                //panel.AppendChild(btn1);
                ////btn1.AppendChild(panel);
                //btn1.AddHandler(Button.ClickEvent, new RoutedEventHandler(Btn_Click));
                //DttBtn.VisualTree = panel;
                //colBtn.CellTemplate = DttBtn;
                //mydatagrid.Columns.Add(colBtn);


            }

        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Salom");
            //   
        }

        //private void Button_Filter_Click(object sender, RoutedEventArgs e)
        //{
        //    MessageBox.Show("Salom");
        //}

        private void Window_Closed(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void Viloyat_cbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string str = Viloyat_cbx.SelectedItem.ToString();


            var hujj = _db.Hujjats.Include(i => i.Viloyat).
               Include(o=>o.HujjatTuri).
                Where(j => j.Viloyat.ViloyatNomi == str).ToList();
            dt.Clear();


            foreach (Hujjat p in hujj)
            {
                dt.Rows.Add(p.Id, p.Matni, p.TuliqIsmi, p.Viloyat?.ViloyatNomi, p.HujjatTuri?.HujjatNomi);
            }
            mydatagrid.DataContext = dt.DefaultView;

        }

        private void Hujjat_cbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string str = Hujjat_cbx.SelectedItem.ToString();


            var hujj = _db.Hujjats.Include(i => i.Viloyat).
               Include(o => o.HujjatTuri).
                Where(j => j.HujjatTuri.HujjatNomi == str).ToList();
            dt.Clear();


            foreach (Hujjat p in hujj)
            {
                dt.Rows.Add(p.Id, p.Matni, p.TuliqIsmi, p.Viloyat?.ViloyatNomi, p.HujjatTuri?.HujjatNomi);
            }
            mydatagrid.DataContext = dt.DefaultView;
        }

        private void search_txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            string str = search_txt.Text.ToString();


            var hujj = _db.Hujjats.Include(i => i.Viloyat).
               Include(o => o.HujjatTuri).
                Where( t=> EF.Functions.Like (t.TuliqIsmi ,$"%{str}%" )).ToList();
            dt.Clear();


            foreach (Hujjat p in hujj)
            {
                dt.Rows.Add(p.Id, p.Matni, p.TuliqIsmi, p.Viloyat?.ViloyatNomi, p.HujjatTuri?.HujjatNomi);
            }
            mydatagrid.DataContext = dt.DefaultView;
        }

        

        private void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Delete bosildi");
        }

        private void Edit_btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Edit bosildi");
        }
    }
}
