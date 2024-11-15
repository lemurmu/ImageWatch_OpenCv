using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using OpenCvSharp;

namespace ImageExDemo
{
    public abstract class CvDisplayGraphicsShape : CvDisplayGraphicsObject
    {
        /// <summary>
        /// 显示颜色
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// 显示尺寸
        /// </summary>
        public float Size { get; set; }


        public Color FocusedColor { get; set; }
        public Point2d MatDisplayOrigin
        {
            get
            {
                return ParentMat == null ? new Point2d(0, 0) : ParentMat.DisplayOrigin;
            }
        }

        public override Size2d PixelSize
        {
            get
            {
                return ParentMat == null ? new Size2d(1, 1) : ParentMat.PixelSize;
            }
        }
        /// <summary>
        /// 显示在图片的位置
        /// </summary>
        public virtual CvDisplayGraphicsMat ParentMat
        {
            get;set;
        }

        public CvDisplayGraphicsShape()
        {
            ParentMat = null;
            Color = Color.Black;
            FocusedColor = Color.LightBlue;
            Size = 1;
        }


        protected bool IsFocusedOrSelected
        {
            get
            {
                return IsFocused || Selected;
            }
        }

        protected Color GetDrawColor()
        {
            return IsFocusedOrSelected ? FocusedColor : Color;
        }


        protected  float GetDrawSize()
        {
            return IsFocusedOrSelected ? Size + 1 : Size;
        }
     

        /// <summary>
        /// 将屏幕坐标系中的点p转换成Mat图片中的像素坐标
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        protected virtual Point2d TranslateToMatPos(Point2d p)
        {
            return TranslateToPixelPos(MatDisplayOrigin,p);
        }
       
        /// <summary>
        /// 将Mat图片中的像素坐标点转换成屏幕坐标系中的点
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        protected virtual Point2d TranslateToScreenPos(Point2d p)
        {
            return TranslateToScreenPos(MatDisplayOrigin,p);
        }

        protected virtual void DrawCircle(Graphics g, Pen pen, PointF p, float radius)
        {
            g.DrawEllipse(pen, new RectangleF(new PointF(p.X-radius,p.Y-radius), new SizeF(radius * 2, radius * 2)));
        }


        protected virtual void DrawRectangle(Graphics g,Pen pen, Rectangle rect)
        {
            g.DrawRectangle(pen, rect);
        }


        protected void DrawCross(Graphics g,PointF p,Color color,float lineLength=10)
        {
            Pen pen = new Pen(color, GetDrawSize());
            float halfLength = lineLength / 2;

            g.DrawLine(pen, p.X - halfLength, p.Y - halfLength,
                p.X + halfLength, p.Y + halfLength); //画十字的 左上到右下线

            g.DrawLine(pen, p.X - halfLength, p.Y + halfLength, 
                p.X + halfLength, p.Y - halfLength);
        }

    }
}
