using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;

using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Xamarin.Android;

namespace OxyPlotApp
{
    [Activity(Label = "@string/app_name", MainLauncher = true, LaunchMode = Android.Content.PM.LaunchMode.SingleTop, Icon = "@drawable/icon")]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.main);

            var viewLine = FindViewById<PlotView>(Resource.Id.plot_view_line);
            viewLine.Model = CreatePlotModel();

            //var viewBar = FindViewById<PlotView>(Resource.Id.plot_view_bar);
            //viewBar.Model = CreateBarModel();
        }

        private PlotModel CreatePlotModel()
        {
            var plotModel = new PlotModel { Title = "Line Chart Demo" };

            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Maximum = 10, Minimum = 0 });

            var series1 = new LineSeries
            {
                MarkerType = MarkerType.Circle,
                MarkerSize = 4,
                MarkerStroke = OxyColors.White
            };

            series1.Points.Add(new DataPoint(0.0, 6.0));
            series1.Points.Add(new DataPoint(1.4, 2.1));
            series1.Points.Add(new DataPoint(2.0, 4.2));
            series1.Points.Add(new DataPoint(3.3, 2.3));
            series1.Points.Add(new DataPoint(4.7, 7.4));
            series1.Points.Add(new DataPoint(6.0, 6.2));
            series1.Points.Add(new DataPoint(8.9, 8.9));

            plotModel.Series.Add(series1);

            return plotModel;
        }

        //private PlotModel CreateBarModel()
        //{
        //    var model = new PlotModel { Title = "Bar Chart Demo" };

        //    return model;

        //    //generate a random percentage distribution between the 5
        //    //cake-types (see axis below)
        //    var rand = new Random();
        //    double[] cakePopularity = new double[5];
        //    double sum = 0.0;
        //    for (int i = 0; i < 5; ++i)
        //    {
        //        cakePopularity[i] = rand.NextDouble();
        //        sum += cakePopularity[i];
        //    }

        //    var barSeries = new BarSeries
        //    {
        //        ItemsSource = new List<BarItem>(new[] {
        //        new BarItem{ Value = (cakePopularity[0] / sum * 100) },
        //        new BarItem{ Value = (cakePopularity[1] / sum * 100) },
        //        new BarItem{ Value = (cakePopularity[2] / sum * 100) },
        //        new BarItem{ Value = (cakePopularity[3] / sum * 100) },
        //        new BarItem{ Value = (cakePopularity[4] / sum * 100) }
        //        }),
        //        LabelPlacement = LabelPlacement.Inside,
        //        LabelFormatString = "{0:.00}%"
        //    };
        //    model.Series.Add(barSeries);

        //    model.Axes.Add(new CategoryAxis
        //    {
        //            Position = AxisPosition.Left,
        //            Key = "CakeAxis",
        //            ItemsSource = new[]
        //                {
        //                    "Apple cake",
        //                    "Baumkuchen",
        //                    "Bundt Cake",
        //                    "Chocolate cake",
        //                    "Carrot cake"
        //                }
        //    });

        //    return model;
        //}
    }
}

