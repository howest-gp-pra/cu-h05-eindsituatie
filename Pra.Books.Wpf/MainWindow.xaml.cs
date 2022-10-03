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
using Pra.Books.Core.Entities;
using Pra.Books.Core.Services;
using Pra.Books.Core.Interfaces;

namespace Pra.Books.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IBookService bibService = new BookServiceMem();
        private bool isNew;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PopulateBooks();
            PopulateAuthors();
            PopulatePublishers();
            ActivateLeft();
        }

        private void PopulateBooks()
        {
            ClearControls();
            lstBooks.ItemsSource = null;
            Author author = (Author)cmbFilterAuthor.SelectedItem;
            Publisher publisher = (Publisher)cmbFilterPublisher.SelectedItem;
            lstBooks.ItemsSource = bibService.GetBooks(author, publisher);
            lstBooks.SelectedValuePath = "Id";
        }

        private void PopulateAuthors()
        {
            cmbFilterAuthor.ItemsSource = null;
            cmbAuthor.ItemsSource = null;

            cmbFilterAuthor.SelectedValuePath = "Id";
            cmbFilterAuthor.DisplayMemberPath = "Name"; 
            cmbAuthor.SelectedValuePath = "Id";
            cmbAuthor.DisplayMemberPath = "Name";

            cmbFilterAuthor.ItemsSource = bibService.GetAuthors();
            cmbAuthor.ItemsSource = bibService.GetAuthors();
        }

        private void PopulatePublishers()
        {
            cmbFilterPublisher.ItemsSource = null;
            cmbPublisher.ItemsSource = null;

            cmbFilterPublisher.SelectedValuePath = "Id";
            cmbFilterPublisher.DisplayMemberPath = "Name";
            cmbPublisher.SelectedValuePath = "Id";
            cmbPublisher.DisplayMemberPath = "Name";

            cmbFilterPublisher.ItemsSource = bibService.GetPublishers();
            cmbPublisher.ItemsSource = bibService.GetPublishers();
        }

        private void ClearControls()
        {
            txtTitle.Text = "";
            txtYear.Text = "";
            cmbAuthor.SelectedIndex = -1;
            cmbPublisher.SelectedIndex = -1;
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

        private void CmbFilterAuthor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PopulateBooks();
        }

        private void CmbFilterPublisher_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PopulateBooks();
        }

        private void BtnClearFilterAuthor_Click(object sender, RoutedEventArgs e)
        {
            // door selectie aan te passen wordt selection changed handler van combobox afgevuurd en zo ook de boekenlijst vernieuwd
            cmbFilterAuthor.SelectedIndex = -1;
        }

        private void BtnClearFilterPublisher_Click(object sender, RoutedEventArgs e)
        {
            cmbFilterPublisher.SelectedIndex = -1;
        }

        private void LstBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstBooks.SelectedItem != null)
            {
                Book book = (Book)lstBooks.SelectedItem;
                txtTitle.Text = book.Title;
                txtYear.Text = book.Year.ToString();
                cmbAuthor.SelectedValue = book.AuthorId;
                cmbPublisher.SelectedValue = book.PublisherId;
            }
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            isNew = true;
            ActivateRight();
            ClearControls();
            txtTitle.Focus();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (lstBooks.SelectedItem != null)
            {
                isNew = false;
                ActivateRight();
                txtTitle.Focus();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            ActivateLeft();
            LstBooks_SelectionChanged(null, null);
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string title = txtTitle.Text.Trim();
            if (title.Length == 0)
            {
                MessageBox.Show("Je dient een titel op te geven!", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                txtTitle.Focus();
                return;
            }

            if (cmbAuthor.SelectedItem == null)
            {
                MessageBox.Show("Je dient een auteur te selecteren!", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                cmbAuthor.Focus();
                return;
            }
            Author author = (Author)cmbAuthor.SelectedItem;
            
            if (cmbPublisher.SelectedItem == null)
            {
                MessageBox.Show("Je dient een uitgever te selecteren!", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                cmbPublisher.Focus();
                return;
            }
            Publisher publisher = (Publisher)cmbPublisher.SelectedItem;
            
            bool yearOk = int.TryParse(txtYear.Text, out int year);
            if (!yearOk)
            {
                MessageBox.Show("Je dient als jaar een getal in te voeren!", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                txtYear.Focus();
                return;
            }

            Book book;
            if (isNew)
            {
                book = new Book(title, author.Id, publisher.Id, year);
                if (!bibService.AddBook(book))
                {
                    MessageBox.Show("We konden het nieuwe boek niet bewaren.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                book = (Book)lstBooks.SelectedItem;
                book.Title = title;
                book.AuthorId = author.Id;
                book.PublisherId = publisher.Id;
                book.Year = year;
                if (!bibService.UpdateBook(book))
                {
                    MessageBox.Show("We konden het boek niet wijzigen.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

            PopulateBooks();
            lstBooks.SelectedValue = book.Id;
            ActivateLeft();
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (lstBooks.SelectedItem != null)
            {
                if (MessageBox.Show("Ben je zeker?", "Boek wissen", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Book book = (Book)lstBooks.SelectedItem;
                    if (!bibService.DeleteBook(book))
                    {
                        MessageBox.Show("We konden het boek niet verwijderen!", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    ClearControls();
                    PopulateBooks();
                }
            }
        }
        
        private void RdbInMemory_Checked(object sender, RoutedEventArgs e)
        {
            bibService = new BookServiceMem();
            Window_Loaded(null, null);
        }

        private void RdbFromDatabase_Checked(object sender, RoutedEventArgs e)
        {
            bibService = new BookServiceDB();
            Window_Loaded(null, null);
        }

        private void BtnAuthors_Click(object sender, RoutedEventArgs e)
        {
            WinAuthors winAuthors = new WinAuthors(bibService);
            bool? authorsUpdated = winAuthors.ShowDialog();
            // code gaat hier verder van zodra winAuthors gesloten wordt
            if(authorsUpdated == true)
            {
                // bewaar geselecteerd boek en auteur
                Guid? bookId = null;
                if (lstBooks.SelectedItem != null)
                    bookId = ((Book)lstBooks.SelectedItem).Id;
                Guid? filterAuthorId = null;
                if (cmbFilterAuthor.SelectedItem != null)
                    filterAuthorId = ((Author)cmbFilterAuthor.SelectedItem).Id;

                // vernieuw auteurs
                PopulateAuthors();
                // herstel selectie in filter
                cmbFilterAuthor.SelectedValue = filterAuthorId;

                // herselecteer huidig boek (zodat auteur vernieuwd wordt)
                lstBooks.UnselectAll();
                lstBooks.SelectedValue = bookId;
            }
        }

        private void BtnPublishers_Click(object sender, RoutedEventArgs e)
        {
            WinPublishers winPublishers = new WinPublishers(bibService);
            bool? publishersUpdated = winPublishers.ShowDialog();
            // code gaat hier verder van zodra winPublishers gesloten wordt
            if (publishersUpdated == true)
            {
                // bewaar geselecteerd boek en uitgeverij
                Guid? bookId = null;
                if (lstBooks.SelectedItem != null)
                    bookId = ((Book)lstBooks.SelectedItem).Id;
                Guid? filterPublisherId = null;
                if (cmbFilterPublisher.SelectedItem != null)
                    filterPublisherId = ((Publisher)cmbFilterPublisher.SelectedItem).Id;

                // vernieuw uitgevers
                PopulatePublishers();
                // herstel selectie in filter
                cmbFilterPublisher.SelectedValue = filterPublisherId;

                // herselecteer huidig boek (zodat uitgeverij vernieuwd wordt)
                lstBooks.UnselectAll();
                lstBooks.SelectedValue = bookId;
            }
        }
    }
}
