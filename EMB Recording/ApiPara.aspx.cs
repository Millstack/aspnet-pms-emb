using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.Net.Http;
using System.Data;

public partial class ApiPara : System.Web.UI.Page
{
    //string connectionString = ConfigurationManager.ConnectionStrings["Ginie"].ConnectionString;

    // Step #1: Create a Class named ApiPara with 4 properties
    public class Api
    {
        public string Command { get; set; }
        public Dictionary<string, string> Parameters { get; set; }
        public string Connection { get; set; }
        public string AccessKey { get; set; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        // Step #2: Create Command to extract data
        string cmd = "Select UserFirstName, UserLastName from UserMaster222 where UserLogin=@LogId";

        // Step #3: Create dictionary to save parameters to pass to Api
        Dictionary<string, string> para = new Dictionary<string, string>();
        para["LogId"] = "Milind";

        // Step #4: Create an instance of the ApiPara class and set values of Parameters
        Api mPara = new Api
        {
            Command = cmd,
            Parameters = para,
            Connection = "Ginie"
        };

        // Step #5: Serialize the parameters with the help of Newtonsoft Json
        string jsonContent = JsonConvert.SerializeObject(mPara);
        StringContent stringContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

        // Step #6: Define the Url to connect to the Api
        string apiUrl = "http://101.53.144.92/wms/api/Get/Table";

        // Step #7: Call Api to get data.
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage response = client.PostAsync(apiUrl, stringContent).Result;

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = response.Content.ReadAsStringAsync().Result;

                // Step #8: Deserialize the JSON response into a DataTable
                DataTable dt = JsonConvert.DeserializeObject<DataTable>(jsonResponse);

                // Now 'dt' contains the data from the API response, and you can use it as needed.
                string message = "Name: " + dt.Rows[0][0].ToString() + " <br/> Surname: " + dt.Rows[0][1].ToString();
                string script = $"alert('{message}');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "messageScript", script, true);
            }
            else
            {
                string message = "Exception";
                string script = $"alert('{message}');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "messageScript", script, true);
            }
        }
    }
}