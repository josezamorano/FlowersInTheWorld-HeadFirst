using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WorldGraphicsBehive
{
    public partial class Form1 : Form
    {
        World world = new World();     
        Size formSize = new Size();
        Flower flower = new Flower();
        bool formInitialization;
        public Form1()
        {
            InitializeComponent();
            formSize.Width = this.Width;
            formSize.Height = this.Height;
            //==Disabled functionality BEGIN==
            /*
            List<Size> worldSizeList = world.SizeOfWorldForm(formSize);
       
            if(worldSizeList.Count>1)
            {
                this.MinimumSize = worldSizeList[0];
                this.MaximumSize = worldSizeList[1];
            }
             */ 
            //==Disabled functionality END=====
            this.MinimumSize = new Size(866, 518);
            this.MaximumSize = new Size(866, 518);

            string keyInfo = "updateLocation";
            flower.SetKeyToUpdateData(keyInfo);
            formInitialization = true;
            flower.FormInitialization(formInitialization);
            flower.GetMainFormHeightAndWidth(formSize.Height, formSize.Width);

            FlowerTimer.Enabled = true;
            FlowerTimer.Interval = 100;
            FlowerTimer.Start();
            LabelFormHeight.Text = this.Height.ToString();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //We Assign the Paint Event to Method so 
            //It can be transferred to another Object
         
            world.PaintLandscapeInWorldForm(e);
            flower.PaintAllFlowersInWorldField(sender, e);
            
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
              
           

           world.ReSizeLandscapeInWorldForm(sender);
           string keyInfo = "updateLocation";
           flower.SetKeyToUpdateData(keyInfo);
           formInitialization = false;
           flower.FormInitialization(formInitialization);
            //========
           //The new width and Height of the form is passed to the variables to be
           //Redirected to the other objects along the methods
           Control control = (Control)sender;
           int height = control.Height;
           int width = control.Width;

           flower.GetMainFormHeightAndWidth(height,width);
            //========
          
           Refresh();

           LabelFormHeight.Text = this.Height.ToString();
        }

        private void FlowerTimer_Tick(object sender, EventArgs e)
        {
            flower.RunFlowerLifeSpan();
            flower.RunFlowerWaitingTimeToDisplayCounter();
            Refresh();  
         
         
        }
    }
}
