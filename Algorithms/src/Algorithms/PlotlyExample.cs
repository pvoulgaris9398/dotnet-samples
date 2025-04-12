using Plotly.NET;
using Plotly.NET.LayoutObjects;
namespace Algorithms
{
    internal static class PlotlyExample
    {

        internal static void Execute()
        {
            double[] x = [.. Enumerable.Range(1, 1000).Select(i => (double)i / 100f)];
            double[] y = [.. Enumerable.Range(1, 1000).Select(i => (double)Math.Pow(i / 100f, 2))];

            LinearAxis xAxis = new();
            xAxis.SetValue("title", "xAxis");
            xAxis.SetValue("showgrid", false);
            xAxis.SetValue("showline", true);

            LinearAxis yAxis = new();
            yAxis.SetValue("title", "yAxis");
            yAxis.SetValue("showgrid", false);
            yAxis.SetValue("showline", true);

            Layout layout = new();
            layout.SetValue("xaxis", xAxis);
            layout.SetValue("yaxis", yAxis);
            layout.SetValue("showlegend", true);

            Trace trace = new("scatter");
            trace.SetValue("x", x);
            trace.SetValue("y", y);
            trace.SetValue("mode", "markers");
            trace.SetValue("name", "Hello from C#");

            GenericChart
                .ofTraceObject(true, trace)
                .WithLayout(layout)
                .Show();
        }
    }
}
