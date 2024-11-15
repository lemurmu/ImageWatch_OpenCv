using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CvPoint = OpenCvSharp.Point;
using SdPoint = System.Drawing.Point;
using CvSize = OpenCvSharp.Size;
using SdSize = System.Drawing.Size;

namespace ImageExDemo
{

    public interface ICvDisplayGraphics
    {
        /// <summary>
        /// 在某绘图事件中重新绘制该图形
        /// </summary>
        /// <param name="e">OnPanit参数</param>
        void OnPaint(System.Windows.Forms.PaintEventArgs e);

        /// <summary>
        /// 屏幕坐标是否在该图形上
        /// </summary>
        /// <param name="pos"></param>
        /// <returns></returns>
        bool IsMouseIn(PointF pos);


        void OnMouseMove(MouseEventArgs e);


        void OnMouseDown(MouseEventArgs e);

        void OnMouseUp(MouseEventArgs e);
    }


    /// <summary>
    /// CvDisplay绘图对象基类
    /// </summary>
    public abstract class CvDisplayGraphicsObject : ICvDisplayGraphics ,IDisposable
    {

        public CvDisplayGraphicsObject()
        {
            Name = string.Empty;
            IsFocused = false;
        }

        public string Name { get; set; }
        /// <summary>
        /// 显示原点
        /// </summary>
        protected Point2d _DisplayOrigin = new Point2d(0,0);

        public virtual Point2d DisplayOrigin
        {
            get
            {
                return _DisplayOrigin;
            }
            set
            {
                Point2d old = _DisplayOrigin;
                _DisplayOrigin = value;
                OnDisplayOriginChanged(old, _DisplayOrigin);
            }
        }

        /// <summary>
        /// 是否获取鼠标焦点
        /// </summary>
        public bool IsFocused { get; set; }

        /// <summary>
        /// 是否被选中
        /// </summary>
        public bool Selected { get; set; }

        protected Size2d _pixelSize;

        public virtual Size2d PixelSize
        {
            get
            {
                return _pixelSize;
            }
            set
            {
                _pixelSize = value;
            }
        }

        protected bool IsLeftMouseDown = false;
        protected SdPoint MouseDownPos;

        public void Select()
        {
            Selected = true;
        }

        public void Focus()
        {
            IsFocused = true;
        }

        public virtual void OnMouseDown(MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                IsLeftMouseDown = true;
                MouseDownPos = e.Location;
                this.Selected = IsMouseIn(e.Location);
            }
        }

        public virtual void OnMouseMove(MouseEventArgs e)
        {

        }

        public virtual void OnMouseUp(MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                IsLeftMouseDown = false;
            }
        }

        public abstract void OnPaint(PaintEventArgs e);

        /// <summary>
        /// 参数重置
        /// </summary>
        public virtual void Reset()
        {
            DisplayOrigin = new Point2d(0, 0);
        }
        public virtual void Dispose()
        {

        }

        /// <summary>
        /// 把pos以origin为原点，转换成像素坐标值返回
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        protected virtual Point2d TranslateToPixelPos(Point2d origin, Point2d pos)
        {
            Point2d  res = new Point2d((pos.X - origin.X) / PixelSize.Width,
                (pos.Y - origin.Y) / PixelSize.Height);
            return res;
        }

        /// <summary>
        /// 把pos以origin为原点，转换成屏幕坐标值返回
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        protected virtual Point2d TranslateToScreenPos(Point2d origin, Point2d pos)
        {
            Point2d res = new Point2d(origin.X + PixelSize.Width * pos.X,
                origin.Y + PixelSize.Height * pos.Y);
            return res;
        }

        protected virtual void OnDisplayOriginChanged(Point2d oldPos,Point2d newPos)
        {

        }

        public abstract bool IsMouseIn(PointF pos);

        public bool IsMouseIn( SdPoint pos)
        {
            return IsMouseIn(new PointF(pos.X, pos.Y));
        }

    
    }
}
