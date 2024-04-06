using System;
using System.Linq;
using System.Windows;

namespace Osterhase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            using (var db = new PersonDB())
            {
                var q =
                    from c in db.Customers
                    select c;

                foreach (var c in q)
                    Console.WriteLine(c.ContactName);
            }
        }
    }
}
