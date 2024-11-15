using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageExDemo
{
    class CvDisplayGraphicsLine : CvDisplayGraphicsDot
    {
        

        public virtual double K
        {
            get;
            set;
        }

        public virtual double Angle
        {
            get
            {
                return double.IsNaN(K)?90:(Math.Atan(K)*180/ Math.PI );
            }
            set
            {
                if (Math.Abs(value) == 90)
                    K = double.NaN;
                else
                    K = Math.Tan(value);
            }
        }

        public double B
        {
            get
            {
                return Y - K * X;
            }
        }

        public CvDisplayGraphicsLine()
        {

        }
        public CvDisplayGraphicsLine(Point2d p,double k):base(p.X,p.Y)
        {
            K = k;
        }


        public double Distance(Point2d p)
        {
            if ( double.IsNaN(K))
                return Math.Abs(p.X - Location.X);
            //点P(X0, Y0)到直线y = kx + b的距离公式
            //d =| kx0 - y0 + b |/ 根号(k²+1)
            else
                return Math.Abs(K * p.X - p.Y + B) / Math.Sqrt(K * K + 1);
        }

        public override void OnPaint(PaintEventArgs e)
        {
            Point2d pStart , pEnd  ;
            Point2d screenPos = TranslateToScreenPos(Location);
            if(K == 0)
            {
                pStart = new Point2d(0, screenPos.Y);
                pEnd = new Point2d(e.ClipRectangle.Width, screenPos.Y);

              
            }
            else if(K > 0)
            {
                Point2d p1 = CaculateLocationByY(0);
                Point2d p2 = CaculateLocationByY(ParentMat.Image.Height);

                pStart = TranslateToScreenPos(p1);
                pEnd = TranslateToScreenPos(p2);
            }
            else if(K<0)
            {
                Point2d p1 = CaculateLocationByX(0);
                Point2d p2 = CaculateLocationByX(ParentMat.Image.Width);

                pStart = TranslateToScreenPos(p1);
                pEnd = TranslateToScreenPos(p2);
            }
            else
            {
                pStart = new Point2d(screenPos.X, 0);
                pEnd = new Point2d(screenPos.X, e.ClipRectangle.Height);
            }
            Pen pen = new Pen(GetDrawColor(), GetDrawSize());
            e.Graphics.DrawLine(pen, (float)pStart.X, (float)pStart.Y, (float)pEnd.X, (float)pEnd.Y);
        }


        public Point2d CaculateLocationByX(double x)
        {
            return new Point2d(x, K * x + B);
        }

        public Point2d CaculateLocationByY(double y)
        {
            return new Point2d(K==0?y:((y - B) / K), y);
        }
    }

}
