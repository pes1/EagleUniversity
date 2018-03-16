using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EagleUniversity.Models
{
    public class DocumentEntity
    {
        public int Id { get; set; }
        public string EntityType { get; set; }
        public string EntityName { get; set; }
        public string EntityFull { get { return EntityType + " " + EntityName; }}
    }
}