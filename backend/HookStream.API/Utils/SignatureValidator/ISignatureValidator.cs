namespace HookStream.API.Utils.SignatureValidator;

public interface ISignatureValidator
{
    bool Validate(string payload, string signatureHeader, string secret);
}