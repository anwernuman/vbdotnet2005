Imports Newtonsoft.Json
Imports Razorpay.Api
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Net
Imports System.Web.Script.Serialization

Public Class Charge
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim paymentId As String = Request.Form("razorpay_payment_id")
        Dim dictData As New Dictionary(Of String, Object)
        dictData.Add("amount", "5000")
        dictData.Add("currency", "INR")
        dictData.Add("receipt", "rcptid_2")
        dictData.Add("payment_capture", "1")
        'Form1.postData(dictData)
        GetData(dictData)
        Dim input As New Dictionary(Of String, Object)()
        input.Add("amount", 100) '; // this amount should be same as transaction amount

        Dim key As String = "rzp_test_rwVNqXFDCnSJjO"
        Dim secret As String = "V0CtXD4fTELpy9wuldZWQNRv"

        Dim client As RazorpayClient = New RazorpayClient(key, secret)

        Dim attributes As New Dictionary(Of String, String)()

        attributes.Add("razorpay_payment_id", paymentId)
        attributes.Add("razorpay_order_id", Request.Form("razorpay_order_id"))
        attributes.Add("razorpay_signature", Request.Form("razorpay_signature"))

        Utils.verifyPaymentSignature(attributes)

        Dim refund As Refund = New Razorpay.Api.Payment(paymentId).Refund()

        Console.WriteLine(refund("id"))
    End Sub
    Public Function GetData(ByVal dictData As Dictionary(Of String, Object)) As Boolean
        'Dim Content As String = ""
        'Dim Content As String = ""
        ' Dim Content As String = ""
        'System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls
        ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
        'Dim Request As WebRequest = WebRequest.Create("https://www.payumoney.com/sandbox/payment/op/getPaymentResponse?merchantKey=VIEuzjD2&merchantTransactionIds=36930a560671f913df6e")
        Dim Request As WebRequest = WebRequest.Create("https://api.razorpay.com/v1/payments")
        Request.Method = "Get"
        Request.UseDefaultCredentials = True
        ' //Response.Write(Content);
        'Dim byteArray As Byte() = Encoding.UTF8.GetBytes(Content)
        Request.ContentType = "application/json"
        'Request.ContentLength = byteArray.Length
        Dim credentials As String = Convert.ToBase64String(Encoding.ASCII.GetBytes("rzp_test_rwVNqXFDCnSJjO" & ":" & "V0CtXD4fTELpy9wuldZWQNRv"))
        Request.Headers(HttpRequestHeader.Authorization) = String.Format("Basic {0}", credentials)
        'Request.Headers.Add(HttpRequestHeader.Authorization, "8TRuD2T8oZ63Ud+6EAFCCDxYh1aICkf5wzfzDK0QK8o=")
        'Using dataStream As Stream = Request.GetRequestStream()
        '    dataStream.Write(byteArray, 0, byteArray.Length)
        '    dataStream.Close()
        'End Using
        'Dim Response1 As HttpWebResponse = HttpWebResponse.GetResponse
        Dim Response1 = Request.GetResponse()
        Dim responseDataStream As Stream = Response1.GetResponseStream()
        Dim reader As StreamReader = New StreamReader(responseDataStream)
        Dim ResponseData As String = reader.ReadToEnd()
        '// Response.Write(ResponseData)
        reader.Close()
        responseDataStream.Close()
        Response1.Close()
        Return False
    End Function
End Class