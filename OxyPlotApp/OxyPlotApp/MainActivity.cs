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

            var viewBar = FindViewById<PlotView>(Resource.Id.plot_view_bar);
            viewBar.Model = CreateBarModel();

            var viewBar2 = FindViewById<PlotView>(Resource.Id.plot_view_bar2);
            viewBar2.Model = CreateBar2Model();
        }


        private PlotModel CreatePlotModel()
        {
            var plotModel = new PlotModel { Title = "Line Chart Demo" };

            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom });
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = 0, Maximum = 10 });

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


        private PlotModel CreateBarModel()
        {
            var model = new PlotModel
            {
                Title = "Bar Chart Demo",
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.BottomCenter,
                LegendOrientation = LegendOrientation.Horizontal,
                LegendBorderThickness = 0
            };

            var barSeries = new BarSeries { Title = "Series 1", StrokeColor = OxyColors.Black, StrokeThickness = 1 };
            barSeries.Items.Add(new BarItem { Value = 25 });
            barSeries.Items.Add(new BarItem { Value = 137 });
            barSeries.Items.Add(new BarItem { Value = 18 });
            barSeries.Items.Add(new BarItem { Value = 40 });

            var categoryAxis = new CategoryAxis { Position = AxisPosition.Left };
            categoryAxis.Labels.Add("Category A");
            categoryAxis.Labels.Add("Category B");
            categoryAxis.Labels.Add("Category C");
            categoryAxis.Labels.Add("Category D");

            var valueAxis = new LinearAxis { Position = AxisPosition.Bottom, MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0 };

            model.Series.Add(barSeries);

            model.Axes.Add(categoryAxis);
            model.Axes.Add(valueAxis);

            return model;
        }


        private PlotModel CreateBar2Model()
        {
            var model = new PlotModel
            {
                Title = "BarSeries",
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.BottomCenter,
                LegendOrientation = LegendOrientation.Horizontal,
                LegendBorderThickness = 0
            };

            var s1 = new BarSeries { Title = "Series 1", StrokeColor = OxyColors.Black, StrokeThickness = 1 };
            s1.Items.Add(new BarItem { Value = 25 });
            s1.Items.Add(new BarItem { Value = 137 });
            s1.Items.Add(new BarItem { Value = 18 });
            s1.Items.Add(new BarItem { Value = 40 });

            var s2 = new BarSeries { Title = "Series 2", StrokeColor = OxyColors.Black, StrokeThickness = 1 };
            s2.Items.Add(new BarItem { Value = 12 });
            s2.Items.Add(new BarItem { Value = 14 });
            s2.Items.Add(new BarItem { Value = 120 });
            s2.Items.Add(new BarItem { Value = 26 });

            var categoryAxis = new CategoryAxis { Position = AxisPosition.Left };
            categoryAxis.Labels.Add("Category A");
            categoryAxis.Labels.Add("Category B");
            categoryAxis.Labels.Add("Category C");
            categoryAxis.Labels.Add("Category D");

            var valueAxis = new LinearAxis { Position = AxisPosition.Bottom, MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0 };

            model.Series.Add(s1);
            model.Series.Add(s2);

            model.Axes.Add(categoryAxis);
            model.Axes.Add(valueAxis);

            return model;
        }
    }
}

