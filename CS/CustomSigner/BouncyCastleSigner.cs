using DevExpress.Pdf;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Encodings;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CustomSigner
{
    // Declare a custom class to calculate a digest value:
    public class BouncyCastleDigestCalculator : IDigestCalculator
    {
        readonly IDigest digest;

        public string AlgorithmOid => DigestUtilities.GetObjectIdentifier(digest.AlgorithmName).Id;

        public BouncyCastleDigestCalculator()
        {
            digest = new Sha512Digest();
        }
        public byte[] ComputeDigest(Stream stream)
        {
            digest.Reset();
            byte[] buffer = new byte[1024 * 1024];
            int readByteCount;
            do
            {
                readByteCount = stream.Read(buffer, 0, buffer.Length);
                if (readByteCount != 0)
                    digest.BlockUpdate(buffer, 0, readByteCount);
            }
            while (readByteCount != 0);
            byte[] result = new byte[GetDigestSize()];
            digest.DoFinal(result, 0);
            return result;
        }
        public int GetDigestSize()
        {
            return digest.GetDigestSize();
        }
    }

    public class BouncyCastleSigner : Pkcs7SignerBase
    {
        //Specify the signing algoritm's OID:
        const string PKCS1RsaEncryption = "1.2.840.113549.1.1.1";

        readonly byte[][] certificate;
        readonly IAsymmetricBlockCipher rsaEngine;

        //Specify a custom digest calculator:
        protected override IDigestCalculator DigestCalculator { get { return new BouncyCastleDigestCalculator(); } }
        protected override string SigningAlgorithmOID => PKCS1RsaEncryption;

        public BouncyCastleSigner(string file, string password, ITsaClient tsaClient) : base(tsaClient)
        {
            //Read PKCS#12 file:
            var pkcs = new Pkcs12Store(File.Open(file, FileMode.Open), password.ToCharArray());

            //Get the certificate's alias:
            var alias = pkcs.Aliases.OfType<string>().First();

            //Get the certificate's chain:
            certificate = pkcs.GetCertificateChain(alias).Select(c => c.Certificate.GetEncoded()).ToArray();

            //Initialize the encryption engine:
            rsaEngine = new Pkcs1Encoding(new RsaBlindedEngine());
            rsaEngine.Init(true, pkcs.GetKey(alias).Key);
        }

        protected override IEnumerable<byte[]> GetCertificates()
        {
            return certificate;
        }

        protected override byte[] SignDigest(byte[] digest)
        {
            //Create the digest info object
            //Encrypted by the signer's private key:
            var dInfo = new DigestInfo(new AlgorithmIdentifier(new DerObjectIdentifier(DigestCalculator.AlgorithmOid), DerNull.Instance), digest);
            byte[] digestInfo = dInfo.GetDerEncoded();
            return rsaEngine.ProcessBlock(digestInfo, 0, digestInfo.Length);
        }
    }

}
