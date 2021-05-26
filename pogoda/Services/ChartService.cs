using System;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using pogoda.Models;

namespace pogoda.Services
{
    class ChartService
    {
        static DateTime date = DateService.CurrentDate;
        static DateTime midnight = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);

        static Weather data = DataService.CurrentData;

        public static PlotModel RenderTemperatureChart()
        {
            var model = new PlotModel { Title = "Temperatura (\u2103)" };

            LinearAxis la = SetLinearAxis();
            la.Minimum = -15;
            la.Maximum = 30;

            model.Axes.Add(la);

            model.Axes.Add(SetDateTimeAxis());

            string valToConvert = data.temperatura.Replace(".", ",");
            double val = Convert.ToDouble(valToConvert);

            FunctionSeries fs = new FunctionSeries();

            /*fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(midnight.AddDays(-8)), 0));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(midnight.AddDays(-7)), 0));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(midnight.AddDays(-6)), 0));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(midnight.AddDays(-5)), 0));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(midnight.AddDays(-4)), 0));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(midnight.AddDays(-3)), 0));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(midnight.AddDays(-2)), 0));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(midnight.AddDays(-1)), 0));*/
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(midnight.AddHours(-5)), val));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(midnight), val));

            model.Series.Add(fs);

            return model;
        }

        public static PlotModel RenderPressureChart()
        {
            var model = new PlotModel { Title = "Ciśnienie (hPa)" };

            LinearAxis la = SetLinearAxis();
            la.Minimum = 960;
            la.Maximum = 1060;

            model.Axes.Add(la);

            model.Axes.Add(SetDateTimeAxis());

            string valToConvert = data.cisnienie.Replace(".", ",");
            double val = Convert.ToDouble(valToConvert);

            FunctionSeries fs = new FunctionSeries();

            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 18)), 978));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 19)), 990));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 20)), 1013));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 21)), 1030));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 22)), 970));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 23)), 1000));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 24)), 998));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(midnight), val));

            model.Series.Add(fs);

            return model;
        }

        public static PlotModel RenderMoistureChart()
        {
            var model = new PlotModel { Title = "Wilgotność (%)" };

            LinearAxis la = SetLinearAxis();
            la.Minimum = 30;
            la.Maximum = 80;

            model.Axes.Add(la);

            model.Axes.Add(SetDateTimeAxis());

            string valToConvert = data.wilgotnosc_wzgledna.Replace(".", ",");
            double val = Convert.ToDouble(valToConvert);

            FunctionSeries fs = new FunctionSeries();

            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 18)), 45));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 19)), 49));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 20)), 50));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 21)), 48));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 22)), 38));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 23)), 55));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 24)), 60));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(midnight), val));

            model.Series.Add(fs);

            return model;
        }

        public static PlotModel RenderWindSpeedChart()
        {
            var model = new PlotModel { Title = "Prędkość wiatru (Beaufort)" };

            LinearAxis la = SetLinearAxis();
            la.MajorStep = 3;
            la.Minimum = 0;
            la.Maximum = 12;

            model.Axes.Add(la);

            model.Axes.Add(SetDateTimeAxis());

            string valToConvert = data.predkosc_wiatru.Replace(".", ",");
            double val = Convert.ToDouble(valToConvert);

            FunctionSeries fs = new FunctionSeries();

            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 18)), 3));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 19)), 3));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 20)), 6));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 21)), 5));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 22)), 5));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 23)), 6));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(new DateTime(2021, 5, 24)), 8));
            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(midnight), val));

            model.Series.Add(fs);

            return model;
        }

        static DateTimeAxis SetDateTimeAxis()
        {
            var today = DateService.CurrentDate;
            var formattedDate = new DateTime(today.Year, today.Month, today.Day, 0, 0, 0);

            var startDate = formattedDate.AddDays(-8);
            var endDate = formattedDate;

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

        static LinearAxis SetLinearAxis()
        {
            var linearAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                IsZoomEnabled = false,
                IsPanEnabled = false,
                MajorGridlineStyle = LineStyle.Dot,
                MajorGridlineColor = OxyColors.LightGray,
                MajorGridlineThickness = 2,
                MajorStep = 10
            };

            return linearAxis;
        }
    }
}
