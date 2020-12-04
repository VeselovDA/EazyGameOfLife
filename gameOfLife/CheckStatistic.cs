using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gameOfLife
{
    class CheckStatistic
    {
        public int Emptystep { get; set; }
        public int venomStep { get; set; }

        public CheckStatistic(int Emptystep,int venomStep)
        {
            this.Emptystep = Emptystep;
            this.venomStep = venomStep;

        }
    }
}
