using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;

namespace YGCGanpati.Models
{
    [NotMapped()]
    public class GraphData
    {
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime TDate { get; set; }
        public decimal Collections { get; set; }
        public decimal Expenses { get; set; }
        public int Balance { get; set; }
    }

    [Table("Events")]
    public class Event
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int EventID { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Event Date")]
        public DateTime EventDate { get; set; }

        [Required]
        [Display(Name = "Event Name")]
        public string EventName { get; set; }
    }


    [Table("Collections")]
    public class Collection
    {

        public Collection()
        {
            this.CollectionDate = YGCGanpati.Models.Common.GetCurrentDate();
            this.CreatedDate = this.CollectionDate;
            this.ModifiedDate = this.CollectionDate;
            this.Amount = 500;
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int CollectionID { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Flat/Shop No")]
        public string FlatNo { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime CollectionDate { get; set; }

        [Required]
        [Range(1, 1000000)]
        public decimal Amount { get; set; }        
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        [NotMapped()]
        public string Name
        {
            get
            {
                return (this.FirstName + " " + this.LastName);
            }
        }

        public virtual ApplicationUser UserProfile { get; set; }       
    }

    [Table("Expenses")]
    public class Expense
    {
        public Expense()
        {
            this.ExpenseDate = YGCGanpati.Models.Common.GetCurrentDate();
            this.CreatedDate = this.ExpenseDate;
            this.ModifiedDate = this.ExpenseDate;
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ExpenseID { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime ExpenseDate { get; set; }
        [Required]
        [Range(1, 9900000)]
        [Display(Name = "Amount")]
        public decimal ExpenseAmount { get; set; }
        [Required]
        [Display(Name = "Expense")]
        public string ExpenseName { get; set; }
        [Required]
        public string Details { get; set; }        
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual ApplicationUser UserProfile { get; set; }
    }

    [Table("Sponsers")]
    public class Sponser
    {
        public Sponser()
        {
            this.SponserDate = YGCGanpati.Models.Common.GetCurrentDate();
            this.CreatedDate = YGCGanpati.Models.Common.GetCurrentDate();
            this.ModifiedDate = this.CreatedDate;
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int SponserID { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Flat/Shop No")]
        public string FlatNo { get; set; }

        [Required]        
        [Display(Name = "Date")]
        public DateTime SponserDate { get; set; }
        [Required]
        [Range(0, 9900000)]
        [Display(Name = "Amount")]
        public decimal Amount { get; set; }
        [Required]
        [Display(Name = "Sponsership")]
        public string Details { get; set; }

        [NotMapped()]
        [Display(Name = "Sponser")]
        public string Name
        {
            get
            {
                return (this.FirstName + " " + this.LastName);
            }
        }

        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual ApplicationUser UserProfile { get; set; }        
    }
}