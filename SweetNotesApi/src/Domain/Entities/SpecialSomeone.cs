using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class SpecialSomeone
{
    public int Id { get; set; }

    [Required]
    [StringLength(45)]
    public string UniqueIdentifier { get; set; }
    
    [Required]
    [StringLength(200)]
    public string FirstName { get; set; }
    
    [Required]
    [StringLength(200)]
    public string LastName { get; set; }

    [StringLength(50)]
    public string? Nickname { get; set; } // TODO: Can be null in GraphQL mutation

    public int UserId { get; set; } // TODO: Remove from GraphQL Schema
    public User User { get; set; }
    public ICollection<Note> Notes { get; set; }
}