using System;
using YoutubeVideos.Helpers;

namespace YoutubeVideos
{
    class Program
    {
        public static void Main(string[] args)
        {
            bool response;

            Console.WriteLine("Get video statistics from dataset? \n True --> 1 \n False --> 0");
            response = int.Parse(Console.ReadLine()) == 1;
            if (response) MainHelper.GetVideoStatisticsFromDataset();

            Console.WriteLine("Get new non trending video statistics from youtube api? \n True --> 1 \n False --> 0");
            response = int.Parse(Console.ReadLine()) == 1;
            if (response) MainHelper.GetNewVideoStatisticsFromYoutubeApi();

            Console.WriteLine("Merge trending and nonTrending videos? \n True --> 1 \n False --> 0");
            response = int.Parse(Console.ReadLine()) == 1;
            if (response) MainHelper.MergeTrendingAndNonTrendingVideoStatistics();

        }
    }
}
