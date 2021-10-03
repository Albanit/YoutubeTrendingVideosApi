using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace YoutubeVideos.Models
{

    public class TrendingVideoModel
    {
        public string VideoId { get; set; }
        public int Views { get; set; }
        public int Likes { get; set; }
        public int Comments { get; set; }
        public int Dislikes { get; set; }
        public DateTime TrendingDate { get; set; }
        public DateTime PublishedDate { get; set; }
        public int DifferenceBetweenPublishAndActualDate 
        { 
            get
            {
                return (TrendingDate - PublishedDate).Days;
            }
        }
        public int IsTrending
        {
            get
            {
                return 1;
            }
        }
    }

    public class TrendingVideoModelMap : ClassMap<TrendingVideoModel>
    {
        public TrendingVideoModelMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.VideoId).Name("video_id");
            Map(m => m.Views).Name("view_count");
            Map(m => m.Likes).Name("likes");
            Map(m => m.Dislikes).Name("dislikes");
            Map(m => m.Comments).Name("comment_count");
            Map(m => m.TrendingDate).Name("trending_date");
            Map(m => m.PublishedDate).Name("publishedAt");
        }
    }
}
