using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.DataProtection;
using Windows.Storage.Streams;

namespace Petrolhead2016XT.Services.SecurityService
{
    class CryptoHelper
    {
        public async Task<string> Decrypt(IBuffer buffProtected, BinaryStringEncoding encoding)
        {
            try
            {
                // Create a DataProtectionProvider object.
                DataProtectionProvider Provider = new DataProtectionProvider();

                // Create a random access stream to contain the encrypted message.
                InMemoryRandomAccessStream inputData = new InMemoryRandomAccessStream();

                // Create a random access stream to contain the decrypted data.
                InMemoryRandomAccessStream unprotectedData = new InMemoryRandomAccessStream();

                // Retrieve an IOutputStream object and fill it with the input (encrypted) data.
                IOutputStream outputStream = inputData.GetOutputStreamAt(0);
                DataWriter writer = new DataWriter(outputStream);
                writer.WriteBuffer(buffProtected);
                await writer.StoreAsync();
                await outputStream.FlushAsync();

                // Retrieve an IInputStream object from which you can read the input (encrypted) data.
                IInputStream source = inputData.GetInputStreamAt(0);

                // Retrieve an IOutputStream object and fill it with decrypted data.
                IOutputStream dest = unprotectedData.GetOutputStreamAt(0);
                await Provider.UnprotectStreamAsync(source, dest);
                await dest.FlushAsync();

                // Write the decrypted data to an IBuffer object.
                DataReader reader2 = new DataReader(unprotectedData.GetInputStreamAt(0));
                await reader2.LoadAsync((uint)unprotectedData.Size);
                IBuffer buffUnprotectedData = reader2.ReadBuffer((uint)unprotectedData.Size);

                // Convert the IBuffer object to a string using the same encoding that was
                // used previously to conver the plaintext string (before encryption) to an
                // IBuffer object.
                String strUnprotected = CryptographicBuffer.ConvertBinaryToString(encoding, buffUnprotectedData);

                // Return the decrypted data.
                return strUnprotected;
            }
            catch (Exception ex)
            {
                App.Telemetry.TrackException(ex);
                return "";

            }

        }
        
        public async Task<IBuffer> Encrypt(string descriptor, string strMsg, BinaryStringEncoding encoding)
        {
            // Create a DataProtectionProvider object for the specified descriptor.
            DataProtectionProvider Provider = new DataProtectionProvider(descriptor);

            // Convert the input string to a buffer.
            IBuffer buffMsg = CryptographicBuffer.ConvertStringToBinary(strMsg, encoding);

            // Create a random access stream to contain the plaintext message.
            InMemoryRandomAccessStream inputData = new InMemoryRandomAccessStream();

            // Create a random access stream to contain the encrypted message.
            InMemoryRandomAccessStream protectedData = new InMemoryRandomAccessStream();

            // Retrieve an IOutputStream object and fill it with the input (plaintext) data.
            IOutputStream outputStream = inputData.GetOutputStreamAt(0);
            DataWriter writer = new DataWriter(outputStream);
            writer.WriteBuffer(buffMsg);
            await writer.StoreAsync();
            await outputStream.FlushAsync();

            // Retrieve an IInputStream object from which you can read the input data.
            IInputStream source = inputData.GetInputStreamAt(0);

            // Retrieve an IOutputStream object and fill it with encrypted data.
            IOutputStream dest = protectedData.GetOutputStreamAt(0);
            await Provider.ProtectStreamAsync(source, dest);
            await dest.FlushAsync();

            //Verify that the protected data does not match the original
            DataReader reader1 = new DataReader(inputData.GetInputStreamAt(0));
            DataReader reader2 = new DataReader(protectedData.GetInputStreamAt(0));
            await reader1.LoadAsync((uint)inputData.Size);
            await reader2.LoadAsync((uint)protectedData.Size);
            IBuffer buffOriginalData = reader1.ReadBuffer((uint)inputData.Size);
            IBuffer buffProtectedData = reader2.ReadBuffer((uint)protectedData.Size);

            if (CryptographicBuffer.Compare(buffOriginalData, buffProtectedData))
            {
                throw new Exception("ProtectStreamAsync returned unprotected data");
            }

            // Return the encrypted data.
            return buffProtectedData;
        }
    }
}
