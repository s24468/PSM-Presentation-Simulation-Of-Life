using System.Windows.Forms;
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.WindowsForms;

namespace DotAndSquares
{
    public partial class ChartWindow : Form
    {
        private LineSeries _series;
        private PlotView _plotView;

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
        }

        public void AddDataPoint(double time, int itemCount)
        {
            _series.Points.Add(new DataPoint(time, itemCount));
            _plotView.InvalidatePlot(true);
        }
    }
}