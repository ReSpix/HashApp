using System;
using System.Windows;

namespace Hash
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Train[] trains = Train.FromFile("C:\\Users\\Yanovich\\Documents\\ДВФУ\\Алгоритмы и структуры\\Хеширование\\trains.txt");
            foreach(Train t in trains)
            {
                table.Add(t);
            }
            UpdateView();
        }

        HashTable table = new HashTable();

        private void Add_click(object sender, RoutedEventArgs e)
        {
            Train t = new Train(Convert.ToInt32(add_number.Text), add_destination.Text, add_time.Text);
            string res = table.Add(t);
            UpdateView(res);
        }

        public void UpdateView(string res = "")
        {
            TableView.Text = table.Show();
            result_label.Content = res;
        }

        private void Find_click(object sender, RoutedEventArgs e)
        {
            int number = Convert.ToInt32(find_number.Text);
            string result = table.Find(number);
            UpdateView(result);
        }

        private void Delete_click(object sender, RoutedEventArgs e)
        {
            int number = Convert.ToInt32(delete_number.Text);
            string result = table.Delete(number);
            UpdateView(result);
        }
    }
}