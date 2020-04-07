<!-- default file list -->
*Files to look at*:
* [Program.cs](./CS/CustomSigner/Program.cs) (VB: [Program.vb](./VB/CustomSigner/Program.vb))
* [BouncyCastleTsaClient.cs](./CS/CustomSigner/BouncyCastleSigner.cs)  (VB :[BouncyCastleTsaClient.vb](./VB/CustomSigner/BouncyCastleSigner.vb))
<!-- default file list end -->

# How to: Use a Custom Signer Class to Apply Signatures to the PDF Document

The PDF Document API allows you to create a custom class to use your own object to create PKCS#7 signatures.

The code sample project shows how to create a **Pkcs7SignerBase** descendant to use a custom signer based on the [Bouncy Castle C# API](https://bouncycastle.org/csharp/index.html).
