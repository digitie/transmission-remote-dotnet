// transmission-remote-dotnet
// http://code.google.com/p/transmission-remote-dotnet/
// Copyright (C) 2009 Alan F
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using TransmissionRemoteDotnet.Settings;

namespace TransmissionRemoteDotnet
{
    public class TransmissionWebClient : WebClient
    {
        private bool authenticate;
        private static string x_transmission_session_id;

        public static string X_transmission_session_id
        {
            get { return x_transmission_session_id; }
            set { x_transmission_session_id = value; }
        }

        public TransmissionWebClient(bool authenticate)
        {
            this.authenticate = authenticate;
        }

        public static bool ValidateServerCertificate(
                    object sender,
                    X509Certificate certificate,
                    X509Chain chain,
                    SslPolicyErrors sslPolicyErrors)
        {
            return sslPolicyErrors != SslPolicyErrors.RemoteCertificateNotAvailable; // we need certificate, but accept untrusted
        }

        public event EventHandler<ResultEventArgs> Completed;
        internal void OnCompleted(ICommand result)
        {
            if (Completed != null)
            {
                Completed(this, new ResultEventArgs() { Result = result });
            }
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            try
            {
                if (request.GetType() == typeof(HttpWebRequest))
                {
                    SetupWebRequest((HttpWebRequest)request, authenticate);
                }
            }
            catch (PasswordEmptyException e)
            {
                this.CancelAsync();
            }
            return request;
        }

        public static void SetupWebRequest(HttpWebRequest request, bool authenticate)
        {
            request.KeepAlive = false;
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            request.UserAgent = "Mozilla/5.0 (X11; U; Linux i686; en-GB; rv:1.9.0.10) Gecko/2009042523 Ubuntu/9.04 (jaunty) Firefox/3.0.10";
            if (x_transmission_session_id != null && authenticate)
                request.Headers["X-Transmission-Session-Id"] = x_transmission_session_id;
            Settings.TransmissionServer settings = Program.Settings.Current;
            if (settings.AuthEnabled && authenticate)
            {
                request.Credentials = new NetworkCredential(settings.Username, settings.ValidPassword);
                request.PreAuthenticate = Program.DaemonDescriptor.Version < 1.40 || Program.DaemonDescriptor.Version >= 1.6;
            }
            if (settings.Proxy.ProxyMode == ProxyMode.Enabled)
            {
                request.Proxy = new WebProxy(settings.Proxy.Host, settings.Proxy.Port);
                if (settings.Proxy.AuthEnabled)
                {
                    request.Proxy.Credentials = new NetworkCredential(settings.Proxy.Username, settings.Proxy.ValidPassword);
                }
            }
            else if (settings.Proxy.ProxyMode == ProxyMode.Disabled)
            {
                request.Proxy = null;
            }
        }
    }
}