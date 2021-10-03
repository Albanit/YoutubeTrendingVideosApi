using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using YoutubeVideos.Data;
using YoutubeVideos.Models;

namespace YoutubeVideos.Helpers
{
    public class CsvHelper
    {
        public static string ConvertToCsv<T>(IEnumerable<T> list) where T : class
        {
            using (var sw = new StringWriter())
            {
                var csv = new CsvWriter(sw, CultureInfo.InvariantCulture);

                var headers = new List<string>();
                foreach (var property in typeof(T).GetProperties())
                {
                    var headerTitle = property.Name;
                    var attrs = property.GetCustomAttributes(true);
                    foreach (var attr in attrs)
                    {
                        var authAttr = attr as CsvHeaderAttribute;
                        if (authAttr != null)
                            headerTitle = authAttr.Title;
                    }
                    csv.WriteField(headerTitle);
                    headers.Add(headerTitle);
                }
                csv.NextRecord();

                foreach (var item in list)
                {
                    foreach (var property in item.GetType().GetProperties())
                    {
                        csv.WriteField(property.GetValue(item));
                    }
                    csv.NextRecord();
                }

                return sw.ToString();
            }
        }

        public static List<NonTrendingVideoModel> ReadNonTrendingVideosFromCsv()
        {
            using (var reader = new StreamReader(AppSettings.NonTrendingVideosPath))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<NonTrendingVideoModelMap>();
                   
                    return  csv.GetRecords<NonTrendingVideoModel>().ToList();
                }
            }
        }

        public static List<TrendingVideoModel> ReadDatasetVideosFromCsv()
        {
            using (var reader = new StreamReader(AppSettings.DatasetPath))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<TrendingVideoModelMap>();

                    return csv.GetRecords<TrendingVideoModel>().ToList();
                }
            }
        }

        public static List<NonTrendingVideoModel> ReadTrendingVideosFromCsv()
        {
            using (var reader = new StreamReader(AppSettings.TrendingVideosPath))
            {
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<NonTrendingVideoModelMap>();

                    return csv.GetRecords<NonTrendingVideoModel>().ToList();
                }
            }
        }

    }

    public class CsvHeaderAttribute : Attribute
    {
        public string Title { get; set; }
    }


}
