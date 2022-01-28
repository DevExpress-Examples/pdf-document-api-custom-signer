<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/247712953/20.1.2%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T871584)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:
* [Program.cs](./CS/CustomSigner/Program.cs) (VB: [Program.vb](./VB/CustomSigner/Program.vb))
* [BouncyCastleTsaClient.cs](./CS/CustomSigner/BouncyCastleSigner.cs)  (VB :[BouncyCastleTsaClient.vb](./VB/CustomSigner/BouncyCastleSigner.vb))
<!-- default file list end -->

# How to: Use a Custom Signer Class to Apply Signatures to the PDF Document

The PDF Document API allows you to create a custom class to use your own object to create PKCS#7 signatures. 

The code sample project shows how to create a **Pkcs7SignerBase** descendant to use a custom signer based on the [Bouncy Castle C# API](https://bouncycastle.org/csharp/index.html).
