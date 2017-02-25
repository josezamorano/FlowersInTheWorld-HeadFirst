using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Windows.Forms;

namespace WorldGraphicsBehive
{
    public class World
    {
        Bitmap natureImage;
        Bitmap hiveInNature;
           
       
        int widthForm;
        int heightForm;
        Size minimumSizeForm = new Size();
        Size maximumSizeForm = new Size();
        List<Size> formSizesList = new List<Size>();

        bool basicDrawing = true; 
       
        int targetImageWidth = (int)Math.Floor(852 * 1.5);
        int targetImageHeight = (int)Math.Floor(480 * 1.5);


        public List<Size> SizeOfWorldForm(Size FormSize)
        {
            if (basicDrawing)
            {
                //Variable Size
                heightForm = FormSize.Height;
                widthForm = FormSize.Width;             
            }
           
            if(!basicDrawing)
            {
                //Fixed Size
                minimumSizeForm = new Size(866, 518);
                formSizesList.Add(minimumSizeForm);
                maximumSizeForm = new System.Drawing.Size(866, 518);
                formSizesList.Add(maximumSizeForm);
            }

            return formSizesList;
        }
        public void PaintLandscapeInWorldForm(PaintEventArgs e)
        {
            
            //Cartoon Images
            natureImage = WorldGraphicsBehive.Properties.Resources._1;
            hiveInNature = WorldGraphicsBehive.Properties.Resources.Hive__outside_;
         
            if (basicDrawing)
            {
                e.Graphics.FillRectangle(Brushes.LightBlue, 0, 0, widthForm, heightForm / 2);
                e.Graphics.FillEllipse(Brushes.Yellow, new Rectangle(50, 35, 70, 70));
                e.Graphics.FillRectangle(Brushes.Green, 0, heightForm / 2, widthForm, heightForm / 2);
                e.Graphics.DrawLine(new System.Drawing.Pen(Color.DarkOliveGreen, 5), new Point(683, 0), new Point(683, 30));
                e.Graphics.DrawImage(hiveInNature, new Rectangle(647, 29, 75, 75));           
            }
        
            else
            {
                e.Graphics.DrawImage(natureImage, new Rectangle(0, 0, natureImage.Width, natureImage.Height));               
                e.Graphics.DrawLine(new System.Drawing.Pen(Color.DarkOliveGreen, 5), new Point(740, 78), new Point(765, 86));                           
                e.Graphics.DrawImage(hiveInNature, new Rectangle(730, 84, 75, 75));
            }          
        }


        public void ReSizeLandscapeInWorldForm(object sender)
        {       
            //=================  
            if (basicDrawing)
            {
                Control control = (Control)sender;
                widthForm = (control.Width > targetImageWidth) ? targetImageWidth : control.Width;
                heightForm = (control.Height > targetImageHeight) ? targetImageHeight : control.Height;
            }
            //================= 
        }
        
    }
}
