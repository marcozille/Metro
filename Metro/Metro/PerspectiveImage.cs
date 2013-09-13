using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using System.Collections.Generic;

namespace Perspective
{
    public struct FloatPointF
    {
        public float X;
        public float Y;
    }

    public struct LookUpValues
    {
        public int Laenge;
        public int Pos;
    }

    public enum WaveModus
    {
        Horizontal,
        Vertical,
        HorizontalWithStrech,
        VerticalWithStrech
    }

    public enum PerspectiveModus
    {
        HorizontalDown,
        HorizontalUp,
        VerticalUp,
        VerticalDown,
        HorizontalDownMode2,
        HorizontalUpMode2,
        VerticalUpMode2,
        VerticalDownMode2
    }

    public class ImmagineProspettiva
    {
        public static Bitmap PerspectiveMode2(Bitmap bitmap, PerspectiveModus Modus, float ShortSideLength, bool AntiAlias, bool AntiAliasOuterEdges, float pts_1_2_Addition, float pts_0_3_Addition)
        {
            Bitmap img = null;
            FloatPointF[,] fp = null;
            Point[,] pt = null;

            try
            {
                img = (Bitmap)bitmap.Clone();

                bool bAlias = AntiAlias;

                int tb7 = (int)Modus;

                bool b4 = false;
                bool b5 = false;

                if (tb7 == 5)
                {
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    tb7 = 6;
                    b5 = true;
                }

                if (tb7 == 4)
                {
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    tb7 = 7;
                    b4 = true;
                }

                fp = new FloatPointF[img.Width, img.Height];
                pt = new Point[img.Width, img.Height];

                if (tb7 == 7)
                {

                    double Faktor = (((double)(img.Width - (double)ShortSideLength) / 2.0D) / (double)img.Height);

                    int w = img.Width;
                    int h = img.Height;

                    //Parallel.For(0, h, y =>
                    for (int y = 0; y < h; y++)
                    {
                        double s1 = ((double)w / 2.0) - (y * Faktor);

                        for (int x = 0; x < w; x++)
                        {
                            double s2 = ((double)w / 2.0) - (double)x;
                            double fa = ((s1 == 0) ? 0 : (s2 / s1));

                            double newX = ((double)x - (y * Faktor * fa));

                            if ((newX >= 0) && (newX < w))
                                fp[x, y].X = (float)newX;
                            else
                                fp[x, y].X = float.MinValue;

                            fp[x, y].Y = y;
                            pt[x, y].X = (int)fp[x, y].X;
                            pt[x, y].Y = y;
                        }
                    }//);
                }

                if (tb7 == 6)
                {
                    double Faktor = (((double)(img.Width - (double)ShortSideLength) / 2.0D) / (double)img.Height);

                    int w = img.Width;
                    int h = img.Height;
                    int h1 = img.Height - 1;

                    //Parallel.For(0, h, j =>
                    for (int j = 0; j < h; j++)
                    {
                        int y = h1 - j;
                        double s1 = ((double)w / 2.0) - (j * Faktor);

                        for (int x = 0; x < w; x++)
                        {
                            double s2 = ((double)w / 2.0) - (double)x;
                            double fa = ((s1 == 0) ? 0 : (s2 / s1));

                            double newX = ((double)x - (j * Faktor * fa));

                            if ((newX >= 0) && (newX < w))
                                fp[x, y].X = (float)newX;
                            else
                                fp[x, y].X = float.MinValue;

                            fp[x, y].Y = y;
                            pt[x, y].X = (int)fp[x, y].X;
                            pt[x, y].Y = y;
                        }
                    }//);
                }

                if (bAlias)
                    OffsetFiAntiAlias(img, fp);
                else
                    OffsetFiAbs(img, pt);

                if (AntiAliasOuterEdges)
                {
                    Bitmap btmp = null;
                    try
                    {
                        btmp = new Bitmap((Bitmap)bitmap.Clone());
                        SetSolid(btmp);
                        btmp = PerspectiveMode2(btmp, Modus, ShortSideLength, bAlias, false, 0F, 0F);

                        if (btmp != null)
                        {
                            if (b4 || b5)
                            {
                                btmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                            }

                            //return img;
                            Bitmap img2 = null;
                            Graphics g = null;
                            TextureBrush tbrush = null;

                            try
                            {
                                img2 = new Bitmap(img.Width, img.Height);
                                g = Graphics.FromImage(img2);
                                g.SmoothingMode = SmoothingMode.AntiAlias;
                                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                tbrush = new TextureBrush(img);
                                PointF[] pts = GetTrapez(btmp);

                                pts[1] = new PointF(pts[1].X - pts_1_2_Addition, pts[1].Y);
                                pts[2] = new PointF(pts[2].X - pts_1_2_Addition, pts[2].Y);
                                pts[0] = new PointF(pts[0].X + pts_0_3_Addition, pts[0].Y);
                                pts[3] = new PointF(pts[3].X + pts_0_3_Addition, pts[3].Y);

                                g.FillPolygon(tbrush, pts);
                                g.Dispose();
                                tbrush.Dispose();
                                img.Dispose();
                                img = img2;
                                btmp.Dispose();
                            }
                            catch
                            {
                                if (img2 != null)
                                    img2.Dispose();
                                if (g != null)
                                    g.Dispose();
                                if (tbrush != null)
                                    tbrush.Dispose();

                                throw new Exception("bla");
                            }
                        }
                        else
                        {
                            throw new Exception("bla");
                        }
                    }
                    catch
                    {
                        if (btmp != null)
                            btmp.Dispose();
                    }
                }

                if (b4 || b5)
                {
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }

                return img;
            }
            catch
            {
                if (img != null)
                    img.Dispose();
                if (fp != null)
                    fp = null;
                if (pt != null)
                    pt = null;

                return null;
            }
        }

