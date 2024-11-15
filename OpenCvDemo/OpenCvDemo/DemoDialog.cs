using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenCvSharp;

namespace ImageExDemo
{
    public partial class DemoDialog : Form
    {

       
        public DemoDialog()
        {
            InitializeComponent();
        }



        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Mat mt = new Mat(ofd.FileName, ImreadModes.AnyColor);
                        _cvDisplay.Image = mt;
                        _cvDisplay.Fit();
                    }
                    
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void toolStripButtonClear_Click(object sender, EventArgs e)
        {
            _cvDisplay.Clear();
        }

        private void toolStripButtonDrawCircle_Click(object sender, EventArgs e)
        {
            double radius = _cvDisplay.GraphicsMat.ShowMatRect.Width / 8;
            double x = _cvDisplay.GraphicsMat.ShowMatRect.X + _cvDisplay.GraphicsMat.ShowMatRect.Width / 2,
                y = _cvDisplay.GraphicsMat.ShowMatRect.Y + _cvDisplay.GraphicsMat.ShowMatRect.Height / 2;
            CvDisplayGraphicsCircle circle = new CvDisplayGraphicsCircle(
               x ,y, radius
                );
            circle.Color = System.Drawing.Color.Green;
            _cvDisplay.GraphicsShapes.Add(circle);
        }

        private void toolStripButtonDrawDot_Click(object sender, EventArgs e)
        {
            
            double x = _cvDisplay.GraphicsMat.ShowMatRect.X + _cvDisplay.GraphicsMat.ShowMatRect.Width / 2,
               y = _cvDisplay.GraphicsMat.ShowMatRect.Y + _cvDisplay.GraphicsMat.ShowMatRect.Height / 2;
            CvDisplayGraphicsDot dot = new CvDisplayGraphicsDot(x, y);
            dot.Color = System.Drawing.Color.Red;
            _cvDisplay.GraphicsShapes.Add(dot);
        }

        private void toolStripButtonDrawLine_Click(object sender, EventArgs e)
        {
            double x = _cvDisplay.GraphicsMat.ShowMatRect.X + _cvDisplay.GraphicsMat.ShowMatRect.Width / 2,
           y = _cvDisplay.GraphicsMat.ShowMatRect.Y + _cvDisplay.GraphicsMat.ShowMatRect.Height / 2;
            double spacing = _cvDisplay.GraphicsMat.ShowMatRect.Width / 8;

            CvDisplayGraphicsLineSegment line = new CvDisplayGraphicsLineSegment(
                new Point2d(x-spacing, y), new Point2d(x + spacing, y));
            line.Color = System.Drawing.Color.Orange;
            _cvDisplay.GraphicsShapes.Add(line);


        }

        private void toolStripButtonDrawRect_Click(object sender, EventArgs e)
        {
            double x = _cvDisplay.GraphicsMat.ShowMatRect.X + _cvDisplay.GraphicsMat.ShowMatRect.Width / 2,
           y = _cvDisplay.GraphicsMat.ShowMatRect.Y + _cvDisplay.GraphicsMat.ShowMatRect.Height / 2;
            double width = _cvDisplay.GraphicsMat.ShowMatRect.Width / 4,
                height  = _cvDisplay.GraphicsMat.ShowMatRect.Height / 4;

            CvDisplayGraphicsRectangle rect = new CvDisplayGraphicsRectangle(new Point2d(x, y), new Size2d(width, height), 0);
            rect.Color = System.Drawing.Color.LightSeaGreen;
            _cvDisplay.GraphicsShapes.Add(rect);
        }
    }
}
