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
                    Color = SkiaSharp.SKColor.Parse("#266489")
                },
                new Microcharts.Entry(400)
                {
                    Label = "February",
                    ValueLabel = "400",
                    Color = SkiaSharp.SKColor.Parse("#68B9C0")
                },
                new Microcharts.Entry(-100)
                {
                    Label = "March",
                    ValueLabel = "-100",
                    Color = SkiaSharp.SKColor.Parse("#90D585")
                },
                new Microcharts.Entry(150)
                {
                    Label = "April",
                    ValueLabel = "150",
                    Color = SkiaSharp.SKColor.Parse("#90D585")
                }
            };

            //var chart = new BarChart() { Entries = entries };
            //var chart = new PointChart() { Entries = entries };
            //var chart = new LineChart() { Entries = entries };
            //var chart = new DonutChart() { Entries = entries };
            //var chart = new RadialGaugeChart() { Entries = entries };
            //var chart = new RadarChart() { Entries = entries };

            var chart = new LineChart()
            {
                Entries = entries,
                LineMode = LineMode.Straight,
                LineSize = 8,
                PointMode = PointMode.Square,
                PointSize = 18,
                BackgroundColor = SkiaSharp.SKColor.Parse("#FFFFFF")
            };

            var chartView = FindViewById<Microcharts.Droid.ChartView>(Resource.Id.chartView);
            chartView.Chart = chart;
        }
    }

}