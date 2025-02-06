using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using Winform_Client;

/// <summary>
/// Summary description for Eadaptor
/// </summary>
public class Eadaptor
{
    // HttpXmlClient HXC;
    string log = "";
    public Eadaptor()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string HTTPPostXMLMessage(string str_xml)
    {
       
        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
        bool compressedresponse = false;
        var uri = new Uri("https://mefhouservices.wisegrid.net/eAdaptor");
        string xml = str_xml;
        var client = new HttpXmlClient(uri, compressedresponse, "mefhou", "mule_scarf243!@in79*");

        using (var sourceStream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
        {
            try
            {
                var response = client.Post(sourceStream);
                var responseStatus = response.StatusCode;

                this.log=((responseStatus == HttpStatusCode.OK ? "Response Received" : "ERROR RESPONSE RECEIVED") + ", Status:- " + (int)responseStatus + " - " + response.ReasonPhrase);

                if (response.Content != null)
                {
                    var stream = response.Content.ReadAsStreamAsync().Result;

                    if (response.Content.Headers.ContentEncoding.Contains("gzip", StringComparer.InvariantCultureIgnoreCase))
                    {
                        stream = new GZipStream(stream, CompressionMode.Decompress);
                    }

                    using (var reader = new StreamReader(stream))
                    {
                        this.log = reader.ReadToEnd();
                    }
                }
              
            }
            catch (Exception exception)
            {
                this.log = "EXCEPTION THROWN DURING POST!!!!";
                this.log = exception.ToString();
              
            }
        }
        return  log;
    }
}