using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoeusV2.Models
{
    public class Theme
    {
        public Color BackColor { get; set; }
        public Color ForeColor { get; set; }
        public Color BoxColor { get; set; }

        public Theme(Color backColor, Color foreColor, Color boxColor)
        {
            BackColor = backColor;
            ForeColor = foreColor;
            BoxColor = boxColor;
        }
    }
}