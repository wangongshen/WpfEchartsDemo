using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WpfEchartsDemo.Entity;

namespace WpfEchartsDemo
{
    public class MainWindowViewModel: ViewModelBase
    {

        public MainWindowViewModel()
        {
            t_temperature_and_humidity t_Temperature_And_Humidity = null;
            Random ra = new Random(1);
            Task.Run(() =>
            {
                while (true)
                {
                    t_Temperature_And_Humidity = new t_temperature_and_humidity();
                    t_Temperature_And_Humidity.temperature = ra.Next(60, 100);
                    t_Temperature_And_Humidity.humidity = ra.Next(40, 80);
                    TemperatureHumidity(t_Temperature_And_Humidity);
                    Task.Delay(1500).Wait();
                }
            });
           
        }

        /// <summary>
        /// 温湿度曲线
        /// </summary>
        private TemperatureHumidityCurveEntity temperatureHumidityCurveEntity;
        public TemperatureHumidityCurveEntity TemperatureHumidityCurveEntity
        {
            get { return temperatureHumidityCurveEntity; }
            set
            {
                temperatureHumidityCurveEntity = value;
                NotifyOfPropertyChange(() => TemperatureHumidityCurveEntity);
            }
        }

        public override bool DisposeViewModelBase()
        {
            throw new NotImplementedException();
        }

        string XData = string.Empty;
        string WData = string.Empty;
        string SData = string.Empty;
        /// <summary>
        /// 温湿度曲线
        /// </summary>
        private void TemperatureHumidity(t_temperature_and_humidity data)
        {

            if (data == null)
                return;
            XData = XData + "," + DateTime.Now.ToString("hh:mm:ss");
            WData = WData + "," + data.temperature;
            SData = SData + "," + data.humidity;
            if (Regex.Matches(XData, ",").Count > 20)
            {
                var XDataIndex = XData.IndexOf(',') + 1;
                var WDataIndex = WData.IndexOf(',') + 1;
                var SDataIndex = SData.IndexOf(',') + 1;
                XData = XData.Remove(0, XDataIndex);
                WData = WData.Remove(0, WDataIndex);
                SData = SData.Remove(0, SDataIndex);
            }
            if (XData.IndexOf(',') == 0)
            {
                XData = XData.TrimStart(',');
            }
            if (WData.IndexOf(',') == 0)
            {
                WData = WData.TrimStart(',');
            }
            if (SData.IndexOf(',') == 0)
            {
                SData = SData.TrimStart(',');
            }
            TemperatureHumidityCurveEntity currentTemp = new TemperatureHumidityCurveEntity();
            currentTemp.XData = XData;
            currentTemp.WData = WData;
            currentTemp.SData = SData;
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                TemperatureHumidityCurveEntity = currentTemp;
            });

        }
    }
}
