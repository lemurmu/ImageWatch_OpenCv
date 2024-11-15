using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageExDemo
{
    public class CvDisplayGraphicsShapeCollection :List<CvDisplayGraphicsShape>
    {

        public CvDisplayGraphicsShapeCollection():this(null)
        {

        }

        public CvDisplayGraphicsShapeCollection(CvDisplayGraphicsMat gMat)
        {
            ParentMat = gMat;
        }

        private CvDisplayGraphicsMat _ParentMat;
        public CvDisplayGraphicsMat ParentMat
        {
            get
            {
                return _ParentMat;
            }
            set
            {
                _ParentMat = value;
                foreach(CvDisplayGraphicsShape shape in this)
                {
                    shape.ParentMat = ParentMat;
                }
            }
        }


        public new void Add(CvDisplayGraphicsShape shape)
        {
            base.Add(shape);
            shape.ParentMat = ParentMat;
        }

        public new void AddRange(IEnumerable<CvDisplayGraphicsShape> range)
        {
            base.AddRange(range);
            foreach (CvDisplayGraphicsShape shape in range)
                shape.ParentMat = ParentMat;

        }

        public new void Insert(int index,CvDisplayGraphicsShape shape)
        {
            base.Insert(index,shape);
            shape.ParentMat = this.ParentMat;
        }


        public new void InsertRange(int index, IEnumerable<CvDisplayGraphicsShape> range)
        {
            base.InsertRange(index, range);
            foreach (CvDisplayGraphicsShape shape in range)
                shape.ParentMat = ParentMat;
        }

        public new void Clear()
        {
            
            foreach(CvDisplayGraphicsShape obj in this)
            {
                obj.Dispose();
            }
            base.Clear();
        }


       
    }
}
