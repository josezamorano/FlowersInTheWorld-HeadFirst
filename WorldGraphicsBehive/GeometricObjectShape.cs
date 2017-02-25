using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Extra Using Directives;
using System.Drawing;

namespace WorldGraphicsBehive
{
    public class GeometricObjectShape
    {
        public Bitmap imageBitmapObject { get; set; }
        public Pen penObject { get; set; }
        public string GeometricObjectName { get; set; }
        public Brush GeometricObjectColor { get; set; }
        public int GeometricObjectLocationX { get; set; }
        public int GeometricObjectLocationY { get; set; }

        public int GeometricObjectLocationX1 { get; set; }
        public int GeometricObjectLocationY1 { get; set; }

        public int GeometricObjectWidth { get; set; }
        public int GeometricObjectHeight { get; set; }




    }
}
