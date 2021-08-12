using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Globalization;
using System.Drawing;
using System.Windows.Data;

namespace SotuvPlatformasi
{
    public class MyBkColorConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DataRow drv = value as DataRow;
           
                string quan = drv["quantity"].ToString();
                if (quan.IndexOf(',') > -1)
                {
                    int index = quan.IndexOf(',');
                    string first = quan.Substring(0, index);
                    string last = quan.Substring(index + 1);
                    quan = first + "." + last;
                }
                double Dquan = double.Parse(quan, CultureInfo.InvariantCulture);
                if (Dquan < 11) //If it's a even number day.
                    return Brushes.Red;
                else if (Dquan < 61)
                    return Brushes.Yellow;
                else
                    return Brushes.Green;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
