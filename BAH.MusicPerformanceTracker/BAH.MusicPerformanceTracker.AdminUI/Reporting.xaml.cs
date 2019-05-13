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
using Newtonsoft.Json.Linq;

namespace BAH.MusicPerformanceTracker.AdminUI
{
    /// <summary>
    /// Interaction logic for Reporting.xaml
    /// </summary>
    public partial class Reporting : Window
    {
        public Reporting()
        {
            InitializeComponent();
        }


        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-apikey", "12345");
            client.BaseAddress = new Uri("http://musicperformancetracker.azurewebsites.net/api/");
            return client;
        }

        private void BtnReportSearches_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                HttpClient client = InitializeClient();

                string result;
                dynamic items;
                HttpResponseMessage performanceResponse;
                Searches searches;

                //Call the API
                performanceResponse = client.GetAsync("Search").Result;

                if (performanceResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //Process response
                    result = performanceResponse.Content.ReadAsStringAsync().Result;

                    //Put json into a performance list

                    searches = JsonConvert.DeserializeObject<Searches>(result);




                    if (searches != null)
                    {
                        searches.Export();
                        MessageBox.Show("Report Generated", "Success");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnReportPerformances_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HttpClient client = InitializeClient();

                string result;
                dynamic items;
                HttpResponseMessage performanceResponse;
                PerformanceList performances;

                //Call the API
                performanceResponse = client.GetAsync("Performance").Result;

                if (performanceResponse.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //Process response
                    result = performanceResponse.Content.ReadAsStringAsync().Result;

                    //Put json into a performance list
                    items = (JArray)JsonConvert.DeserializeObject(result);
                    performances = items.ToObject<PerformanceList>();
                    

                    if (performances != null)
                    {
                        performances.Export();
                        MessageBox.Show("Report Generated", "Success");
                    }

                    

                }
                else
                {
                    throw new Exception("Error: " + performanceResponse.ReasonPhrase);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
