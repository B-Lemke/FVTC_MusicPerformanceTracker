using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BAH.MusicPerformanceTracker.PublicUI.Models
{
    public class SearchDropDown
    {
        public SearchType Type { get; set; }
    }

    public enum SearchType
    {
        Performance,
        Piece
    }
}