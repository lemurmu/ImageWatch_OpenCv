using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Point = OpenCvSharp.Point;
using Size = OpenCvSharp.Size;

namespace ImageExDemo
{
    class CvDisplay : PictureBox
    {
        #region 内部操作数据

     

        protected CvDisplayGraphicsMat _cdgMat; //Mat绘制类


        protected bool _isMouseMoving = false; //鼠标是否允许移动
        protected Point _mouseDownLocation; //鼠标点下的坐标

        protected System.Drawing.Point _mouseLocation; //鼠标实时位置


        protected Point _mousePixcelLocation; //鼠标放置位置的像素实际坐标



        #endregion



        #region 事件

        /// <summary>
        /// 当前像元位置变化
        /// </summary>
        public event EventHandler<PosChangedEventArgs> PositionChanged; 


        #endregion

        #region 公开属性


        public CvDisplayGraphicsMat GraphicsMat
        {
            get
            {
                return _cdgMat;
            }
        }

        public enum AutoDisplayMode
        {
            Original,
            Fit,
            Full
        }

        /// <summary>
        /// 绘图元素集合
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public CvDisplayGraphicsShapeCollection GraphicsShapes
        {
            get
            {
                return GraphicsMat.GraphicsShapes;
            }
        }

        [EditorBrowsable(EditorBrowsableState.Always)]
        [CategoryAttribute("CvDisplay"), DescriptionAttribute("自动显示图片模式")]
        public AutoDisplayMode AutoDisplay
        {
            get;
            set;
        }



        [EditorBrowsable(EditorBrowsableState.Always)]
        [CategoryAttribute("CvDisplay"), DescriptionAttribute("OpenCv2 Mat图片数据类")]
        public new Mat Image
        {
            get
            {
                return _cdgMat.Image;
            }
            set
            {
                _cdgMat.Image = value;
                ImageResize();
            }
        }


        [EditorBrowsable(EditorBrowsableState.Never)]
        public override Image BackgroundImage
        {
            get
            {
                return base.BackgroundImage;
            }
            set
            {
                base.BackgroundImage = null;
            }
        }


        #endregion

        public CvDisplay()
        {
            _cdgMat = new CvDisplayGraphicsMat();
            DoubleBuffered = true;

            AutoDisplay = AutoDisplayMode.Original;
            this.ContextMenuStrip = new ContextMenuStrip();

            ContextMenuStrip.Items.Add("Fit image", null, OnFitImageClick);
            ContextMenuStrip.Items.Add("Original image", null, OnOriginalImageClick);
            ContextMenuStrip.Items.Add("Full image", null, OnFullImageClick);

            ContextMenuStrip.Items.Add("Save as", null, OnSaveAsClick);


        }


        #region 事件处理

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            Fit();
        }
        protected virtual void OnFitImageClick(object sender, EventArgs e)
        {
            Fit();
        }

        protected virtual void OnOriginalImageClick(object sender, EventArgs e)
        {
            OriginalSize();
        }

        protected virtual void OnFullImageClick(object sender, EventArgs e)
        {
            Full();
        }

