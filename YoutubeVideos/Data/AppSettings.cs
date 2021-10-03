using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeVideos.Data
{
    public class AppSettings
    {

        public static readonly string ApiKey = "AIzaSyAepnuRe4tqYLFiIu57IjUA9F4-OQSVQD4";

        public static readonly string YoutubeApiSearch = "https://youtube.googleapis.com/youtube/v3/search?part=snippet&maxResults=50&key=" + ApiKey + "&pageToken=";
        public static readonly int SearchListCost = 100;

        public static readonly string YoutubeApiVideo = "https://youtube.googleapis.com/youtube/v3/videos?part=statistics&key=" + ApiKey + "&id=";
        public static readonly int VideoListCost = 1;

        public static readonly int MaxCostOfRequestsEachDay = 9900;

        public static readonly string NonTrendingVideosPath = @"C:\Users\pc\Desktop\NonTrendingVideos.csv";

        public static readonly string TrendingVideosPath = @"C:\Users\pc\Desktop\Youtube\TrendingVideos.csv";

        public static readonly string TrendingNonTrendingVideosPath = @"C:\Users\pc\Desktop\Youtube\TrendingNonTrendingVideos.csv";

        public static readonly string DatasetPath = @"C:\Users\pc\Desktop\Youtube\TrendingVideosData.csv";



    }
}
