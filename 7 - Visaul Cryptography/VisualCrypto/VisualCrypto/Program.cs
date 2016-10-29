using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisualCrypto
{
    class Program
    {
        public enum Bricks
        {
            type1_1,
            type1_2,

            type2_1,
            type2_2,

            type3_1,
            type3_2,
        }

        public static void SetBrickPixelsByType(Bricks type, Bitmap bmp, int x, int y)
        {
            switch (type)
            {
                //BW
                //WB
                case Bricks.type1_1:
                    bmp.SetPixel(x, y, black);
                    bmp.SetPixel(x + 1, y, white);
                    bmp.SetPixel(x, y + 1, white);
                    bmp.SetPixel(x + 1, y + 1, black);
                    break;

                //WB
                //BW
                case Bricks.type1_2:
                    bmp.SetPixel(x, y, white);
                    bmp.SetPixel(x + 1, y, black);
                    bmp.SetPixel(x, y + 1, black);
                    bmp.SetPixel(x + 1, y + 1, white);
                    break;

                //WB
                //WB
                case Bricks.type2_1:
                    bmp.SetPixel(x, y, white);
                    bmp.SetPixel(x + 1, y, black);
                    bmp.SetPixel(x, y + 1, white);
                    bmp.SetPixel(x + 1, y + 1, black);
                    break;

                //BW
                //BW
                case Bricks.type2_2:
                    bmp.SetPixel(x, y, black);
                    bmp.SetPixel(x + 1, y, white);
                    bmp.SetPixel(x, y + 1, black);
                    bmp.SetPixel(x + 1, y + 1, white);
                    break;

                //WW
                //BB
                case Bricks.type3_1:
                    bmp.SetPixel(x, y, white);
                    bmp.SetPixel(x + 1, y, white);
                    bmp.SetPixel(x, y + 1, black);
                    bmp.SetPixel(x + 1, y + 1, black);
                    break;

                //BB
                //WW
                case Bricks.type3_2:
                    bmp.SetPixel(x, y, black);
                    bmp.SetPixel(x + 1, y, black);
                    bmp.SetPixel(x, y + 1, white);
                    bmp.SetPixel(x + 1, y + 1, white);
                    break;


            }

        }
        public static int st2, st3, st4, st5, st6;
        public static int st1 = st2 = st3 = st4 = st5 = st6 = 0;

        public static Color black = Color.FromArgb(0, 0, 0);
        public static Color white = Color.FromArgb(255, 255, 255);
        public static void SetBricksByColor(Color color, Bitmap bmp1, Bitmap bmp2, int x, int y)
        {
            x *= 2;
            y *= 2;
            var rand = new Random(x*y);
            int random = rand.Next(0, 1200);
            if (random < 200) st1++;
            else if (random < 400) st2++;
            else if (random < 600) st3++;
            else if (random < 800) st4++;
            else if (random < 1000) st5++;
            else st6++;

            if (color == black)
            {
                if (random < 200)
                {
                    SetBrickPixelsByType(Bricks.type1_1, bmp1, x, y);
                    SetBrickPixelsByType(Bricks.type1_2, bmp2, x, y);
                }
                else if (random < 400)
                {
                    SetBrickPixelsByType(Bricks.type1_2, bmp1, x, y);
                    SetBrickPixelsByType(Bricks.type1_1, bmp2, x, y);
                }
                else if (random < 600)
                {
                    SetBrickPixelsByType(Bricks.type2_1, bmp1, x, y);
                    SetBrickPixelsByType(Bricks.type2_2, bmp2, x, y);
                }
                else if (random < 800)
                {
                    SetBrickPixelsByType(Bricks.type2_2, bmp1, x, y);
                    SetBrickPixelsByType(Bricks.type2_1, bmp2, x, y);
                }
                else if (random < 1000)
                {
                    SetBrickPixelsByType(Bricks.type3_1, bmp1, x, y);
                    SetBrickPixelsByType(Bricks.type3_2, bmp2, x, y);
                }
                else //if (random <= 1200)
                {
                    SetBrickPixelsByType(Bricks.type3_2, bmp1, x, y);
                    SetBrickPixelsByType(Bricks.type3_1, bmp2, x, y);
                }
            }
            else //if (color == white)
            {
                if (random < 200)
                {
                    SetBrickPixelsByType(Bricks.type1_1, bmp1, x, y);
                    SetBrickPixelsByType(Bricks.type1_1, bmp2, x, y);
                }
                else if (random < 400)
                {
                    SetBrickPixelsByType(Bricks.type1_2, bmp1, x, y);
                    SetBrickPixelsByType(Bricks.type1_2, bmp2, x, y);
                }
                else if (random < 600)
                {
                    SetBrickPixelsByType(Bricks.type2_1, bmp1, x, y);
                    SetBrickPixelsByType(Bricks.type2_1, bmp2, x, y);
                }
                else if (random < 800)
                {
                    SetBrickPixelsByType(Bricks.type2_2, bmp1, x, y);
                    SetBrickPixelsByType(Bricks.type2_2, bmp2, x, y);
                }
                else if (random < 1000)
                {
                    SetBrickPixelsByType(Bricks.type3_1, bmp1, x, y);
                    SetBrickPixelsByType(Bricks.type3_1, bmp2, x, y);
                }
                else //if (random <= 1200)
                {
                    SetBrickPixelsByType(Bricks.type3_2, bmp1, x, y);
                    SetBrickPixelsByType(Bricks.type3_2, bmp2, x, y);
                }
            }
        }


        static void Main(string[] args)
        {
            Bitmap bmpBase = new Bitmap("base1.png");
            Bitmap bmpPartOne = new Bitmap(bmpBase.Width * 2, bmpBase.Height * 2);
            Bitmap bmpPartTwo = new Bitmap(bmpBase.Width * 2, bmpBase.Height * 2);
            Bitmap bmpOneAndTwoMerged = new Bitmap(bmpBase.Width * 2, bmpBase.Height * 2);
            for (int i = 0; i < bmpBase.Width; i++)
                for (int j = 0; j < bmpBase.Height; j++)
                    SetBricksByColor(bmpBase.GetPixel(i, j), bmpPartOne, bmpPartTwo, i, j);

            bmpPartOne.Save(@"1.png", ImageFormat.Png);
            bmpPartTwo.Save(@"2.png", ImageFormat.Png);

            for (int i = 0; i < bmpBase.Width * 2; i++)
            {
                for (int j = 0; j < bmpBase.Height * 2; j++)
                {
                    if (bmpPartOne.GetPixel(i, j) == black || bmpPartTwo.GetPixel(i, j) == black) bmpOneAndTwoMerged.SetPixel(i, j, black);
                    else bmpOneAndTwoMerged.SetPixel(i, j, white);
                }
            }
            bmpOneAndTwoMerged.Save(@"merged.png", ImageFormat.Png);

        }
    }
}
