using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Note
{
    public int Id { get; set; }

    [Required]
    [StringLength(150)]
    public string Message { get; set; }
    
    [Required]
    public DateTime CreatedUTC { get; set; }

    public int UserId { get; set; }
    public User User { get; set; }

    public int SpecialSomeoneId { get; set; }
    public SpecialSomeone SpecialSomeone { get; set; }
}