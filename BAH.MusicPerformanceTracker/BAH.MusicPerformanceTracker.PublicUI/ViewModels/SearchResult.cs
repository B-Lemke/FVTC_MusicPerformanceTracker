using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BAH.MusicPerformanceTracker.BL;

namespace BAH.MusicPerformanceTracker.PublicUI.ViewModels
{
    public class SearchResult
    {

        public SearchType Type { get; set; }
        public PerformanceList PerformanceList { get; set; }
        public PieceList PieceList { get; set; }
        public ComposerList ComposerList { get; set; }
        public SearchType SearchMode { get; set; }



        public SearchResult()
        {
            PerformanceList = new PerformanceList();
            PieceList = new PieceList();
            ComposerList = new ComposerList();
        }

    }
    public enum SearchType
    {
        Performance,
        Piece,
        Composer
    }
}