using System;
using System.Windows;
using System.Windows.Controls;
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

        private void PopulateBooks(Book bookToSelect = null)
        {
            ClearControls();

            lstBooks.SelectedValuePath = "Id";
            lstBooks.ItemsSource = null;
            Author author = (Author)cmbFilterAuthor.SelectedItem;
            Publisher publisher = (Publisher)cmbFilterPublisher.SelectedItem;
            lstBooks.ItemsSource = bibService.GetBooks(author, publisher);

            if(bookToSelect != null)
            {
                lstBooks.SelectedValue = bookToSelect.Id;
            }
        }

        private void PopulateAuthors()
        {
            cmbFilterAuthor.ItemsSource = null;
            cmbAuthor.ItemsSource = null;

            cmbFilterAuthor.SelectedValuePath = "Id";
            cmbAuthor.SelectedValuePath = "Id";

            cmbFilterAuthor.ItemsSource = bibService.Authors;
            cmbAuthor.ItemsSource = bibService.Authors;
        }

        private void PopulatePublishers()
        {
            cmbFilterPublisher.ItemsSource = null;
            cmbPublisher.ItemsSource = null;

            cmbFilterPublisher.SelectedValuePath = "Id";
            cmbPublisher.SelectedValuePath = "Id";

            cmbFilterPublisher.ItemsSource = bibService.Publishers;
            cmbPublisher.ItemsSource = bibService.Publishers;
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
            // door selectie aan te passen wordt selection changed handler van combobox afgevuurd
            // en zo uiteindelijk ook de boekenlijst automatisch vernieuwd
            cmbFilterAuthor.SelectedIndex = -1;
        }

        private void BtnClearFilterPublisher_Click(object sender, RoutedEventArgs e)
        {
            // door selectie aan te passen wordt selection changed handler van combobox afgevuurd
            // en zo uiteindelijk ook de boekenlijst automatisch vernieuwd
            cmbFilterPublisher.SelectedIndex = -1;
        }

        private void LstBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstBooks.SelectedItem != null)
            {
                Book book = (Book)lstBooks.SelectedItem;
                txtTitle.Text = book.Title;
                txtYear.Text = book.Year.ToString();
                cmbAuthor.SelectedValue = book.Author.Id;
                cmbPublisher.SelectedValue = book.Publisher.Id;
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
                ShowError("Je dient een titel op te geven!", "Fout", txtTitle);
                return;
            }

            Author author = (Author)cmbAuthor.SelectedItem;
            if (author == null)
            {
                ShowError("Je dient een auteur te selecteren!", "Fout", cmbAuthor);
                return;
            }
            
            Publisher publisher = (Publisher)cmbPublisher.SelectedItem;
            if (publisher == null)
            {
                ShowError("Je dient een uitgeverij te selecteren!", "Fout", cmbPublisher);
                return;
            }
            
            bool yearOk = int.TryParse(txtYear.Text, out int year);
            if (!yearOk)
            {
                ShowError("Je dient als jaar een getal in te voeren!", "Fout", txtYear);
                return;
            }

            if (isNew)
            {
                AddBook(title, author, publisher, year);
            }
            else
            {
                Book book = (Book)lstBooks.SelectedItem;
                UpdateBook(book, title, author, publisher, year);
            }
        }

        private void AddBook(string title, Author author, Publisher publisher, int year)
        {
            try
            {
                Book book = new Book(title, author, publisher, year);
                if (!bibService.AddBook(book))
                {
                    throw new Exception("Nieuw boek kon niet bewaard worden");
                }
                RefreshBooksAfterUpdate(book);
            }
            catch (Exception ex)
            {
                // exceptie kan vanuit twee plaatsen optreden:
                // 1: vanuit constructor Book indien een van de gegevens niet geldig is (dus vanuit class lib)
                // 2: de exceptie die hierboven opgegooid wordt indien het opslaan niet lukt
                ShowError(ex.Message, "Fout bij aanmaken boek");
            }
        }

        private void UpdateBook(Book book, string title, Author author, Publisher publisher, int year)
        {
            try
            {
                book.Title = title;
                book.Author = author;
                book.Publisher = publisher;
                book.Year = year;
                if (!bibService.UpdateBook(book))
                {
                    throw new Exception("Wijziging boek kon niet bewaard worden");
                }
                RefreshBooksAfterUpdate(book);
            }
            catch (Exception ex)
            {
                // exceptie kan vanuit twee plaatsen optreden:
                // 1: vanuit de setter van een van de properties indien de waarde niet geldig is (dus vanuit class lib)
                // 2: de exceptie die hierboven opgegooid wordt indien het opslaan niet lukt
                ShowError(ex.Message, "Fout bij wijzigen boek");
            }
        }

        private void RefreshBooksAfterUpdate(Book updatedBook)
        {
            PopulateBooks(updatedBook);
            ActivateLeft();
        }

        private void ShowError(string message, string title, Control controlToFocus = null)
        {
            MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);
            if(controlToFocus != null)
            {
                controlToFocus.Focus();
            }
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
            winAuthors.ShowDialog();
            // code gaat hier verder van zodra winAuthors gesloten wordt
            if(winAuthors.IsUpdated)
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
            winPublishers.ShowDialog();
            // code gaat hier verder van zodra winPublishers gesloten wordt
            if (winPublishers.IsUpdated)
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
