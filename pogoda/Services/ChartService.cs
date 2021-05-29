using System;
using System.Collections.Generic;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using pogoda.Models;

namespace pogoda.Services
{
    class ChartService
    {
        static Weather? data;
        static List<StationMeasurement>? measurements;

        public static void SetMeasurements()
        {
            measurements = DatabaseService.Get();
        }

        public static PlotModel RenderTemperatureChart()
        {
            data = DataService.CurrentData;

            var model = new PlotModel { Title = "Temperatura (\u2103)" };

            SetLinearAxis(ref model, -15, 30);

            model.Axes.Add(SetDateTimeAxis());

            if (measurements != null)
            {
                foreach (var measurement in measurements)
                {
                    if (measurement.station == data.stacja)
                    {
                        Console.WriteLine(measurement.station);
                        model.Series.Add(AddFunctionSeries(measurement.temperature));
                    }
                }
            }

            return model;
        }

        public static PlotModel RenderPressureChart()
        {
            data = DataService.CurrentData;

            var model = new PlotModel { Title = "Ciśnienie (hPa)" };

            SetLinearAxis(ref model, 960, 1060);

            model.Axes.Add(SetDateTimeAxis());

            if (measurements != null)
            {
                foreach (var measurement in measurements)
                {
                    if (measurement.station == data.stacja)
                    {
                        model.Series.Add(AddFunctionSeries(measurement.pressure));
                    }
                }
            }

            return model;
        }

        public static PlotModel RenderMoistureChart()
        {
            data = DataService.CurrentData;

            var model = new PlotModel { Title = "Wilgotność (%)" };

            SetLinearAxis(ref model, 0, 100, 20);

            model.Axes.Add(SetDateTimeAxis());

            if (measurements != null)
            {
                foreach (var measurement in measurements)
                {
                    if (measurement.station == data.stacja)
                    {
                        model.Series.Add(AddFunctionSeries(measurement.moisture));
                    }
                }
            }

            return model;
        }

        public static PlotModel RenderWindSpeedChart()
        {
            data = DataService.CurrentData;

            var model = new PlotModel { Title = "Prędkość wiatru (Beaufort)" };

            SetLinearAxis(ref model, 0, 12, 3);

            model.Axes.Add(SetDateTimeAxis());

            if (measurements != null)
            {
                foreach (var measurement in measurements)
                {
                    if (measurement.station == data.stacja)
                    {
                        model.Series.Add(AddFunctionSeries(measurement.windSpeed));
                    }
                }
            }

            return model;
        }

        static FunctionSeries AddFunctionSeries(List<Measurement> data)
        {
            DateTime date = Convert.ToDateTime(data[0].day);
            FunctionSeries fs = new FunctionSeries();

            fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(date.AddHours(-3)), data[0].value));
            for (int i = 0; i < data.Count; i++)
            {
                date = Convert.ToDateTime(data[i].day);
                fs.Points.Add(new DataPoint(DateTimeAxis.ToDouble(date), data[i].value));
            }

            return fs;
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

        static void SetLinearAxis(ref PlotModel model, int min, int max, int step = 10)
        {
            var linearAxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                IsZoomEnabled = false,
                IsPanEnabled = false,
                MajorGridlineStyle = LineStyle.Dot,
                MajorGridlineColor = OxyColors.LightGray,
                MajorGridlineThickness = 2,
                MajorStep = step,
                Minimum = min,
                Maximum = max
            };

            model.Axes.Add(linearAxis);
        }
    }
}
