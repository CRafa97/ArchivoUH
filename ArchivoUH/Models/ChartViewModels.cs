using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Dynamic;

namespace ArchivoUH.Models
{
    //Predefined index charts
    public class IndexCharts
    {
        public BarChart BarChart { get; set; }

        public PieChart PieChart { get; set; }

        public PieChart FacChart { get; set; }

        public AreaChart Comparison { get; set; }
    }

    public class BarChart
    {
        public string Label1 { get; set; }
        public string Label2 { get; set; }

        public IEnumerable<string> Labels { get; set; }

        public IEnumerable<int> Data1 { get; set; }

        public IEnumerable<int> Data2 { get; set; }

        public string Type => "bar";
    
        public string CanvasName { get; set; }

        public bool Legend { get; set; }

        public int MaxTick { get; set; }
    }

    public class PieChart
    {
        public IEnumerable<string> Labels { get; set; }
        public IEnumerable<int> Data { get; set; }

        public IEnumerable<string> BkColor { get; set; }

        public IEnumerable<string> HoverBkColor { get; set; }

        public string Type => "doughnut";

        public string CanvasName { get; set; }

        public bool Legend { get; set; }
    }

    //predefined area chart view model
    public class AreaChart
    {
        public IEnumerable<string> Labels { get; set; }
        public IEnumerable<int> Data1 { get; set; }
        public IEnumerable<int> Data2 { get; set; }

        public string Type => "area";

        public string CanvasName { get; set; }
        public bool Legend { get; set; }
    }
}