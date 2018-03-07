using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EagleUniversity.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string DocumentName { get; set; }
        public string DocumentContent { get; set; }
        public DateTime UploadDate { get; set; }
        public DateTime DueDate { get; set; }
    }
}