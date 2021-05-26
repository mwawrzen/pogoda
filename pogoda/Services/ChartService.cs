using System;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace pogoda.Services
{
    class ChartService
    {
        public static PlotModel RenderTemperatureChart()
        {
            var model = new PlotModel { Title = "Temperatura (\u2103)" };

            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                IsZoomEnabled = false,
                IsPanEnabled = false,
                MajorGridlineStyle = LineStyle.Dot,
                MajorGridlineColor = OxyColors.LightGray,
                MajorGridlineThickness = 2,
                MajorStep = 10,
                Minimum = -15,
                Maximum = 30
            });

            model.Axes.Add(SetDateTimeAxis());

            FunctionSeries fs = new FunctionSeries();
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 18)), 8));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 19)), 10));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 20)), 6));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 21)), 8));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 22)), 14));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 23)), 16));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 24)), 20));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Today), 17));

            model.Series.Add(fs);

            return model;
        }

        public static PlotModel RenderPressureChart()
        {
            var model = new PlotModel { Title = "Ciśnienie (hPa)" };

            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                IsZoomEnabled = false,
                IsPanEnabled = false,
                MajorGridlineStyle = LineStyle.Dot,
                MajorGridlineColor = OxyColors.LightGray,
                MajorGridlineThickness = 2,
                MajorStep = 10,
                Minimum = 960,
                Maximum = 1060
            });

            model.Axes.Add(SetDateTimeAxis());

            FunctionSeries fs = new FunctionSeries();
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 18)), 978));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 19)), 990));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 20)), 1013));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 21)), 1030));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 22)), 970));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 23)), 1000));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 24)), 998));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Today), 1013));

            model.Series.Add(fs);

            return model;
        }

        public static PlotModel RenderMoistureChart()
        {
            var model = new PlotModel { Title = "Wilgotność (%)" };

            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                IsZoomEnabled = false,
                IsPanEnabled = false,
                MajorGridlineStyle = LineStyle.Dot,
                MajorGridlineColor = OxyColors.LightGray,
                MajorGridlineThickness = 2,
                MajorStep = 10,
                Minimum = 30,
                Maximum = 80
            });

            model.Axes.Add(SetDateTimeAxis());

            FunctionSeries fs = new FunctionSeries();
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 18)), 45));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 19)), 49));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 20)), 50));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 21)), 48));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 22)), 38));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 23)), 55));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 24)), 60));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Today), 64));

            model.Series.Add(fs);

            return model;
        }

        public static PlotModel RenderWindSpeedChart()
        {
            var model = new PlotModel { Title = "Prędkość wiatru (Beaufort)" };

            model.Axes.Add(new LinearAxis
            {
                Position = AxisPosition.Left,
                IsZoomEnabled = false,
                IsPanEnabled = false,
                MajorGridlineStyle = LineStyle.Dot,
                MajorGridlineColor = OxyColors.LightGray,
                MajorGridlineThickness = 2,
                MajorStep = 3,
                Minimum = 0,
                Maximum = 12
            });

            model.Axes.Add(SetDateTimeAxis());

            FunctionSeries fs = new FunctionSeries();
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 18)), 3));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 19)), 3));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 20)), 6));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 21)), 5));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 22)), 5));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 23)), 6));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 24)), 8));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(DateTime.Today), 5));

            model.Series.Add(fs);

            return model;
        }

        static DateTimeAxis SetDateTimeAxis()
        {
            var startDate = DateTime.Today.AddDays(-8);
            var endDate = DateTime.Today;

            var minValue = DateTimeAxis.ToDouble(startDate);
            var maxValue = DateTimeAxis.ToDouble(endDate);

            var dateTimeAxis = new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                IsZoomEnabled = false,
                IsPanEnabled = false,
                MajorGridlineStyle = LineStyle.Dot,
                MajorGridlineColor = OxyColors.LightGray,
                MajorGridlineThickness = 2,
                MajorStep = 1,
                MinorStep = 1,
                Minimum = minValue,
                Maximum = maxValue,
                StringFormat = "dd MMMM"
            };

            return dateTimeAxis;
        }
    }
}
