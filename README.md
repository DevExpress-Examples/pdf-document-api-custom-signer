<!-- default file list -->
*Files to look at*:
* [Program.cs](./CS/CustomSigner/Program.cs) (VB: [Program.vb](./VB/CustomSigner/Program.vb))
* [BouncyCastleTsaClient.cs](./CS/CustomSigner/BouncyCastleSigner.cs)  (VB :[BouncyCastleTsaClient.vb](./VB/CustomSigner/BouncyCastleSigner.vb))
<!-- default file list end -->

# How to: Use a Custom Signer Class to Apply Signatures to the PDF Document

The PDF Document API allows you to replace a built-in PKCS#7 signature builder (the Pkcs7Signer class) with a custom signer. Refer to the [Sign Documents](https://docs.devexpress.com/OfficeFileAPI/114623/pdf-document-api/document-security/sign-documents?v=20.2) documentation article for more information.

The code sample project shows how to create a **Pkcs7SignerBase** descendant to implement a custom signer based on the [Bouncy Castle C# API](https://bouncycastle.org/csharp/index.html) and calculate a document hash using a custom digest calculator.

To sign a PDF file using an external web service, retrieve a certificate/a certificate chain from the service and calculate the document hash. Then, sign the calculated document hash with a private key obtained from the external service in the SignDigest method of the BouncyCastleSigner class.
