using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorldGraphicsBehive.FlowerHelper;


//Extra Using Directives
using System.Drawing;
using WorldGraphicsBehive.Properties;

namespace WorldGraphicsBehive
{
    public class Flower
    {
        List<FlowerData> flowersList = new List<FlowerData>();
      
        Random rand = new Random();
        Bitmap imageFlower = WorldGraphicsBehive.Properties.Resources.plant16;
        bool flowerRepositioning = false;
        Point flowerPosition;
        private string keyToUpdateFlowerData;
        bool formInitialization;

        int maxWaitingTimeToDisplayAFlower = 9;
        int flowerWidth = 95;
        int flowerHeight = 100;

        int mainFormHeight;
        int mainFormWidth;
        
        //Flower life span
        int minLifeSpanFlower=30;
        int maxLifeSpanFlower = 100;
 

        int totalFlowersInField =2;
        
        bool flowerReplacementInField = false;

        public void GetMainFormHeightAndWidth(int height, int width)
        {
            mainFormHeight = height;
            mainFormWidth = width;

        }

        public void SetKeyToUpdateData(string keyInformation)
        {
            keyToUpdateFlowerData = keyInformation;
        }

        public void FormInitialization(bool value)
        {
            formInitialization = value;
        }
        public void PaintAllFlowersInWorldField(object sender,PaintEventArgs e)
        {
            try
            {

            
                //At the beginning there are no flowers
                if(flowersList.Count==0)
                {
                    CreateFlower(totalFlowersInField);
                    SetFlowerLifeSpan();
                }
           
                if(flowersList.Count>0)
                {
                    for (int i = 0; i < flowersList.Count; i++)
                    {
                  
                       
                        SetFlowerRandomPosition( i);
                        PaintFlowerInWorldForm(i,(PaintEventArgs)e);
                        RemoveFlowerAtEndOfLifeCycle(FlowerLifeCycle.Dissapeareance);
                    }
                }

               //When one flower dies, it needs to be replaced.
                if(flowersList.Count<totalFlowersInField)
                {
                    CreateFlower(totalFlowersInField);
                    SetFlowerLifeSpan();
                }

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private int SetWaitingTimeToDisplayAFlower()
        {
            int waitingTimeToCreateAFlower = rand.Next(1, maxWaitingTimeToDisplayAFlower);
            return waitingTimeToCreateAFlower;
        }

        private void CreateFlower(int numberOfFlowersInField)
        {
            int startingNumber = 0;
            if(flowersList.Count < numberOfFlowersInField)
            {
                if(flowerReplacementInField==true)
                {
                    startingNumber = flowersList.Count;
                }
               
                for (int i = startingNumber; i < numberOfFlowersInField; i++)
                {
                    FlowerData flowerInfo = new FlowerData();
                    flowerInfo.FlowerDataKeyStatus ="updateLocation";
                    flowerInfo.FlowerWaitingTimeToDisplay = SetWaitingTimeToDisplayAFlower();
                    flowersList.Add(flowerInfo);                    
                }              
            }         
        }

        private List<int>ReScaleFlower(Bitmap originalFile)
        {
            List<int> scaleList = new List<int>();
            var brush = new SolidBrush(Color.Black);

            var image = new Bitmap(originalFile);
            double resultWidth = (double)flowerWidth/image.Width;
            double resultHeight = (double)flowerHeight/image.Height;
            double scale = Math.Min(resultWidth,resultHeight );

            var bmp = new Bitmap((int)flowerWidth, (int)flowerHeight );
            var graph = Graphics.FromImage(bmp);

            var scaleWidth = (int)(image.Width * scale);
            var scaleHeight = (int)(image.Height * scale);

            graph.FillRectangle(brush, new Rectangle(0,0,flowerWidth, flowerHeight));
            graph.DrawImage(image, new Rectangle(((int)flowerWidth-scaleWidth)/2, ((int)flowerHeight-scaleHeight)/2,scaleWidth, scaleHeight));


            scaleList.Add(scaleWidth);
            scaleList.Add(scaleHeight);

            return scaleList;
        }

    //    private void SetFlowerRandomPosition(object sender, EventArgs e, int flowerNumber)
       // private void SetFlowerRandomPosition(int formHeight, int formWidth, int flowerNumber)
        private void SetFlowerRandomPosition(int flowerNumber)
        {
            bool enableFlowerRepositioning;
            //==Form Information BEGIN===
            /*
            int formWidth = 0;
            int formHeight = 0;
        
            var res = sender.GetType();

           
            dynamic control;
            try
            {
                if (typeof(Form1).Equals(res))
                {
                    control = (Control)sender;
                    formWidth = control.Width;
                    formHeight = control.Height;
                }
            }
            catch(Exception ex)
            {
               throw new Exception(ex.Message);
            }
        */
            //==Form Information END======

            //int segment = formHeight / 3;
            int segment = mainFormHeight / 3;
            //Flowers live in the second segment, at the bottom of the soil area
            //int maxHeight = formHeight - flowerHeight - 40;
            //int maxWidth = formWidth - flowerWidth - 10;
            int maxHeight = mainFormHeight - flowerHeight - 40;
            int maxWidth = mainFormWidth - flowerWidth - 10;
            //int flowerLivableArea =(int)Math.Ceiling( segment * 2.0);
            int flowerLivableArea = (int)Math.Ceiling(segment*2.0);
            if (flowerLivableArea >= maxHeight) { flowerLivableArea = 10; maxHeight = 100; }
           
            //=====
            if (formInitialization == true) 
            { 
                enableFlowerRepositioning = true; 
            }
            else
            {
                enableFlowerRepositioning = flowerRepositioning;
            }
            if (flowersList.Count > 0)
            {
                for (int i = 0; i < flowersList.Count; i++)
                {
                    reshuffleAgain:
                    if (i == flowerNumber)
                    {
                        
                        if (flowersList[i].FlowerDataKeyStatus.Equals("updateLocation"))
                        {
                            int positionX = rand.Next(10, maxWidth);
                            int positionY = rand.Next(flowerLivableArea, maxHeight);                      
                            flowersList[i].FlowerLocationX = positionX;
                            flowersList[i].FlowerLocationY = positionY;

                            //=====
                            //Here we verify if our current flower is placed above and on top of previous
                            //drawn flowers in an unnatural way
                            bool imagesOvelap = CompareFlowersLocations(flowerNumber);
                            if (imagesOvelap)
                            {
                                goto reshuffleAgain;
                            }
                            else
                            {
                                flowersList[i].FlowerDataKeyStatus = "lockData";
                            }
                        }
                        //=====
                    }
                    else if(i>flowerNumber)
                    {
                        break;
                    }
                }
            }
           
        }

      
        private void PaintFlowerInWorldForm(int flowerNumber, PaintEventArgs e)
        {
            int valueX = 0;
            int valueY = 0;
            int scaleWidth = 0;
            int scaleHeight = 0;
            if(flowersList.Count>flowerNumber)
            {
                for(int i=0; i<flowersList.Count; i++)
                {
                    if(flowerNumber == i)
                    {
                        valueX = flowersList[i].FlowerLocationX;
                        valueY = flowersList[i].FlowerLocationY;

                        //==
                        //Here we define the life cicle of the flower based on the information
                        //provided regarding the flower's stage
                        SetFlowerLifeCycleStage();

                        if (flowersList[i].FlowerWaitingTimeToDisplay <= flowersList[i].FlowerWaitingTimeCounter)
                        {
                            if (flowersList[i].FlowerImageStage == null)
                            {
                                List<int> scaleValues = ReScaleFlower(imageFlower);
                                scaleWidth = scaleValues[0];
                                scaleHeight = scaleValues[1];
                                e.Graphics.DrawImage(imageFlower, valueX, valueY, flowerWidth, flowerHeight);
                                // e.Graphics.DrawImage(imageFlower, valueX, valueY, scaleWidth, scaleHeight);

                            }
                            else
                            {
                                //List<int> scaleValues = ReScaleFlower(flowersList[i].FlowerImageStage);
                                //scaleWidth = scaleValues[0];
                                //scaleHeight = scaleValues[1];
                                // e.Graphics.DrawImage(flowersList[i].FlowerImageStage, valueX, valueY, scaleWidth, scaleHeight);
                                e.Graphics.DrawImage(flowersList[i].FlowerImageStage, valueX, valueY, flowerWidth, flowerHeight);
                            }
                            flowersList[i].FlowerWaitingTimeToDisplay = 0;
                            flowersList[i].FlowerWaitingTimeCounter = 0;
                        }
                        

                       //==
                      

                        //e.Graphics.DrawImage(imageFlower,valueX,valueY,flowerWidth,flowerHeight);
                        using (Pen pen = new Pen(Color.Red))
                        {
                            e.Graphics.DrawRectangle(pen, valueX, valueY, flowerWidth + 3, flowerHeight + 3);
                            var brush = new SolidBrush(Color.Black);

                            e.Graphics.FillEllipse(brush,new Rectangle(flowersList[i].FlowerLibationPointX,flowersList[i].FlowerLibationPointY,5,5));

                        }
                        
                    }
                    else if(i>flowerNumber)
                    {
                        break;
                    }
                }
            }      
        }


        public bool CompareFlowersLocations(int comparingFlowerIndex)
        {
            bool imagesOverlap = false;
            
            if(flowersList.Count()>comparingFlowerIndex)
            {
                //====
                
                var comparingFlower = flowersList[comparingFlowerIndex];
                int vectorCompareX = comparingFlower.FlowerLocationX;
                int vectorCompareY = comparingFlower.FlowerLocationY;
                Point aComparingFlower = new Point() {X=vectorCompareX,Y=vectorCompareY  };
                Point bComparingFlower = new Point() { X = vectorCompareX, Y = vectorCompareY +flowerHeight};
                Point cComparingFlower = new Point() { X = vectorCompareX+flowerWidth, Y = vectorCompareY+flowerHeight };
                Point dComparingFlower = new Point() { X = vectorCompareX+flowerWidth, Y = vectorCompareY };
                //====


                Point aPointImageBase = new Point();
                Point bPointImageBase = new Point();
                Point cPointImageBase = new Point();
                Point dPointImageBase = new Point();

                for(int i=0; i<flowersList.Count();i++)
                {
                    int vectorX = flowersList[i].FlowerLocationX;
                    int vectorY = flowersList[i].FlowerLocationY;
                   
                   
                    aPointImageBase.X = vectorX;
                    aPointImageBase.Y = vectorY;

                    bPointImageBase.X = vectorX;
                    bPointImageBase.Y = vectorY+flowerHeight;

                  
                    cPointImageBase.X = vectorX+flowerWidth;
                    cPointImageBase.Y = vectorY+flowerHeight;
                    dPointImageBase.X = vectorX+flowerWidth;
                    dPointImageBase.Y = vectorY;

                    if(i<comparingFlowerIndex)
                    {
                        if(aPointImageBase.Y >aComparingFlower.Y)
                        {
                            if ((aPointImageBase.X <= cComparingFlower.X && aPointImageBase.Y <= cComparingFlower.Y)
                            ||
                            (dPointImageBase.X >= bComparingFlower.X && dPointImageBase.Y <= bComparingFlower.Y)
                            )
                            {
                                imagesOverlap = true;
                            }
                        }

                        /*****This logics makes no flower to overlap
                        if( (aPointImageBase.X < cComparingFlower.X && aPointImageBase.Y < cComparingFlower.Y)
                            &&
                            (dPointImageBase.Y < bComparingFlower.Y && dPointImageBase.X > bComparingFlower.X)
                            )
                        {
                            imagesOverlap = true;
                        }
                        *******************************************/
                       
                    }
                    else
                    {
                        break;
                    }

                }

                
            }
            return imagesOverlap;
        }

        public void SetFlowerLifeSpan()
        {
            foreach(var flower in flowersList)
            {
                if(flower.FlowerLifeSpan==0)
                {
                    int life = rand.Next(minLifeSpanFlower,maxLifeSpanFlower);
                    flower.FlowerLifeSpan = life;
                    
                }
            }
        }

        public void RunFlowerLifeSpan()
        {
            int cycle = 1;

            foreach(var flower in flowersList)
            {
                flower.FlowerLifeSpanUsage = flower.FlowerLifeSpanUsage + cycle; 
            }

        }

        int counter = 0;
        public void RunFlowerWaitingTimeToDisplayCounter()
        {
            foreach (var flower in flowersList)
            {
                flower.FlowerWaitingTimeCounter = flower.FlowerWaitingTimeCounter + counter;
            }
            counter++;
        }

        public void SetFlowerLifeCycleStage()
        {
           // Enum lifeCycleStage = FlowerLifeCycle.Birth1 ;
            double lifeStage;
            foreach(var flower in flowersList)
            {
                lifeStage = ( (double)flower.FlowerLifeSpanUsage/flower.FlowerLifeSpan);
          
                //00% Birth1,
               if(lifeStage>=0 && lifeStage<0.08)
               {
                  flower.FlowerStage=  FlowerLifeCycle.Birth1;
                  flower.FlowerImageStage = Resources.plant01;
                  //Make libation Point Zero
                  flower.FlowerLibationPointX = 0;
                  flower.FlowerLibationPointY = 0;
               }
                //08% EarlyStage1,
               if (lifeStage > 0.08 && lifeStage < 0.1)
               {
                   flower.FlowerStage = FlowerLifeCycle.EarlyStage1;
                   flower.FlowerImageStage = Resources.plant02;
                   //Make libation Point Zero
                   flower.FlowerLibationPointX = 0;
                   flower.FlowerLibationPointY = 0;
               }
               //10% EarlyStage2,
               if (lifeStage > 0.1 && lifeStage < 0.13)
               {
                   flower.FlowerStage = FlowerLifeCycle.EarlyStage2;
                   flower.FlowerImageStage = Resources.plant03;
                   //Make libation Point Zero
                   flower.FlowerLibationPointX = 0;
                   flower.FlowerLibationPointY = 0;
               }
                //13% EarlyStage3,
               if (lifeStage > 0.13 && lifeStage < 0.2)
               {
                   flower.FlowerStage = FlowerLifeCycle.EarlyStage3;
                   flower.FlowerImageStage = Resources.plant04;
                   //Make libation Point Zero
                   flower.FlowerLibationPointX = 0;
                   flower.FlowerLibationPointY = 0;
               }
                //20% EarlyStage4,
               if (lifeStage > 0.2 && lifeStage < 0.25)
               {
                   flower.FlowerStage = FlowerLifeCycle.EarlyStage4;
                   flower.FlowerImageStage = Resources.plant05;
                   //Make libation Point Zero
                   flower.FlowerLibationPointX = 0;
                   flower.FlowerLibationPointY = 0;
               }
                //25% Growth1,
               if (lifeStage > 0.25 && lifeStage < 0.3)
               {
                   flower.FlowerStage = FlowerLifeCycle.Growth1;
                   flower.FlowerImageStage = Resources.plant06;
                   //Make libation Point Zero
                   flower.FlowerLibationPointX = 0;
                   flower.FlowerLibationPointY = 0;
               }
                //30% Growth2,
               if (lifeStage > 0.3 && lifeStage < 0.34)
               {
                   flower.FlowerStage = FlowerLifeCycle.Growth2;
                   flower.FlowerImageStage = Resources.plant07;
                   //Make libation Point Zero
                   flower.FlowerLibationPointX = 0;
                   flower.FlowerLibationPointY = 0;
               }
                //34% Growth3,
               if (lifeStage > 0.34 && lifeStage < 0.38)
               {
                   flower.FlowerStage = FlowerLifeCycle.Growth3;
                   flower.FlowerImageStage = Resources.plant08;
                   //Make libation Point Zero
                   flower.FlowerLibationPointX = 0;
                   flower.FlowerLibationPointY = 0;
               }
                //38% Growth4,
               if (lifeStage > 0.38 && lifeStage < 0.45)
               {
                   
                    flower.FlowerStage = FlowerLifeCycle.Growth4;
                    flower.FlowerImageStage = Resources.plant09;
                    //Make libation Point Zero
                    flower.FlowerLibationPointX = 0;
                    flower.FlowerLibationPointY = 0;
               }
                //45% MidStage1,
               if (lifeStage > 0.45 && lifeStage < 0.5)
               {
                   flower.FlowerStage = FlowerLifeCycle.MidStage1;
                   flower.FlowerImageStage = Resources.plant10;
                   //Make libation Point Zero
                   flower.FlowerLibationPointX = 0;
                   flower.FlowerLibationPointY = 0;
               }
                //50% MidStage2,
               if (lifeStage > 0.5 && lifeStage < 0.55)
               {
                   flower.FlowerStage = FlowerLifeCycle.MidStage2;
                   flower.FlowerImageStage = Resources.plant11;
                   //Make libation Point Zero
                   flower.FlowerLibationPointX = 0;
                   flower.FlowerLibationPointY = 0;
               }
                //55% MidStage3,
               if (lifeStage > 0.55 && lifeStage < 0.59)
               {
                   flower.FlowerStage = FlowerLifeCycle.MidStage3;
                   flower.FlowerImageStage = Resources.plant12;
                   //Add libation Point
                   flower.FlowerLibationPointX  = flower.FlowerLocationX + 47;
                   flower.FlowerLibationPointY = flower.FlowerLocationY + 20;
                  
               }
                //59% Blossom1,
               if (lifeStage > 0.59 && lifeStage < 0.63)
               {
                   flower.FlowerStage = FlowerLifeCycle.Blossom1;
                   flower.FlowerImageStage = Resources.plant13;
                   //Add libation Point
                   flower.FlowerLibationPointX = flower.FlowerLocationX + 48;
                   flower.FlowerLibationPointY = flower.FlowerLocationY + 14;
               }
                //63% Blossom2,
               if (lifeStage > 0.63 && lifeStage < 0.68)
               {
                   flower.FlowerStage = FlowerLifeCycle.Blossom2;
                   flower.FlowerImageStage = Resources.plant14;
                   //Add libation Point
                   flower.FlowerLibationPointX = flower.FlowerLocationX + 45;
                   flower.FlowerLibationPointY = flower.FlowerLocationY + 11;
               }
                //68% Blossom3,
               if (lifeStage > 0.68 && lifeStage < 0.75)
               {
                   flower.FlowerStage = FlowerLifeCycle.Blossom3;
                   flower.FlowerImageStage = Resources.plant15;
                   //Add libation Point
                   flower.FlowerLibationPointX = flower.FlowerLocationX + 45;
                   flower.FlowerLibationPointY = flower.FlowerLocationY + 14;
               }
                //75% Blossom4,
               if (lifeStage > 0.75 && lifeStage < 0.82)
               {
                   flower.FlowerStage = FlowerLifeCycle.Blossom4;
                   flower.FlowerImageStage = Resources.plant16;
                   //Add libation Point
                   flower.FlowerLibationPointX = flower.FlowerLocationX + 45;
                   flower.FlowerLibationPointY = flower.FlowerLocationY + 14;
               }
               //82% Maturity1,
               if (lifeStage > 0.82 && lifeStage < 0.88)
               {
                   flower.FlowerStage = FlowerLifeCycle.Maturity1;
                   flower.FlowerImageStage = Resources.plant17;
                   //Add libation Point
                   flower.FlowerLibationPointX = flower.FlowerLocationX + 43;
                   flower.FlowerLibationPointY = flower.FlowerLocationY + 26;
               }
                //88% Maturity2,
               if (lifeStage > 0.88 && lifeStage < 0.93)
               {
                   flower.FlowerStage = FlowerLifeCycle.Maturity2;
                   flower.FlowerImageStage = Resources.plant18;
                   //Add libation Point
                   flower.FlowerLibationPointX = flower.FlowerLocationX + 43;
                   flower.FlowerLibationPointY = flower.FlowerLocationY + 25;
               }
                //93% LateStage1,    
               if (lifeStage > 0.93 && lifeStage < 1)
               {
                   flower.FlowerStage = FlowerLifeCycle.LateStage1;
                   flower.FlowerImageStage = Resources.plant19;
                   //Make libation Point Zero
                   flower.FlowerLibationPointX = 0;
                   flower.FlowerLibationPointY = 0;
               }
                //100% Death1
               if (lifeStage > 1 && lifeStage <1.03)
               {
                   flower.FlowerStage = FlowerLifeCycle.Death1;
                   flower.FlowerImageStage = Resources.plant20;
                   //Make libation Point Zero
                   flower.FlowerLibationPointX = 0;
                   flower.FlowerLibationPointY = 0;
               }
               //103% Dissapeareance
               if (lifeStage > 1.03)
               {
                   flower.FlowerStage = FlowerLifeCycle.Dissapeareance;
                   flower.FlowerImageStage = Resources.plant20;
               }

            }
            //return lifeCycleStage;
        }

        //==
        public void RemoveFlowerAtEndOfLifeCycle(Enum flowerStage)
        {
            foreach(var flower in flowersList)
            {
                //if(flower.FlowerStage.ToString().Equals("Death1"))
                if(flower.FlowerStage.Equals(flowerStage))
                {
                    flowersList.Remove(flower);                  
                    break;
                }
            }
        }



        //==



    }
}
