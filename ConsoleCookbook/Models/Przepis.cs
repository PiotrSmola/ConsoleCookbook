using System.ComponentModel.DataAnnotations;

namespace ConsoleCookbook.Models
{
    public class Przepis
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Nazwa { get; set; } = string.Empty;
        
        [Required]
        public string Instrukcje { get; set; } = string.Empty;
        
        public int CzasPrzygotowania { get; set; } // w minutach
        
        public int LiczbaOsob { get; set; }
        
        public DateTime DataDodania { get; set; } = DateTime.Now;
        
        // Relacja jeden do wielu - jeden przepis ma wiele składników
        public virtual ICollection<Skladnik> Skladniki { get; set; } = new List<Skladnik>();
    }
}
