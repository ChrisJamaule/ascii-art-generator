using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asciify                      
{
    public class BitmapAscii
    {
        //Declared private fields - protection levels.
        private int _kernelHeight;
        private int _kernelWidth;
        private int _pixelX;
        private int _pixelY;
        private double normalGray;
        private StringBuilder asciiImage = new StringBuilder();
        private bool go = true;
   


        //Constructor
        public BitmapAscii(int kernelHeight, int kernelWidth)
        {
            _kernelHeight = kernelHeight;
            _kernelWidth = kernelWidth;
        }

   

        //Method accepts bitmap and returns string(builder)
        internal string Asciitize(Bitmap bmp)
        {
            double normalized;
            string ascii;
            List<Color> colors = new List<Color>();

            if (_kernelHeight == 1 && _kernelWidth == 1)
            {
                    //Travesing through each pixel in bitmap image
                    for (int y = 0; y < bmp.Height; y++)
                    {
                        for (int x = 0; x < bmp.Width; x++)
                        {
                            //Get the color of each pixel.
                            Color col = bmp.GetPixel(x, y);

                            normalized = AveragePixel(col);

                            ascii = GrayToString(normalized);

                            asciiImage.Append(ascii); //Append string to stringbuilder.

                        }//Inner_Loop

                        asciiImage.Append("\r\n");

                    }//End_Loop                                                                                       

                    return asciiImage.ToString(); //ascii image
            }
            else
            {
                
                while (go)
                {

                    //DEFINING KERNEL SIZE
                    for (int y = _pixelY; y < _pixelY + _kernelHeight; y++)
                    {
                        for (int x = _pixelX; x < _pixelX + _kernelWidth; x++)
                        {
                            if (_pixelX + _kernelWidth < bmp.Width && _pixelY + _kernelHeight < bmp.Height)
                            {
                                Color col = bmp.GetPixel(x, y);

                                colors.Add(col); //Add pixel color to List 
                            }
                        }
                    }//EndLoop

                    if (colors.Count == (_kernelHeight * _kernelWidth))
                    {
                        normalized = AverageColor(colors);

                        ascii = GrayToString(normalized);

                        asciiImage.Append(ascii);

                        colors.Clear();
                    }

                    if (_pixelX + _kernelWidth < bmp.Width)
                    {
                        _pixelX += _kernelWidth;
                    }
                    else
                    {
                        if (_pixelY + _kernelHeight < bmp.Height)
                        {
                            _pixelY += _kernelHeight;

                            _pixelX = 0;

                            asciiImage.Append("\r\n");
                        }
                        else
                        {
                            go = false;
                        }
                    }


                }//End_While

                return asciiImage.ToString(); //returns entire ascii image

            }//End_If/Else

        }//End_Asciitize


        //Method accepts pixel color and returns normalized greyscale value/Overloaded method
        public double AveragePixel(Color color)
        {
            double average = (color.R + color.G + color.B) / 3;

            if(average > 255) //value can only be 0-255
            {
                average = average / 3;
            }
            else { normalGray = average / 255; }
            
            return normalGray;
        }

        //Method accepts list of objs and returns normalized greyscale value.
        public double AverageColor(List<Color> colors)
        {
            double colorValue = 0;  

            for(int i = 0; i < colors.Count; i++)
            {
                colorValue = (colors[i].R + colors[i].G + colors[i].B) / 3;

                normalGray += colorValue / 255;
            }

            normalGray = normalGray / colors.Count;

            return normalGray;
        }

        //Method accepts 3 ints and returns normalized greyscale value.
        public double AveragePixel(int R, int G, int B)
        {
            double average = (R + G + B) / 3;

            if(average > 255)
            {
                average = average / 3;
            }
            else { normalGray = average / 255; }

            return normalGray;
        }

        //Method accepts normalized greyscale value and returns ascii symbol.
        public string GrayToString(double gray)
        {
            string asciiSym = "";

            if(gray >= 0.0 && gray <= 0.10)
            {
                asciiSym = "@";
            }
            else if(gray >= 0.11 && gray <= 0.20)
            {
                asciiSym = "&";
            }
            else if(gray >= 0.21 && gray <= 0.30)
            {
                 asciiSym = "%";
            }
            else if (gray >= 0.31 && gray <= 0.40)
            {
                asciiSym = "#";
            }
            else if (gray >= 0.41 && gray <= 0.50)
            {
                asciiSym = "+";
            }
            else if (gray >= 0.51 && gray <= 0.60)
            {
                asciiSym = "=";
            }
            else if (gray >= 0.61 && gray <= 0.70)
            {
                asciiSym = "*";
            }
            else if (gray >= 0.71 && gray <= 0.80)
            {
                asciiSym = "-";
            }
            else if (gray >= 0.81 && gray <= 0.90)
            {
                asciiSym = ".";
            }
            else if (gray >= 0.91 && gray <= 1.0)
            {
                asciiSym = " ";
            }

            return asciiSym;
        }

        

    }//End_Class

}//End_Namespace
