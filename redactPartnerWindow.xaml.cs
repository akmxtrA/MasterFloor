using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
    /// Логика взаимодействия для redactPartnerWindow.xaml
    /// </summary>
    public partial class redactPartnerWindow : Window
    {
        MasterFloorEntities db = new MasterFloorEntities();
        public redactPartnerWindow()
        {
            InitializeComponent();
        }

        private void redactBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Запись таблиц из БД в переменные
                var partner = db.partners.FirstOrDefault(x => x.id == Partner.id);
                var partnerType = db.partnerType.FirstOrDefault(x => x.id == partner.id_partnerType);
                var partnerAdress = db.adress.FirstOrDefault(x => x.id == partner.id_adress);
                var partnerContact = db.partnerContact.FirstOrDefault(x => x.id == partner.id_contact);

                //Если все поля заполненны, то записываем изменённые данные в БД
                if (nameCompanyTxt.Text != "" && typeBox.Text != "" && ratingTxt.Text != "" && indexTxt.Text != "" && areaTxt.Text != "" && cityTxt.Text != "" && streetTxt.Text != "" && numberHomeTxt.Text != "" && lastnameTxt.Text != "" && nameTxt.Text != "" && fathernameTxt.Text != "" && telephoneTxt.Text != "" && emailTxt.Text != "")
                {
                    partnerContact.name = nameTxt.Text;
                    partnerContact.lastname = lastnameTxt.Text;
                    partnerContact.fathername = fathernameTxt.Text;
                    partnerContact.telephone = telephoneTxt.Text;
                    partnerContact.email = emailTxt.Text;
                    partner.id_partnerType = Convert.ToInt32(typeBox.SelectedValue);
                    partnerAdress.postalCode = Convert.ToInt32(indexTxt.Text);
                    partnerAdress.area = areaTxt.Text;
                    partnerAdress.city = cityTxt.Text;
                    partnerAdress.street = streetTxt.Text;
                    partnerAdress.houseNumber = numberHomeTxt.Text;
                    partner.name = nameCompanyTxt.Text;
                    partner.rating = Convert.ToInt32(ratingTxt.Text);
                    partner.id_adress = partnerAdress.id;
                    partner.id_contact = partnerContact.id;

                    db.SaveChanges();
                    MessageBox.Show("Партнёр " + partner.name + " успешно изменён!");
                }
                else
                {
                    MessageBox.Show("Заполните все поля и повторите попытку.");
                }
            }
            catch
            {
                MessageBox.Show("Возникла ошибка, попробуйте снова.");
            }
            
        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            clear();
            MessageBox.Show("Поля успешно очищены.");
        }

        //Метод очистки всех  полей на форме
        private void clear()
        {
            nameCompanyTxt.Text = string.Empty;
            typeBox.Text = string.Empty;
            ratingTxt.Text = string.Empty;
            indexTxt.Text = string.Empty;
            areaTxt.Text = string.Empty;
            cityTxt.Text = string.Empty;
            streetTxt.Text = string.Empty;
            numberHomeTxt.Text = string.Empty;
            lastnameTxt.Text = string.Empty;
            nameTxt.Text = string.Empty;
            fathernameTxt.Text = string.Empty;
            telephoneTxt.Text = string.Empty;
            emailTxt.Text = string.Empty;
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Заполнение комбобокса из БД
            typeBox.ItemsSource = db.partnerType.ToList();
            typeBox.DisplayMemberPath = "name";
            typeBox.SelectedValuePath = "id";

            //Запись таблиц из БД в переменные
            Partner part = new Partner();
            var partner = db.partners.FirstOrDefault(x => x.id == Partner.id);
            var partnerType = db.partnerType.FirstOrDefault(x => x.id == partner.id_partnerType);
            var partnerAdress = db.adress.FirstOrDefault(x => x.id == partner.id_adress);
            var partnerContact = db.partnerContact.FirstOrDefault(x => x.id == partner.id_contact);

            //Заполнение элементов на форме данными о изменяемом партнёре
            nameCompanyTxt.Text = partner.name;
            typeBox.Text = partnerType.name;
            ratingTxt.Text = Convert.ToString(partner.rating);
            indexTxt.Text = Convert.ToString(partnerAdress.postalCode);
            areaTxt.Text = partnerAdress.area;
            cityTxt.Text = partnerAdress.city;
            streetTxt.Text = partnerAdress.street;
            numberHomeTxt.Text = partnerAdress.houseNumber;
            lastnameTxt.Text = partnerContact.lastname;
            nameTxt.Text = partnerContact.name;
            fathernameTxt.Text = partnerContact.fathername;
            telephoneTxt.Text = partnerContact.telephone;
            emailTxt.Text = partnerContact.email;
        }
    }
}
