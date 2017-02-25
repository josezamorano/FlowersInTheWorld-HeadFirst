using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Extra using Directives
using System.Drawing;

namespace WorldGraphicsBehive.FlowerHelper
{
    public class FlowerData
    {
        public int flowerId { get; set; }

        public string FlowerDataKeyStatus { get; set; }
        public int FlowerLocationX { get; set; }
        public int FlowerLocationY { get; set; }

        public int FlowerLifeSpan { get; set; }
        public double FlowerLifeSpanUsage { get; set; }
        //Life cycle
        public Enum FlowerStage{get; set;}

        public Bitmap FlowerImageStage { get; set; }

        public int FlowerWaitingTimeToDisplay { get; set; }
        public int FlowerWaitingTimeCounter { get; set; }
       

        //Libation point
        public int FlowerLibationPointX { get; set; }
        public int FlowerLibationPointY { get; set; }
    }
}
