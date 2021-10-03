using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using YoutubeVideos.Models;
using System.IO;
using YoutubeVideos.Data;

namespace YoutubeVideos.Helpers
{
    public class MainHelper
    {
       
        public static void GetNewVideoStatisticsFromYoutubeApi()
        {
            var newNonTrendingVideos = new List<NonTrendingVideoModel>();
            var totalCostOfRequests = 0;

            try
            {
                var nonTrendingVideosThatExists = CsvHelper.ReadNonTrendingVideosFromCsv();
                var nextPageToken = "";

                while (totalCostOfRequests < AppSettings.MaxCostOfRequestsEachDay)
                {
                    var youtubeVideos = ApiHelper.GetRandomNonTrendingYoutubeVideos(nextPageToken);
                    youtubeVideos.items = youtubeVideos.items.Where(x => x.id.kind == "youtube#video").ToList();
                    nextPageToken = youtubeVideos.nextPageToken;
                    totalCostOfRequests += AppSettings.SearchListCost;
                    var videoIdPublishTime = youtubeVideos.items.GroupBy(x => x.id.videoId).ToDictionary(x => x.Key, y => y.FirstOrDefault().snippet.publishedAt);

                    var newVideoIds = videoIdPublishTime.Keys.ToList();
                    totalCostOfRequests += AppSettings.VideoListCost;
                    if (newVideoIds.Count == 0) continue;

                    var nonTrendingVideoStatistics = ApiHelper.GetStatisticsByVideoIds(newVideoIds);
                    newNonTrendingVideos.AddRange(nonTrendingVideoStatistics.items.Select(x => new NonTrendingVideoModel()
                    {
                        VideoId = x.id,
                        Views = Convert.ToInt32(x.statistics.viewCount),
                        Likes = Convert.ToInt32(x.statistics.likeCount),
                        Comments = Convert.ToInt32(x.statistics.commentCount),
                        Dislikes = Convert.ToInt32(x.statistics.dislikeCount),
                        DifferenceBetweenPublishAndActualDate = (DateTime.Now.Date - videoIdPublishTime[x.id].Date).Days,
                        IsTrending = 0
                    }).ToList());


                }

                nonTrendingVideosThatExists.AddRange(newNonTrendingVideos);

                var csvModel = CsvHelper.ConvertToCsv(nonTrendingVideosThatExists);

                File.WriteAllText(AppSettings.NonTrendingVideosPath, csvModel);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error:GetNewVideoStatisticsFromYoutubeApi" + ex);
            }

        }

        public static void GetVideoStatisticsFromDataset()
        {
            try
            {
                var trendingVideos = CsvHelper.ReadDatasetVideosFromCsv();

                var csvModel = CsvHelper.ConvertToCsv(trendingVideos);

                File.WriteAllText(AppSettings.TrendingVideosPath, csvModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:GetVideoStatisticsFromDataset" + ex);
            }
        }

        public static void MergeTrendingAndNonTrendingVideoStatistics()
        {
            var nonTrendingVideos = CsvHelper.ReadNonTrendingVideosFromCsv();
            var trendingVideos = CsvHelper.ReadTrendingVideosFromCsv();
            nonTrendingVideos.AddRange(trendingVideos);
            
            var csvModel = CsvHelper.ConvertToCsv(nonTrendingVideos);

            File.WriteAllText(AppSettings.TrendingNonTrendingVideosPath, csvModel);

        }
    }
}
