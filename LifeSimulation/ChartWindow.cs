using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace DotAndSquares
{
    public partial class ChartWindow : Form
    {
        private LineSeries _series;
        private PlotView _plotView;
        public int TotalEatenSquares { get; set; }

        public ChartWindow()
        {
            InitializeComponent();
            InitializePlotModel();
           
        }

        private void InitializePlotModel()
        {
            _plotView = new PlotView
            {
                Dock = DockStyle.Fill
            };
            Controls.Add(_plotView);

            var plotModel = new PlotModel { Title = "Liczba zjedzonych rzeczy względem czasu" };
            _series = new LineSeries { Title = "Liczba zjedzonych rzeczy" };

            plotModel.Series.Add(_series);
            _plotView.Model = plotModel;
            var xAxis = new LinearAxis { Position = AxisPosition.Bottom, Title = "Czas [s]", Minimum = 0, Maximum = 60 };
            var yAxis = new LinearAxis { Position = AxisPosition.Left, Title = "Liczba zjedzonych rzeczy", Minimum = 0, Maximum = 100 };

            plotModel.Axes.Add(xAxis);
            plotModel.Axes.Add(yAxis);
        }

        // public void AddDataPoint(double time, double itemCount)
        // {
        //     _series.Points.Add(new DataPoint(time, itemCount));
        //     _plotView.InvalidatePlot(true);
        // }
        public void AddDataPoint(int eatenSquares, double gameTime)
        {
            // Dodajemy punkt danych do serii wykresu
            _series.Points.Add(new DataPoint(gameTime, eatenSquares));

            // Aktualizujemy wykres
            _plotView.InvalidatePlot(true);
        }
    }
}