using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using BAH.MusicPerformanceTracker.BL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BAH.MusicPerformanceTracker.PublicUI.ViewModels;

namespace BAH.MusicPerformanceTracker.PublicUI.Controllers
{
    public class SearchController : Controller
    {
        //Get the log to log searches
        log4net.ILog log = log4net.LogManager.GetLogger("Search.Logger");

        PerformanceList performances;
        PieceList pieces;

        // GET: Search
        public ActionResult Index(string sortOrder, string searchQuery, string currentFilter, string ddlSearchTable)
        {
            //Store the previous search
            if (string.IsNullOrEmpty(searchQuery))
            {
                searchQuery = currentFilter;
            }

            ViewBag.CurrentFilter = searchQuery;

            if(ddlSearchTable == "Performance" || ddlSearchTable == null)
            {


                performances = new PerformanceList();

                //Initialize Cient
                HttpClient client = InitializeClient();

                //Call the API
                HttpResponseMessage response = client.GetAsync("Performance").Result;

                //Deserialize the json
                string result = response.Content.ReadAsStringAsync().Result;
                dynamic items = (JArray)JsonConvert.DeserializeObject(result);
                performances = items.ToObject<PerformanceList>();

                IEnumerable<Performance> filteredPerformances;

                //Filter by search box
                if (!string.IsNullOrEmpty(searchQuery))
                {
                    log4net.LogicalThreadContext.Properties["tableName"] = "Performances";
                    log4net.LogicalThreadContext.Properties["searchId"] = Guid.NewGuid();
                    filteredPerformances = performances.Where(p => p.Name.ToLower().Contains(searchQuery.ToLower()));
                    if (filteredPerformances.Count() <= 0)
                    {
                        if (log.IsWarnEnabled)
                        {
                            log.Warn(searchQuery);
                        }
                    }
                    else
                    {
                        if (log.IsInfoEnabled)
                        {
                            log.Info(searchQuery);
                        }
                    }
                }
                else
                {
                    filteredPerformances = performances;
                }



                //Set the sorting options
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

                System.Collections.Generic.List<Performance> sortedPerformances;

                switch (sortOrder)
                {
                    case "name_desc":
                        sortedPerformances = filteredPerformances.OrderByDescending(p => p.Name).ToList();
                        break;
                    case "Date":
                        sortedPerformances = filteredPerformances.OrderBy(p => p.PerformanceDate).ToList();
                        break;
                    case "date_desc":
                        sortedPerformances = filteredPerformances.OrderByDescending(p => p.PerformanceDate).ToList();
                        break;
                    default:
                        sortedPerformances = filteredPerformances.OrderBy(p => p.Name).ToList();
                        break;
                }

                SearchResult searchResult = new SearchResult();
                searchResult.PerformanceList.AddRange(sortedPerformances);


                searchResult.SearchMode = SearchType.Performance;
                return View(searchResult);
            }
            else if (ddlSearchTable == "Piece")
            {
                pieces = new PieceList();

                //Initialize Cient
                HttpClient client = InitializeClient();

                //Call the API
                HttpResponseMessage response = client.GetAsync("Piece").Result;

                //Deserialize the json
                string result = response.Content.ReadAsStringAsync().Result;
                dynamic items = (JArray)JsonConvert.DeserializeObject(result);
                pieces = items.ToObject<PieceList>();

                IEnumerable<Piece> filteredPieces;

                //Filter by search box
                if (!string.IsNullOrEmpty(searchQuery))
                {
                    log4net.LogicalThreadContext.Properties["tableName"] = "Pieces";
                    log4net.LogicalThreadContext.Properties["searchId"] = Guid.NewGuid();
                    filteredPieces = pieces.Where(p => p.Name.ToLower().Contains(searchQuery.ToLower()));
                    if (filteredPieces.Count() <= 0)
                    {
                        if (log.IsWarnEnabled)
                        {
                            log.Warn(searchQuery);
                        }
                    }
                    else
                    {
                        if (log.IsInfoEnabled)
                        {
                            log.Info(searchQuery);
                        }
                    }
                }
                else
                {
                    filteredPieces = pieces;
                }



                //Set the sorting options
                ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                ViewBag.YearSortParm = sortOrder == "Year" ? "year_desc" : "Year";

                System.Collections.Generic.List<Piece> sortedPieces;

                switch (sortOrder)
                {
                    case "name_desc":
                        sortedPieces = filteredPieces.OrderByDescending(p => p.Name).ToList();
                        break;
                    case "Year":
                        sortedPieces = filteredPieces.OrderBy(p => p.YearWritten).ToList();
                        break;
                    case "year_desc":
                        sortedPieces = filteredPieces.OrderByDescending(p => p.YearWritten).ToList();
                        break;
                    default:
                        sortedPieces = filteredPieces.OrderBy(p => p.Name).ToList();
                        break;
                }

                SearchResult searchResult = new SearchResult();
                searchResult.PieceList.AddRange(sortedPieces);

                searchResult.SearchMode = SearchType.Piece;
                return View(searchResult);
            }
            else
            {
                SearchResult searchResult = new SearchResult();
                return View(searchResult);
            }
        }





        private static HttpClient InitializeClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("x-apikey", "12345");
            client.BaseAddress = new Uri("http://musicperformancetracker.azurewebsites.net/api/");
            return client;
        }
    }
}