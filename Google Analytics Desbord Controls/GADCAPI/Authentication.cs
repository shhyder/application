using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace GADCAPI
{
    public class Authentication
    {
        public static string getSessionTokenClientLogin(string email, string password)
        {
            //Google analytics requires certain variables to be POSTed
            string postData = "Email=" + email + "&Passwd=" + password;

            //defined - should not channge much
            postData = postData + "&accountType=HOSTED_OR_GOOGLE" + "&service=analytics" + "&source=testcomp-testapp-1";

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] data = encoding.GetBytes(postData);

            HttpWebRequest myRequest = (HttpWebRequest)WebRequest.Create("https://www.google.com/accounts/ClientLogin");
            myRequest.Method = "POST";
            myRequest.ContentType = "application/x-www-form-urlencoded";
            myRequest.ContentLength = data.Length;
            Stream newStream = myRequest.GetRequestStream();

            // Send the data.
            newStream.Write(data, 0, data.Length);
            newStream.Close();

            HttpWebResponse myResponse = (HttpWebResponse)myRequest.GetResponse();
            Stream responseBody = myResponse.GetResponseStream();

            Encoding encode = System.Text.Encoding.GetEncoding("utf-8");
            StreamReader readStream = new StreamReader(responseBody, encode);

            //returned from Google Analytics API
            string response = readStream.ReadToEnd();

            //get the data we need
            string[] auth = response.Split(new string[] { "Auth=" }, StringSplitOptions.None);

            //return it (the authorization token)
            return auth[1];
        }

    }
}
