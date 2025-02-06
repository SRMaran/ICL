using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// Summary description for getICLxml
/// </summary>
public class getICLxml
{
    private static readonly HttpClient client = new HttpClient();
    public getICLxml()
    {
        //
        // TODO: Add constructor logic here
        //
    }
 


   

    public string ExecuteApiCallSync(string strXml)
    {
        return ExecuteAPICallAsync(strXml).GetAwaiter().GetResult();
    }

    public async Task<string> ExecuteAPICallAsync(string strXml)
    {
        try
        {
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

            var request = new HttpRequestMessage(HttpMethod.Get, "https://mefhouservices.wisegrid.net/eAdaptor");
            request.Headers.Add("Authorization", "Basic bWVmaG91Om11bGVfc2NhcmYyNDMhQGluNzkq"); // Replace with a secure method
            request.Headers.Add("Cookie", "WEBSVC=c478bbe8f3282df3");

         //   string xmlPayload = @"";

            var content = new StringContent(strXml, Encoding.UTF8, "application/xml");
            request.Content = content;


            using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead))
            {
                response.EnsureSuccessStatusCode();

                using (var responseStream = await response.Content.ReadAsStreamAsync())
                using (var reader = new StreamReader(responseStream))
                {
                    return await reader.ReadToEndAsync();
                }
            }

            
        }
        catch (HttpRequestException httpEx)
        {
            return "HTTP Request Error:'"+ httpEx.Message+"'";
        }
        catch (Exception ex)
        {
            return "General Error: '"+ex.Message+"'";
        }
    }


}