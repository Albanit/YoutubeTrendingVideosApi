using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace YoutubeVideos.Models
{
    public class NonTrendingVideoModel
    {
        public string VideoId { get; set; }
        public int Views { get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; }
        public int Dislikes { get; set; }
        public int DifferenceBetweenPublishAndActualDate { get; set; }
        public int IsTrending { get; set; }
    }

    public class NonTrendingVideoModelMap : ClassMap<NonTrendingVideoModel>
    {
        public NonTrendingVideoModelMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.VideoId).Name("VideoId");
            Map(m => m.Views).Name("Views");
            Map(m => m.Likes).Name("Likes");
            Map(m => m.Dislikes).Name("Dislikes");
            Map(m => m.Comments).Name("Comments");
            Map(m => m.DifferenceBetweenPublishAndActualDate).Name("DifferenceBetweenPublishAndActualDate");
            Map(m => m.IsTrending).Name("IsTrending");
        }
    }

}
