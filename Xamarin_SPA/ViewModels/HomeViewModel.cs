using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Xamarin_SPA.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public HomeViewModel()
        {
            Title = "Retrieve a PDF";

            Documents = new List<string>();
        }
        
        public List<string> Documents { get; set; }
        public string SelectedDocument { get; set; }
    }
}
