// *****************************************************************************
// * Copyright(c) 2022 Luna Alfien
// *
// * Permission is hereby granted, free of charge, to any person obtaining a
// * copy of this software and associated documentation files (the "Software"),
// * to deal in the Software without limitation the rights to use, copy, modify,
// * and/or merge copies of the Software, and to permit persons to whom the
// * Software is furnished to do so, subject to the following conditions:
// *
// * The Software shall not be redistributed, published, or otherwise made
// * available to any person, subject or entity if the Software has been
// * modified.
// *
// * The above copyright notice and this permission notice shall be included in
// * all copies or substantial portions of the Software.
// * 
// * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// * THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// * DEALINGS IN THE SOFTWARE.
// *****************************************************************************

using System;
using System.IO;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.OpenSsl;

namespace PPKSharp
{
    // This class can use both public and private keys, but
    // public keys can only decrypt data encrypted with a private key, and
    // private keys can only decrypt data encrypted with a public key
    public class PPK
    {
        private string Key;
        public PPK(string key)
        {
            Key = key;
        }

        // Key can be updated with a new key, but cannot be read to prevent accidentally leaking your keys
        public void SetKey(string key)
        {
            Key = key;
        }

        // encrypt clear text data
        public string Encrypt(string clear)
        {
            var bytesToEncrypt = Encoding.UTF8.GetBytes(clear);

            var encryptEngine = new Pkcs1Encoding(new RsaEngine());

            using (var txtreader = new StringReader(Key))
            {
                var keyParameter = (AsymmetricKeyParameter)new PemReader(txtreader).ReadObject();

                encryptEngine.Init(true, keyParameter);
            }

            var encrypted = Convert.ToBase64String(encryptEngine.ProcessBlock(bytesToEncrypt, 0, bytesToEncrypt.Length));
            return encrypted;

        }

        // decrypt data that was encrypted using the opposite key
        public string Decrypt(string encrypted)
        {
            var bytesToDecrypt = Convert.FromBase64String(encrypted);

            var decryptEngine = new Pkcs1Encoding(new RsaEngine());

            using (var txtreader = new StringReader(Key))
            {
                var keyParameter = (AsymmetricKeyParameter)new PemReader(txtreader).ReadObject();

                decryptEngine.Init(false, keyParameter);
            }

            var decrypted = Encoding.UTF8.GetString(decryptEngine.ProcessBlock(bytesToDecrypt, 0, bytesToDecrypt.Length));
            return decrypted;
        }
    }
}
