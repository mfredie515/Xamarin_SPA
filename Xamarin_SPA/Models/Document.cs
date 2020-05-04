using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_SPA.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string Filename { get; set; }
        public byte[] Data { get; set; }
    }
}
