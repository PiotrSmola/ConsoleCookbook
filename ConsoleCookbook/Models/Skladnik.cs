using System.ComponentModel.DataAnnotations;

namespace ConsoleCookbook.Models
{
    public class Skladnik
    {
        public int Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        public string Nazwa { get; set; } = string.Empty;
        
        [Required]
        [MaxLength(50)]
        public string Ilosc { get; set; } = string.Empty; // np. "2 szklanki", "500g"
        
        // Klucz obcy do przepisu
        public int PrzepisId { get; set; }
        
        // Właściwość nawigacyjna do przepisu
        public virtual Przepis Przepis { get; set; } = null!;
    }
}
