using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YorgiControls.Model
{
    internal class Decade
    {
        public int FromYear {get; set;}
        public int ToYear { get; set; }

        public override string ToString()
        {
            return string.Format("{0}-\n{1}", this.FromYear, this.ToYear);
        }

    }
}
