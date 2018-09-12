using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YGCGanpati.Models
{
    [Table("QuizQuestions")]
    public class QuizQuestion
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int QuestionID { get; set; }

        [Required]
        [Display(Name = "Question")]
        public string Question { get; set; }

        public string ImgURL { get; set; }

        [Required]
        [Display(Name = "Option1")]
        public string Option1 { get; set; }

        [Required]
        [Display(Name = "Option2")]
        public string Option2 { get; set; }

        [Required]
        [Display(Name = "Option3")]
        public string Option3 { get; set; }

        [Required]
        [Display(Name = "Option4")]
        public string Option4 { get; set; }
        [Required]
        [Display(Name = "Answer")]
        public int Answer { get; set; }

        [Required]
        [Display(Name = "Dispaly Date")]
        public DateTime DisplayDate { get; set; }

        public virtual ICollection<QuizAnswer> QuizAnswers { get; set; }
    }

    [Table("QuizAnswers")]
    public class QuizAnswer
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int AnswerID { get; set; }
        [Required]
        public int QuestionID { get; set; }

        [Required]
        [Display(Name = "Answer")]
        public int Answer { get; set; }

        [Required]
        [Display(Name = "Answer Date")]
        public DateTime AnswerDate { get; set; }
        public int Points { get; set; }
        public virtual QuizQuestion QuizQuestion { get; set; }
        public virtual ApplicationUser UserProfile { get; set; }
    }

    [NotMapped]
    public class UserAnswer : QuizQuestion
    {
        public int? AnswerID { get; set; }
        public int? Answers { get; set; }
        public DateTime? AnswerDate { get; set; }
        public int? Points { get; set; }
        public int? TimeTaken { get; set; }
        public string UserProfile_ID { get; set; }
    }

}