using System;
using System.Collections.Generic;
using System.Text;
using Xamarin_SPA.Models;

namespace Xamarin_SPA.ViewModels
{
    public class PdfViewModel : BaseViewModel
    {
        public Pdf Pdf { get; set; }

        public PdfViewModel(Pdf pdf = null)
        {
            this.Pdf = pdf;
        }
    }
}
