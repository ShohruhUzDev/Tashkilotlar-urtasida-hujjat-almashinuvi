
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
        Microsoft.Office.Interop.Excel.Application excel;
        Microsoft.Office.Interop.Excel.Workbook workBook;
        Microsoft.Office.Interop.Excel.Worksheet workSheet;
        Microsoft.Office.Interop.Excel.Range cellRange;
        public MainWindow()
        {
            InitializeComponent();
            dt.Columns.Add("NO");
            dt.Columns.Add("Matni");
            dt.Columns.Add("Tuliq_ismi");
            dt.Columns.Add("Viloyat_Nomi");
            dt.Columns.Add("Hujjat_turi");
       
        }

        private void Create_btn_Click(object sender, RoutedEventArgs e)
        {
            Create c = new Create();
            c.ShowDialog();

            
        }

        private void Exprort_btn_Click(object sender, RoutedEventArgs e)
        {
            using (HujjatContext db = new HujjatContext())
            {
                var hujjat = db.Hujjats.Include(i => i.HujjatTuri)
                    .Include(j => j.Viloyat).ToList();


                dt.Clear();

                //datatable ni tuldirayapmiz. 
                foreach (Hujjat i in hujjat)
                {
                    dt.Rows.Add(i.Id, i.Matni, i.TuliqIsmi, i.Viloyat?.ViloyatNomi, i.HujjatTuri?.HujjatNomi);
                }



                
               


            }
           // GenerateExcel(dt);
        }
        //private void GenerateExcel(DataTable DtIN)
        //{
        //    IList<Hujjat> listhujjat = new List<Hujjat>();
        //    for (int i = 0; i < 5; i++)
        //    {
        //        listhujjat.Add(new Hujjat
        //        {
        //            Id = Convert.ToInt32("Id " + i),
        //            Matni = "Matni " + i,
        //            TuliqIsmi = "Ismi " + i,
        //           ViloyatId =Convert.ToInt32( "Viloyat " + i),
        //           HujjatTuriId=Convert.ToInt32("Hujjatid"+i)
        //        });
        //    }
        // var dgExcel;
        //    dgExcel.ItemsSource = listhujjat;
        //    try
        //    {
        //        excel = new Microsoft.Office.Interop.Excel.Application();
        //        excel.DisplayAlerts = false;
        //        excel.Visible = false;
        //        workBook = excel.Workbooks.Add(Type.Missing);
        //        workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.ActiveSheet;
        //        workSheet.Name = "LearningExcel";
        //        System.Data.DataTable tempDt = DtIN;
        //        dgExcel.ItemsSource = tempDt.DefaultView;
        //        workSheet.Cells.Font.Size = 11;
        //        int rowcount = 1;
        //        for (int i = 1; i <= tempDt.Columns.Count; i++) //taking care of Headers.  
        //        {
        //            workSheet.Cells[1, i] = tempDt.Columns[i - 1].ColumnName;
        //        }
        //        foreach (System.Data.DataRow row in tempDt.Rows) //taking care of each Row  
        //        {
        //            rowcount += 1;
        //            for (int i = 0; i < tempDt.Columns.Count; i++) //taking care of each column  
        //            {
        //                workSheet.Cells[rowcount, i + 1] = row[i].ToString();
        //            }
        //        }
        //        cellRange = workSheet.Range[workSheet.Cells[1, 1], workSheet.Cells[rowcount, tempDt.Columns.Count]];
        //        cellRange.EntireColumn.AutoFit();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
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


                dt.Clear();

                //datatable ni tuldirayapmiz. 
                foreach ( Hujjat i in hujjat)
                {
                    dt.Rows.Add(i.Id, i.Matni, i.TuliqIsmi, i.Viloyat?.ViloyatNomi, i.HujjatTuri?.HujjatNomi);
                }


                
                _db.Hujjats.Load();
                var hujjat1 = db.Hujjats.Include(i => i.HujjatTuri)
                   .Include(j => j.Viloyat).ToList();

                mydatagrid.ItemsSource = dt.DefaultView;
                

            }

        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Salom");
            //   
        }

       

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
            try
            {
                DataRowView row = (DataRowView)mydatagrid.SelectedItem;

                int id1 = Convert.ToInt32(row["NO"]);
                using(HujjatContext h=new HujjatContext())
                {
                    var hujjat3 = h.Hujjats.FirstOrDefault(i => i.Id == id1);
                    if(hujjat3!=null)
                    {
                        h.Hujjats.Remove(hujjat3);
                        h.SaveChanges();
                        var hujjat =h.Hujjats.Include(i => i.HujjatTuri)
                   .Include(j => j.Viloyat).ToList();


                        dt.Clear();

                        //datatable ni tuldirayapmiz. 
                        foreach (Hujjat i in hujjat)
                        {
                            dt.Rows.Add(i.Id, i.Matni, i.TuliqIsmi, i.Viloyat?.ViloyatNomi, i.HujjatTuri?.HujjatNomi);
                        }
                        mydatagrid.DataContext = dt.DefaultView;

                    }

                    else
                    {
                        MessageBox.Show("Bu hujjat topilmadi");
                    }
                }

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            
           
        }

        private void Edit_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView row = (DataRowView)mydatagrid.SelectedItem;

                int id1 = Convert.ToInt32(row["NO"]);

                Update update = new Update(id1);

                this.Hide();
                update.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
