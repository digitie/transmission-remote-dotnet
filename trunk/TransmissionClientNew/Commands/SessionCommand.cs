using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jayrock.Json;
using System.Windows.Forms;

namespace TransmissionClientNew.Commmands
{
    class SessionCommand : TransmissionCommand
    {
        public SessionCommand(JsonObject response)
        {
            Program.sessionData = response;
            Program.failCount = 0;
        }

        public void Execute()
        {
            if (!Program.Connected)
            {
                Program.Connected = true;
                Program.form.RefreshWorker.RunWorkerAsync(true);
                if (Program.uploadArgs != null)
                {
                    Program.form.CreateUploadWorker().RunWorkerAsync(Program.uploadArgs);
                    Program.uploadArgs = null;
                }
            }
        }
    }
}
