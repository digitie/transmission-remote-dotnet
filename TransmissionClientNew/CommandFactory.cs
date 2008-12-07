﻿/* This factory class is responsible for dispatching JSON requests and
 * torrent uploads, and also creating an object using the command design
 * pattern which contains the logic for updating the UI. */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using Jayrock.Json;
using Jayrock.Json.Conversion;
using System.IO;
using TransmissionClientNew.Commmands;

namespace TransmissionClientNew
{
    public class CommandFactory
    {
        public static TransmissionCommand UploadFile(string file)
        {
            LocalSettingsSingleton settings = LocalSettingsSingleton.Instance;
            if (!Program.Connected || file == null || !file.EndsWith(".torrent", StringComparison.CurrentCultureIgnoreCase) || !File.Exists(file))
            {
                return null;
            }
            try
            {
                using (TransmissionWebClient wc = new TransmissionWebClient())
                {
                    wc.UploadFile(settings.url + "upload?paused=" + (settings.startPaused ? "true" : "false"), file);
                }
                if (!Program.form.RefreshWorker.IsBusy)
                {
                    Program.form.RefreshWorker.RunWorkerAsync(false);
                }
            }
            catch (Exception ex)
            {
                return new ErrorCommand(ex);
            }
            return null;
        }

        public static TransmissionCommand Request(JsonObject data)
        {
            string str_response = null;
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(LocalSettingsSingleton.Instance.url + "rpc");
                TransmissionWebClient.SetupWebRequest(request);
                request.Method = "POST";
                request.ContentType = "application/json";
                string json = data.ToString();
                request.ContentLength = json.Length;
                StreamWriter stOut = new StreamWriter(request.GetRequestStream(), System.Text.Encoding.ASCII);
                stOut.Write(json);
                stOut.Close();
                StreamReader stIn = new StreamReader(request.GetResponse().GetResponseStream());
                str_response = stIn.ReadToEnd();
                stIn.Close();
                JsonObject response = (JsonObject)JsonConvert.Import(str_response);
                if (response["result"].ToString() != "success")
                {
                    return new ErrorCommand("Unsuccessful request", str_response);
                }
                switch (((JsonNumber)response["tag"]).ToInt16())
                {
                    case (short)ResponseTag.SessionGet:
                        return new SessionCommand(response);
                    case (short)ResponseTag.TorrentGet:
                        return new TorrentGetCommand(response, false);
                    case (short)ResponseTag.TorrentGetLoop:
                        return new TorrentGetCommand(response, true);
                    case (short)ResponseTag.UpdateFiles:
                        return new UpdateFilesCommand(response);
                    case (short)ResponseTag.UpdatePriorities:
                        return new UpdatePrioritiesCommand(response);
                    case (short)ResponseTag.DoNothing:
                        return new NoCommand();
                }
            }
            catch (InvalidCastException)
            {
                return new ErrorCommand("Unable to parse the following response", str_response != null ? str_response : "Null");
            }
            catch (Exception ex)
            {
                return new ErrorCommand(ex);
            }
            return new ErrorCommand("Unknown response tag", str_response);
        }
    }
}