        public static Bitmap PerspectiveMode2(Bitmap bitmap, PerspectiveModus Modus, float ShortSideLength, float LongSideLength, bool AntiAlias, bool AntiAliasOuterEdges, float pts_1_2_Addition, float pts_0_3_Addition, PointF[] Trapez)
        {
            Bitmap img = null;
            Bitmap imgNew = null;
            FloatPointF[,] fp = null;
            Point[,] pt = null;
            Graphics gx = null;

            try
            {
                img = (Bitmap)bitmap.Clone();

                bool bAlias = AntiAlias;

                int tb7 = (int)Modus;

                bool b4 = false;
                bool b5 = false;

                if (tb7 == 5)
                {
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    tb7 = 6;
                    b5 = true;
                }

                if (tb7 == 4)
                {
                    img.RotateFlip(RotateFlipType.Rotate90FlipNone);
                    tb7 = 7;
                    b4 = true;
                }

                imgNew = new Bitmap((int)Math.Ceiling(LongSideLength), img.Height);

                fp = new FloatPointF[imgNew.Width, imgNew.Height];
                pt = new Point[imgNew.Width, imgNew.Height];

                double Add = (LongSideLength - (double)img.Width) / 2.0D;

                gx = Graphics.FromImage(imgNew);
                gx.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gx.DrawImage(img, 0, 0, imgNew.Width, imgNew.Height); //(img, (float)Add, 0F);
                gx.Dispose();

                if (img != null)
                    img.Dispose();
                img = imgNew;

                if (tb7 == 7)
                    imgNew.RotateFlip(RotateFlipType.RotateNoneFlipY);

                if (tb7 == 7)
                {
                    double Faktor = (((double)(imgNew.Width - (double)ShortSideLength) / 2.0D) / (double)imgNew.Height);

                    int w = imgNew.Width;
                    int h = imgNew.Height;

                    //Parallel.For(0, h, y =>
                    for (int y = 0; y < h; y++)
                    {
                        double s1 = ((double)w / 2.0) - (y * Faktor);

                        for (int x = 0; x < w; x++)
                        {
                            double s2 = ((double)w / 2.0) - (double)x;
                            double fa = ((s1 == 0) ? 0 : (s2 / s1));

                            double newX = ((double)x - (y * Faktor * fa));

                            if ((newX >= 0) && (newX < w))
                                fp[x, y].X = (float)newX;
                            else
                                fp[x, y].X = float.MinValue;

                            fp[x, y].Y = y;
                            pt[x, y].X = (int)fp[x, y].X;
                            pt[x, y].Y = y;
                        }
                    }//);
                }

                if (tb7 == 6)
                {
                    double Faktor = (((double)(imgNew.Width - (double)ShortSideLength) / 2.0D) / (double)imgNew.Height);

                    int w = imgNew.Width;
                    int h = imgNew.Height;
                    int h1 = imgNew.Height - 1;

                    //Parallel.For(0, h, j =>
                    for (int j = 0; j < h; j++)
                    {
                        int y = h1 - j;
                        double s1 = ((double)w / 2.0) - (j * Faktor);

                        for (int x = 0; x < w; x++)
                        {
                            double s2 = ((double)w / 2.0) - (double)x;
                            double fa = ((s1 == 0) ? 0 : (s2 / s1));

                            double newX = ((double)x - (j * Faktor * fa));

                            if ((newX >= 0) && (newX < w))
                                fp[x, y].X = (float)newX;
                            else
                                fp[x, y].X = float.MinValue;

                            fp[x, y].Y = y;
                            pt[x, y].X = (int)fp[x, y].X;
                            pt[x, y].Y = y;
                        }
                    }//);
                }

                if (bAlias)
                    OffsetFiAntiAlias(imgNew, fp);
                else
                    OffsetFiAbs(imgNew, pt);

                if (tb7 == 7)
                    imgNew.RotateFlip(RotateFlipType.RotateNoneFlipY);

                if (AntiAliasOuterEdges)
                {
                    Bitmap img2 = null;
                    Graphics g = null;
                    TextureBrush tbrush = null;

                    try
                    {
                        img2 = new Bitmap(img.Width, img.Height);
                        g = Graphics.FromImage(img2);
                        g.SmoothingMode = SmoothingMode.AntiAlias;
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        tbrush = new TextureBrush(img);
                        PointF[] pts = Trapez;

                        pts[1] = new PointF(pts[1].X - pts_1_2_Addition, pts[1].Y);
                        pts[2] = new PointF(pts[2].X - pts_1_2_Addition, pts[2].Y);
                        pts[0] = new PointF(pts[0].X + pts_0_3_Addition, pts[0].Y);
                        pts[3] = new PointF(pts[3].X + pts_0_3_Addition, pts[3].Y);

                        g.FillPolygon(tbrush, pts);
                        g.Dispose();
                        tbrush.Dispose();
                        img.Dispose();
                        img = img2;
                    }
                    catch
                    {
                        if (img2 != null)
                            img2.Dispose();
                        if (g != null)
                            g.Dispose();
                        if (tbrush != null)
                            tbrush.Dispose();
                    }
                }

                if (b4 || b5)
                {
                    img.RotateFlip(RotateFlipType.Rotate270FlipNone);
                }

                return img;
            }
            catch
            {
                if (img != null)
                    img.Dispose();
                if (imgNew != null)
                    imgNew.Dispose();
                if (fp != null)
                    fp = null;
                if (pt != null)
                    pt = null;
                if (gx != null)
                    gx.Dispose();

                return null;
            }
        }

        private static PointF[] GetTrapez(Bitmap b)
        {
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height),
                               ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            int stride = bmData.Stride;

            System.IntPtr Scan0 = bmData.Scan0;

            PointF[] pts = new PointF[4];

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                bool ready = false;

                for (int y = 0; y < b.Height; y++)
                {
                    bool mode2 = false;

                    if (ready)
                        break;

                    for (int x = 0; x < b.Width; x++)
                    {
                        if ((p[3] != (byte)0) && (mode2 == false))
                        {
                            pts[0] = new PointF(x, y);

                            mode2 = true;
                        }

                        if ((p[3] == (byte)0) && (mode2))
                        {
                            pts[1] = new PointF(x - 1, y);

                            mode2 = false;
                            ready = true;
                            break;
                        }

                        if ((x == b.Width - 1) && (mode2))
                        {
                            pts[1] = new PointF(x, y);

                            mode2 = false;
                            ready = true;
                            break;
                        }

                        p += 4;
                    }
                }

                p = (byte*)(void*)Scan0;
                ready = false;

                for (int y = b.Height - 1; y >= 0; y--)
                {
                    bool mode2 = false;

                    if (ready)
                        break;

                    for (int x = 0; x < b.Width; x++)
                    {
                        if ((p[y * stride + x * 4 + 3] != (byte)0) && (mode2 == false))
                        {
                            pts[3] = new PointF(x, y);

                            mode2 = true;
                        }

                        if ((p[y * stride + x * 4 + 3] == (byte)0) && (mode2))
                        {
                            pts[2] = new PointF(x - 1, y);

                            mode2 = false;
                            ready = true;
                            break;
                        }

                        if ((x == b.Width - 1) && (mode2))
                        {
                            pts[2] = new PointF(x, y);

                            mode2 = false;
                            ready = true;
                            break;
                        }
                    }
                }
            }

            b.UnlockBits(bmData);

