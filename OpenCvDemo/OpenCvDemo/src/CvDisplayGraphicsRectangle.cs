using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenCvSharp;

namespace ImageExDemo
{
    class CvDisplayGraphicsRectangle: CvDisplayGraphicsDot
    {

        private CvDisplayGraphicsLineSegment AB;
        private CvDisplayGraphicsLineSegment BC;
        private CvDisplayGraphicsLineSegment CD;
        private CvDisplayGraphicsLineSegment AD;


        public override CvDisplayGraphicsMat ParentMat
        {   get => base.ParentMat;
            set
            {
                base.ParentMat = value;
                if(AB!= null)
                    AB.ParentMat = value;
                if (BC != null)
                    BC.ParentMat = value;
                if (CD != null)
                    CD.ParentMat = value;
                if (AD != null)
                    AD.ParentMat = value;
            }
        }

        public CvDisplayGraphicsRectangle(Point2d center,Size2d size,double angle)
        {
            Location = center;
            RectSize = size;
            Angle = angle;


            AB = new CvDisplayGraphicsLineSegment();
            BC = new CvDisplayGraphicsLineSegment();
            CD = new CvDisplayGraphicsLineSegment();
            AD = new CvDisplayGraphicsLineSegment();
        }

        private double _Angle;

        public double Angle
        {
            get
            {
                return _Angle;
            }
            set
            {
                _Angle = value;  
            }
        }

        public Size2d RectSize;

        protected enum SelectedType
        {
            None,
            Center,
            WidthEdge,
            HeightEdge,
            Rotate
        }

        protected SelectedType _SelectedType = SelectedType.None;

        public double Width
        {
            get
            {
                return RectSize.Width;
            }
            set
            {
                RectSize.Width = value;
            }
        }

        public double Height
        {
            get
            {
                return RectSize.Height;
            }
            set
            {
                RectSize.Height = value;
            }
        }


        public override bool IsMouseIn(PointF pos)
        {
            bool bCenter = base.IsMouseIn(pos);

            _SelectedType = SelectedType.None;

            if (bCenter)
            {
                _SelectedType = SelectedType.Center;
            }
            else
            {
                Point2d[] points = GetPoints(false);

                AB.Start = points[0];
                AB.End = points[1];

                BC.Start = points[1];
                BC.End = points[2];

                CD.Start = points[2];
                CD.End = points[3];

                AD.Start = points[3];
                AD.End = points[0];

                if (new CvDisplayGraphicsDot(BC.Mid) { ParentMat = this.ParentMat }.IsMouseIn(pos))
                {
                    _SelectedType = SelectedType.Rotate;
                }
                else if(AB.IsMouseIn(pos) || CD.IsMouseIn(pos))
                {
                    _SelectedType = SelectedType.WidthEdge;
                }
                else if (BC.IsMouseIn(pos) || AD.IsMouseIn(pos))
                {
                    _SelectedType = SelectedType.HeightEdge;
                }
                 
            }

            return _SelectedType != SelectedType.None;
            
        }


        public override void OnMouseMove(MouseEventArgs e)
        {
            if(IsLeftMouseDown)
            {
                double xmove = e.X - MouseDownPos.X, ymove = e.Y - MouseDownPos.Y;
                if (_SelectedType == SelectedType.HeightEdge)
                {
                    Height +=  (ymove/PixelSize.Height) ;
                    MouseDownPos = e.Location;
                }
                else if (_SelectedType == SelectedType.WidthEdge)
                {
                    Width += (xmove/PixelSize.Width );
                    MouseDownPos = e.Location;
                }
                else if(_SelectedType == SelectedType.Rotate)
                {

                    Point2d center = TranslateToScreenPos(Location);
                    CvDisplayGraphicsLineSegment line1 = new CvDisplayGraphicsLineSegment(center, new Point2d(MouseDownPos.X, MouseDownPos.Y)),
                        line2 = new CvDisplayGraphicsLineSegment(center, new Point2d(e.X, e.Y));
                    Angle += Caculate.Angle(line1.K, line2.K);
                    Console.WriteLine(Angle);
                    MouseDownPos = e.Location;
                }
                else
                    base.OnMouseMove(e);
            }
            
        }

        Point2d[] GetPoints(bool isSreen)
        {
            List<Point2d> ps = new List<Point2d>();
            foreach (Point2f p in new RotatedRect(new Point2f((float)X, (float)Y),
                new Size2f((float)Width, (float)Height), (float)Angle).Points())
            {
                Point2d sp = isSreen? TranslateToScreenPos(new Point2d(p.X, p.Y)):
                    new Point2d(p.X, p.Y);
                ps.Add(sp);
            }
            return ps.ToArray();
        }

        public override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            List<PointF> ps = new List<PointF>();
            foreach(Point2f p in new RotatedRect(new Point2f((float)X,(float)Y),
                new Size2f((float)Width,(float)Height),(float)Angle).Points())
            {
                Point2d sp = TranslateToScreenPos(new Point2d(p.X, p.Y));
                ps.Add( new PointF((float)sp.X, (float)sp.Y));
            }
            Pen pen = new Pen(GetDrawColor(), GetDrawSize());
            e.Graphics.DrawPolygon(pen, ps.ToArray());

            if(_SelectedType != SelectedType.None)
            {
                base.DrawCircle(e.Graphics, new Pen(Color.Green, 3), new PointF((ps[2].X + ps[1].X) / 2, (ps[2].Y + ps[1].Y) / 2), 5);
            }

        }


        
    }
}
