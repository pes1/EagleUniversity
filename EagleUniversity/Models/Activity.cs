using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EagleUniversity.Models
{
    //public enum Epass { FM = 1, EM, FMEM };

    public class Activity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Maximum lenght 30 characters")]
        public string ActivityName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        [AStartDateTest]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        //[AEndDateTest]
        public DateTime EndDate { get; set; }

        //public Epass Pass { get; set; }
        public int ModuleId { get; set; }
        public int ActivityTypeId { get; set; }

        //Nav Prop
        public virtual ActivityType ActivityTypes { get; set; }
        public virtual Module Modules { get; set; }

        public virtual ICollection<ActivityDocument> DocumentActivityAssignments { get; set; }
    } //- of class Activity






    //- Called by a inserting "[MStartDateTest]" in front of the property. 
    //- The                   "Attribute" part of the class name is dropped by convention.
    public class AStartDateTestAttribute : ValidationAttribute
    {
        //private DateTime _dateTime;
        //public StartDateTest(int Year) //- parameter in the annotation, if any.
        //{                              //- like this: [StartDateTest(1)]
        //    _dateTime = _DateTime;     //- need a cast to create a timespan.
        //}

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //var Type = validationContext.ObjectInstance.GetType();     //- test to see the type of the instance

            var model = (Models.Activity)validationContext.ObjectInstance;
            //if (value != null) // lets check if we have some value
            //{
            //    if (value is DateTime) // check if it is a valid Date and Time object
            //    {
            DateTime _StartDate = Convert.ToDateTime(value);
            //- model.CourseId 
            //- model.CourseId is an integer

            using (ApplicationDbContext db = new ApplicationDbContext())
            {
                Module vModule = db.Modules.Find(model.ModuleId);
                if (vModule == null)
                {
                    return new ValidationResult("A course must be choosen.");
                }
                DateTime startDateModule = vModule.StartDate;
                if (_StartDate <= startDateModule)
                {
                    return new ValidationResult("Start Date must start after the course start date.");
                }
            }
            //- removed to make the seed pass
            //{
            //    return new ValidationResult("Start Date must be a future date.");
            //}
            //else { return ValidationResult.Success; }   //- The Date fulfilled the requirements.
            return ValidationResult.Success;
        }
    }//- of class StartDateTestAttribute



    public class AEndDateTestAttribute : ValidationAttribute
    {
        //private DateTime _dateTime;
        //public StartDateTest(int Year) //- parameter in the annotation, if any.
        //{                              //- like this: [StartDateTest(1)]
        //    _dateTime = _DateTime;     //- need a cast to create a timespan.
        //}

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //var Type = validationContext.ObjectInstance.GetType();     //- test to see the type of the instance
            var model = (Models.Activity)validationContext.ObjectInstance;
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
                Module vModule = db.Modules.Find(model.ModuleId);

                if (vModule == null)
                {
                    return new ValidationResult("A course must be choosen.");
                }
                DateTime startDateModule = vModule.StartDate; //-
                if (!(_EndDate >= startDateModule))
                {
                    return new ValidationResult("End Date of activity must be after the module start date.");
                }
                DateTime endDateModule = vModule.EndDate;
                if (!(_EndDate <= endDateModule))
                {                           //- "End Date of activity must be befor or same as end date of course."
                    return new ValidationResult("Must be befor or same as end date of course.");
                }
            }

            return ValidationResult.Success; //- The Dates fulfilled the requirements.


        }
    }//- of class EndDateTestAttribute




















} //- of namespace