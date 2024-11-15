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
using static System.Net.Mime.MediaTypeNames;
using Point = OpenCvSharp.Point;
using Size = OpenCvSharp.Size;

namespace ImageExDemo
{
    /// <summary>
    /// 需要绘制的Mat对象
    /// </summary>
    public class CvDisplayGraphicsMat : CvDisplayGraphicsObject
    {
        protected Mat _Image = null;
        public Mat Image {
            get {
                return _Image;
            }
            set {
                if (_Image != null) {
                    _Image.Dispose();
                    _Image = null;
                }
                if (value != null)
                    _Image = new Mat(value, new Rect(0, 0, value.Width, value.Height));
                Reset();
            }
        }

        /// <summary>
        /// 实际显示在屏幕区域内的ROI
        /// </summary>
        public Rect2d DispRect {
            get {
                return new Rect2d(DisplayOrigin, _displaySize);
            }
        }

        /// <summary>
        /// 实际显示在屏幕可见区域内的图片ROI
        /// </summary>
        public Rect ShowMatRect { get; protected set; }

        /// <summary>
        /// 单像素在屏幕中显示的大小
        /// </summary>
        public override Size2d PixelSize {
            get {
                return _pixelSize;

            }
            set {
                _pixelSize = value;
                if (Image == null)
                    _displaySize = new Size2d(0, 0);
                else
                    _displaySize = new Size2d(
                        Image.Width * _pixelSize.Width, Image.Height * _pixelSize.Height
                        );

            }
        }

        protected Size2d _displaySize;

        /// <summary>
        /// 整张图片需要显示在屏幕中的大小
        /// </summary>
        public Size2d DisplaySize {
            get {
                return _displaySize;
            }
        }


        public CvDisplayGraphicsShapeCollection GraphicsShapes {
            get; protected set;
        }


        public CvDisplayGraphicsMat() {
            PixelSize = new Size2d(1, 1);

            GraphicsShapes = new CvDisplayGraphicsShapeCollection(this);
        }



        #region override


        public override void OnMouseDown(MouseEventArgs e) {
            base.OnMouseDown(e);
        }

        public override void OnMouseMove(MouseEventArgs e) {
            base.OnMouseMove(e);
        }
        public override void Reset() {
            base.Reset();
            PixelSize = new Size2d(1, 1);

        }
        public override void Dispose() {
            if (_Image != null) {
                _Image.Dispose();
            }
            base.Dispose();

        }

