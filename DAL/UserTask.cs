using System;
using System.ComponentModel.DataAnnotations;
using DevExpress.ExpressApp.Validation;

namespace EasyTask4.DAL
{
    public class UserTask
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(200, ErrorMessage = "Title cannot be longer than 200 characters.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Status is required.")]
        public string Status { get; set; }


        public string CreatedBy { get; set; }

        public string Assignedto { get; set; }
        public DateTime CreationDate { get; set; }

        public DateTime UpdateDat { get; set; }
    }
}