using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageExDemo
{
    public static class MatHelper
    {

        #region 取图片指定坐标的通道数值
        public static int GetMatChannelValues(Mat mt, int x, int y, out object[] channelValues)
        {
            List<object> objres = new List<object>();
            if (x >= 0 && x < mt.Width && y >= 0 && y < mt.Height)
            {
                MatType type = mt.Type();
                string typeString = type.ToString();


                if (typeString.Contains("CV_8"))
                {
                    foreach (object obj in _GetMatChannelValues_Byte(mt, x, y))
                        objres.Add(obj);
                }
                else if (typeString.Contains("CV_16U"))
                {
                    foreach (object obj in _GetMatChannelValues_UInt16(mt, x, y))
                        objres.Add(obj);
                }
                else if (typeString.Contains("CV_16S"))
                {
                    foreach (object obj in _GetMatChannelValues_Int16(mt, x, y))
                        objres.Add(obj);
                }
                else if (typeString.Contains("CV_32S"))
                {
                    foreach (object obj in _GetMatChannelValues_Int32(mt, x, y))
                        objres.Add(obj);
                }
                else if (typeString.Contains("CV_32F"))
                {
                    foreach (object obj in _GetMatChannelValues_Float(mt, x, y))
                        objres.Add(obj);
                }
                else if (typeString.Contains("CV_64F"))
                {
                    foreach (object obj in _GetMatChannelValues_Double(mt, x, y))
                        objres.Add(obj);
                }
            }

            channelValues = objres.ToArray();
            return objres.Count;
        }

        //// 泛型T没办法转指针 T*,因而没办法写成泛型函数
        //static T[] _GetMatChannelValues<T>(Mat mt,int x,int y) where T:struct
        //{
        //    List<T> lstRes = new List<T>();
        //    int channels = mt.Type().Channels;
        //    unsafe
        //    {
        //        for (int i = 0; i < channels; i++)
        //        {
        //            IntPtr ptr = mt.Ptr(y);


        //            T* tPtr = (T*)ptr.ToPointer();
        //            lstRes.Add(tPtr[x * channels + i]);
        //        }
        //    }
        //    return lstRes.ToArray();
        //}


        static byte[] _GetMatChannelValues_Byte(Mat mt, int x, int y)
        {
            List<byte> lstRes = new List<byte>();
            int channels = mt.Type().Channels;
            unsafe
            {
                for (int i = 0; i < channels; i++)
                {
                    IntPtr ptr = mt.Ptr(y);
                    byte* tPtr = (byte*)ptr.ToPointer();
                    lstRes.Add(tPtr[x * channels + i]);
                }
            }
            return lstRes.ToArray();
        }

        static UInt16[] _GetMatChannelValues_UInt16(Mat mt, int x, int y)
        {
            List<UInt16> lstRes = new List<UInt16>();
            int channels = mt.Type().Channels;
            unsafe
            {
                for (int i = 0; i < channels; i++)
                {
                    IntPtr ptr = mt.Ptr(x);
                    UInt16* tPtr = (UInt16*)ptr.ToPointer();
                    lstRes.Add(tPtr[y * channels + i]);
                }
            }
            return lstRes.ToArray();
        }

        static Int16[] _GetMatChannelValues_Int16(Mat mt, int x, int y)
        {
            List<Int16> lstRes = new List<Int16>();
            int channels = mt.Type().Channels;
            unsafe
            {
                for (int i = 0; i < channels; i++)
                {
                    IntPtr ptr = mt.Ptr(x);
                    Int16* tPtr = (Int16*)ptr.ToPointer();
                    lstRes.Add(tPtr[y * channels + i]);
                }
            }
            return lstRes.ToArray();
        }

        static Int32[] _GetMatChannelValues_Int32(Mat mt, int x, int y)
        {
            List<Int32> lstRes = new List<Int32>();
            int channels = mt.Type().Channels;
            unsafe
            {
                for (int i = 0; i < channels; i++)
                {
                    IntPtr ptr = mt.Ptr(x);
                    Int32* tPtr = (Int32*)ptr.ToPointer();
                    lstRes.Add(tPtr[y * channels + i]);
                }
            }
            return lstRes.ToArray();
        }


        static float[] _GetMatChannelValues_Float(Mat mt, int x, int y)
        {
            List<float> lstRes = new List<float>();
            int channels = mt.Type().Channels;
            unsafe
            {
                for (int i = 0; i < channels; i++)
                {
                    IntPtr ptr = mt.Ptr(x);
                    float* tPtr = (float*)ptr.ToPointer();
                    lstRes.Add(tPtr[y * channels + i]);
                }
            }
            return lstRes.ToArray();
        }

        static double[] _GetMatChannelValues_Double(Mat mt, int x, int y)
        {
            List<double> lstRes = new List<double>();
            int channels = mt.Type().Channels;
            unsafe
            {
                for (int i = 0; i < channels; i++)
                {
                    IntPtr ptr = mt.Ptr(x);
                    double* tPtr = (double*)ptr.ToPointer();
                    lstRes.Add(tPtr[y * channels + i]);
                }
            }
            return lstRes.ToArray();
        }

       

        #endregion
    }
}
