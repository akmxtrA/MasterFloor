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
    /// Логика взаимодействия для materialForProduct.xaml
    /// </summary>
    public partial class materialForProduct : Window
    {
        MasterFloorEntities db = new MasterFloorEntities();
        public materialForProduct()
        {
            InitializeComponent();
        }

        private int material(int idTypeProduct, int count, int idTypeMaterial, double param1, double param2)
        {
            //Запись таблиц из БД в переменные
            var typeProduct = db.productType.FirstOrDefault(x => x.id == idTypeProduct);
            var material = db.material.FirstOrDefault(x => x.id == idTypeMaterial);
            var materialType = db.materialType.FirstOrDefault(x => x.id == material.id_typeMaterial);
            //Расчёт нужного количества материалов для n-количества продукта
            double materialForOne = param1 * param2 * (double)typeProduct.coefficient;
            double materialForAll = materialForOne * count;
            double brak = materialForAll * (double)materialType.percentDefective;
            return Convert.ToInt32(Math.Floor(materialForAll + brak));

        }
        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void goBtn_Click(object sender, RoutedEventArgs e)
        {
            //Запись таблиц из БД в переменные
            var materialForManufacturer = db.materialForManufacturer.FirstOrDefault(x => x.id_products == (int)productBox.SelectedValue);
            var materialList = db.material.FirstOrDefault(x => x.id == materialForManufacturer.id_material);
            var materialType = db.materialType.FirstOrDefault(x => x.id == materialList.id_typeMaterial);
            //Вывод ответа в лейбл при помощи метода с расчётом
            answerLabel.Content = "Ответ: " + material((int)productBox.SelectedValue, Convert.ToInt32(countTxt.Text), (int)materialType.id, Convert.ToDouble(firstParametrTxt.Text), Convert.ToDouble(secondParametrTxt.Text));
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Заполнение комбобокса из БД
            productBox.ItemsSource = db.products.ToList();
            productBox.DisplayMemberPath = "name";
            productBox.SelectedValuePath = "id";
        }
    }
}
