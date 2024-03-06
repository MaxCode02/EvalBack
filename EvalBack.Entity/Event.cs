using System.ComponentModel.DataAnnotations;

namespace EvalBack.Entity
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Location { get; set; }
    }
}