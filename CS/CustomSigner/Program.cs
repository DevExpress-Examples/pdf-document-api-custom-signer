using DevExpress.Pdf;
using System;
using System.Diagnostics;

namespace CustomSigner
{
    static class Program
    {
        static void Main(string[] args)
        {
            using (var signer = new PdfDocumentSigner(@"Document.pdf"))
            {
                //Create a timestamp:
                ITsaClient tsaClient = new PdfTsaClient(new Uri(@"http://timestamp.apple.com/ts01"), PdfHashAlgorithm.SHA256);

                //Specify the signature's field name and location:
                var description = new PdfSignatureFieldInfo(1);
                description.Name = "SignatureField";
                description.SignatureBounds = new PdfRectangle(10, 10, 50, 150);

                //Create a custom signer object:
                var bouncyCastleSigner = new BouncyCastleSigner("certificate.pfx", "123", tsaClient);

                //Apply a signature to a new form field:
                var signatureBuilder = new PdfSignatureBuilder(bouncyCastleSigner, description);
                
                //Specify the image and signer information:
                signatureBuilder.SetImageData(System.IO.File.ReadAllBytes("signature.jpg"));
                signatureBuilder.Location = "LOCATION";
                
                //Sign and save the document:
                signer.SaveDocument("SignedDocument.pdf", signatureBuilder);
                Process.Start("SignedDocument.pdf");
            }
            return;
        }
    }
}
