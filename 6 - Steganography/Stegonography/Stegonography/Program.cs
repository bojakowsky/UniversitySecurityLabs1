using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AForge;
using AForge.Video;
using AForge.Video.FFMPEG;
using AForge.Video.DirectShow;
using System.Drawing;
using System.Drawing.Imaging;
using AForge.Video.VFW;

namespace Stegonography
{
    class Program
    {
        //Algorithm
        //First frame - the frame on which all informations about decoding will be stored
        //The algorithm starts with 4 cordinates ((0+x)/2,0), (0,(0+y)/2), (x,(0+y)/2), ((0+x)/2, y)
        //We store cordinate data on bitmap pixel, the next cordinates are stored on the half of the sector between neighbour cordinates (creating another rectangle)
        public class Stegonography
        {
            public string TextToEncode;
            public int NumberOfFrames;
            public int Width;
            public int Height;
            public int FPS;
            public HashSet<AForge.Point> DataCordinates;
            public List<int> FramesToStoreCordinates;

            private AForge.Point ReturnPointInstance(int x, int y)
            {
                AForge.Point point = new AForge.Point();
                point.X = x;
                point.Y = y;
                return point;
            }

            private int CalculateHowManyCoordinatesCanBeSavedPerFrame()
            {
                int i = 0;
                int width = Width;
                int height = Height;
                do
                {
                    width /= 2;
                    height /= 2;
                    i++;
                } while ((height != 1 || width != 1));
                return i * i;
            }

            private int CalculateTotalFramesNeeded()
            {
                return (int)Math.Ceiling((double)TextToEncode.Length / (double)CalculateHowManyCoordinatesCanBeSavedPerFrame());
            }

            public Stegonography(string textToEncode, int numberOfFrames, int width, int height, int fps)
            {
                TextToEncode = textToEncode;
                NumberOfFrames = numberOfFrames;
                Width = width - 1;
                Height = height - 1;
                FPS = fps;

                FillAllDataCordinates();
                ChooseFramesToStoreCordinates();

            }

            private void FindGCDPair(out int a, out int b, int begin)
            {
                int pom;
                a = begin * (begin - 3) % Width;
                b = begin * (begin + 7) % Height;

                bool go = true;
                int gcdA;
                int gcdB;

                while (go)
                {
                    a = (a - 1) % Width;
                    b = (b + 1) % Height;

                    gcdA = a;
                    gcdB = b;
                    while (gcdB != 0)
                    {
                        pom = gcdB;
                        gcdB = gcdA % gcdB;
                        gcdA = pom;
                    }
                    if (gcdA == 2) go = false;
                }
            }

            private void ChooseFramesToStoreCordinates()
            {
                int framesNeededToSaveCoordinates = CalculateTotalFramesNeeded();
                HashSet<int> BuforFramesToStoreCordinates = new HashSet<int>();
                int i = 7;
                while (BuforFramesToStoreCordinates.Count != framesNeededToSaveCoordinates)
                {
                    int a;
                    int b;
                    FindGCDPair(out a, out b, i);

                    BuforFramesToStoreCordinates.Add(((Width - a) * (Height + b) * i) % NumberOfFrames);
                    i++;
                }
                FramesToStoreCordinates = BuforFramesToStoreCordinates.ToList();
                FramesToStoreCordinates.Sort();
            }

