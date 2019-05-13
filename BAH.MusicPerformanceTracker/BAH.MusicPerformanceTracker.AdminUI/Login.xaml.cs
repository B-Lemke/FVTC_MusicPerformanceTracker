using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using BAH.MusicPerformanceTracker.BL;
using Newtonsoft.Json;

namespace BAH.MusicPerformanceTracker.AdminUI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = InitializeClient();
            HttpResponseMessage response = client.GetAsync("User?userName=" + txtUsername.Text + "&userPass=" + txtPassword.Password).Result;


            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                string result = response.Content.ReadAsStringAsync().Result;
                User user = JsonConvert.DeserializeObject<User>(result);
                Reporting reporting = new Reporting();
                reporting.Show();

                this.Close();

            }
            else
            {
                MessageBox.Show("Cannot Log In", "Error");
            }
        }


        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-apikey", "12345");
            client.BaseAddress = new Uri("http://musicperformancetracker.azurewebsites.net/api/");
            return client;
        }
    }
}
