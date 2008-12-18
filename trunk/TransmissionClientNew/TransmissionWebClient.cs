﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace TransmissionRemoteDotnet
{
    class TransmissionWebClient : WebClient
    {
        public static bool ValidateServerCertificate(
                    object sender,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors)
        {
            return sslPolicyErrors != SslPolicyErrors.RemoteCertificateNotAvailable; // we need certificate, but accept untrusted
        }

        /* Some proxies may require a known user agent? */
        public const string userAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1)";

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            if (request.GetType() == typeof(HttpWebRequest))
            {
                SetupWebRequest((HttpWebRequest)request);
            }
            return request;
        }

        public static void SetupWebRequest(HttpWebRequest request)
        {
            /* Very important !!! Fixes bug in .NET (or shttpd?) */
            request.KeepAlive = false;
            request.UserAgent = TransmissionWebClient.userAgent;
            LocalSettingsSingleton settings = LocalSettingsSingleton.Instance;

            if (settings.authEnabled)
            {
                request.Credentials = new NetworkCredential(settings.user, settings.pass);
                request.PreAuthenticate = Program.transmissionVersion < 1.40;
            }
            if (settings.proxyEnabled == 1)
            {
                request.Proxy = new WebProxy(settings.proxyHost, settings.proxyPort);
                if (settings.proxyAuth)
                {
                    request.Proxy.Credentials = new NetworkCredential(settings.proxyUser, settings.proxyPass);
                }
            }
            else if (settings.proxyEnabled == 2)
            {
                request.Proxy = null;
            }
        }
    }
}