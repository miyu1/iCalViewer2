using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;

namespace WebLoader {
    class WebdavLoader : HttpLoader {
        public WebdavLoader( string url ) : base( url ) {
        }

        public WebdavLoader( string url, string user, string pass )
            : base ( url, user, pass )
        {
        }
        
        public override void Load()
        {
            base.Load( "PROPFIND" );
            if( Response == null ){
                return;
            }

            // if not multistatus, do nothing
            if( (int)Response.StatusCode != 207 ){
                return;
            }

            Uri uri = new Uri(Url);
            // string host = uri.Scheme + "://" + uri.Host + ":" + uri.Port + "/";
            string host = uri.Scheme + "://" + uri.Host + ":" + uri.Port;

            XmlDocument doc = new XmlDocument();
            doc.LoadXml( ResponseContent );

            XmlNode multistatus = FirstChildNodeByName( doc, "multistatus" );
            List<XmlNode> responseList = null;
            if( multistatus != null ){
                responseList = ChildNodesByName( multistatus, "response" );
            }

            List<string> hrefList = new List<string>();

            if( responseList != null ){
                for( int i = 0; i < responseList.Count;i ++ ){
                    XmlNode response = responseList[i];
                    XmlNode propstat =
                        FirstChildNodeByName( response, "propstat" );
                    if( propstat != null ){
                        XmlNode status = FirstChildNodeByName( propstat,
                                                               "status" );

                        if( status != null ){
                            string statusString = NodeText( status );
                            string[] stats = statusString.Split( ' ' );
                            if (stats.Length <= 1 || stats[1] != "200") {
                                continue;
                            }

                        }
                    }

                    XmlNode prop = null;
                    if( propstat != null ){
                        prop = FirstChildNodeByName( propstat, "prop" );
                    }
                    XmlNode resourceType = null;
                    if( prop != null ){
                        resourceType =
                            FirstChildNodeByName( prop, "resourcetype" );
                    }
                    if( resourceType != null ){
                        XmlNode collection =
                            FirstChildNodeByName( resourceType, "collection" );
                        if( collection != null ){
                            continue;
                        }
                    }


                    XmlNode href = FirstChildNodeByName( response, "href" );
                    hrefList.Add( NodeText( href ) );
                }
            }

            string resultContent = "";
            for( int i = 0; i < hrefList.Count; i++ ){
                this.Url = host + hrefList[i];
                base.Load();
                if( this.ResponseStatus != HttpStatusCode.OK ){
                    break;
                }
                resultContent += ResponseContent;
            }
            this.ResponseContent = resultContent;
        }

        protected override void UpdateRequest()
        {
            if (this.Request.Method == "PROPFIND") {
                this.Request.Headers["Depth"] = "1";
            }
        }
 
        XmlNode FirstChildNodeByName ( XmlNode node, string name ) {
            for( int i=0; i < node.ChildNodes.Count; i++ ){
                XmlNode child = node.ChildNodes[i];
                if( child.LocalName == name ){
                    return child;
                }
            }
            return null;
        }

        List<XmlNode> ChildNodesByName(  XmlNode node, string name ) {

            List<XmlNode> list = new List<XmlNode>();

            for( int i=0; i < node.ChildNodes.Count; i++ ){
                XmlNode child = node.ChildNodes[i];
                if( child.LocalName == name ){
                    list.Add( child );
                }
            }
            return list;
        }

        string NodeText( XmlNode node ){
            for( int i=0; i < node.ChildNodes.Count; i++ ){
                XmlNode child = node.ChildNodes[i];
                if( child.NodeType == XmlNodeType.Text ){
                    return child.InnerText;
                }
            }
            return null;
        }
    }
}