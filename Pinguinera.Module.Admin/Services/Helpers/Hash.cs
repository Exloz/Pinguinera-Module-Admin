using System.Security.Cryptography;
using System.Text;

namespace pinguinera_final_module.Services.Helpers;

public static class Hash {

    public static string GenerateHash( string password, byte[] salt ) {

        using var sha256 = new SHA256Managed();
        var passwordBytes = Encoding.UTF8.GetBytes( password );
        var saltedPassword = new byte[passwordBytes.Length + salt.Length];

        Buffer.BlockCopy( passwordBytes, 0, saltedPassword, 0, passwordBytes.Length );
        Buffer.BlockCopy( salt, 0, saltedPassword, passwordBytes.Length, salt.Length );

        var hashedBytes = sha256.ComputeHash( saltedPassword );

        var hashedPasswordWithSalt = new byte[hashedBytes.Length + salt.Length];
        Buffer.BlockCopy( salt, 0, hashedPasswordWithSalt, 0, salt.Length );
        Buffer.BlockCopy( hashedBytes, 0, hashedPasswordWithSalt, salt.Length, hashedBytes.Length );

        return Convert.ToBase64String( hashedPasswordWithSalt );
    }

    public static byte[] GenerateSalt()
    {
        var salt = new byte[16];
        RandomNumberGenerator.Fill( salt );
        return salt;
    }
}