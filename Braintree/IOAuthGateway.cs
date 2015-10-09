using System;

namespace Braintree
{
    public interface IOAuthGateway
    {
        string ComputeSignature(string message);
        string ConnectUrl(OAuthConnectUrlRequest request);
        ResultImpl<IOAuthCredentials> CreateTokenFromCode(OAuthCredentialsRequest request);
        ResultImpl<IOAuthCredentials> CreateTokenFromRefreshToken(OAuthCredentialsRequest request);
    }
}