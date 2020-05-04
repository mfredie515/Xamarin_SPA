using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_SPA.Models;
using Xamarin_SPA.ViewModels;

namespace Xamarin_SPA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        private const string LOCALHOST = @"https://localhost:44350";

        public HomePage()
        {
            InitializeComponent();            
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(LOCALHOST);

                HttpResponseMessage message = await client.GetAsync("/api/document");

                var content = await message.Content.ReadAsStringAsync();

                var documents = JsonConvert.DeserializeObject<IEnumerable<Document>>(content);

                ((HomeViewModel)BindingContext).Documents = new List<string>();
                ((HomeViewModel)BindingContext).Documents.AddRange(documents.Select(d => d.Filename));

                foreach (Document document in documents)
                    cboDocument.Items.Add(document.Filename);
            }
        }

        private async void btnRetrieve_Clicked(object sender, EventArgs e)
        {
            Pdf pdf;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(LOCALHOST);

                HttpResponseMessage message = await client.GetAsync("/api/document?id=" + cboDocument.SelectedIndex);
                var content = await message.Content.ReadAsStringAsync();
                var document = JsonConvert.DeserializeObject<IEnumerable<Document>>(content);
                pdf = new Pdf
                {
                    Base64Encoded = System.Convert.ToBase64String(document.First().Data)
                };
            }

            await Navigation.PushAsync(new PdfPage(new ViewModels.PdfViewModel(pdf)));
        }
    }
}