        protected virtual void OnSaveAsClick(object sender, EventArgs e)
        {
            if (Image == null) return;
            using (SaveFileDialog ofd = new SaveFileDialog())
            {
                ofd.Filter = "Bitmap|*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    SaveAs(ofd.FileName);
                }
            }
        }

        #endregion


        #region 父类重载
        protected override void OnMouseDown(MouseEventArgs e)
        {

            bool shapeMove = false;
            foreach(CvDisplayGraphicsShape shape in GraphicsShapes)
            {
                shape.OnMouseDown(e);
                shapeMove |= shape.Selected;
            }


            if (e.Button == MouseButtons.Left && !shapeMove)
            {
                this.Cursor = Cursors.SizeAll;
                _isMouseMoving = true;
                _mouseDownLocation = new Point(e.Location.X, e.Location.Y);
            }

            Refresh();
            base.OnMouseDown(e);
        }

        protected virtual void ImageResize()
        {
            switch (AutoDisplay)
            {
                case AutoDisplayMode.Original:
                    OriginalSize();
                    break;
                case AutoDisplayMode.Fit:
                    Fit();
                    break;
                case AutoDisplayMode.Full:
                    Full();
                    break;
            }
        }
        protected override void OnResize(EventArgs e)
        {
            if (this.Width != 0 && this.Height != 0)
            {
                //ImageResize();
            }

            base.OnResize(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            foreach (CvDisplayGraphicsShape shape in GraphicsShapes)
            {
                shape.OnMouseUp(e);
            }
            this.Cursor = Cursors.Default;
            _isMouseMoving = false;
            base.OnMouseUp(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                Zoom(2, 2, new PointF(e.X, e.Y));
            }
            else
            {
                Zoom(0.5, 0.5, new PointF(e.X, e.Y));
            }
            base.OnMouseWheel(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            _mouseLocation = e.Location;
       

            foreach(CvDisplayGraphicsShape shape in GraphicsShapes)
            {
                shape.OnMouseMove(e);
            }
            if (_isMouseMoving && Image != null)
            {
                //移动图片
                Point nowLocation = new Point(e.X, e.Y);
                Point move = (nowLocation - _mouseDownLocation);

                SyncUpdateOrigin( _cdgMat.DisplayOrigin + move);

                //Refresh();
                _mouseDownLocation = nowLocation;

            }
            else if (_cdgMat.IsMouseIn(e.Location))
            {
                //坐标在绘图区域内
                //记录实际像素点和颜色 ，提示在tooltip上
                this.Cursor = Cursors.Cross;
                Point p = _cdgMat.TransformPixelPostion(e.Location);
                if (!p.Equals(_mouseLocation) && !p.Equals(_mousePixcelLocation))
                {
                    string tip = string.Format("({0},{1})", p.X, p.Y);
                    object[] res = null;
                    MatHelper.GetMatChannelValues(Image, p.X, p.Y, out res);
                    tip += " [";
                    foreach (object obj in res)
                    {
                        tip += obj + ",";
                    }
                    tip = tip.Substring(0, tip.Length - 1) + ']';

                    Console.WriteLine(tip);

                    if (PositionChanged != null)
                        PositionChanged(this, new PosChangedEventArgs(p, res));
                }

                _mousePixcelLocation = p;
            }
            else
            {
                //坐标不在绘图区域内
                _mousePixcelLocation = new Point(-1, -1);
            }

            Refresh();
            base.OnMouseMove(e);
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics gh = e.Graphics;
            gh.Clear(this.BackColor);
            if (Image != null)
            {
                _cdgMat.OnPaint(e);

            }

        }

        #endregion

        #region 内部使用函数

        /// <summary>
        /// 同步更新所有绘图的原点
        /// </summary>
        /// <param name="p"></param>
        protected void SyncUpdateOrigin(Point2d p)
        {
            _cdgMat.DisplayOrigin = p;

        }
        static System.Drawing.Point ConvertCvPoint2DrawingPoint(Point p)
        {
            return new System.Drawing.Point(p.X, p.Y);
        }

        static Point ConvertDrawingPoint2CvPoint(System.Drawing.Point p)
        {
            return new Point(p.X, p.Y);
        }

       
        #endregion

        #region 对外接口

        /// <summary>
        /// 图片缩放
        /// </summary>
        /// <param name="scale">x,y等比例缩放参数</param>
        public void Zoom(double scale)
        {
            Zoom(scale, scale);
        }

        /// <summary>
        /// 另存为
        /// </summary>
        /// <param name="filepath"></param>
        public void SaveAs(string filepath)
        {
            if (Image == null) return;
            Cv2.ImWrite(filepath, Image);
        }

        /// <summary>
        /// 图片缩放
        /// </summary>
        /// <param name="xScale">x缩放参数</param>
        /// <param name="yScale">y缩放参数</param>
        public void Zoom(double xScale, double yScale)
        {
            Zoom(xScale, yScale, new PointF(0, 0));
        }

        /// <summary>
        /// 根据某个原点进行缩放
        /// </summary>
        /// <param name="xScale">x缩放参数</param>
        /// <param name="yScale">y缩放参数</param>
        /// <param name="zoomOrign">缩放参考点</param>
        public void Zoom(double xScale, double yScale, PointF zoomOrign)
        {
            if (Image == null) return;
            
            double newXPixelSize = Math.Abs(xScale) * _cdgMat.PixelSize.Width;
            double newYPixelSize = Math.Abs(yScale) * _cdgMat.PixelSize.Height;
            if (newXPixelSize > 0 && newYPixelSize > 0)
            {
                int dispPixelX = (int)(this.Width / newXPixelSize),
                    dispPixelY = (int)(this.Height / newYPixelSize);
                if (dispPixelX < 1 || dispPixelY < 1) //最少显示一个像素点
                    return;

              
                if (_cdgMat.IsMouseIn(zoomOrign)) //如果在聚焦在图片某点放大
                {
                    //变换前 图片绘制坐标原点距离 当前鼠标鼠标的距离
                    double disX = zoomOrign.X - _cdgMat.DisplayOrigin.X,
                        disY = zoomOrign.Y - _cdgMat.DisplayOrigin.Y;

                    //缩放后的距离
                    disX *= xScale;
                    disY *= yScale;

                    //同步更新所有需要绘图的元素的原点
                    SyncUpdateOrigin( new Point2d(zoomOrign.X - disX, zoomOrign.Y - disY));
                }
                _cdgMat.PixelSize = new Size2d(newXPixelSize, newYPixelSize);
                Refresh();
            }
        }

        /// <summary>
        /// 整个图片充满控件
        /// </summary>
        public virtual void Full()
        {
            if (Image == null) return;
            //换算单个像素尺寸
            _cdgMat.PixelSize= new Size2d( this.Width / (double)Image.Width,this.Height / (double)Image.Height);

            _cdgMat.DisplayOrigin = new Point2d(0, 0);
           
            Refresh();
        }

        /// <summary>
        /// 自适应图片的横纵比最大化
        /// </summary>
        public virtual void Fit()
        {
            if (Image == null) return;
            Size2d newsize = new Size2d();
            double hvScale1 = this.Width / (double)this.Height,//控件横纵比
            hvScale2 = Image.Width / (double)Image.Height;//图片横纵比


            //根据横纵比算出实际上画图的大小
            if (hvScale1 > hvScale2)
            {
                newsize.Height = this.Height;
                newsize.Width = (Image.Width * ((double)newsize.Height / Image.Height));
            }
            else
            {
                newsize.Width = this.Width;
                newsize.Height = (Image.Height * ((double)newsize.Width / Image.Width));
            }


            //计算单像素尺寸
            _cdgMat.PixelSize =new Size2d( newsize.Width / (double)Image.Width, newsize.Height / (double)Image.Height);

            SyncUpdateOrigin(new Point2d((this.Width - _cdgMat.DispRect.Width) / 2,
                (this.Height - _cdgMat.DispRect.Height) / 2));

            Refresh();
        }

        /// <summary>
        /// 恢复图片原始比例
        /// </summary>
        public virtual void OriginalSize()
        {
            if (Image == null) return;
            _cdgMat.PixelSize = new Size2d(1, 1);
           SyncUpdateOrigin( new Point2d(0, 0));

            Refresh();
        }


        public virtual void Clear()
        {
            Image = null;
            GraphicsShapes.Clear();
            Refresh(); 
        }
        #endregion

    }
}