            private void FillAllDataCordinates()
            {
                //Lets call the rectangle sides left: L, top: T, right: R, down: D
                AForge.Point T = ReturnPointInstance(Width / 2, 0);
                AForge.Point R = ReturnPointInstance(Width, Height / 2);
                AForge.Point D = ReturnPointInstance(Width / 2, Height);
                AForge.Point L = ReturnPointInstance(0, Height / 2);
                DataCordinates = new HashSet<AForge.Point>();
                DataCordinates.Add(T);
                DataCordinates.Add(R);
                DataCordinates.Add(D);
                DataCordinates.Add(L);

                int numberOfCordinates = CalculateHowManyCoordinatesCanBeSavedPerFrame();
                for (int i = 0; i < (numberOfCordinates) - 4; i += 4)
                {

                    T = ReturnPointInstance((int)(DataCordinates.ElementAt(i).X + DataCordinates.ElementAt(i + 1).X) / 2,
                        (int)(DataCordinates.ElementAt(i).Y + DataCordinates.ElementAt(i + 1).Y) / 2);

                    R = ReturnPointInstance((int)(DataCordinates.ElementAt(i + 1).X + DataCordinates.ElementAt(i + 2).X) / 2,
                        (int)(DataCordinates.ElementAt(i + 1).Y + DataCordinates.ElementAt(i + 2).Y) / 2);

                    D = ReturnPointInstance((int)(DataCordinates.ElementAt(i + 2).X + DataCordinates.ElementAt(i + 3).X) / 2,
                        (int)(DataCordinates.ElementAt(i + 2).Y + DataCordinates.ElementAt(i + 3).Y) / 2);

                    L = ReturnPointInstance((int)(DataCordinates.ElementAt(i + 3).X + DataCordinates.ElementAt(i).X) / 2,
                        (int)(DataCordinates.ElementAt(i + 3).Y + DataCordinates.ElementAt(i).Y) / 2);

                    DataCordinates.Add(T);
                    DataCordinates.Add(R);
                    DataCordinates.Add(D);
                    DataCordinates.Add(L);
                }
            }
        }

