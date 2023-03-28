using Plotly.NET;
using Plotly.NET.LayoutObjects;
using System.Collections.Generic;

namespace Core.Infra.Services.GraphPlotters
{
    public class GraphPlotter
    {

        public static void PlotChart(List<double> listResult, List<double> listOfExpectedResults, List<double> yValues)
        {
            double[] xValues = listResult.ToArray();
            double[] xValues2 = listOfExpectedResults.ToArray();

            var chart = GenerateChart(xValues, yValues.ToArray(), "results");
            var chart2 = GenerateChart(xValues2, yValues.ToArray(), "expected");
            var chartList = new List<GenericChart.GenericChart>();

            chartList.Add(chart);
            chartList.Add(chart2);

            var combinedChart = Chart.Combine(chartList);

            combinedChart.Show();
        }


        private static GenericChart.GenericChart GenerateChart(double[] xValues, double[] yValues, string legend)
        {
            Trace trace;

            LinearAxis xAxis = new LinearAxis();
            xAxis.SetValue("title", "xAxis");
            xAxis.SetValue("showgrid", false);
            xAxis.SetValue("showline", true);

            LinearAxis yAxis = new LinearAxis();
            yAxis.SetValue("title", "yAxis");
            yAxis.SetValue("showgrid", false);
            yAxis.SetValue("showline", true);

            var layout = new Layout();
            layout.SetValue("xaxis", xAxis);
            layout.SetValue("yaxis", yAxis);
            layout.SetValue("showlegend", true);

            trace = new Plotly.NET.Trace("scatter");
            trace.SetValue("x", xValues);
            trace.SetValue("y", yValues);

            trace.SetValue("mode", "markers");
            trace.SetValue("name", legend);

            return GenericChart
                            .ofTraceObject(true, trace)
                            .WithLayout(layout);
        }
    }
}
