using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EagleUniversity.Models
{
    public class Module
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Maximum lenght 30 characters")]
        public string ModuleName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [MStartDateTest]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [MEndDateTest]
        public DateTime EndDate { get; set; }

        public int CourseId { get; set; }
        //Nav Prop
        public virtual Course Course { get; set; }
        public virtual ICollection<Activity> Activities { get; set; }
        public virtual ICollection<ModuleDocument> DocumentModuleAssignments { get; set; }
    }





    //- Called by a inserting "[MStartDateTest]" in front of the property. 
    //- The                   "Attribute" part of the class name is dropped by convention.
    public class MStartDateTestAttribute : ValidationAttribute

    {
        //private DateTime _dateTime;
        //public StartDateTest(int Year) //- parameter in the annotation, if any.
        //{                              //- like this: [StartDateTest(1)]
        //    _dateTime = _DateTime;     //- need a cast to create a timespan.
        //}

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (Models.Module)validationContext.ObjectInstance;
            //if (value != null) // lets check if we have some value
            //{
            //    if (value is DateTime) // check if it is a valid Date and Time object
            //    {
            DateTime _StartDate = Convert.ToDateTime(value);
            //- model.CourseId 
            //- model.CourseId is an integer

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Course vCourse = db.Courses.Find(model.CourseId);
                if (vCourse == null)
                {
                    return new ValidationResult("A course must be choosen.");
                }
                DateTime startDateCourse = vCourse.StartDate;
                if (_StartDate <= startDateCourse)
                {
                    return new ValidationResult("Start Date must start after the course start date.");
                }

            }
            if (_StartDate < DateTime.Now)
            {
                return new ValidationResult("Start Date must be a future date.");
            }
            else { return ValidationResult.Success; }   //- The Date fulfilled the requirements.

        }
    }//- of class StartDateTestAttribute



    public class MEndDateTestAttribute : ValidationAttribute
    {
        //private DateTime _dateTime;
        //public StartDateTest(int Year) //- parameter in the annotation, if any.
        //{                              //- like this: [StartDateTest(1)]
        //    _dateTime = _DateTime;     //- need a cast to create a timespan.
        //}

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (Models.Module)validationContext.ObjectInstance;
            //if (value != null) // lets check if we have some value
            //{
            //    if (value is DateTime) // check if it is a valid Date and Time object
            //    {
            DateTime _EndDate = Convert.ToDateTime(value);

            if (!(model.StartDate <= _EndDate))
            {
                return new ValidationResult("End Date must be a later date than the start date.");
            }


            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Course vCourse = db.Courses.Find(model.CourseId);
                if (vCourse == null)
                {
                    return new ValidationResult("A course must be choosen.");
                }
                DateTime startDateCourse = vCourse.StartDate; //-
                if (!(_EndDate >= startDateCourse))
                {
                    return new ValidationResult("End Date of module must be after the course start date.");
                }
                DateTime endDateCourse = vCourse.EndDate;
                if (!(_EndDate <= endDateCourse))
                {                           //- "End Date of module must be befor or same as end date of course."
                    return new ValidationResult("Must be befor or same as end date of course.");
                }
            }

            return ValidationResult.Success; //- The Dates fulfilled the requirements.


        }
    }//- of class EndDateTestAttribute






















}//- of namespace