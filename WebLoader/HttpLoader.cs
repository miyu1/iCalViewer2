using System;
using System.IO;
using System.Net;


namespace WebLoader {
    class HttpLoader {
        protected string Url;
        protected string OriginalUrl;
        protected string User = null;
        protected string Password = null;

        public HttpWebRequest Request;
        public HttpWebResponse Response = null;
        public WebException WebException = null;

        public HttpStatusCode ResponseStatus = (HttpStatusCode)0;
        public string ResponseContent = null;

        public StreamWriter LogWriter = null;

        public HttpLoader( string url ){
            this.Url = url;
        }

        public HttpLoader( string url, string user, string pass ){
            this.Url = url;
            this.OriginalUrl = url;
            this.User = user;
            this.Password = pass;
        }

        public virtual void Load()
        {
            Load( "GET" );
        }

        public void Load( string method )
        {
            bool retry = true;
            while( retry ){
                Load1Time( method );

                if( Response == null ){
                    break;
                }
                switch (Response.StatusCode) {

                case HttpStatusCode.MovedPermanently : 
                case HttpStatusCode.MultipleChoices :
                // case HttpStatusCode.Ambiguous :
                // case HttpStatusCode.Moved :
                case HttpStatusCode.Found :
                // case HttpStatusCode.Redirect :
                case HttpStatusCode.SeeOther :
                // case HttpStatusCode.RedirectMethod :
                case HttpStatusCode.UseProxy :
                case HttpStatusCode.TemporaryRedirect :
                // case HttpStatusCode.RedirectKeepVerb :
                    Url = Response.Headers["location"];
                    break;

                default:
                    retry = false;
                    break;
                }
            }
            if( Response != null ){
                ResponseStatus = Response.StatusCode;

                Stream stream = Response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                ResponseContent = reader.ReadToEnd();
                reader.Close();
                stream.Close();

                if( LogWriter != null ){
                    LogWriter.WriteLine( "Response Code:{0}",
                                         Response.StatusCode );
                    LogWriter.WriteLine( "Response:" );
                    LogWriter.WriteLine( ResponseContent );
                    LogWriter.Flush();
                }
            } else {
                ResponseStatus = 0;
                if( LogWriter != null ){
                    LogWriter.WriteLine( "No Response?" );
                    if( WebException != null ){
                        LogWriter.WriteLine( "{0} {1}",
                                             WebException.Status,
                                             WebException.Message );
                    }
                    LogWriter.Flush();
                }                
            }
        }

        void Load1Time( string method )
        {
            if( LogWriter != null ){
                LogWriter.WriteLine( "Request Method:{0}  URL:{1}",
                                     method, Url );
                LogWriter.Flush();
            }

            Request = (HttpWebRequest)WebRequest.Create(Url);
            Request.Method = method;

            if( this.User != null && this.User.Length > 0 ){
                Request.Credentials =
                    // new NetworkCredential( this.User, this.Password );
                    new HttpLoaderCredential( this.User, this.Password );

                if( LogWriter != null ){
                    LogWriter.WriteLine( "User:{0}", this.User );
                    LogWriter.Flush();
                }
            }
            Request.AllowAutoRedirect = false;
            // Request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 6.1; ja; rv:1.9.1.5) Gecko/20091102 Firefox/3.5.5";
            UpdateRequest();

            try {
                this.Response =
                    (HttpWebResponse)Request.GetResponse();
            } catch( WebException we ) {
                this.WebException = we;
                this.Response = (HttpWebResponse)we.Response;
            }
        }

        protected virtual void UpdateRequest()
        {
        }

        public void Close()
        {
            if( this.Response != null ){
                this.Response.Close();
                this.Response = null;
            }

            if (this.LogWriter != null) {
                this.LogWriter.Close();
            }
        }
    }

    public class HttpLoaderCredential : ICredentials
    {
        string User;
        string Password;

        public HttpLoaderCredential( string user, string password ) {
            this.User = user;
            this.Password = password;
        }

        public NetworkCredential GetCredential( Uri uri, string authType ) {
            if( authType.ToLower() == "basic" ||
                authType.ToLower() == "digest" ){

                return new NetworkCredential( User, Password );
            }
            return null;
        }
    }

}
