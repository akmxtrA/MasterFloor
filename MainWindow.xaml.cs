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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MasterFloor
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MasterFloorEntities db = new MasterFloorEntities();
        public MainWindow()
        {
            InitializeComponent();
        }

        //Выбор партнёра из списка
        private void partnersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var partner = db.partners.FirstOrDefault(x => x.id == (int)partnersList.SelectedValue);
            MessageBox.Show("Выбран партнёр: " + partner.name);
            Partner.id = partner.id;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Partner.id = 0;

            //Группировка записей по Партнёру в таблице истории реализации продуктов партнёрами
            double discount;
            var partnerCounts = db.partnerProducts.GroupBy(p => p.id_partner).Select(g => new { id_partner = g.Key, total_count = g.Sum(p => p.count) }).ToList();
            //Рассчёт персональной скидки по общему количеству реализации продуктов партнёром
            var partnerCountsWithDiscount = new List<PartnerDiscount>();
            for (int i = 0; i < partnerCounts.Count; i++)
            {
                if (partnerCounts[i].total_count > 300000)
                {
                    discount = 0.15;
                }
                else if (partnerCounts[i].total_count > 50000 && partnerCounts[i].total_count <= 300000)
                {
                    discount = 0.1;
                }
                else if (partnerCounts[i].total_count > 10000 && partnerCounts[i].total_count < 50000)
                {
                    discount = 0.05;
                }
                else
                {
                    discount = 0;
                }
                var partnerDiscount = new PartnerDiscount
                {
                    id_partner = Convert.ToInt32(partnerCounts[i].id_partner),
                    total_count = Convert.ToInt32(partnerCounts[i].total_count),
                    discount = discount
                };

                partnerCountsWithDiscount.Add(partnerDiscount);

            }

            //Запись таблиц из БД в переменные
            var partners = db.partners.ToList();
            var contacts = db.partnerContact.ToList();
            var type = db.partnerType.ToList();
            //Соединение таблиц в одну через ключи
            var partnersType = from p in partners join t in type on p.id_partnerType equals t.id select new { id = p.id, name = p.name, rating = p.rating, id_contact = p.id_contact, type = t.name };
            var partnersContacts = from p in partnersType join c in contacts on p.id_contact equals c.id select new { id = p.id, name = p.name, rating = p.rating, type = p.type, director = c.lastname + " " + c.name + " " + c.fathername, telephone = c.telephone };
            var partnersContactsWithDiscount = from p in partnersContacts join d in partnerCountsWithDiscount on p.id equals d.id_partner select new { id = p.id, name = p.name, rating = p.rating, type = p.type, director = p.director, telephone = p.telephone, discount = d.discount };

            //Вывод списка партнёров
            partnersList.ItemsSource = partnersContactsWithDiscount.ToList();
        }

        private void addPartnerBtn_Click(object sender, RoutedEventArgs e)
        {
            addPartnerWindow addPartner = new addPartnerWindow();
            addPartner.Show();
            this.Close();
        }

        private void redactPartnerBtn_Click(object sender, RoutedEventArgs e)
        {
            if(Partner.id != 0)
            {
                redactPartnerWindow redactPartner = new redactPartnerWindow();
                redactPartner.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Сначала выберите партнёра для изменения.");
            }           
        }

        private void historyRealizationBtn_Click(object sender, RoutedEventArgs e)
        {
            historyRealization historyRealization = new historyRealization();
            historyRealization.Show();
            this.Close();
        }

        private void materialBtn_Click(object sender, RoutedEventArgs e)
        {
            materialForProduct materialForProduct = new materialForProduct();
            materialForProduct.Show();
            this.Close();
        }
    }
}
