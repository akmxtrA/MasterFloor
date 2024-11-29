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

namespace MasterFloor
{
    /// <summary>
    /// Логика взаимодействия для historyRealization.xaml
    /// </summary>
    public partial class historyRealization : Window
    {
        MasterFloorEntities db = new MasterFloorEntities();
        public historyRealization()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Запись таблиц из БД в переменные
            var partnerProducts = db.partnerProducts.ToList();
            var partners = db.partners.ToList();
            var products = db.products.ToList();

            //Соединение таблиц в одну с помощью ключей
            var partners_products = from pa in partnerProducts join pr in products on pa.id_products equals pr.id select new { idPartner = pa.id_partner, count = pa.count, date = pa.date, nameProduct = pr.name};
            var partnerProd = from pa in partners_products join pr in partners on pa.idPartner equals pr.id select new { partner = pr.name, count = pa.count, date = pa.date, product = pa.nameProduct };

            //Вывод списка с историей реализации продуктов партнёром
            historyGrid.ItemsSource = partnerProd.ToList();
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