        static void Main(string[] args)
        {
            #region TESTS
            //VideoFileReader reader = new VideoFileReader();
            //// open video file
            ////reader.Open("Helicopter_DivXHT_ASP.divx");
            //reader.Open("Audioslave_-_Be_Yourself.avi");
            //// check some of its attributes
            //Console.WriteLine("Video title: " + "Audioslave_ - _Be_Yourself.avi");
            //Console.WriteLine("width:  " + reader.Width);
            //Console.WriteLine("height: " + reader.Height);
            //Console.WriteLine("fps:    " + reader.FrameRate);
            //Console.WriteLine("codec:  " + reader.CodecName);
            //Console.WriteLine("frames: " + reader.FrameCount);

            ////var writer = new VideoFileWriter();
            //AVIWriter writer = new AVIWriter();
            //writer.FrameRate = reader.FrameRate;
            //writer.Open("test.avi", reader.Width, reader.Height);

            ////writer.Open("test.avi", reader.Width, reader.Height, reader.FrameRate, VideoCodec.MPEG4, 1000);
            //for (int x = 0; x < 100; x++)
            //{
            //    Bitmap videoFrame = reader.ReadVideoFrame();
            //    if (x < 10)
            //        for (int i = 0; i < reader.Width; i++)
            //        {
            //            for (int j = 0; j < reader.Height; j++)
            //            {
            //                videoFrame.SetPixel(i, j, Color.FromArgb(222, 222, 222));
            //                Color p = videoFrame.GetPixel(i, j);
            //            }
            //        }

            //    using (Bitmap oldBmp = new Bitmap(videoFrame))
            //    using (Bitmap newBmp = new Bitmap(oldBmp))
            //    using (Bitmap targetBmp = newBmp.Clone(new Rectangle(0, 0, newBmp.Width, newBmp.Height), PixelFormat.Format24bppRgb))
            //    {
            //        writer.AddFrame(targetBmp);
            //    }
            //    //writer.WriteVideoFrame(videoFrame);
            //    //writer.WriteVideoFrame(videoFrame.Clone(new Rectangle(0, 0, reader.Width, reader.Height), PixelFormat.Format24bppRgb));
            //    videoFrame.Dispose();
            //}
            //writer.Close();
            //reader.Close();

            //reader = new VideoFileReader();
            //reader.Open("test.avi");
            //Console.WriteLine("\nVideo title:  " + "test.avi");
            //Console.WriteLine("width:  " + reader.Width);
            //Console.WriteLine("height: " + reader.Height);
            //Console.WriteLine("fps:    " + reader.FrameRate);
            //Console.WriteLine("codec:  " + reader.CodecName);
            //Console.WriteLine("frames: " + reader.FrameCount);

            //for (int i = 0; i < reader.FrameCount; i++)
            //{
            //    Bitmap videoFrame = reader.ReadVideoFrame();
            //    Color c = videoFrame.GetPixel(0, 0);
            //}
            #endregion

            VideoFileReader reader = new VideoFileReader();
            // open video file
            reader.Open("Audioslave_-_Be_Yourself.avi");
            // check some of its attributes
            Console.WriteLine("Video title: " + "Audioslave_ - _Be_Yourself.avi");
            Console.WriteLine("width:  " + reader.Width);
            Console.WriteLine("height: " + reader.Height);
            Console.WriteLine("fps:    " + reader.FrameRate);
            Console.WriteLine("codec:  " + reader.CodecName);
            Console.WriteLine("frames: " + reader.FrameCount);

            string encode =
                @"Lorem ipsum dolor sit amet quam. Nam eget gravida tempor, dolor gravida sollicitudin, urna nec nibh malesuada euismod, nulla in vehicula vitae, dapibus aliquam cursus aliquet. Aliquam id lorem. Suspendisse potenti. In fermentum. Proin lacus. Aenean ut nonummy rutrum. In justo non enim fringilla orci. Donec nisl nulla non augue. Vestibulum ante sit amet, elementum quis, interdum rhoncus, dolor sit amet pede. Duis ac erat. Duis luctus, quam nibh consectetuer ac, rhoncus eget, dapibus aliquam purus. Praesent quis orci. Phasellus ipsum primis in faucibus scelerisque. Vestibulum ante volutpat at, mattis sem. Integer adipiscing. Vestibulum vitae dui pulvinar ligula, et lacus sagittis lacus. Phasellus facilisis in, adipiscing felis cursus wisi nibh ac turpis gravida vitae, ultricies vitae, ligula. Fusce posuere in, mauris. Nullam eleifend lacinia, diam eros, sagittis lorem. Etiam leo ac turpis vitae ornare pulvinar sem luctus et leo. Sed ultricies in, pulvinar nulla sit amet sapien faucibus a, ultricies viverra accumsan, dolor urna, eu tortor. Suspendisse nec turpis egestas. Praesent elit tincidunt risus auctor scelerisque, quam congue risus. Aliquam feugiat sagittis ac, mollis vel, eros. Quisque placerat velit ornare ultrices posuere eu, nisl. Fusce mollis, purus lacinia aliquet enim sodales in, purus. Phasellus nec scelerisque sem. Sed ligula non felis. Curabitur magna auctor mattis. Pellentesque mattis sed, vestibulum sapien, lacinia neque. Etiam urna eu wisi. Aenean non dui. Nullam justo vulputate nunc. Praesent magna ut diam mauris, volutpat commodo. Cras sit amet tellus tortor, fermentum ut, ligula. Curabitur magna arcu, eget lacus nulla id nonummy laoreet, est congue ac, tincidunt in, purus. Proin cursus lectus, luctus diam. Nullam risus sit amet, volutpat non, dolor. Nullam laoreet, nulla id odio nec tempor diam eu lobortis urna. Vestibulum leo. Quisque pretium eget, dolor. Ut nonummy. Phasellus id arcu. Cras vitae metus. Curabitur quis nibh lacus, congue sit amet cursus dui non felis. In urna. Sed laoreet vel, quam. Mauris neque. Nulla ligula placerat porttitor. Class aptent taciti sociosqu ad litora torquent per inceptos hymenaeos. Nunc massa nisl, sollicitudin augue id sollicitudin quis, tincidunt eget, cursus sapien. Proin non urna. Suspendisse luctus id, lacinia erat. Fusce nonummy sagittis. Curabitur quis nulla facilisis faucibus sem laoreet hendrerit ac, suscipit vitae, ipsum. Fusce nonummy condimentum sit amet, libero. Aliquam ut tortor. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per conubia nostra, per inceptos hymenaeos. Fusce tristique, sollicitudin.";
            Stegonography s = new Stegonography(encode, (int)reader.FrameCount, reader.Width, reader.Height, reader.FrameRate);
            Console.WriteLine("Text length: " + encode.Length);
            int frame = 0;

            Color oldData;
            Color newData;
            int rgb = 0;
            int letterPtr = 0;

            SaveVideo newVideo = new SaveVideo(s);
            for (int i = 0; i < s.NumberOfFrames - 1; i++)
            {
                Bitmap videoFrame = reader.ReadVideoFrame();
                if (s.FramesToStoreCordinates[frame] == i)
                {

                    for (int cord = 0; cord < s.DataCordinates.Count(); cord++)
                    {
                        int x = (int)s.DataCordinates.ElementAt(cord).X;
                        int y = (int)s.DataCordinates.ElementAt(cord).Y;

                        oldData = videoFrame.GetPixel(x, y);

                        if (rgb == 0)
                            newData = Color.FromArgb(oldData.R, oldData.G, Convert.ToByte(s.TextToEncode[letterPtr]));
                        else if (rgb == 1)
                            newData = Color.FromArgb(oldData.R, Convert.ToByte(s.TextToEncode[letterPtr]), oldData.B);
                        else
                            newData = Color.FromArgb(Convert.ToByte(s.TextToEncode[letterPtr]), oldData.G, oldData.B);
                        rgb = (rgb + 1) % 3;
                        letterPtr++;
                        videoFrame.SetPixel(x, y, newData);

                    }

                    frame++;
                }
                newVideo.AddFrame(videoFrame);
                videoFrame.Dispose();
            }
            reader.Close();
            newVideo.ExecuteSaving();

            Decoder(s);
        }

