using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class User
{
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string FirstName { get; set; }
    
    [Required]
    [StringLength(200)]
    public string LastName { get; set; }
    
    [Required]
    [StringLength(256)]
    public string EmailAddress { get; set; }
    
    [Required] 
    [StringLength(60)]
    public string Password { get; set; }
    // https://code-maze.com/dotnet-secure-passwords-bcrypt/

    [Required]
    public DateTime SignedUpUTC { get; set; }

    public ICollection<Note> Notes { get; set; } = new List<Note>();

    public ICollection<SpecialSomeone> SpecialSomeones { get; set; } = 
        new List<SpecialSomeone>();
}