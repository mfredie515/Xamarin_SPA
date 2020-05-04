using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin_SPA.Models;
using Xamarin_SPA.ViewModels;

namespace Xamarin_SPA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PdfPage : ContentPage
    {
        private PdfViewModel pdfViewModel;

        public PdfPage()
        {
            InitializeComponent();

            var pdf = new Pdf();

            pdfViewModel = new PdfViewModel(pdf);
            BindingContext = pdfViewModel;
        }

        public PdfPage(PdfViewModel pdfViewModel)
        {
            InitializeComponent();

            BindingContext = this.pdfViewModel = pdfViewModel;
        }
    }
}