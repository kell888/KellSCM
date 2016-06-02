using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Net;

namespace KellSCM
{
    /// <summary>
    /// 串口帮助静态类
    /// </summary>
    public static class ComUtility
    {
        /// <summary>
        /// 获取十六进制的字符串
        /// </summary>
        /// <param name="data"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string GetHex(byte data, bool format = false)
        {
            string fo = string.Empty;
            if (format)
                fo = "0x";
            return fo + data.ToString("X2");
        }
        /// <summary>
        /// 将十六进制的字符串格式化为标准形式
        /// </summary>
        /// <param name="hex"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string Format(string hex, bool format)
        {
            string fo = string.Empty;
            string[] fs = hex.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            if (fs.Length > 1)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string f in fs)
                {
                    if (format)
                    {
                        if (!f.StartsWith("0x"))
                            sb.Append("0x" + f);
                        else
                            sb.Append(f);
                    }
                    else
                    {
                        if (f.StartsWith("0x"))
                            sb.Append(f.Substring(2));
                        else
                            sb.Append(f);
                    }
                    sb.Append(" ");
                }
                fo = sb.ToString().TrimEnd(' ');
            }
            else
            {
                fo = hex;
                if (format)
                {
                    if (!hex.StartsWith("0x"))
                        fo = "0x" + hex;
                }
                else
                {
                    if (hex.StartsWith("0x"))
                        fo = hex.Substring(2);
                }
            }
            return fo;
        }
        /// <summary>
        /// 获取十六进制的字符串
        /// </summary>
        /// <param name="data"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string GetHex(byte[] data, bool format = false)
        {
            string fo = string.Empty;
            if (format)
                fo = "0x";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(fo + data[i].ToString("X2") + " ");
            }
            return sb.ToString();
        }
        /// <summary>
        /// 十六进制转换为字节数组
        /// </summary>
        /// <param name="StrHex"></param>
        /// <returns></returns>
        public static byte[] StrHexToBin(string StrHex)
        {
            StrHex = StrHex.Trim();
            string[] temp = StrHex.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            byte[] buf = new byte[temp.Length];
            for (int i = 0; i < temp.Length; i++)
            {
                buf[i] = System.Convert.ToByte(temp[i], 16);
            }
            return buf;
        }
        /// <summary>
        /// 获取默认的正规索引数组
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static int[] GetDefaultIndexs(int length)
        {
            int[] indexs = new int[length];
            for (int i = 0; i < length; i++)
            {
                indexs[i] = i;
            }
            return indexs;
        }
        /// <summary>
        /// 合并两个字节数组为一个大数组
        /// </summary>
        /// <param name="data1"></param>
        /// <param name="data2"></param>
        /// <returns></returns>
        public static byte[] MergeData(byte[] data1, byte[] data2)
        {
            int len1 = 0, len2 = 0;
            if (data1 != null) len1 = data1.Length;
            if (data2 != null) len2 = data2.Length;
            byte[] data = new byte[len1 + len2];
            if (data.Length > 0)
            {
                for (int i = 0; i < len1; i++)
                {
                    data[i] = data1[i];
                }
                for (int i = 0; i < len2; i++)
                {
                    data[len1 + i] = data2[i];
                }
            }
            return data;
        }
        /// <summary>
        /// 字节数组转换为对象数组
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static object[] Convert(byte[] data)
        {
            object[] result = null;
            if (data != null)
            {
                result = new object[data.Length];
                data.CopyTo(result, 0);
            }
            return result;
        }
        /// <summary>
        /// 获取UInt16数字的高低字节的值
        /// </summary>
        /// <param name="pari"></param>
        /// <param name="pari1"></param>
        /// <param name="pari2"></param>
        public static void GetBytes(ushort pari, out byte pari1, out byte pari2)
        {
            byte[] data = System.BitConverter.GetBytes(pari);
            pari1 = data[0];
            pari2 = data[1];
        }
    }
}
