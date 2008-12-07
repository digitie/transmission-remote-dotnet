using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jayrock.Json;
using System.Windows.Forms;
using System.IO;

namespace TransmissionClientNew
{
    class Requests
    {
        public static JsonObject SessionGet()
        {
            JsonObject request = new JsonObject();
            request.Put("method", "session-get");
            request.Put("tag", (int)ResponseTag.SessionGet);
            return request;
        }

        public static JsonObject Generic(string method, JsonArray ids)
        {
            JsonObject request = new JsonObject();
            request.Put("method", method);
            JsonObject arguments = new JsonObject();
            if (ids != null)
            {
                arguments.Put("ids", ids);
            }
            request.Put("arguments", arguments);
            request.Put("tag", (int)ResponseTag.DoNothing);
            return request;
        }

        public static JsonObject Files(JsonArray ids)
        {
            JsonObject request = new JsonObject();
            request.Put("method", "torrent-get");
            request.Put("tag", (int)ResponseTag.UpdateFiles);
            JsonObject arguments = new JsonObject();
            arguments.Put("ids", ids);
            JsonArray fields = new JsonArray();
            fields.Put("files");
            fields.Put("id");
            arguments.Put("fields", fields);
            request.Put("arguments", arguments);
            return request;
        }
        
        public static JsonObject Priorities(int id)
        {
            JsonObject request = new JsonObject();
            request.Put("method", "torrent-get");
            request.Put("tag", (int)ResponseTag.UpdatePriorities);
            JsonObject arguments = new JsonObject();
            JsonArray ids = new JsonArray();
            ids.Put(id);
            arguments.Put("ids", ids);
            JsonArray fields = new JsonArray();
            fields.Put("files");
            fields.Put("id");
            fields.Put("priorities");
            fields.Put("wanted");
            fields.Put("downloadLimit");
            fields.Put("downloadLimitMode");
            fields.Put("uploadLimit");
            fields.Put("uploadLimitMode");
            arguments.Put("fields", fields);
            request.Put("arguments", arguments);
            return request;
        }

        public static JsonObject TorrentGet(Boolean beginLoop)
        {
            JsonObject request = new JsonObject();
            request.Put("method", "torrent-get");
            request.Put("tag", beginLoop ? (int)ResponseTag.TorrentGetLoop : (int)ResponseTag.TorrentGet);
            JsonObject arguments = new JsonObject();
            JsonArray fields = new JsonArray(new string[]{"id", "addedDate","announceURL","comment","creator","dateCreated","haveValid","error","errorString","eta","hashString","haveUnchecked","haveValid","id","isPrivate","leechers","leftUntilDone","name","peersGettingFromUs","peersKnown","peersSendingToUs","rateDownload","rateUpload","seeders","sizeWhenDone","status","swarmSpeed","totalSize","uploadedEver","peers"});
            arguments.Put("fields", fields);
            request.Put("arguments", arguments);
            return request;
        }
    }
}