            return pts;
        }

        public static void SetSolid(Bitmap b)
        {
            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            int scanline = bmData.Stride;

            System.IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nOffset = bmData.Stride - b.Width * 4;
                int nWidth = b.Width;
                int nHeight = b.Height;

                //Parallel.For(0, nHeight, y =>
                for (int y = 0; y < nHeight; y++)
                {
                    for (int x = 0; x < nWidth; x++)
                    {
                        p[x * 4 + y * scanline + 3] = (Byte)255;
                    }
                }//);
            }

            b.UnlockBits(bmData);
        }

        //getls noch p
        public static LookUpValues GetLongestVert(Bitmap bmp)
        {
            int i = 0;
            int l = 0;

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            int scanline = bmData.Stride;

            System.IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nWidth = bmp.Width;
                int nHeight = bmp.Height;

                for (int x = 0; x < nWidth; x++)
                {
                    for (int y = 0; y < nHeight; y++)
                    {
                        if (p[x * 4 + y * scanline + 3] != (Byte)0)
                        {
                            int ii = 1;
                            while ((y + ii < nHeight) && (p[x * 4 + (y + ii) * scanline + 3] != (Byte)0))
                            {
                                ii++;
                            }
                            if (ii > i)
                            {
                                i = ii;
                                l = x;
                            }

                            y += ii - 1;
                        }
                    }
                }
            }

            bmp.UnlockBits(bmData);

            LookUpValues lv = new LookUpValues();
            lv.Laenge = i;
            lv.Pos = l;

            return lv;
        }

        public static LookUpValues GetShortestVert(Bitmap bmp)
        {
            int i = bmp.Height;
            int l = 0;

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            int scanline = bmData.Stride;

            System.IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nWidth = bmp.Width;
                int nHeight = bmp.Height;

                for (int x = 0; x < nWidth; x++)
                {
                    for (int y = 0; y < nHeight; y++)
                    {
                        if (p[x * 4 + y * scanline + 3] != (Byte)0)
                        {
                            int ii = 1;
                            while ((y + ii < nHeight) && (p[x * 4 + (y + ii) * scanline + 3] != (Byte)0))
                            {
                                ii++;
                            }
                            if (ii < i)
                            {
                                i = ii;
                                l = x;
                            }

                            y += ii - 1;
                        }
                    }
                }
            }

            bmp.UnlockBits(bmData);

            LookUpValues lv = new LookUpValues();
            lv.Laenge = i;
            lv.Pos = l;

            return lv;
        }

        public static LookUpValues GetLongestHorz(Bitmap bmp)
        {
            int i = 0;
            int l = 0;

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            int scanline = bmData.Stride;

            System.IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nWidth = bmp.Width;
                int nHeight = bmp.Height;

                for (int y = 0; y < nHeight; y++)
                {
                    for (int x = 0; x < nWidth; x++)
                    {
                        if (p[x * 4 + y * scanline + 3] != (Byte)0)
                        {
                            int ii = 1;
                            while ((x + ii < nWidth) && (p[(x + ii) * 4 + y * scanline + 3] != (Byte)0))
                            {
                                ii++;
                            }
                            if (ii > i)
                            {
                                i = ii;
                                l = y;
                            }

                            x += ii - 1;
                        }
                    }
                }
            }

            bmp.UnlockBits(bmData);

            LookUpValues lv = new LookUpValues();
            lv.Laenge = i;
            lv.Pos = l;

            return lv;
        }

        public static LookUpValues GetShortestHorz(Bitmap bmp)
        {
            int i = bmp.Width;
            int l = 0;

            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            int scanline = bmData.Stride;

            System.IntPtr Scan0 = bmData.Scan0;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                int nWidth = bmp.Width;
                int nHeight = bmp.Height;

                for (int y = 0; y < nHeight; y++)
                {
                    for (int x = 0; x < nWidth; x++)
                    {
                        if (p[x * 4 + y * scanline + 3] != (Byte)0)
                        {
                            int ii = 1;
                            while ((x + ii < nWidth) && (p[(x + ii) * 4 + y * scanline + 3] != (Byte)0))
                            {
                                ii++;
                            }
                            if (ii < i)
                            {
                                i = ii;
                                l = y;
                            }

                            x += ii - 1;
                        }
                    }
                }
            }

            bmp.UnlockBits(bmData);

            LookUpValues lv = new LookUpValues();
            lv.Laenge = i;
            lv.Pos = l;

            return lv;
        }
        //getls noch p

        //Schlleifen mit Abbrüchen noch paralellisieren
        public static Bitmap Wave(Bitmap bitmap, WaveModus Modus, float WaveHeight, double HalfWaves, float Move, bool AntiAlias)
        {
            Bitmap img = null;
            Bitmap bmp = null;
            Bitmap bmp2 = null;
            Bitmap bmp4 = null;
            Double[] Al = new Double[] { };
            Graphics fg = null;
            Graphics g = null;

            try
            {
                img = (Bitmap)bitmap.Clone();
                int tb7 = (int)Modus;

                float tb2 = WaveHeight;

                Double tb6 = HalfWaves;

                float tb4 = Move;

                bool bAlias = AntiAlias;

                bool b = false;
                int l = -1;

                if (tb7 == 1)
                {
                    bmp = new Bitmap(img.Width + ((int)Math.Ceiling(tb2) * 2), img.Height);

                    bmp2 = new Bitmap(img.Width + ((int)Math.Ceiling(tb2) * 2), img.Height);

                    fg = Graphics.FromImage(bmp);
                    fg.DrawImage(img, (tb2 * 2), 0);
                    fg.Dispose();

                    Al = new Double[bmp.Height];

                    int fPoint = (int)((double)bmp.Height / (tb6 * 2.0) + tb4);
                    int h = img.Height;

                    //Parallel.For(0, h, y =>
                    for (int y = 0; y < h; y++)
                    {
                        Double fip = Math.Sin(((Double)y + (Double)tb4) / ((Double)h / (Math.PI * tb6))) * tb2;
                        Double fip_y = fip + tb2;

                        Al[y] = fip_y;
                    }//);

                    b = OffsetFilterAntiAlias_x(bmp2, bmp, Al, tb2, bAlias, fPoint);

                }

                if (tb7 == 0)
                {
                    bmp = new Bitmap(img.Width, img.Height + ((int)Math.Ceiling(tb2) * 2));

                    bmp2 = new Bitmap(img.Width, img.Height + ((int)Math.Ceiling(tb2) * 2));

                    fg = Graphics.FromImage(bmp);
                    fg.DrawImage(img, 0, (tb2 * 2));
                    fg.Dispose();

                    Al = new Double[bmp.Width];

                    int fPoint = (int)((double)bmp.Width / (tb6 * 2.0) + tb4);
                    int w = img.Width;

                    //Parallel.For(0, w, x =>
                    for (int x = 0; x < w; x++)
                    {
                        Double fip = Math.Sin(((Double)x + (Double)tb4) / ((Double)w / (Math.PI * tb6))) * tb2;
                        Double fip_x = fip + tb2;

                        Al[x] = fip_x;
                    }//);

                    b = OffsetFilterAntiAlias_y(bmp2, bmp, Al, tb2, bAlias, fPoint);

                }

                if (tb7 == 3)
                {
                    bmp = new Bitmap(img.Width + ((int)Math.Ceiling(tb2) * 2), img.Height);

                    bmp4 = new Bitmap(img.Width + ((int)Math.Ceiling(tb2) * 2), img.Height);

                    fg = Graphics.FromImage(bmp);
                    fg.DrawImage(img, (tb2 * 2), 0);
                    fg.Dispose();

                    Al = new Double[bmp.Height];

                    int fPoint = (int)((double)bmp.Height / (tb6 * 2.0) + tb4);
                    int h = img.Height;

                    //Parallel.For(0, h, y =>
                    for (int y = 0; y < h; y++)
                    {
                        Double fip = Math.Sin(((Double)y + (Double)tb4) / ((Double)h / (Math.PI * tb6))) * tb2;
                        Double fip_y = fip + tb2;

                        Al[y] = fip_y;
                    }//);

                    List<double> Al2 = new List<double>();
                    double gLength = 0.0;
                    double lV = 0.0;

                    for (int y = 0; y < bmp.Height; y++)
                    {
                        //Höhe
                        Double fip_x = Math.Sin(((Double)y + (Double)tb4) / ((Double)img.Height / (Math.PI * tb6))) * tb2;
                        //Steigung
                        Double fip = fip_x - lV;
                        lV = fip_x;
                        //Winkel
                        Double fip_y = Math.Atan(Math.Abs(fip));
                        //Länge
                        Double fip_L = Math.Cos(fip_y);

                        fip_L = Math.Abs(1.0 / fip_L);

                        gLength += fip_L;

                        if (gLength > (double)bmp.Height)
                        {
                            fip_L = gLength - (double)bmp.Height;
                            Al2.Add(fip_L);
                            break;
                        }

                        Al2.Add(fip_L);
                    }

                    b = OffsetFilterAntiAlias_x(bmp4, bmp, Al, tb2, bAlias, fPoint);

                    if (b)
                        l = SinYFilter(bmp4, Al2, true);

                    bmp2 = new Bitmap(bmp.Width, Al2.Count);

                    g = Graphics.FromImage(bmp2);
                    g.DrawImage(bmp4, 0, 0);
                    g.Dispose();

                    bmp4.Dispose();

                }

                if (tb7 == 2)
                {
                    bmp = new Bitmap(img.Width, img.Height + ((int)Math.Ceiling(tb2) * 2));

                    bmp4 = new Bitmap(img.Width, img.Height + ((int)Math.Ceiling(tb2) * 2));

                    fg = Graphics.FromImage(bmp);
                    fg.DrawImage(img, 0, (tb2 * 2));
                    fg.Dispose();

                    Al = new Double[bmp.Width];

                    int fPoint = (int)((double)bmp.Width / (tb6 * 2.0) + tb4);

                    List<double> Al2 = new List<double>();
                    double gLength = 0.0;
                    double lV = 0.0;

                    for (int x = 0; x < bmp.Width; x++)
                    {
                        //Höhe
                        Double fip_y = Math.Sin(((Double)x + (Double)tb4) / ((Double)img.Width / (Math.PI * tb6))) * tb2;
                        //Steigung
                        Double fip = fip_y - lV;
                        lV = fip_y;
                        //Winkel
                        Double fip_x = Math.Atan(Math.Abs(fip));
                        //Länge
                        Double fip_L = Math.Cos(fip_x);

                        fip_L = Math.Abs(1.0 / fip_L);

                        gLength += fip_L;

                        if (gLength > (double)bmp.Width)
                        {
                            fip_L = gLength - (double)bmp.Width;
                            Al2.Add(fip_L);
                            break;
                        }

                        Al2.Add(fip_L);
                    }

                    int w = img.Width;

                    //Parallel.For(0, w, x =>
                    for (int x = 0; x < w; x++)
                    {
                        Double fip = Math.Sin(((Double)x + (Double)tb4) / ((Double)w / (Math.PI * tb6))) * tb2;
                        Double fip_x = fip + tb2;

                        Al[x] = fip_x;
                    }//);

                    b = OffsetFilterAntiAlias_y(bmp4, bmp, Al, tb2, bAlias, fPoint);

                    if (b)
                        l = SinXFilter(bmp4, Al2, true);

                    bmp2 = new Bitmap(Al2.Count, bmp4.Height);

                    g = Graphics.FromImage(bmp2);
                    g.DrawImage(bmp4, 0, 0);
                    g.Dispose();

                    bmp4.Dispose();

                }

                bmp.Dispose();

                if (!b)
                    MessageBox.Show("Mindestens ein Fehler ist während der Verarbeitung aufgetreten.");

                if ((l == -1) && ((tb7 == 2) || (tb7 == 3)))
                    MessageBox.Show("Mindestens ein Fehler ist während der Verarbeitung aufgetreten.");

                return bmp2;
            }
            catch
            {
                if (img != null)
                    img.Dispose();
                if (bmp != null)
                    bmp.Dispose();
                if (bmp2 != null)
                    bmp2.Dispose();
                if (bmp4 != null)
                    bmp4.Dispose();

                if (Al != null)
                    Al = null;
                if (fg != null)
                    fg.Dispose();
                if (g != null)
                    g.Dispose();

                return null;
            }
        }

        //Folgende vier Methoden basieren auf Christian Graus' Bilinear_Resample_Routine http://www.codeproject.com/KB/GDI-plus/displacementfilters.aspx

        public static bool OffsetFiAbs(Bitmap b, Point[,] offset)
        {
            Bitmap bSrc = null;
            BitmapData bmData = null;
            BitmapData bmSrc = null;

            try
            {
                bSrc = (Bitmap)b.Clone();

                // GDI+ still lies to us - the return format is BGR, NOT RGB.
                bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

                int scanline = bmData.Stride;

                System.IntPtr Scan0 = bmData.Scan0;
                System.IntPtr SrcScan0 = bmSrc.Scan0;

                unsafe
                {
                    byte* p = (byte*)(void*)Scan0;
                    byte* pSrc = (byte*)(void*)SrcScan0;

                    int nWidth = b.Width;
                    int nHeight = b.Height;

                    //Parallel.For(0, nHeight, y =>
                    for (int y = 0; y < nHeight; y++)
                    {
                        for (int x = 0; x < nWidth; ++x)
                        {
                            int xOffset = offset[x, y].X;
                            int yOffset = offset[x, y].Y;

                            if (yOffset >= 0 && yOffset < nHeight && xOffset >= 0 && xOffset < nWidth)
                            {
                                p[y * scanline + x * 4] = pSrc[(yOffset * scanline) + (xOffset * 4)];
                                p[y * scanline + x * 4 + 1] = pSrc[(yOffset * scanline) + (xOffset * 4) + 1];
                                p[y * scanline + x * 4 + 2] = pSrc[(yOffset * scanline) + (xOffset * 4) + 2];
                                p[y * scanline + x * 4 + 3] = pSrc[(yOffset * scanline) + (xOffset * 4) + 3];
                            }

                            if (yOffset == float.MinValue || xOffset == float.MinValue)
                            {
                                p[y * scanline + x * 4] = (byte)0;
                                p[y * scanline + x * 4 + 1] = (byte)0;
                                p[y * scanline + x * 4 + 2] = (byte)0;
                                p[y * scanline + x * 4 + 3] = (byte)0;
                            }
                        }
                    }//);
                }

                b.UnlockBits(bmData);
                bSrc.UnlockBits(bmSrc);

                bSrc.Dispose();

                return true;
            }
            catch
            {
                try
                {
                    b.UnlockBits(bmData);
                }
                catch
                {

                }

                try
                {
                    bSrc.UnlockBits(bmSrc);
                }
                catch
                {

                }

                if (bSrc != null)
                    bSrc.Dispose();

                return false;
            }
        }

        public static bool OffsetFiAntiAlias(Bitmap b, FloatPointF[,] fp)
        {
            Bitmap bSrc = null;
            BitmapData bmData = null;
            BitmapData bmSrc = null;

            try
            {
                bSrc = (Bitmap)b.Clone();

                // GDI+ still lies to us - the return format is BGR, NOT RGB.
                bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

                int scanline = bmData.Stride;

                System.IntPtr Scan0 = bmData.Scan0;
                System.IntPtr SrcScan0 = bmSrc.Scan0;

                unsafe
                {
                    byte* p = (byte*)(void*)Scan0;
                    byte* pSrc = (byte*)(void*)SrcScan0;

                    int nWidth = b.Width;
                    int nHeight = b.Height;

                    //Parallel.For(0, nHeight, y =>
                    for (int y = 0; y < nHeight; y++)
                    {
                        for (int x = 0; x < nWidth; x++)
                        {
                            int ceil_x, ceil_y;
                            double p1, p2;

                            double xOffset = fp[x, y].X;
                            double yOffset = fp[x, y].Y;

                            // Setup

                            int floor_x = (int)Math.Floor(xOffset);
                            int floor_y = (int)Math.Floor(yOffset);

                            if (floor_x == nWidth - 1)
                            {
                                floor_x -= 1;
                            }

                            if (floor_y == nHeight - 1)
                            {
                                floor_y -= 1;
                            }

                            if (floor_x != float.MinValue)
                                ceil_x = floor_x + 1;
                            else
                                ceil_x = floor_x;

                            if (floor_y != float.MinValue)
                                ceil_y = floor_y + 1;
                            else
                                ceil_y = floor_y;

                            double fraction_x = xOffset - floor_x;
                            double fraction_y = yOffset - floor_y;
                            double one_minus_x = 1.0 - fraction_x;
                            double one_minus_y = 1.0 - fraction_y;

                            double dOut = 0.0;

                            if (floor_y >= 0 && ceil_y < nHeight && floor_x >= 0 && ceil_x < nWidth)
                            {
                                // Blue

                                p1 = one_minus_x * (double)(pSrc[floor_y * scanline + floor_x * 4]) +
                                    fraction_x * (double)(pSrc[floor_y * scanline + ceil_x * 4]);

                                p2 = one_minus_x * (double)(pSrc[ceil_y * scanline + floor_x * 4]) +
                                    fraction_x * (double)(pSrc[ceil_y * scanline + 4 * ceil_x]);

                                dOut = (one_minus_y * p1 + fraction_y * p2);
                                p[x * 4 + y * scanline] = (Byte)Math.Max(Math.Min(dOut, 255.0), 0.0);

                                // Green

                                p1 = one_minus_x * (double)(pSrc[floor_y * scanline + floor_x * 4 + 1]) +
                                    fraction_x * (double)(pSrc[floor_y * scanline + ceil_x * 4 + 1]);

                                p2 = one_minus_x * (double)(pSrc[ceil_y * scanline + floor_x * 4 + 1]) +
                                    fraction_x * (double)(pSrc[ceil_y * scanline + 4 * ceil_x + 1]);

                                dOut = (one_minus_y * p1 + fraction_y * p2);
                                p[x * 4 + y * scanline + 1] = (Byte)Math.Max(Math.Min(dOut, 255.0), 0.0);

                                // Red

                                p1 = one_minus_x * (double)(pSrc[floor_y * scanline + floor_x * 4 + 2]) +
                                    fraction_x * (double)(pSrc[floor_y * scanline + ceil_x * 4 + 2]);

                                p2 = one_minus_x * (double)(pSrc[ceil_y * scanline + floor_x * 4 + 2]) +
                                    fraction_x * (double)(pSrc[ceil_y * scanline + 4 * ceil_x + 2]);

                                dOut = (one_minus_y * p1 + fraction_y * p2);
                                p[x * 4 + y * scanline + 2] = (Byte)Math.Max(Math.Min(dOut, 255.0), 0.0);

                                // Alpha

                                p1 = one_minus_x * (double)(pSrc[floor_y * scanline + floor_x * 4 + 3]) +
                                    fraction_x * (double)(pSrc[floor_y * scanline + ceil_x * 4 + 3]);

                                p2 = one_minus_x * (double)(pSrc[ceil_y * scanline + floor_x * 4 + 3]) +
                                    fraction_x * (double)(pSrc[ceil_y * scanline + 4 * ceil_x + 3]);

                                dOut = (one_minus_y * p1 + fraction_y * p2);
                                p[x * 4 + y * scanline + 3] = (Byte)Math.Max(Math.Min(dOut, 255.0), 0.0);
                            }

                            else if (fp[x, y].X != float.MinValue || fp[x, y].Y != float.MinValue)
                            {
                                if (fp[x, y].X < 0.0 && fp[x, y].X >= -1.0)
                                {
                                    if ((floor_y * scanline + floor_x * 4 >= 0) && (ceil_y < nHeight) && (ceil_x < nWidth))
                                    {
                                        // Blue

                                        p1 = one_minus_x * (double)(pSrc[floor_y * scanline + ceil_x * 4]) +
                                            fraction_x * (double)(pSrc[floor_y * scanline + ceil_x * 4]);

                                        p2 = one_minus_x * (double)(pSrc[ceil_y * scanline + ceil_x * 4]) +
                                            fraction_x * (double)(pSrc[ceil_y * scanline + 4 * ceil_x]);

                                        dOut = (one_minus_y * p1 + fraction_y * p2);
                                        p[x * 4 + y * scanline] = (Byte)Math.Max(Math.Min(dOut, 255.0), 0.0);

                                        // Green

                                        p1 = one_minus_x * (double)(pSrc[floor_y * scanline + ceil_x * 4 + 1]) +
                                            fraction_x * (double)(pSrc[floor_y * scanline + ceil_x * 4 + 1]);

                                        p2 = one_minus_x * (double)(pSrc[ceil_y * scanline + ceil_x * 4 + 1]) +
                                            fraction_x * (double)(pSrc[ceil_y * scanline + 4 * ceil_x + 1]);

                                        dOut = (one_minus_y * p1 + fraction_y * p2);
                                        p[x * 4 + y * scanline + 1] = (Byte)Math.Max(Math.Min(dOut, 255.0), 0.0);

                                        // Red

                                        p1 = one_minus_x * (double)(pSrc[floor_y * scanline + ceil_x * 4 + 2]) +
                                            fraction_x * (double)(pSrc[floor_y * scanline + ceil_x * 4 + 2]);

                                        p2 = one_minus_x * (double)(pSrc[ceil_y * scanline + ceil_x * 4 + 2]) +
                                            fraction_x * (double)(pSrc[ceil_y * scanline + 4 * ceil_x + 2]);

                                        dOut = (one_minus_y * p1 + fraction_y * p2);
                                        p[x * 4 + y * scanline + 2] = (Byte)Math.Max(Math.Min(dOut, 255.0), 0.0);

                                        // Alpha

                                        p1 = fraction_y * (double)(pSrc[ceil_y * scanline + floor_x * 4 + 3]) +
                                            one_minus_y * (double)(pSrc[floor_y * scanline + floor_x * 4 + 3]);

                                        dOut = p1;
                                        p[x * 4 + y * scanline + 3] = (Byte)Math.Max(Math.Min(dOut, 255.0), 0.0);
                                    }
                                }

                                if (fp[x, y].Y < 0.0 && fp[x, y].Y >= -1.0)
                                {
                                    if ((ceil_y * scanline + floor_x * 4 >= 0) && (ceil_y < nHeight) && (ceil_x < nWidth))
                                    {
                                        // Blue

                                        p1 = one_minus_x * (double)(pSrc[ceil_y * scanline + floor_x * 4]) +
                                            fraction_x * (double)(pSrc[ceil_y * scanline + ceil_x * 4]);

                                        p2 = one_minus_x * (double)(pSrc[ceil_y * scanline + floor_x * 4]) +
                                            fraction_x * (double)(pSrc[ceil_y * scanline + 4 * ceil_x]);

                                        dOut = (one_minus_y * p1 + fraction_y * p2);
                                        p[x * 4 + y * scanline] = (Byte)Math.Max(Math.Min(dOut, 255.0), 0.0);

                                        // Green

                                        p1 = one_minus_x * (double)(pSrc[ceil_y * scanline + floor_x * 4 + 1]) +
                                            fraction_x * (double)(pSrc[ceil_y * scanline + ceil_x * 4 + 1]);

                                        p2 = one_minus_x * (double)(pSrc[ceil_y * scanline + floor_x * 4 + 1]) +
                                            fraction_x * (double)(pSrc[ceil_y * scanline + 4 * ceil_x + 1]);

                                        dOut = (one_minus_y * p1 + fraction_y * p2);
                                        p[x * 4 + y * scanline + 1] = (Byte)Math.Max(Math.Min(dOut, 255.0), 0.0);

                                        // Red

                                        p1 = one_minus_x * (double)(pSrc[ceil_y * scanline + floor_x * 4 + 2]) +
                                            fraction_x * (double)(pSrc[ceil_y * scanline + ceil_x * 4 + 2]);

                                        p2 = one_minus_x * (double)(pSrc[ceil_y * scanline + floor_x * 4 + 2]) +
                                            fraction_x * (double)(pSrc[ceil_y * scanline + 4 * ceil_x + 2]);

                                        dOut = (one_minus_y * p1 + fraction_y * p2);
                                        p[x * 4 + y * scanline + 2] = (Byte)Math.Max(Math.Min(dOut, 255.0), 0.0);

                                        // Alpha

                                        p1 = one_minus_x * (double)(pSrc[ceil_y * scanline + floor_x * 4 + 3]) +
                                            fraction_x * (double)(pSrc[ceil_y * scanline + ceil_x * 4 + 3]);

                                        dOut = p1;
                                        p[x * 4 + y * scanline + 3] = (Byte)Math.Max(Math.Min(dOut, 255.0), 0.0);
                                    }
                                }
                            }

                            if (yOffset == float.MinValue || xOffset == float.MinValue)
                            {
                                p[x * 4 + y * scanline] = (byte)0;
                                p[x * 4 + y * scanline + 1] = (byte)0;
                                p[x * 4 + y * scanline + 2] = (byte)0;
                                p[x * 4 + y * scanline + 3] = (byte)0;
                            }
                        }
                    }//);
                }

                b.UnlockBits(bmData);
                bSrc.UnlockBits(bmSrc);

                bSrc.Dispose();

                return true;
            }
            catch
            {
                try
                {
                    b.UnlockBits(bmData);
                }
                catch
                {

                }

                try
                {
                    bSrc.UnlockBits(bmSrc);
                }
                catch
                {

                }

                if (bSrc != null)
                    bSrc.Dispose();

                return false;
            }
        }

        public static bool OffsetFilterAntiAlias_x(Bitmap b, Bitmap bSrc, Double[] fp, Double f, bool bAlias, int fPoint)
        {
            BitmapData bmData = null;
            BitmapData bmSrc = null;

            try
            {
                // GDI+ still lies to us - the return format is BGR, NOT RGB.
                bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

                int scanline = bmData.Stride;

                System.IntPtr Scan0 = bmData.Scan0;
                System.IntPtr SrcScan0 = bmSrc.Scan0;

                unsafe
                {
                    byte* p = (byte*)(void*)Scan0;
                    byte* pSrc = (byte*)(void*)SrcScan0;

                    int nWidth = b.Width;
                    int nHeight = b.Height;

                    //Parallel.For(0, nHeight, y =>
                    for (int y = 0; y < nHeight; y++)
                    {
                        for (int x = 0; x < nWidth; x++)
                        {
                            double p1;

                            double xOffset = fp[y] + (double)x;

                            // Setup

                            int floor_x = (int)Math.Floor(xOffset);
                            int ceil_x = floor_x + 1;
                            double fraction_x = xOffset - floor_x;
                            double one_minus_x = 1.0 - fraction_x;

                            if (x == (int)(fp[y] * -1) + (int)(f * 2) - 1)
                            {

                            }
                            else
                            {
                                if (y >= 0 && y < nHeight && floor_x >= 0 && ceil_x < nWidth)
                                {
                                    // Blue

                                    p1 = one_minus_x * (double)(pSrc[y * scanline + floor_x * 4]) +
                                        fraction_x * (double)(pSrc[y * scanline + ceil_x * 4]);

                                    p[x * 4 + y * scanline] = (Byte)Math.Max(Math.Min(p1, 255.0), 0.0);

                                    // Green

                                    p1 = one_minus_x * (double)(pSrc[y * scanline + floor_x * 4 + 1]) +
                                        fraction_x * (double)(pSrc[y * scanline + ceil_x * 4 + 1]);

                                    p[x * 4 + y * scanline + 1] = (Byte)Math.Max(Math.Min(p1, 255.0), 0.0);

                                    // Red

                                    p1 = one_minus_x * (double)(pSrc[y * scanline + floor_x * 4 + 2]) +
                                        fraction_x * (double)(pSrc[y * scanline + ceil_x * 4 + 2]);

                                    p[x * 4 + y * scanline + 2] = (Byte)Math.Max(Math.Min(p1, 255.0), 0.0);


                                    // Alpha

                                    p1 = one_minus_x * (double)(pSrc[y * scanline + floor_x * 4 + 3]) +
                                        fraction_x * (double)(pSrc[y * scanline + ceil_x * 4 + 3]);

                                    p[x * 4 + y * scanline + 3] = (Byte)Math.Max(Math.Min(p1, 255.0), 0.0);

                                    if (bAlias)
                                    {
                                        if (x == (int)(f * 2 - fp[y] + 1))
                                        {
                                            p[x * 4 + y * scanline + 3] = (Byte)(p1 * fraction_x);
                                            if (fraction_x == 0.0 && x > 0 && y % fPoint == 0)
                                            {
                                                p[x * 4 + y * scanline + 3] = p[(x - 1) * 4 + y * scanline + 3];
                                                p[(x - 1) * 4 + y * scanline + 3] = (Byte)Math.Max(Math.Min((p1 * fraction_x), 255.0), 0.0);
                                            }
                                            if (fraction_x == 0.0 && x > 0 && y % fPoint != 0)
                                            {
                                                p[x * 4 + y * scanline + 3] = (Byte)p1;
                                                p[(x - 1) * 4 + y * scanline + 3] = (Byte)Math.Max(Math.Min((p1 * fraction_x), 255.0), 0.0);
                                            }
                                        }

                                        if (x == (int)(nWidth - fp[y] - 1))
                                        {
                                            p[x * 4 + y * scanline + 3] = (Byte)Math.Max(Math.Min((p1 * one_minus_x), 255.0), 0.0);
                                        }
                                    }
                                }

                            }

                        }
                    }//);
                }

                b.UnlockBits(bmData);
                bSrc.UnlockBits(bmSrc);

                return true;
            }
            catch
            {
                try
                {
                    b.UnlockBits(bmData);
                }
                catch
                {

                }

                try
                {
                    bSrc.UnlockBits(bmSrc);
                }
                catch
                {

                }
                return false;
            }
        }

        public static bool OffsetFilterAntiAlias_y(Bitmap b, Bitmap bSrc, Double[] fp, Double f, bool bAlias, int fPoint)
        {
            BitmapData bmData = null;
            BitmapData bmSrc = null;

            try
            {
                // GDI+ still lies to us - the return format is BGR, NOT RGB.
                bmData = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);
                bmSrc = bSrc.LockBits(new Rectangle(0, 0, bSrc.Width, bSrc.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

                int scanline = bmData.Stride;

                System.IntPtr Scan0 = bmData.Scan0;
                System.IntPtr SrcScan0 = bmSrc.Scan0;

                unsafe
                {
                    byte* p = (byte*)(void*)Scan0;
                    byte* pSrc = (byte*)(void*)SrcScan0;

                    int nWidth = b.Width;
                    int nHeight = b.Height;

                    //Parallel.For(0, nWidth, x =>
                    for (int x = 0; x < nWidth; x++)
                    {
                        for (int y = 0; y < nHeight; y++)
                        {
                            double p1;

                            double yOffset = fp[x] + (double)y;

                            // Setup

                            int floor_y = (int)Math.Floor(yOffset);
                            int ceil_y = floor_y + 1;
                            double fraction_y = yOffset - floor_y;
                            double one_minus_y = 1.0 - fraction_y;

                            if ((y == (int)(fp[x] * -1) + (int)(f * 2) - 1))
                            {

                            }
                            else
                            {

                                if (floor_y >= 0 && ceil_y < nHeight && x >= 0 && x < nWidth)
                                {
                                    // Blue

                                    p1 = one_minus_y * (double)(pSrc[floor_y * scanline + x * 4]) +
                                        fraction_y * (double)(pSrc[ceil_y * scanline + x * 4]);

                                    p[x * 4 + y * scanline] = (Byte)Math.Max(Math.Min(p1, 255.0), 0.0);

                                    // Green

                                    p1 = one_minus_y * (double)(pSrc[floor_y * scanline + x * 4 + 1]) +
                                        fraction_y * (double)(pSrc[ceil_y * scanline + x * 4 + 1]);

                                    p[x * 4 + y * scanline + 1] = (Byte)Math.Max(Math.Min(p1, 255.0), 0.0);

                                    // Red

                                    p1 = one_minus_y * (double)(pSrc[floor_y * scanline + x * 4 + 2]) +
                                        fraction_y * (double)(pSrc[ceil_y * scanline + x * 4 + 2]);

                                    p[x * 4 + y * scanline + 2] = (Byte)Math.Max(Math.Min(p1, 255.0), 0.0);

                                    // Alpha

                                    p1 = one_minus_y * (double)(pSrc[floor_y * scanline + x * 4 + 3]) +
                                        fraction_y * (double)(pSrc[ceil_y * scanline + x * 4 + 3]);

                                    p[x * 4 + y * scanline + 3] = (Byte)Math.Max(Math.Min(p1, 255.0), 0.0);

                                    if (bAlias)
                                    {
                                        if (y == (int)(f * 2 - fp[x] + 1))
                                        {
                                            p[x * 4 + y * scanline + 3] = (Byte)(p1 * fraction_y);
                                            if (fraction_y == 0.0 && y > 0 && x % fPoint == 0)
                                            {
                                                p[x * 4 + y * scanline + 3] = p[x * 4 + (y - 1) * scanline + 3];
                                                p[x * 4 + (y - 1) * scanline + 3] = (Byte)Math.Max(Math.Min((p1 * fraction_y), 255.0), 0.0);
                                            }
                                            if (fraction_y == 0.0 && y > 0 && x % fPoint != 0)
                                            {
                                                p[x * 4 + y * scanline + 3] = (Byte)p1;
                                                p[x * 4 + (y - 1) * scanline + 3] = (Byte)Math.Max(Math.Min((p1 * fraction_y), 255.0), 0.0);
                                            }
                                        }

                                        if (y == (int)(nHeight - fp[x] - 1))
                                        {
                                            p[x * 4 + y * scanline + 3] = (Byte)Math.Max(Math.Min((p1 * one_minus_y), 255.0), 0.0);
                                        }
                                    }
                                }

                            }
                        }
                    }//);
                }

                b.UnlockBits(bmData);
                bSrc.UnlockBits(bmSrc);

                return true;
            }
            catch
            {
                try
                {
                    b.UnlockBits(bmData);
                }
                catch
                {

                }

                try
                {
                    bSrc.UnlockBits(bmSrc);
                }
                catch
                {

                }
                return false;
            }
        }

        public static int SinXFilter(Bitmap bmp, List<double> Al2, bool bSmoothing)
        {
            Bitmap b = bmp;

            int nWidth = b.Width;
            int nHeight = b.Height;

            FloatPointF[,] fp = null;
            Point[,] pt = null;

            try
            {
                fp = new FloatPointF[nWidth, nHeight];
                pt = new Point[nWidth, nHeight];

                int al2c = Al2.Count;

                //Parallel.For(0, nHeight, y =>
                for (int y = 0; y < nHeight; y++)
                {
                    double val = 0.0;

                    for (int x = 0; x < al2c; ++x)
                    {
                        double newX = val + Al2[x];
                        val = newX;

                        if (newX > 0 && newX < nWidth)
                        {
                            fp[x, y].X = (float)newX;
                            pt[x, y].X = (int)newX;
                        }
                        else
                        {
                            fp[x, y].X = 0.0F;
                            pt[x, y].X = 0;
                        }

                        double newY = y;

                        if (newY > 0 && newY < nHeight)
                        {
                            fp[x, y].Y = (float)newY;
                            pt[x, y].Y = (int)newY;
                        }
                        else
                        {
                            fp[x, y].Y = 0.0F;
                            pt[x, y].Y = 0;
                        }
                    }
                }//);

                int l = -1;

                for (int x = nWidth - 1; x >= 0; x--)
                {
                    for (int y = nHeight - 1; y >= 0; y--)
                    {
                        if (pt[x, y].X > 0)
                        {
                            l = x;
                            break;
                        }
                    }
                }

                if (bSmoothing)
                    OffsetFiAntiAlias(b, fp);
                else
                    OffsetFiAbs(b, pt);

                return l;
            }
            catch
            {
                return -1;
            }
        }

        public static int SinYFilter(Bitmap bmp, List<double> Al2, bool bSmoothing)
        {
            Bitmap b = bmp;

            int nWidth = b.Width;
            int nHeight = b.Height;

            FloatPointF[,] fp = null;
            Point[,] pt = null;

            try
            {
                fp = new FloatPointF[nWidth, nHeight];
                pt = new Point[nWidth, nHeight];

                int al2c = Al2.Count;

                //Parallel.For(0, nWidth, x =>
                for (int x = 0; x < nWidth; x++)
                {
                    double val = 0.0;

                    for (int y = 0; y < al2c; ++y)
                    {
                        double newX = x;

                        if (newX > 0 && newX < nWidth)
                        {
                            fp[x, y].X = (float)newX;
                            pt[x, y].X = (int)newX;
                        }
                        else
                        {
                            fp[x, y].X = 0.0F;
                            pt[x, y].X = 0;
                        }

                        double newY = val + Al2[y];
                        val = newY;

                        if (newY > 0 && newY < nHeight)
                        {
                            fp[x, y].Y = (float)newY;
                            pt[x, y].Y = (int)newY;
                        }
                        else
                        {
                            fp[x, y].Y = 0.0F;
                            pt[x, y].Y = 0;
                        }
                    }
                }//);

                int l = -1;

                for (int x = nWidth - 1; x >= 0; x--)
                {
                    for (int y = nHeight - 1; y >= 0; y--)
                    {
                        if (pt[x, y].Y > 0)
                        {
                            l = x;
                            break;
                        }
                    }
                }

                if (bSmoothing)
                    OffsetFiAntiAlias(b, fp);
                else
                    OffsetFiAbs(b, pt);

                return l;
            }
            catch
            {
                return -1;
            }
        }

        public static Bitmap ScanForPic(Bitmap bmp)
        {
            // GDI+ still lies to us - the return format is BGR, NOT RGB.
            BitmapData bmData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppArgb);

            int scanline = bmData.Stride;

            System.IntPtr Scan0 = bmData.Scan0;

            Point top = new Point(), left = new Point(), right = new Point(), bottom = new Point();
            bool complete = false;

            unsafe
            {
                byte* p = (byte*)(void*)Scan0;

                for (int y = 0; y < bmp.Height; y++)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        if (p[3] != 0)
                        {
                            top = new Point(x, y);
                            complete = true;
                            break;
                        }

                        p += 4;
                    }
                    if (complete)
                        break;
                }

                p = (byte*)(void*)Scan0;
                complete = false;

                for (int y = bmp.Height - 1; y >= 0; y--)
                {
                    for (int x = 0; x < bmp.Width; x++)
                    {
                        if (p[x * 4 + y * scanline + 3] != 0)
                        {
                            bottom = new Point(x + 1, y + 1);
                            complete = true;
                            break;
                        }
                    }
                    if (complete)
                        break;
                }

                p = (byte*)(void*)Scan0;
                complete = false;

                for (int x = 0; x < bmp.Width; x++)
                {
                    for (int y = 0; y < bmp.Height; y++)
                    {
                        if (p[x * 4 + y * scanline + 3] != 0)
                        {
                            left = new Point(x, y);
                            complete = true;
                            break;
                        }
                    }
                    if (complete)
                        break;
                }

                p = (byte*)(void*)Scan0;
                complete = false;

                for (int x = bmp.Width - 1; x >= 0; x--)
                {
                    for (int y = 0; y < bmp.Height; y++)
                    {
                        if (p[x * 4 + y * scanline + 3] != 0)
                        {
                            right = new Point(x + 1, y + 1);
                            complete = true;
                            break;
                        }
                    }
                    if (complete)
                        break;
                }
            }

            bmp.UnlockBits(bmData);

            Rectangle rectangle = new Rectangle(left.X, top.Y, right.X - left.X, bottom.Y - top.Y);

            try
            {
                Bitmap b = new Bitmap(rectangle.Width, rectangle.Height);
                Graphics g = Graphics.FromImage(b);
                g.DrawImage(bmp, 0, 0, rectangle, GraphicsUnit.Pixel);
                g.Dispose();

                return b;
            }
            catch
            {
                return null;
            }
        }

        public static Bitmap MakeRectangular(Bitmap bmp, int l, int s)
        {
            if (l == 0)
            {
                throw new Exception("l must not be 0.");
            }

            FloatPointF[,] fp = null;
            Bitmap b = null;

            try
            {
                fp = new FloatPointF[bmp.Width, bmp.Height];
                double faktor = ((double)(l - s) / 2.0D) / (double)bmp.Height;
                double h = (double)l / 2.0D;

                //double faktor = ((double)bmp.Width / 2.0D) / (double)bmp.Height;
                //double h = (double)bmp.Width / 2.0D;

                b = (Bitmap)bmp.Clone();
                int w = bmp.Width;
                int h1 = bmp.Height;

                //Parallel.For (0, h1, y =>
                for (int y = 0; y < h1; y++)
                {
                    for (int x = 0; x < w; x++)
                    {
                        double newX = x + ((y * faktor) *
                           ((h - (double)x) / h)
                           );

                        fp[x, y].X = (float)newX;
                        fp[x, y].Y = y;
                    }
                }//);

                OffsetFiAntiAlias(b, fp);

                return b;
            }
            catch
            {
                if (b != null)
                    b.Dispose();
                if (fp != null)
                    fp = null;
                return null;
            }
        }
    }
}