        public bool ShowRgb { set; get; }
        public override void OnPaint(PaintEventArgs e) {
            if (Image != null) {
                try {
                    Rect showMatRect = new Rect(); //需要裁减的图片范围
                    System.Drawing.PointF drawImageStartPos = new System.Drawing.PointF(); //绘制showMatRect的起始点
                    if (DispRect.X < 0) {
                        //显示区域的起始点X不在屏幕内
                        showMatRect.X = (int)(Math.Abs(DispRect.X) / PixelSize.Width);
                        drawImageStartPos.X = (float)(showMatRect.X * PixelSize.Width + DispRect.X);
                    }
                    else {
                        showMatRect.X = 0;
                        drawImageStartPos.X = (float)DispRect.X;
                    }
                    showMatRect.Width = (int)((e.ClipRectangle.Width - drawImageStartPos.X) / PixelSize.Width) + 1;

                    if (DispRect.Y < 0) {
                        //显示区域的起始点Y不在屏幕内
                        showMatRect.Y = (int)(Math.Abs(DispRect.Y) / PixelSize.Height);
                        drawImageStartPos.Y = (float)(showMatRect.Y * PixelSize.Height + DispRect.Y);
                    }
                    else {
                        showMatRect.Y = 0;
                        drawImageStartPos.Y = (float)DispRect.Y;
                    }
                    showMatRect.Height = (int)((e.ClipRectangle.Height - drawImageStartPos.Y) / PixelSize.Height) + 1;


                    AdjustMatRect(Image, ref showMatRect);//调整需要显示Mat区域，以免截取的区域超出图片范围

                    using (Mat displayMat = new Mat(Image, showMatRect)) {
                        //计算截取区域需要显示在屏幕中的大小
                        CvSize drawSize = new CvSize((int)(displayMat.Width * PixelSize.Width),
                       (int)(displayMat.Height * PixelSize.Height));

                        if (drawSize.Width < 1) drawSize.Width = 1;
                        if (drawSize.Height < 1) drawSize.Height = 1;
                        Mat resizeMat = new Mat();

                        //以Nearest的方式缩放图片尺寸
                        Cv2.Resize(displayMat, resizeMat, drawSize, 0, 0, InterpolationFlags.Nearest);

                        //缩放完的图片直接画在控件上
                        System.Drawing.Image drawImage = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(resizeMat);
                        e.Graphics.DrawImage(drawImage, drawImageStartPos);

                        // 遍历每个像素并绘制 RGB 值
                        if (PixelSize.Width > 40) {

                            for (int y = 0; y < displayMat.Height; y++) {
                                for (int x = 0; x < displayMat.Width; x++) {
                                    // 获取当前像素的 BGR 值
                                    Vec3b bgr = displayMat.At<Vec3b>(y, x);

                                    StringBuilder sb = new StringBuilder();
                                    sb.Append(bgr[2].ToString() + "\r\n");
                                    sb.Append(bgr[1].ToString() + "\r\n");
                                    sb.Append(bgr[0].ToString());

                                    string rgbText = sb.ToString(); // OpenCV 使用 BGR 格式
                                    float startX = drawImageStartPos.X;
                                    float startY = drawImageStartPos.Y;

                                    Font font = new Font("Microsoft Yahei ", 8, FontStyle.Regular);

                                    // 指定字符串格式
                                    StringFormat sf = new StringFormat();
                                    sf.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
                                    // 1. 计算字符串的高度和宽度（不限定高度和宽度）
                                    SizeF sizeF = e.Graphics.MeasureString(rgbText, font, new PointF(0, 0), sf);
                                    float xp = (float)(startX + x * PixelSize.Width + PixelSize.Width / 2.0f - sizeF.Width / 2);
                                    float yp = (float)(startY + y * PixelSize.Height + PixelSize.Height / 2.0f - sizeF.Height / 2);
                                    PointF point = new PointF(xp, yp);
                                    Pen pen = new Pen(Color.Gray, 1);

                                    e.Graphics.DrawRectangle(pen, (float)(startX + x * PixelSize.Width), (float)(startY + y * PixelSize.Height), (float)(PixelSize.Width), (float)(PixelSize.Height));
                                    e.Graphics.FillRectangle(new SolidBrush(Color.Gray), xp - 2, yp - 2, sizeF.Width + 2, sizeF.Height + 2);
                                    e.Graphics.DrawString(rgbText, font, Brushes.White, point);
                                }
                            }
                        }
                    }
                    ShowMatRect = showMatRect;
                }
                catch (Exception ex) {
                    Console.WriteLine(ex.Message);
                }
            }

            foreach (CvDisplayGraphicsShape shape in GraphicsShapes) {
                shape.OnPaint(e);
            }
        }

        public override bool IsMouseIn(PointF pos) {
            return DispRect.Contains(pos.X, pos.Y);
        }


        #endregion

        #region public method


        /// <summary>
        /// 转换屏幕坐标为图片中的像素坐标
        /// </summary>
        /// <param name="pos">屏幕坐标</param>
        /// <returns></returns>
        public CvPoint TransformPixelPostion(SdPoint pos) {
            CvPoint res = new CvPoint(-1, -1);
            if (IsMouseIn(pos)) {
                res.X = (int)((pos.X - DispRect.X) / PixelSize.Width);
                res.Y = (int)((pos.Y - DispRect.Y) / PixelSize.Height);
            }
            return res;
        }
        #endregion

        #region protected method

        /// <summary>
        /// 调整显示的图片区域，以免截取的mat越界
        /// </summary>
        /// <param name="mt"></param>
        /// <param name="rect"></param>
        protected void AdjustMatRect(Mat mt, ref Rect rect) {
            //调整XY坐标
            if (rect.X < 0)
                rect.X = 0;
            if (rect.X >= mt.Width)
                rect.X = mt.Width - 1;
            if (rect.Y < 0)
                rect.Y = 0;
            if (rect.Y >= mt.Height)
                rect.Y = mt.Height - 1;

            //调整长宽
            if (rect.Width + rect.X > mt.Width)
                rect.Width = mt.Width - rect.X;
            if (rect.Height + rect.Y > mt.Height)
                rect.Height = mt.Height - rect.Y;
        }
        #endregion
    }
}
