﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Net;
using Linkhub;

namespace Popbill
{
    public abstract class BaseService
    {
        private const string ServiceID_REAL = "POPBILL";
        private const string ServiceID_TEST = "POPBILL_TEST";
        private const String ServiceURL_REAL = "https://popbill.linkhub.co.kr";
        private const String ServiceURL_TEST = "https://popbill_test.linkhub.co.kr";
        private const String APIVersion = "1.0";
        private const String CRLF = "\r\n";
          
        private Dictionary<String, Token> _tokenTable = new Dictionary<String, Token>();
        private bool _IsTest;
        private Authority _LinkhubAuth;
        private List<String> _Scopes = new List<string>();

        public bool IsTest
        {
            set { _IsTest = value; }
            get { return _IsTest; }
        }

        public BaseService(String LinkID, String SecretKey)
        {
            _LinkhubAuth = new Authority(LinkID, SecretKey);
            _Scopes.Add("member");
        }

        public void AddScope(String scope)
        {
            _Scopes.Add(scope);
        }


        public String GetPopbillURL(String CorpNum, String UserID, String TOGO)
        {
            URLResponse response = httpget<URLResponse>("/?TG=" + TOGO, CorpNum, UserID);

            return response.url;
        }

        public Response JoinMember(JoinForm joinInfo)
        {
            if (joinInfo == null) throw new PopbillException(-99999999, "No JoinForm");

            String PostData = toJsonString(joinInfo);

            return httppost<Response>("/Join", "", "", PostData, null);
            
        }

        public Response CheckIsMember(String CorpNum, String LinkID)
        {
            return httpget<Response>("/Join?CorpNum=" + CorpNum + "&LID=" + LinkID, null, null);
        }

        protected String ServiceID
        {
            get
            {
                return _IsTest ? ServiceID_TEST : ServiceID_REAL;
            }
        }
        protected String ServiceURL
        {
            get
            {
                return _IsTest ? ServiceURL_TEST : ServiceURL_REAL;
            }
        }
        public Double GetBalance(String CorpNum)
        {
            try
            {
                return _LinkhubAuth.getBalance(getSession_Token(CorpNum), ServiceID);
            }
            catch (LinkhubException le)
            {
                throw new PopbillException(le);
            }
        }
        public Double GetPartnerBalance(String CorpNum)
        {
            try
            {
                return _LinkhubAuth.getPartnerBalance(getSession_Token(CorpNum), ServiceID);
            }
            catch (LinkhubException le)
            {
                throw new PopbillException(le);
            }
        }


        #region protected

