using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EagleUniversity.Models
{
    public class commonEntity
    {
        public int Id { get; set; }
        public string EntityType { get; set; }
        public string returnController { get; set; }
        public string returnMethod { get; set; }
        public int returnId { get; set; }
        public string returnTarget { get; set; }
    }
}