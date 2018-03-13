using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        public int DocumentTypeId { get; set; }
        //Nav Prop
        public virtual DocumentType DocumentTypes { get; set; }

        public virtual ICollection<CourseDocument> CourseDocumentAssignments { get; set; }
        public virtual ICollection<ModuleDocument> ModuleDocumentAssignments { get; set; }
        public virtual ICollection<ActivityDocument> ActivityDocumentAssignments { get; set; }
    }
}