using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPF_Client
{
    public class Notice
    {
        public async void Notice_by_Background_Color(Brush background)
        {
            string alert_color = "#FFF74646";
            var ex_brush = background;
            var converter = new System.Windows.Media.BrushConverter();
            background = (Brush)converter.ConvertFromString(alert_color)!;
            await Task.Delay(700);
            background = ex_brush;
        }
    }
}
