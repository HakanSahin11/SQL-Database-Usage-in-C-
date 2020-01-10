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
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data;
using System.Globalization;


namespace Database_sql
{
    public partial class MainWindow : Window
    {
        public StreamingEntities Db = new StreamingEntities();
        public User CurrentUser;
        public Film FilmList;

        public int? CurrentUse = null;
        public List<string> TxtList { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Initiate();
        }

        public void Initiate()
        {
            TxtList = new List<string>();
            DataContext = this;

            comboboxList.SelectedIndex = 0;
            listViewItem.SelectedIndex = 0;
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!listViewItem.Items.IsEmpty)
            {
                if (CurrentUse == 1)
                {
                    CurrentUser = Db.Users.Include("UserInfo").Where(x => x.UserName == listViewItem.SelectedItem.ToString()).FirstOrDefault();
                    UserSearch(false);
                }
                else if (CurrentUse == 2)
                {

                    FilmList = Db.Films.Include("FilmDetail").Where(x => x.Titel == listViewItem.SelectedItem.ToString()).FirstOrDefault();
                    MovieSearch(false);
                }
                txtSearch.Text = string.Empty;
            }
        }

        private void ComboboxList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CurrentUse == 2 || CurrentUse == null)
            {
                CurrentUse = 1;
            }

            else if (CurrentUse == 1)
            {
                CurrentUse = 2;
            }
            ListViewSetup();
        }

        public void ListViewSetup()
        {
            listViewItem.Items.Clear();
            TxtList.Clear();

            if (CurrentUse == 1)
            {
                foreach (var item in Db.Users)
                {
                    listViewItem.Items.Add(item.UserName.ToLower());
                    TxtList.Add(item.UserName.ToLower());
                }

                listViewItem.SelectedIndex = 0;
                UserSearch(true);

            }
            else if (CurrentUse == 2)
            {
                foreach (var item in Db.Films)
                {
                    listViewItem.Items.Add(item.Titel.ToLower());
                    TxtList.Add(item.Titel.ToLower());
                }
                listViewItem.SelectedIndex = 0;
                MovieSearch(true);
            }
        }

        public void UserSearch(bool? change)
        {
            if (change == true)
            {
                lbField1.Content = "Selected User:";
                LbField2.Content = "Password:";
                LbField3.Content = "Email:";
                LbField4.Content = "FirstName:";
                LbField5.Content = "LastName:";
                LbField6.Visibility = Visibility.Visible;
                LbField6.Content = "Address:";
                txtField6.Visibility = Visibility.Visible;
            }
            txtField1.Text = CurrentUser.UserName;
            txtField2.Text = CurrentUser.Password;
            txtField3.Text = CurrentUser.Email;
            txtField4.Text = CurrentUser.UserInfo.FirstName;
            txtField5.Text = CurrentUser.UserInfo.LastName;
            txtField6.Text = CurrentUser.UserInfo.Address;
        }

        public void MovieSearch(bool? change)
        {
            if (change == true)
            {
                lbField1.Content = "Titel:";
                LbField2.Content = "Genre:";
                LbField3.Content = "Rating:";
                LbField4.Content = "Description:";
                LbField5.Content = "Playtime:";
                LbField6.Visibility = Visibility.Hidden;
                txtField6.Visibility = Visibility.Hidden;
            }
            txtField1.Text = FilmList.Titel;
            txtField2.Text = FilmList.Genre;
            txtField3.Text = FilmList.Rating.ToString();
            txtField4.Text = FilmList.FilmDetail.Description;
            txtField5.Text = FilmList.FilmDetail.Playtime.ToString();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (checkBox.IsChecked == true)
            {
                Gridname.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                listViewItem.Background = new SolidColorBrush(Color.FromRgb(128, 128, 128));
                txtSearch.Background = new SolidColorBrush(Color.FromRgb(220, 220, 220));
            }

            else
            {
                Gridname.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                listViewItem.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                txtSearch.Background = new SolidColorBrush(Color.FromRgb(255, 255, 255));
            }
        }

        private void BtnSubmit_Click(object sender, RoutedEventArgs e)
        {
            listViewItem.SelectedItem = txtSearch.Text;
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentUse == 1)
            {
                SqlSetup("[User]", "UserName", txtField1.Text, CurrentUser.Id);
                SqlSetup("[User]", "Password", txtField2.Text, CurrentUser.Id);
                SqlSetup("[User]", "Email", txtField3.Text, CurrentUser.Id);
                SqlSetup("[UserInfo]", "FirstName", txtField4.Text, CurrentUser.Id);
                SqlSetup("[UserInfo]", "LastName", txtField5.Text, CurrentUser.Id);
                SqlSetup("[UserInfo]", "Address", txtField1.Text, CurrentUser.Id);
                MessageBox.Show("Worked");
                Db = new StreamingEntities();
                ListViewSetup();
                return;
            }
            else if (CurrentUse == 2)
            {
                SqlSetup("[Film]", "Titel", txtField1.Text, CurrentUser.Id);
                SqlSetup("[Film]", "Genre", txtField2.Text, CurrentUser.Id);
                SqlSetup("[Film]", "Rating", txtField3.Text, CurrentUser.Id);
                SqlSetup("[FilmDetails]", "Description", txtField4.Text, CurrentUser.Id);
                SqlSetup("[FilmDetails]", "Playtime", txtField5.Text, CurrentUser.Id);
                MessageBox.Show("Worked");
                Db = new StreamingEntities();
                ListViewSetup();
                return;
            }
            MessageBox.Show("Error! Not saved!");
        }
        public void SqlSetup(string table, string section, string value, int Id)
        {
            var connectionString = @"Data Source=HAKAN-SKOLEPC;Initial Catalog=Streaming; Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            string CmdCommand = $"UPDATE {table} SET {section} = '{value}' WHERE id = {Id}";
            var conn = new SqlConnection(connectionString);
            var cmd = new SqlCommand(CmdCommand, conn);
            var adapter = new SqlDataAdapter(cmd);
            var resultTable = new DataTable();
            adapter.Fill(resultTable);
        }

    }
}
