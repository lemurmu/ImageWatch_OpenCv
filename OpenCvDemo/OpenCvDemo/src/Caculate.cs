using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageExDemo
{
    public static class Caculate
    {
        public static double Distance(Point2d p1,Point2d p2)
        {
            return Hypotenuse((p2.X - p1.X) ,(p2.Y - p1.Y));
        }


        public static double Hypotenuse(double edg1,double edg2)
        {
            return Math.Sqrt(Math.Pow(edg1, 2) + Math.Pow(edg2, 2));
        }


        public static bool RotateRectContains(RotatedRect rRect,Point2d p)
        {
            
            Point2f[] points = rRect.Points();
            
            double ab = new CvDisplayGraphicsLineSegment(points[0].X, points[0].Y, points[1].X, points[1].Y).Distance(p),
                bc = new CvDisplayGraphicsLineSegment(points[2].X, points[2].Y, points[1].X, points[1].Y).Distance(p),
                cd = new CvDisplayGraphicsLineSegment(points[2].X, points[2].Y, points[3].X, points[3].Y).Distance(p),
                ad = new CvDisplayGraphicsLineSegment(points[3].X, points[3].Y, points[0].X, points[0].Y).Distance(p);

            //舍弃小数位，否则可能超出一点点
            return (int)ab + (int)cd <= rRect.Size.Width && (int)bc + (int)ad <= rRect.Size.Height;
        }


        public static double Angle(double k1,double k2)
        {
            if (double.IsNaN(k1))
                return 90 - k2;
            else if (double.IsNaN(k2))
                return 90 - k1;
            
            double tan_k = 0; //直线夹角正切值
            double lines_arctan;//直线斜率的反正切值
            tan_k = (k2 - k1) / (1 + k2 * k1); //求直线夹角的公式
            lines_arctan = Math.Atan(tan_k) * 180.0 / 3.1415926;
            return lines_arctan;

        }
    }
}
