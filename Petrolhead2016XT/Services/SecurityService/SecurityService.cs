using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace Petrolhead2016XT.Services.SecurityService
{
    public class SecurityService
    {

        private CryptoHelper CryptoHelper = new CryptoHelper();
        private AuthenticationHelper AuthHelper = new AuthenticationHelper();

        public async Task CheckVerificationAvailability()
        {
            await AuthHelper.VerifyAndDisplayAuthentication();
        }

        public async Task VerifyAuthentication(string msg)
        {
            await AuthHelper.RequestAuthentication(msg);
        }
        public async Task<string> DecryptData(IBuffer encrypted)
        {
            return await CryptoHelper.Decrypt(encrypted, Windows.Security.Cryptography.BinaryStringEncoding.Utf16BE);
        }

        public async Task<IBuffer> EncryptData(string data)
        {
            return await CryptoHelper.Encrypt("LOCAL=user", data, Windows.Security.Cryptography.BinaryStringEncoding.Utf16BE);
        }

    }
}
