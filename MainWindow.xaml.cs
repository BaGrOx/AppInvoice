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

namespace AppInvoice
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private int clientID = 0;
        private bool changeClientInformation = false;
        private void btnLoadClients_Click(object sender, RoutedEventArgs e)
        {
            var db = new Database1Entities();

            
            gridClients.ItemsSource = db.Table.ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var db = new Database1Entities();

            var clientObject = new Table()
            {
                FullName = txtFullName.Text,
                Mail = txtMail.Text,
                Company = txtCompany.Text,
                NIP = txtNIP.Text,
                Telephone = txtTelephone.Text,
                City = txtCity.Text,
                Address = txtAddress.Text
            };


            db.Table.Add(clientObject);
            db.SaveChanges();
            gridClients.Items.Refresh();
            ClearTextBox();

        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    btnSave.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                    break;
            }
        }

        public void ClearTextBox()
        {
            List<TextBox> clientsInformation = new List<TextBox> { txtFullName, txtMail, txtCompany,
                txtNIP, txtTelephone, txtCity, txtAddress};

            foreach (var item in clientsInformation)
            {
                item.Text = string.Empty;
                item.Text = null;
            }
        }

        private void btnChangeClients_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void btnDELETE_Click(object sender, RoutedEventArgs e)
        {


            var db = new Database1Entities();
            var r = from d in db.Table
                    where d.Id == clientID
                    select d;


            Table obj = r.SingleOrDefault();
            if (obj != null)
            {
                db.Table.Remove(obj);
                db.SaveChanges();
            }
        }

        private void gridClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           

            if(gridClients.SelectedIndex >= 0)
            {
                if(gridClients.SelectedItems.Count >= 0)
                {
                    if(gridClients.SelectedItems[0].GetType() == typeof(Table))
                    {
                        var rowSelected = (Table)gridClients.SelectedItems[0];
                        clientID = rowSelected.Id;
                        changeClientInformation = true;
                        
                    }
                }
            }

        }

    }
}
