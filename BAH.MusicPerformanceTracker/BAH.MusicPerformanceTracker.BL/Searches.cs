using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BAH.MusicPerformanceTracker.PL;
using BAH.Utilities.Reporting;

namespace BAH.MusicPerformanceTracker.BL
{

    public class Search
    {
        public string SearchQuery { get; set; }
        public DateTime SearchDate { get; set; }
        public string SearchSource { get; set; }
        public string SearchLevel { get; set; }
        public string SearchTable { get; set; }

    }


    public class Searches
    {
        public List<Search> SearchList { get; set; }

        public Searches()
        {
            this.SearchList = new List<Search>();
        }


        public void Load()
        {
            using (MusicEntities dc = new MusicEntities())
            {
                var results = dc.SearchLogs;

                foreach (var search in results)
                {
                    Search newSearch = new Search
                    {
                        SearchDate = search.SearchDate.GetValueOrDefault(),
                        SearchLevel = search.SearchLevel,
                        SearchQuery = search.SearchQuery,
                        SearchSource = search.SearchLogger,
                        SearchTable = search.SearchTable
                    };

                    this.SearchList.Add(newSearch);
                }

            }
        }

        public void Export()
        {

            try
            {
                string[,] data = new string[this.SearchList.Count + 1, 5];
                int counter = 0;

                data[counter, 0] = "Search Query";
                data[counter, 1] = "Search Table";
                data[counter, 2] = "Search Found";
                data[counter, 3] = "Search Date";
                data[counter, 4] = "Search Source";
                counter++;

                foreach (var s in this.SearchList)
                {
                    data[counter, 0] = s.SearchQuery;
                    data[counter, 1] = s.SearchTable;
                    data[counter, 2] = s.SearchLevel == "INFO" ? "Yes" : "No";
                    data[counter, 3] = s.SearchDate.ToString();
                    data[counter, 4] = s.SearchSource;
                    counter++;
                }

                Excel.Export("searches_" +  DateTime.Now.Month + "-" + DateTime.Now.Day + "-" + DateTime.Now.Year + ".xlsx", data);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
