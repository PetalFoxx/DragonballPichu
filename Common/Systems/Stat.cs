using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonballPichu.Common.Systems
{
    public class Stat
    {
        public string name;
        public float value;
        public float multiplier;
        public Stat(string name, float value) 
        {
            this.name = name;
            setValue(value);
            this.multiplier = 1f;
            roundValue();
        }

        public float getValue() { roundValue(); return (this.value * multiplier); }
        public void setValue(float val) { this.value = val; roundValue(); }


        public void increaseValue(float value) { this.value += value; roundValue(); }
        public void decreaseValue(float value) { this.value -= value; roundValue(); }

        //this would lose precision after the hundredths place, so I must keep it in mind
        public void roundValue()
        {
            this.value = (float)((Math.Round(value * 100)) / 100f);
        }
    }
}
