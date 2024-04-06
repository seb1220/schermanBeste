using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

namespace DBDEMO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataConnection db;
        ITable<Book> Books;
        public MainWindow()
        {
            InitializeComponent();

            var options = new DataOptions().UseSQLiteMicrosoft("Data Source=books.db");

            //db = new DataConnection(options);
            db = new DataConnection("SQLite", "Data Source=books.db");

            //Books = db.GetTable<Book>();

            // Debug.WriteLine("Name: " + Books.DatabaseName);
            // Debug.WriteLine("Table Name: " + Books.TableName);
            //Debug.WriteLine("Name: " + Books.Count());

            TestDaten.ItemsSource = db.GetTable<Book>();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Book book = new Book
            {
                Name = txtName.Text,
                Year = (int)sldYear.Value,
                Author = txtAuthor.Text,
            };

            db.Insert(book);

            TestDaten.ItemsSource = db.GetTable<Book>();
        }

        private void btnAdd_Click_1(object sender, RoutedEventArgs e)
        {
            foreach (Book book in db.GetTable<Book>().Where(x => x.Author.Contains("a")))
            {
                book.Year += 10;
                db.Update(book);
            }
            TestDaten.ItemsSource = db.GetTable<Book>();
        }
    }

    [Table("Books")]
    public class Book
    {
        [PrimaryKey, Identity, Column("ID")]
        public int BookID { get; set; }

        [Column("NAME")]
        public string Name { get; set; }

        [Column("YEAR")]
        public int Year { get; set; }

        [Column("AUTHOR")]
        public string Author { get; set; }
    }
}
