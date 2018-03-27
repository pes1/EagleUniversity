using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EagleUniversity.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 1, ErrorMessage = "Maximum lenght 30 characters")]
        [Display(Name = "Course Name")]
        public string CourseName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[StartDateTest] //- Creates problem for the SEED. Temporarily removed.
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [EndDateTest]
        public DateTime EndDate { get; set; }

        public string OwnerId { get; set; }

        //Nav Prop
        public virtual ICollection<Module> Modules { get; set; }
        public virtual ICollection<Assignments> UserCourseAssignments { get; set; }
        public virtual ICollection<CourseDocument> DocumentCourseAssignments { get; set; }
    }


    //public class StartDateTestAttribute : ValidationAttribute
    //{
    //    //private DateTime _dateTime;
    //    //public StartDateTest(int Year) //- parameter in the annotation, if any.
    //    //{                              //- like this: [StartDateTest(1)]
    //    //    _dateTime = _DateTime;     //- need a cast to create a timespan.
    //    //}

    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        var model = (Models.Course)validationContext.ObjectInstance;
    //        //if (value != null) // lets check if we have some value
    //        //{
    //        //    if (value is DateTime) // check if it is a valid Date and Time object
    //        //    {
    //        DateTime _StartDate = Convert.ToDateTime(value);

    //        //- removed to make the seed pass
    //        //if (_StartDate <= DateTime.Now)
    //        //{
    //        //    return new ValidationResult("Start Date must be a future date.");
    //        //}
    //        //else { return ValidationResult.Success; }   //- The Date fulfilled the requirements.

    //        return ValidationResult.Success;
    //    }
    //}//- of class StartDateTestAttribute



    public class EndDateTestAttribute : ValidationAttribute
    {
        //private DateTime _dateTime;
        //public StartDateTest(int Year) //- parameter in the annotation, if any.
        //{                              //- like this: [StartDateTest(1)]
        //    _dateTime = _DateTime;     //- need a cast to create a timespan.
        //}

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var model = (Models.Course)validationContext.ObjectInstance;
            //if (value != null) // lets check if we have some value
            //{
            //    if (value is DateTime) // check if it is a valid Date and Time object
            //    {
            DateTime _EndDate = Convert.ToDateTime(value);

            if (!(model.StartDate <= _EndDate))
            {
                return new ValidationResult("End Date must be a later than the start date.");
            }
            else { return ValidationResult.Success; }   //- The Date fulfilled the requirements.
        }
    }//- of class EndDateTestAttribute
















}//- of namespace
