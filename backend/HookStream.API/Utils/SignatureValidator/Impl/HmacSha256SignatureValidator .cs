using System.Security.Cryptography;
using System.Text;

namespace HookStream.API.Utils.SignatureValidator.Impl;

public class HmacSha256SignatureValidator : ISignatureValidator
{
    public bool Validate(string payload, string signatureHeader, string secret)
    {
        var key = Encoding.UTF8.GetBytes(secret);
        using var hmac = new HMACSHA256(key);
        var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(payload));
        var hashString = "sha256=" + Convert.ToHexString(hash).ToLowerInvariant();

        return hashString == signatureHeader;
    }
}