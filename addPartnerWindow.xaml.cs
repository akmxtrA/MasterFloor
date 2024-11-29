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
    /// Логика взаимодействия для addPartnerWindow.xaml
    /// </summary>
    public partial class addPartnerWindow : Window
    {
        MasterFloorEntities db = new MasterFloorEntities();
        public addPartnerWindow()
        {
            InitializeComponent();
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }

        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            clear();
            MessageBox.Show("Поля успешно очищены.");
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (nameCompanyTxt.Text != "" && typeBox.Text != "" && ratingTxt.Text != "" && indexTxt.Text != "" && areaTxt.Text != "" && cityTxt.Text != "" && streetTxt.Text != "" && numberHomeTxt.Text != "" && lastnameTxt.Text != "" && nameTxt.Text != "" && fathernameTxt.Text != "" && telephoneTxt.Text != "" && emailTxt.Text != "")
                {
                    //Запись данных в таблицу с контактами партнёра
                    partnerContact partnerContact = new partnerContact()
                    {
                        name = nameTxt.Text,
                        lastname = lastnameTxt.Text,
                        fathername = fathernameTxt.Text,
                        telephone = telephoneTxt.Text,
                        email = emailTxt.Text
                    };
                    db.partnerContact.Add(partnerContact);
                    //Запись данных в таблицу адреса партнёра
                    adress adress = new adress()
                    {
                        postalCode = Convert.ToInt32(indexTxt.Text),
                        area = areaTxt.Text,
                        city = cityTxt.Text,
                        street = streetTxt.Text,
                        houseNumber = numberHomeTxt.Text

                    };
                    db.adress.Add(adress);
                    //Запись данных в таблицу с партнёром
                    partners partners = new partners()
                    {
                        name = nameCompanyTxt.Text,
                        rating = Convert.ToInt32(ratingTxt.Text),
                        id_adress = adress.id,
                        id_contact = partnerContact.id,
                        id_partnerType = (int)typeBox.SelectedValue
                    };
                    db.partners.Add(partners);
                    //Запись данных в таблицу истории реализации продуктов партнёром
                    partnerProducts partnerProducts = new partnerProducts()
                    {
                        id_partner = partners.id,
                        count = 0
                    };
                    db.partnerProducts.Add(partnerProducts);
                    db.SaveChanges();
                    MessageBox.Show("Партнёр " + partners.name + " успешно добавлен!");

                    clear();
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

        //Метод для очистки всех полей на форме
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Заполнение комбобокса из БД
            typeBox.ItemsSource = db.partnerType.ToList();
            typeBox.DisplayMemberPath = "name";
            typeBox.SelectedValuePath = "id";
        }
    }
}
