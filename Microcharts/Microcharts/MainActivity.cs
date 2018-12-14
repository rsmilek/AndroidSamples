using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using DevPirates;
using DevPirates.Microcharts;
using DevPirates.Microcharts.Droid;

namespace Microcharts
{

    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var entries = new[]
            {
                new Microcharts.Entry(200)
                {
                    Label = "January",
                    ValueLabel = "200",
                    Color = SkiaSharp.SKColor.Parse("#2c3e50")
                },
                new Microcharts.Entry(400)
                {
                    Label = "February",
                    ValueLabel = "400",
                    Color = SkiaSharp.SKColor.Parse("#77d065")
                },
                new Microcharts.Entry(-100)
                {
                    Label = "March",
                    ValueLabel = "-100",
                    Color = SkiaSharp.SKColor.Parse("#b455b6")
                },
                new Microcharts.Entry(150)
                {
                    Label = "April",
                    ValueLabel = "150",
                    Color = SkiaSharp.SKColor.Parse("#3498db")
                }
            };

            //var chart = new BarChart() { Entries = entries };
            //var chart = new PointChart() { Entries = entries };
            //var chart = new LineChart() { Entries = entries };
            //var chart = new DonutChart() { Entries = entries };
            //var chart = new RadialGaugeChart() { Entries = entries };
            //var chart = new RadarChart() { Entries = entries };

            var barChart = new BarChart()
            {
                Entries = entries,
                LabelTextSize = 36
            };

            var lineChart = new LineChart()
            {
                Entries = entries,
                LineMode = LineMode.Straight,
                LineSize = 8,
                PointMode = PointMode.Square,
                PointSize = 18,
                LabelTextSize = 36
            };

            var lineChart2 = new LineChart()
            {
                Entries = entries,
                LineMode = LineMode.Spline,
                LineSize = 8,
                PointMode = PointMode.Circle,
                PointSize = 18,
                LabelTextSize = 36
            };

            var chartViewBar = FindViewById<Microcharts.Droid.ChartView>(Resource.Id.chartViewBar);
            chartViewBar.Chart = barChart;

            var chartViewLine = FindViewById<Microcharts.Droid.ChartView>(Resource.Id.chartViewLine);
            chartViewLine.Chart = lineChart;

            var chartViewLine2 = FindViewById<Microcharts.Droid.ChartView>(Resource.Id.chartViewLine2);
            chartViewLine2.Chart = lineChart2;
        }
    }

}