        protected String toJsonString(Object graph)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(graph.GetType());
                ser.WriteObject(ms, graph);
                ms.Seek(0, SeekOrigin.Begin);
                return new StreamReader(ms).ReadToEnd();
            }
        }
        protected T fromJson<T>(Stream jsonStream)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            return (T)ser.ReadObject(jsonStream);
        }

        private String getSession_Token(String CorpNum)
        {
            Token _token = null;
            
            if(_tokenTable.ContainsKey(CorpNum))
            {
                _token = _tokenTable[CorpNum];
            }
            
            bool expired = true;
            if (_token != null)
            {
                DateTime expiration = DateTime.Parse( _token.expiration);

                expired = expiration < DateTime.Now;
            }

            if (expired)
            {
                try
                {
                    _token = _LinkhubAuth.getToken(ServiceID, CorpNum, _Scopes);

                    if (_tokenTable.ContainsKey(CorpNum))
                    {
                        _tokenTable.Remove(CorpNum);
                    }
                    _tokenTable.Add(CorpNum, _token);
                }
                catch (LinkhubException le)
                {
                    throw new PopbillException(le);
                }
            }

            return _token.session_token;
        }

        protected T httpget<T>(String url, String CorpNum, String UserID)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ServiceURL + url);

            if (String.IsNullOrEmpty(CorpNum) == false)
            {
                String bearerToken = getSession_Token(CorpNum);
                request.Headers.Add("Authorization", "Bearer" + " " + bearerToken);
            }

            request.Headers.Add("x-lh-version", APIVersion);

            if (String.IsNullOrEmpty(UserID) == false)
            {
                request.Headers.Add("x-pb-userid", UserID);
            }

            request.Method = "GET";

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stReadData = response.GetResponseStream();

                return fromJson<T>(stReadData);

            }
            catch (Exception we)
            {
                if (we is WebException && ((WebException)we).Response != null)
                {
                    Stream stReadData = ((WebException)we).Response.GetResponseStream();
                    Response t = fromJson<Response>(stReadData);
                    throw new PopbillException(t.code, t.message);
                }
                throw new PopbillException(-99999999, we.Message);
            }

        }

        protected T httppost<T>(String url, String CorpNum, String UserID, String PostData, String httpMethod)
        {
         
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ServiceURL + url);

            request.ContentType = "application/json;";

            if (String.IsNullOrEmpty(CorpNum) == false)
            {
                String bearerToken = getSession_Token(CorpNum);
                request.Headers.Add("Authorization", "Bearer" + " " + bearerToken);
            }

            request.Headers.Add("x-lh-version", APIVersion);

            if (String.IsNullOrEmpty(UserID) == false)
            {
                request.Headers.Add("x-pb-userid", UserID);
            }

            if (String.IsNullOrEmpty(httpMethod) == false)
            {
                request.Headers.Add("X-HTTP-Method-Override", httpMethod);
            }

            request.Method = "POST";

            if (String.IsNullOrEmpty(PostData)) PostData = "";

            byte[] btPostDAta = Encoding.UTF8.GetBytes(PostData);

            request.ContentLength = btPostDAta.Length;

            request.GetRequestStream().Write(btPostDAta, 0, btPostDAta.Length);

            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stReadData = response.GetResponseStream();

                return fromJson<T>(stReadData);

            }
            catch (Exception we)
            {
                if (we is WebException && ((WebException)we).Response != null)
                {
                    Stream stReadData = ((WebException)we).Response.GetResponseStream();
                    Response t = fromJson<Response>(stReadData);
                    throw new PopbillException(t.code, t.message);
                }
                throw new PopbillException(-99999999, we.Message);
            }
        }

        protected T httppostFile<T>(String url, String CorpNum, String UserID, String form, List<UploadFile> UploadFiles, String httpMethod)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ServiceURL + url);

            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");


            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.KeepAlive = true;
            request.Method = "POST";

            if (String.IsNullOrEmpty(CorpNum) == false)
            {
                String bearerToken = getSession_Token(CorpNum);
                request.Headers.Add("Authorization", "Bearer" + " " + bearerToken);
            }

            request.Headers.Add("x-lh-version", APIVersion);

            if (String.IsNullOrEmpty(UserID) == false)
            {
                request.Headers.Add("x-pb-userid", UserID);
            }

            if (String.IsNullOrEmpty(httpMethod) == false)
            {
                request.Headers.Add("X-HTTP-Method-Override", httpMethod);
            }

            Stream wstream = request.GetRequestStream();

            
            if (String.IsNullOrEmpty(form) == false)
            {
                String formBody = "--" + boundary + CRLF;
                formBody += "content-disposition: form-data; name=\"form=\"" + CRLF;
                formBody += "content-type: Application/json; charset=utf-8" + CRLF + CRLF;
                formBody += form + "\r\n";
                byte[] btFormBody = Encoding.UTF8.GetBytes(formBody);

                wstream.Write(btFormBody, 0, btFormBody.Length);
            }

            foreach (UploadFile f in UploadFiles)
            {
                String fileHeader = "--" + boundary + CRLF;
                fileHeader += "content-disposition: form-data; name=\"" + f.FieldName + "\"; filename=\"" + f.FileName + "\"" + CRLF;
                fileHeader += "content-type: Application/octet-stream" + CRLF + CRLF;

                byte[] btFileHeader = Encoding.UTF8.GetBytes(fileHeader);

                wstream.Write(btFileHeader, 0, btFileHeader.Length);

                byte[] buffer = new byte[32768];
                int read;
                while ((read = f.FileData.Read(buffer, 0, buffer.Length)) > 0)
                {
                    wstream.Write(buffer, 0, read);
                }
            }

            String boundaryFooter = CRLF + "--" + boundary + "--" + CRLF;
            byte[] btboundaryFooter = Encoding.UTF8.GetBytes(boundaryFooter);

            wstream.Write(btboundaryFooter, 0, btboundaryFooter.Length);

            //wstream.Flush();
            wstream.Close();
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream stReadData = response.GetResponseStream();

                return fromJson<T>(stReadData);

            }
            catch (Exception we)
            {
                if (we is WebException && ((WebException)we).Response != null)
                {
                    Stream stReadData = ((WebException)we).Response.GetResponseStream();
                    Response t = fromJson<Response>(stReadData);
                    throw new PopbillException(t.code, t.message);
                }
                throw new PopbillException(-99999999, we.Message);
            }
        }
        #endregion


        public class UploadFile
        {
            public String FieldName;
            public String FileName;
            public Stream FileData;
        }

        [DataContract]
        public class URLResponse
        {
            [DataMember]
            public String url;
        }

        [DataContract]
        public class UnitCostResponse
        {
            [DataMember]
            public Single unitCost;
        }

    }
}