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

            var viewTime = FindViewById<PlotView>(Resource.Id.plot_view_time);
            viewTime.Model = CreatePlotTimeModel();

            var viewLine = FindViewById<PlotView>(Resource.Id.plot_view_line);
            viewLine.Model = CreatePlotLineModel();

            var viewBar = FindViewById<PlotView>(Resource.Id.plot_view_bar);
            viewBar.Model = CreateBarModel();

            var viewBar2 = FindViewById<PlotView>(Resource.Id.plot_view_bar2);
            viewBar2.Model = CreateBar2Model();
        }


        private PlotModel CreatePlotTimeModel()
        {
            var model = new PlotModel { Title = "Time Axes Chart Demo" };

            model.Axes.Add(new LinearAxis()
            {
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.None,

                MajorStep = 200.0,
                MinorStep = 50.0,

                Unit = "W"
            });

            model.Axes.Add(new DateTimeAxis()
            {
                Position = AxisPosition.Bottom,
                StringFormat = "HH:mm",

                //IntervalType = DateTimeIntervalType.Hours,
                //MinorIntervalType = DateTimeIntervalType.Minutes,


                //IntervalLength = DateTimeAxis.ToDouble(DateTime.Parse("12:00:00")),
                //IntervalLength = 12,


                //MajorGridlineThickness = DateTimeAxis.ToDouble(DateTime.Parse("00:30:00")),


                //MajorStep = DateTimeAxis.ToDouble(DateTime.Parse("02:00:00")),
                //MajorStep = 0.08,
                ////MajorStep = 0.25,
                //MinorStep = DateTimeAxis.ToDouble(DateTime.Parse("00:30:00")),
                //MinorStep = 0.25,

                Minimum = DateTimeAxis.ToDouble(DateTime.Parse("00:00:00")),
                Maximum = DateTimeAxis.ToDouble(DateTime.Parse("23:59:59")),

                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.None,

                //PositionAtZeroCrossing = false,

                IsZoomEnabled = false,
                IsPanEnabled = false,

                Unit = "Hours"
            });

            var cs = new LineSeries();
            for (int i = 0; i < 24; i++) //generate test values
            {
                string timeStr = Convert.ToString(i) + ":00:00";
                DateTime time = DateTime.Parse(timeStr);
                var dp = new DataPoint(DateTimeAxis.ToDouble(time), i * i);
                cs.Points.Add(dp);
            }

            model.Series.Add(cs);

            return model;
        }


        private PlotModel CreatePlotLineModel()
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
                Title = "Column Chart Demo",
                LegendPlacement = LegendPlacement.Outside,
                LegendPosition = LegendPosition.BottomCenter,
                LegendOrientation = LegendOrientation.Horizontal,
                LegendBorderThickness = 0
            };

            var categoryAxis = new CategoryAxis { Position = AxisPosition.Bottom, IsZoomEnabled = false, IsPanEnabled = false };
            categoryAxis.Labels.Add("Category A");
            categoryAxis.Labels.Add("Category B");
            categoryAxis.Labels.Add("Category C");
            categoryAxis.Labels.Add("Category D");

            var valueAxis = new LinearAxis { Position = AxisPosition.Left, MinimumPadding = 0, MaximumPadding = 0.06, AbsoluteMinimum = 0, IsZoomEnabled = false, IsPanEnabled = false };

            model.Axes.Add(categoryAxis);
            model.Axes.Add(valueAxis);

            var columnSeries = new ColumnSeries { Title = "Series 1", StrokeColor = OxyColors.Black, StrokeThickness = 0 };
            columnSeries.Items.Add(new ColumnItem { Value = 25 });
            columnSeries.Items.Add(new ColumnItem { Value = 137 });
            columnSeries.Items.Add(new ColumnItem { Value = 18 });
            columnSeries.Items.Add(new ColumnItem { Value = 40 });

            model.Series.Add(columnSeries);

            return model;
        }


        private PlotModel CreateBar2Model()
        {
            var model = new PlotModel
            {
                Title = "Bar Chart Demo",
                LegendPlacement = LegendPlacement.Inside,
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

