using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jayrock.Json;

namespace TransmissionClientNew.Commmands
{
    class UpdatePrioritiesCommand : TransmissionCommand
    {
        private JsonObject response;

        public UpdatePrioritiesCommand(JsonObject response)
        {
            this.response = response;
            Program.failCount = 0;
        }

        public void Execute()
        {
            JsonObject arguments = (JsonObject)response["arguments"];
            JsonArray torrents = (JsonArray)arguments["torrents"];
            JsonObject torrent = (JsonObject)torrents[0];
            int id = ((JsonNumber)torrent["id"]).ToInt32();
            TorrentInfoDialog form;
            if (Program.infoDialogs.ContainsKey(id))
            {
                form = Program.infoDialogs[id];
            }
            else
            {
                return;
            }
            UpdateFilesCommand.UpdateFiles(torrent, form);
            if (torrent["uploadLimitMode"] != null)
            {
                form.UploadLimitEnable.Enabled = form.DownloadLimitEnable.Enabled = true;
                form.UploadLimitField.Enabled = form.UploadLimitEnable.Checked = ((JsonNumber)torrent["uploadLimitMode"]).ToBoolean();
                form.UploadLimitField.Value = ((JsonNumber)torrent["uploadLimit"]).ToInt32();
                form.DownloadLimitField.Enabled = form.DownloadLimitEnable.Checked = ((JsonNumber)torrent["downloadLimitMode"]).ToBoolean();
                form.DownloadLimitField.Value = ((JsonNumber)torrent["downloadLimit"]).ToInt32();
                form.FilesListView.Enabled = true;
                Toolbox.StripeListView(form.FilesListView);
            }
            form.StatusStripLabel.Text = "";
        }
    }
}
