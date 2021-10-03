using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using YoutubeVideos.Data;
using YoutubeVideos.Models;
using static YoutubeVideos.Models.SearchModel;

namespace YoutubeVideos.Helpers
{
    public class ApiHelper
    {
        public static SearchModel.Root GetRandomNonTrendingYoutubeVideos(string nextPageToken)
        {
            var requestLink = AppSettings.YoutubeApiSearch + nextPageToken;
            var request = (HttpWebRequest)WebRequest.Create(requestLink);
            request.Method = "GET";
            request.ContentType = "application/json";
            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var responseString = streamReader.ReadToEnd();
                var responseDeserialized = JsonConvert.DeserializeObject<SearchModel.Root>(responseString);

                return responseDeserialized;
            }
        }

        public static VideoModel.Root GetStatisticsByVideoIds(List<string> videoIds)
        {
            var ids = string.Join("%2C",videoIds);
            var requestLink = AppSettings.YoutubeApiVideo + ids;
            var request = (HttpWebRequest)WebRequest.Create(requestLink);
            request.Method = "GET";
            request.ContentType = "application/json";
            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var responseString = streamReader.ReadToEnd();
                var responseDeserialized = JsonConvert.DeserializeObject<VideoModel.Root>(responseString);

                return responseDeserialized;
            }
        }
    }
}
