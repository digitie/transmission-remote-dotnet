using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransmissionClientNew
{
    public enum ResponseTag
    {
        SessionGet, TorrentGet, TorrentGetLoop, DoNothing, UpdateFiles, UpdatePriorities
    }

    public interface TransmissionCommand
    {
        void Execute();
    }
}