        public static void Decoder(Stegonography s)
        {
            var reader = new VideoFileReader();
            reader.Open("test.avi");
            Console.WriteLine("\nVideo title:  " + "test.avi");
            Console.WriteLine("width:  " + reader.Width);
            Console.WriteLine("height: " + reader.Height);
            Console.WriteLine("fps:    " + reader.FrameRate);
            Console.WriteLine("codec:  " + reader.CodecName);
            Console.WriteLine("frames: " + reader.FrameCount);

            string decoded = "";
            int frame = 0;

            int rgb = 0;
            for (int i = 0; i < reader.FrameCount; i++)
            {
                Bitmap videoFrame = reader.ReadVideoFrame();
                if (s.FramesToStoreCordinates[frame] == i)
                {

                    for (int cord = 0; cord < s.DataCordinates.Count(); cord++)
                    {
                        int x = (int)s.DataCordinates.ElementAt(cord).X;
                        int y = (int)s.DataCordinates.ElementAt(cord).Y;

                        var color = videoFrame.GetPixel(x, y);
                        var data = "";
                        if (rgb == 0)
                            data = Convert.ToChar(color.B).ToString();
                        else if (rgb == 1)
                            data = Convert.ToChar(color.G).ToString();
                        else
                            data = Convert.ToChar(color.R).ToString();
                        rgb = (rgb + 1) % 3;

                        decoded += data;
                    }

                    frame++;
                }
                videoFrame.Dispose();
            }
            Console.WriteLine("\n" + decoded);
            reader.Close();

            Console.ReadKey();
        }

        public class SaveVideo
        {
            public void AddFrame(Bitmap bitmap)
            {
                //writer.WriteVideoFrame(bitmap);
                writer.AddFrame(bitmap);
            }

            Stegonography stegonography;

            public SaveVideo(Stegonography steg)
            {
                stegonography = steg;
                //Saving into new AVI
                // create instance of video writer

                //Uncommented till VideoFileWriter is compressing the data!
                //writer = new VideoFileWriter();
                //writer.Open("test.avi", stegonography.Width + 1, stegonography.Height + 1, stegonography.FPS, VideoCodec.MPEG4);

                //Empty constructor is creating writer with DIB codec which means no compression
                writer = new AVIWriter();
                writer.FrameRate = steg.FPS;
                writer.Open("test.avi", steg.Width+1, steg.Height+1);

            }
            //VideoFileWriter writer;
            AVIWriter writer;
            public void ExecuteSaving()
            {
                writer.Close();
            }
        }
    }
}
