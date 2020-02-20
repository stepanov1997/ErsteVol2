using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Erste.Util
{
    public class TimetableItem
    {
        public TimeSpan vrijemeOd { get; set; }
        public TimeSpan vrijemeDo { get; set; }
        public string jezik { get; set; }
        public string nivo { get; set; }
        public string dan { get; set; }
        public int? GrupaId { get; set; }
        public termin termin { get; set; }
        public override string ToString()
        {
            return vrijemeOd.ToString(@"hh\:mm") + " - " + vrijemeDo.ToString(@"hh\:mm") + Environment.NewLine + jezik + " " + nivo;
        }
    }
}
