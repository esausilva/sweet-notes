namespace Application.Helpers;

public static class StringHelpers
{
    public static string GenerateRandomUrlSafeString(string append, int length = 45)
    {
        var chars = @"ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                          append +
                          "abcdefghijklmnopqrstuvwxyz" +
                          "0123456789" +
                          "!*.-_~@(";
 
        var random = new Random();
        var randomString = new string(
            Enumerable
                .Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)])
                .ToArray()
        );
        
        return randomString;
    }
}