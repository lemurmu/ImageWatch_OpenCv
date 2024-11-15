using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageExDemo
{
    class CvDisplayGraphicsCircle : CvDisplayGraphicsDot
    {
        protected enum SelectedType
        {
            None,
            Center,
            Edge
        }

        protected SelectedType _SelectedType =  SelectedType.None;

        public double Radius { get; set; }

        public CvDisplayGraphicsCircle(double x,double y,double radius):base(x,y)
        {
            this.Radius = radius;
        }
        public override bool IsMouseIn(PointF pos)
        {
            //throw new NotImplementedException();
            bool centerin= base.IsMouseIn(pos);

            double distance = Caculate.Distance(DisplayOrigin, new Point2d(pos.X, pos.Y));

            double r = Radius * PixelSize.Width;
            bool edgein = distance <= r + _focusRange && distance >= r - _focusRange;

            if (centerin)
                _SelectedType = SelectedType.Center;
            else if (edgein)
                _SelectedType = SelectedType.Edge;
            else
                _SelectedType = SelectedType.None;

            return centerin || edgein;
        }

        public override void OnMouseMove(MouseEventArgs e)
        {
            if(_SelectedType == SelectedType.Center)
                base.OnMouseMove(e);
            else if(Selected && IsLeftMouseDown && _SelectedType == SelectedType.Edge)
            {
                double moveX = e.X - MouseDownPos.X, moveY = e.Y - MouseDownPos.Y;
                Size2d pixelSize = PixelSize;

                double moveRadius =Caculate.Hypotenuse(moveX * pixelSize.Width, moveY * pixelSize.Height);

                double distanceNew= Caculate.Distance(DisplayOrigin, new Point2d(e.X, e.Y)),
                    distancOld = Caculate.Distance(DisplayOrigin, new Point2d(MouseDownPos.X, MouseDownPos.Y));

                if (distanceNew < distancOld)
                    Radius -= moveRadius;
                else
                    Radius += moveRadius;

                MouseDownPos = e.Location;
            }
        }

        public override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Point2d p = TranslateToScreenPos(Location);
            Size2d size = PixelSize;
            double rw = size.Width * Radius, rh = size.Height * Radius;
            float drawSize = GetDrawSize();
            Pen pen = new Pen(GetDrawColor(), drawSize);

            RectangleF rect = new RectangleF(
                (float)(p.X - rw), (float)(p.Y - rh), (float)(2* rw), (float)(2*rh)
                );

            
            e.Graphics.DrawEllipse(pen, rect);
        }

      
      
    }
}
