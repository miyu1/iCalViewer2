using System;
using System.IO;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebLoader {
    class Program {
        static void Main(string[] args) {
            string urlString = null;
            string path = null;
            string uid = null;
            string pass = null;
            string log = null;

            bool urlSet = false;
            for( int i = 0; i < args.Length ; i++ ) {
                if( args[i].ToLower().StartsWith( "/user:" ) ){
                    uid = args[i].Substring( 6 );
                } else if( args[i].ToLower().StartsWith( "/password:" ) ){
                    pass = args[i].Substring( 10 );
                } else if( args[i].ToLower().StartsWith( "/log:" ) ){
                    log = args[i].Substring( 5 );
                }else if( !urlSet ){
                    urlString = args[i];
                    urlSet = true;
                } else {
                    path = args[i];
                }
            }

            if (urlString == null || path == null) {
                Console.WriteLine("Usage WebLoader <url> <path to download> [/user:<userid> /password:<password>");
                return;
            }

            HttpLoader loader = null;
            if (urlString.ToLower().StartsWith("webdavs") ||
                urlString.ToLower().StartsWith("caldavs")) {
                urlString = "https" + urlString.Substring(7);
                loader = new WebdavLoader(urlString, uid, pass);
            } else if (urlString.ToLower().StartsWith("webdav") ||
                       urlString.ToLower().StartsWith("caldav")) {
                urlString = "http" + urlString.Substring(6);
                loader = new WebdavLoader(urlString, uid, pass);
            } else {
                loader = new HttpLoader(urlString, uid, pass);
            }

            FileStream logStream = null;
            if( log != null ){
                logStream = new FileStream( log, FileMode.Create );
                loader.LogWriter = new StreamWriter( logStream );
            }

            loader.Load();

            string result = loader.ResponseContent;

            FileStream fstream = null;
            StreamWriter writer = null;
            if( loader.ResponseStatus == HttpStatusCode.OK ){
                fstream = new FileStream( path + ".success",
                                          FileMode.Create );
                writer = new StreamWriter(fstream);
            } else  {
                fstream = new FileStream( path + ".fail",
                                          FileMode.Create );
                writer = new StreamWriter(fstream);
                if (loader.Response != null) {
                    writer.WriteLine("{0} {1}",
                                      ((int)loader.ResponseStatus),
                                      loader.ResponseStatus);
                }
                if( loader.WebException != null ){
                    writer.WriteLine( "{0} {1}",
                                      loader.WebException.Status,
                                      loader.WebException.Message );
                }
            }
            if( result != null ){
                writer.Write(result);
            }
            writer.Close();
            fstream.Close();
            loader.Close();

            if (logStream != null) {
                logStream.Close();
            }

            /*
            
            try {
                Uri url = new Uri("http://komo:8008/calendars/users/miyako/calendar");
                HttpWebRequest request =
                    (HttpWebRequest)WebRequest.Create(url);
                request.Method = "PROPFIND";
                request.Credentials = new NetworkCredential( "miyako", "p0p0cro");
                request.AllowAutoRedirect = false;
                HttpWebResponse response = 
                    (HttpWebResponse)request.GetResponse();

                if (response.StatusCode == HttpStatusCode.Moved ||
                    response.StatusCode == HttpStatusCode.MovedPermanently) {
                    
                    url = new Uri(response.GetResponseHeader("location"));
                    response.Close();

                    request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "PROPFIND";
                    request.Credentials = new NetworkCredential("miyako", "p0p0cro");
                    request.AllowAutoRedirect = false;
                    response = (HttpWebResponse)request.GetResponse();
                }
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                string result = reader.ReadToEnd();
            }
            catch (WebException we) {
                Stream stream = we.Response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                string result2 = reader.ReadToEnd();
            }
            */

        }
    }

}
