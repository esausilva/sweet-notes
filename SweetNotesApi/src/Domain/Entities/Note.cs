using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Note
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Message { get; set; }
    
    [Required]
    public DateTime CreatedUtc { get; set; }
    
    public User User { get; set; }
    
    public SpecialSomeone SpecialSomeone { get; set; }
}