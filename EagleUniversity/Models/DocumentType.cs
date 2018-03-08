using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EagleUniversity.Models
{
    public class DocumentType
    {
        public int Id { get; set; }
        public string DocumentTypeName { get; set; }
        //nav prop
        public virtual ICollection<Document> Document { get; set; }
    }
}