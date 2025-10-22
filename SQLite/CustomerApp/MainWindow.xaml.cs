using CustomerApp.Data;
using Microsoft.Win32;
using SQLite;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CustomerApp;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window {
    private List<Customer> _customers = new List<Customer>();

    public MainWindow() {
        InitializeComponent();
        //↓イメージを作る（変換）
        //PictureImage.Source = ;
        ReadDatabase();

        CustomerListView.ItemsSource = _customers;
    }

    private void ReadDatabase() {
        using (var connection = new SQLiteConnection(App.databasePath)) {
            connection.CreateTable<Customer>();
            _customers = connection.Table<Customer>().ToList();
        }
    }

    //画像ボタン
    private void PictureButton_Click(object sender, RoutedEventArgs e) {
        OpenFileDialog ofd = new OpenFileDialog();
        var ret = ofd.ShowDialog();
        if(ret ?? false) {
            
        }
    }

    //保存ボタン
    private void SaveButton_Click(object sender, RoutedEventArgs e) {
        var customer = new Customer() {
            Name = NameTextBox.Text,
            Phone = PhoneTextBox.Text,
            Address = AddressTextBox.Text,
            //Picture = PictureImage.,
        };

        using (var connection = new SQLiteConnection(App.databasePath)) {
            connection.CreateTable<Customer>();
            connection.Insert(customer);
        }

        ReadDatabase();
        CustomerListView.ItemsSource = _customers;
    }

    //削除ボタン
    private void DeleteButton_Click(object sender, RoutedEventArgs e) {
        var item = CustomerListView.SelectedItem as Customer;

        if (item == null) {
            MessageBox.Show("行を選択してください");
            return;
        }

        using (var connection = new SQLiteConnection(App.databasePath)) {
            connection.CreateTable<Customer>();
            connection.Delete(item);
            ReadDatabase();
            CustomerListView.ItemsSource = _customers;
        }
    }

    //更新ボタン
    private void UpdateButton_Click(object sender, RoutedEventArgs e) {

        var selectedCustomer = CustomerListView.SelectedItem as Customer;
        if (selectedCustomer == null) return;

        using (var connection = new SQLiteConnection(App.databasePath)) {
            connection.CreateTable<Customer>();

            var customer = new Customer() {
                Name = NameTextBox.Text,
                Phone = PhoneTextBox.Text,
                Address = AddressTextBox.Text,
                //Picture = PictureImage.,
            };

            connection.Update(customer);

            ReadDatabase();
            CustomerListView.ItemsSource = _customers;
        }
    }

    //リストビューのフィルタリング
    private void SearchTextBox_SelectionChanged(object sender, RoutedEventArgs e) {
        var filterList = _customers.Where(p => p.Name.Contains(SearchTextBox.Text));

        CustomerListView.ItemsSource = filterList;
    }

    //リストビューから１レコード選択
    private void CustomerListView_SelectionChanged(object sender, SelectionChangedEventArgs e) {
        var selectedCustomer = CustomerListView.SelectedItem as Customer;

        if (selectedCustomer == null) {
            return;
        }

        NameTextBox.Text = selectedCustomer.Name;
        PhoneTextBox.Text = selectedCustomer.Phone;
        AddressTextBox.Text = selectedCustomer.Address;
        //PictureImage. = selectedCustomer.Picture;
    }
}