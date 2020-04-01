Imports DevExpress.Pdf
Imports System
Imports System.Diagnostics

Namespace CustomSigner
	Friend NotInheritable Class Program

		Private Sub New()
		End Sub

		Shared Sub Main(ByVal args() As String)
			Using signer = New PdfDocumentSigner("Document.pdf")
				'Create a timestamp:
				Dim tsaClient As ITsaClient = New PdfTsaClient(New Uri("https://freetsa.org/tsr"), PdfHashAlgorithm.SHA256)

				'Specify the signature's field name and location:
				Dim description = New PdfSignatureFieldInfo(1)
				description.Name = "SignatureField"
				description.SignatureBounds = New PdfRectangle(10, 10, 50, 150)

				'Create a custom signer object:
				Dim bouncyCastleSigner = New BouncyCastleSigner("certificate.pfx", "123", tsaClient)

				'Apply a signature to a new form field:
				Dim signatureBuilder = New PdfSignatureBuilder(bouncyCastleSigner, description)

				'Specify the image and signer information:
				signatureBuilder.SetImageData(System.IO.File.ReadAllBytes("signature.jpg"))
				signatureBuilder.Location = "LOCATION"

				'Sign and save the document:
				signer.SaveDocument("SignedDocument.pdf", signatureBuilder)
				Process.Start("SignedDocument.pdf")
			End Using
			Return
		End Sub
	End Class
End Namespace
