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
    /// Interaction logic for WinAuthors.xaml
    /// </summary>
    public partial class WinAuthors : Window
    {
        public IBookService bibService;
        public bool isUpdated = false;
        private bool isNew;
        public WinAuthors()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateAuthors();
            ActivateLeft();
        }
        private void PopulateAuthors()
        {
            lstAuthors.SelectedValuePath = "Id";
            lstAuthors.DisplayMemberPath = "Name";
            lstAuthors.ItemsSource = bibService.GetAuthors();
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
        private void LstAuthors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ClearControls();
            if(lstAuthors.SelectedItem != null)
            {
                Author author = (Author)lstAuthors.SelectedItem;
                txtName.Text = author.Name;
                lstBooks.ItemsSource = bibService.GetBooks(author, null);
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
            if (lstAuthors.SelectedItem != null)
            {
                isNew = false;
                ActivateRight();
                txtName.Focus();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstAuthors.SelectedItem != null)
            {
                Author author = (Author)lstAuthors.SelectedItem;
                if(bibService.IsAuthorInUse(author))
                {
                    MessageBox.Show("Deze auteur is nog in gebruik en kan niet verwijderd worden!", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (MessageBox.Show("Ben je zeker?", "Auteur wissen", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (!bibService.DeleteAuthor(author))
                    {
                        MessageBox.Show("We konden deze auteur niet verwijderen!", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    isUpdated = true;
                    ClearControls();
                    PopulateAuthors();
                }
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text.Trim();
            if (name.Length == 0)
            {
                MessageBox.Show("Je dient een naam op te geven!", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                txtName.Focus();
                return;
            }
            Author author;
            if (isNew)
            {
                author = new Author(name);
                if (!bibService.AddAuthor(author))
                {
                    MessageBox.Show("We konden de nieuwe auteur niet bewaren.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                author = (Author)lstAuthors.SelectedItem;
                author.Name = name;
                if (!bibService.UpdateAuthor(author))
                {
                    MessageBox.Show("We konden de auteur niet wijzigen.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            isUpdated = true;
            PopulateAuthors();
            lstAuthors.SelectedValue = author.Id;
            LstAuthors_SelectionChanged(null, null);
            ActivateLeft();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ActivateLeft();
            LstAuthors_SelectionChanged(null, null);
        }




    }
}
