Imports Newtonsoft.Json
Imports Razorpay.Api
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Net
Imports System.Web.Script.Serialization

Public Class payment
    Inherits System.Web.UI.Page
    Public orderId As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'ServicePointManager.Expect100Continue = True
        'ServicePointManager.SecurityProtocol = (SecurityProtocolType)
        '//ServicePointManager.Expect100Continue = true;
        '//ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;

    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        ' Dim Form1 As New Form1("http://192.168.254.104:8000")
        Dim dictData As New Dictionary(Of String, Object)
        dictData.Add("amount", "5000")
        dictData.Add("currency", "INR")
        dictData.Add("receipt", "rcptid_2")
        dictData.Add("payment_capture", "1")
        'Form1.postData(dictData)
        postData(dictData)

        ' Dim Content As String = "{""amount"":""5000"",""currency"":""INR"",""receipt"":""rcptid_2"",""payment_capture"":""1""}"
        'Dim Content As String = ""
        '' Dim Content As String = ""
        ''System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls
        'ServicePointManager.SecurityProtocol = DirectCast(3072, SecurityProtocolType)
        ''Dim Request As WebRequest = WebRequest.Create("https://www.payumoney.com/sandbox/payment/op/getPaymentResponse?merchantKey=VIEuzjD2&merchantTransactionIds=36930a560671f913df6e")
        'Dim Request As WebRequest = WebRequest.Create("https://api.razorpay.com/v1/orders")
        'Request.Method = "POST"
        'Request.UseDefaultCredentials = True
        '' //Response.Write(Content);
        'Dim byteArray As Byte() = Encoding.UTF8.GetBytes(Content)
        'Request.ContentType = "application/json"
        'Request.ContentLength = byteArray.Length
        'Dim credentials As String = Convert.ToBase64String(Encoding.ASCII.GetBytes("rzp_test_rwVNqXFDCnSJjO" & ":" & "V0CtXD4fTELpy9wuldZWQNRv"))
        'Request.Headers(HttpRequestHeader.Authorization) = String.Format("Basic {0}", credentials)
        ''Request.Headers.Add(HttpRequestHeader.Authorization, "8TRuD2T8oZ63Ud+6EAFCCDxYh1aICkf5wzfzDK0QK8o=")
        'Using dataStream As Stream = Request.GetRequestStream()
        '    dataStream.Write(byteArray, 0, byteArray.Length)
        '    dataStream.Close()
        'End Using
        ''Dim Response1 As HttpWebResponse = HttpWebResponse.GetResponse
        'Dim Response1 = Request.GetResponse()
        'Dim responseDataStream As Stream = Response1.GetResponseStream()
        'Dim reader As StreamReader = New StreamReader(responseDataStream)
        'Dim ResponseData As String = reader.ReadToEnd()
        ''// Response.Write(ResponseData)
        'reader.Close()
        'responseDataStream.Close()
        'Response1.Close()
        'Dim result As Responsevalue = JsonConvert.DeserializeObject(Of Responsevalue)(ResponseData)
        'Dim amount As String
        ''If (txtpartialaxis.Text = "") Then
        ''    amount = Label15.Text.Trim
        ''Else
        ''    amount = txtpartialaxis.Text.Trim
        ''End If

    End Sub

    Public Function postData(ByVal dictData As Dictionary(Of String, Object)) As Boolean
        Dim webClient As New WebClient()
        Dim resByte As Byte()
        Dim resString As String
        Dim reqString() As Byte

        Try
            webClient.Headers("content-type") = "application/json"
            Dim credentials As String = Convert.ToBase64String(Encoding.ASCII.GetBytes("rzp_test_rwVNqXFDCnSJjO" & ":" & "V0CtXD4fTELpy9wuldZWQNRv"))
            webClient.Headers(HttpRequestHeader.Authorization) = String.Format("Basic {0}", credentials)
            'webClient.Headers.Add("apikey", apikey_favoriot)
            reqString = Encoding.Default.GetBytes(JsonConvert.SerializeObject(dictData, Formatting.Indented))
            resByte = webClient.UploadData("https://api.razorpay.com/v1/orders", "post", reqString)
            resString = Encoding.Default.GetString(resByte)
            Dim yourJSONString = JsonConvert.SerializeObject(resString)
            Dim js As New JavaScriptSerializer()
            Dim someObject1 As Dictionary(Of String, Object) = js.Deserialize(Of Dictionary(Of String, Object))(resString)
            Dim orderid As String = someObject1("id").ToString()
            Dim result As Responsevalue = JsonConvert.DeserializeObject(Of Responsevalue)(resString)
            Console.WriteLine(resString)
            webClient.Dispose()
            Return True
        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
        Return False
    End Function
End Class