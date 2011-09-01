using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;

namespace ReleaseMan.Models
{
    public class Issue
    {
        [Key]
        public int ID { get; set; }
        public int? ReleaseId { get; set; }

        [Required]
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public bool Fixed { get; set; }

        public virtual Release Release { get; set; }
    }

}