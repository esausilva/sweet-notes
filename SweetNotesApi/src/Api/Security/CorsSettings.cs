using System.ComponentModel.DataAnnotations;

namespace Api.Security;

public record CorsSettings(string FrontendOrigin);