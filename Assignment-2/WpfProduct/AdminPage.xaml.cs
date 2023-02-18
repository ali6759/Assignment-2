using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
using Newtonsoft.Json;
using System.Web;

namespace WpfProduct
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Window
    {
        public AdminPage()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7206/");

            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("Product").Result;
            var product = response.Content.ReadAsStringAsync().Result;

            dynamic jsonClass = JsonConvert.DeserializeObject<dynamic>(product);
            DGrid.ItemsSource = jsonClass;

        }

        private void DelButton_Click(object sender, RoutedEventArgs e)
        {

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7206/");
            HttpResponseMessage response = client.DeleteAsync("Product/" + TxtDelProdId.Text).Result;
            var product = response.Content.ReadAsStringAsync().Result;


            

            client.DefaultRequestHeaders.Accept.Add(
               new MediaTypeWithQualityHeaderValue("application/json"));

             response = client.GetAsync("Product").Result;
             product = response.Content.ReadAsStringAsync().Result;

            dynamic jsonClass = JsonConvert.DeserializeObject<dynamic>(product);
            DGrid.ItemsSource = jsonClass;


        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7206/");

            client.DefaultRequestHeaders.Accept.Add(
             new MediaTypeWithQualityHeaderValue("application/json"));
            var value = new Dictionary<string, string>
             {
                { "productId", txtProductAdd.Text },
                { "productName", txtNameAdd.Text },
                { "Amount", txtAmountAdd.Text },
                { "Price", txtPriceAdd.Text }
             };

            StringContent queryString = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PutAsync("Product", queryString).Result;
            var product = response.Content.ReadAsStringAsync().Result;

            response = client.GetAsync("Product").Result;
            product = response.Content.ReadAsStringAsync().Result;

            dynamic jsonClass = JsonConvert.DeserializeObject<dynamic>(product);
            DGrid.ItemsSource = jsonClass;
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7206/");

            client.DefaultRequestHeaders.Accept.Add(
             new MediaTypeWithQualityHeaderValue("application/json"));
            var value = new Dictionary<string, string>
             {
                { "productId", txtProductUpdate.Text },
                { "productName", txtNameUpdate.Text },
                { "Amount", txtAmountUpdate.Text },
                { "Price", txtPriceUpdate.Text }
             };

            StringContent queryString = new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");

            HttpResponseMessage response = client.PostAsync("Product", queryString).Result;
            var product = response.Content.ReadAsStringAsync().Result;

            response = client.GetAsync("Product").Result;
            product = response.Content.ReadAsStringAsync().Result;

            dynamic jsonClass = JsonConvert.DeserializeObject<dynamic>(product);
            DGrid.ItemsSource = jsonClass;
        }
    }
}
