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
using Pra.Books.Core.Entities;
using Pra.Books.Core.Services;
using Pra.Books.Core.Interfaces;

namespace Pra.Books.Wpf
{
    /// <summary>
    /// Interaction logic for WinPublishers.xaml
    /// </summary>
    public partial class WinPublishers : Window
    {
        public IBookService bibService;
        public bool isUpdated = false;
        private bool isNew;
        public WinPublishers()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PopulatePublishers();
            ActivateLeft();
        }
        private void PopulatePublishers()
        {
            lstPublishers.SelectedValuePath = "Id";
            lstPublishers.DisplayMemberPath = "Name";
            lstPublishers.ItemsSource = bibService.GetPublishers();
        }
        private void ClearControls()
        {
            txtName.Text = "";
            lstBooks.ItemsSource = null;
            lstBooks.Items.Refresh();
        }
        private void ActivateLeft()
        {
            grpLeft.IsEnabled = true;
            grpRight.IsEnabled = false;
            btnSave.Visibility = Visibility.Hidden;
            btnCancel.Visibility = Visibility.Hidden;
        }
        private void ActivateRight()
        {
            grpLeft.IsEnabled = false;
            grpRight.IsEnabled = true;
            btnSave.Visibility = Visibility.Visible;
            btnCancel.Visibility = Visibility.Visible;
        }

        private void LstPublishers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearControls();
            if (lstPublishers.SelectedItem != null)
            {
                Publisher publisher = (Publisher)lstPublishers.SelectedItem;
                txtName.Text = publisher.Name;
                lstBooks.ItemsSource = bibService.GetBooks(null, publisher);
            }
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            isNew = true;
            ActivateRight();
            ClearControls();
            txtName.Focus();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lstPublishers.SelectedItem != null)
            {
                isNew = false;
                ActivateRight();
                txtName.Focus();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstPublishers.SelectedItem != null)
            {
                Publisher publisher = (Publisher)lstPublishers.SelectedItem;
                if (bibService.IsPublisherInUse(publisher))
                {
                    MessageBox.Show("Deze uitgever is nog in gebruik en kan niet verwijderd worden!", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (MessageBox.Show("Ben je zeker?", "Uitgever wissen", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (!bibService.DeletePublisher(publisher))
                    {
                        MessageBox.Show("We konden deze uitgever niet verwijderen!", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    isUpdated = true;
                    ClearControls();
                    PopulatePublishers();
                }
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text.Trim();
            if (name.Length == 0)
            {
                MessageBox.Show("Je dient een naam op te geven !", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                txtName.Focus();
                return;
            }
            Publisher publisher;
            if (isNew)
            {
                publisher = new Publisher(name);
                if (!bibService.AddPublisher(publisher))
                {
                    MessageBox.Show("We konden de nieuwe uitgever niet bewaren.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                publisher = (Publisher)lstPublishers.SelectedItem;
                publisher.Name = name;
                if (!bibService.UpdatePublisher(publisher))
                {
                    MessageBox.Show("We konden de uitgever niet wijzigen.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            isUpdated = true;
            PopulatePublishers();
            lstPublishers.SelectedValue = publisher.Id;
            LstPublishers_SelectionChanged(null, null);
            ActivateLeft();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ActivateLeft();
            LstPublishers_SelectionChanged(null, null);
        }

    }
}
