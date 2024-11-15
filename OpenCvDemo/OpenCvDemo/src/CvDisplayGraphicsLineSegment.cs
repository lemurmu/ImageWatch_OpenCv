using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageExDemo
{
    class CvDisplayGraphicsLineSegment : CvDisplayGraphicsLine
    {
        public Point2d Start { get; set; }

        public Point2d End { get; set; }


        protected enum SelectedType
        {
            None,
            Start,
            End,
            Line
        }

        protected SelectedType _SelectedType = SelectedType.None;

        public override Point2d Location
        {
            get
            {
                return new Point2d((Start.X + End.X) / 2, (Start.Y + End.Y) / 2);
            }

        }

        public override double K
        {
            get
            {
                return End.X == Start.X?double.NaN:((End.Y - Start.Y) / (End.X - Start.X));
            }
        }


        public Point2d Mid
        {
            get
            {
                return new Point2d((Start.X + End.X) / 2, (Start.Y + End.Y) / 2);
            }
        }


        public double Length
        {
            get
            {
                return Caculate.Distance(Start, End);
            }
        }
        public override double Angle { get => base.Angle; }

        public CvDisplayGraphicsLineSegment():this(0,0,0,0)
        {

        }

        public CvDisplayGraphicsLineSegment(Point2d pStart,Point2d pEnd)
        {
            Start = pStart;
            End = pEnd;

        }

        public CvDisplayGraphicsLineSegment(double x1,double y1,double x2,double y2):
            this(new Point2d(x1,y1),new Point2d(x2,y2))
        {

        }

        public override bool IsMouseIn(PointF pos)
        {
            CvDisplayGraphicsDot startDot = new CvDisplayGraphicsDot(Start);
            startDot.ParentMat = this.ParentMat;
            CvDisplayGraphicsDot endDot = new CvDisplayGraphicsDot(End);
            endDot.ParentMat = this.ParentMat;

            Point2d rectPos = TranslateToScreenPos(Location);
            double length = Caculate.Distance(TranslateToScreenPos(Start), TranslateToScreenPos(End));
            RotatedRect rotateRect = new RotatedRect(new Point2f((float)rectPos.X, (float)rectPos.Y),
                new Size2f(length, 6),(float)Angle);

            _SelectedType = SelectedType.None;
            if (startDot.IsMouseIn(pos))
                _SelectedType = SelectedType.Start;
            else if (endDot.IsMouseIn(pos))
                _SelectedType = SelectedType.End;
            else if (Caculate.RotateRectContains(rotateRect, new Point2d(pos.X, pos.Y)))
                _SelectedType = SelectedType.Line;
            return _SelectedType!= SelectedType.None;
        }


        public override void OnMouseMove(MouseEventArgs e)
        {
            if (IsLeftMouseDown)
            {
                double xmove = e.X - MouseDownPos.X,
                    ymove = e.Y - MouseDownPos.Y;

                if (_SelectedType == SelectedType.Start || _SelectedType == SelectedType.Line)
                {
                    Point2d screenStart = TranslateToScreenPos(Start);
                    screenStart.X += xmove;
                    screenStart.Y += ymove;
                    Start = TranslateToMatPos(screenStart);

                    MouseDownPos = e.Location;
                }
                if(_SelectedType == SelectedType.End || _SelectedType == SelectedType.Line)
                {
                    Point2d screenEnd = TranslateToScreenPos(End);
                    screenEnd.X += xmove;
                    screenEnd.Y += ymove;
                    End = TranslateToMatPos(screenEnd);
                    MouseDownPos = e.Location;
                }
            }
            base.OnMouseMove(e);
           
           
        }

        public override void OnPaint(PaintEventArgs e)
        {
          //  base.OnPaint(e);

            Point2d start = TranslateToScreenPos(Start),
                end = TranslateToScreenPos(End);

            Pen pen = new Pen(GetDrawColor(), GetDrawSize());
            e.Graphics.DrawLine(pen, new PointF((float)start.X, (float)start.Y), new PointF((float)end.X, (float)end.Y));

            if(_SelectedType != SelectedType.None)
            {
                SolidBrush brush = new SolidBrush(GetDrawColor());
                float radius = 3;
                e.Graphics.FillRectangle(brush, new RectangleF(new PointF((float)start.X - radius, (float)start.Y - radius), 
                    new SizeF(radius * 2, radius * 2)));

                e.Graphics.FillRectangle(brush, new RectangleF(new PointF((float)end.X - radius, (float)end.Y - radius),
                    new SizeF(radius * 2, radius * 2)));
            }
        }
    }
}
