using CefSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfEchartsDemo.Entity;

namespace WpfEchartsDemo
{
    /// <summary>
    /// Usr_TemperatureHumidityCurve_View.xaml 的交互逻辑
    /// </summary>
    public partial class Usr_TemperatureHumidityCurve_View : UserControl
    {
        bool _webBrowserInit = false;
        public string XData { get; set; }
        public string WData { get; set; }
        public string SData { get; set; }

        public Usr_TemperatureHumidityCurve_View()
        {
            InitializeComponent();
            this.Loaded += Usr_ProportionOfArchivesStorage_View_Loaded;
        }

        private void Usr_ProportionOfArchivesStorage_View_Loaded(object sender, RoutedEventArgs e)
        {
            LoadInit();
        }

        private void LoadInit()
        {
            var url = AppDomain.CurrentDomain.BaseDirectory + "Web\\Charts\\TemperatureHumidityCurve.html";
            this.webBrowser.Address = url;
            this.webBrowser.LoadingStateChanged += WebBrowser_LoadingStateChanged;
        }

        public TemperatureHumidityCurveEntity DataSource
        {
            get { return (TemperatureHumidityCurveEntity)GetValue(DataSourceProperty); }
            set
            {
                SetValue(DataSourceProperty, value);

            }
        }
        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register("DataSource", typeof(TemperatureHumidityCurveEntity), typeof(Usr_TemperatureHumidityCurve_View), new PropertyMetadata(Usr_TemperatureHumidityCurve_View.PropertyChanged));

        private static void PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            Usr_TemperatureHumidityCurve_View usr_BoxPlotChartView = d as Usr_TemperatureHumidityCurve_View;
            TemperatureHumidityCurveEntity boxPlotDataSource = e.NewValue as TemperatureHumidityCurveEntity;
            if (boxPlotDataSource != null)
            {
                usr_BoxPlotChartView.XData = boxPlotDataSource.XData;
                usr_BoxPlotChartView.WData = boxPlotDataSource.WData;
                usr_BoxPlotChartView.SData = boxPlotDataSource.SData;
            }
            if (usr_BoxPlotChartView._webBrowserInit)
                usr_BoxPlotChartView.SetData();
        }
        private void WebBrowser_LoadingStateChanged(object sender, CefSharp.LoadingStateChangedEventArgs e)
        {
            if (e.IsLoading == false)
            {

                LoadData();
                _webBrowserInit = true;
            }
        }
        private void LoadData()
        {
            string size = string.Format("[{0},{1}]", this.ActualWidth - 20, this.ActualHeight - 20);
            this.webBrowser.ExecuteScriptAsync("SetChartSize", size);
            this.webBrowser.ExecuteScriptAsync("InitializeChart", new object[] { XData, WData, SData });
        }

        private void SetData()
        {
            this.webBrowser.ExecuteScriptAsync("SetDataChart", new object[] { XData, WData, SData });
        }
    }
}
