using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace KellSCM
{
    /// <summary>
    /// 配置项静态类
    /// </summary>
    public static class Const
    {
        /// <summary>
        /// 读取单片机的超时时间（默认为1000毫秒）
        /// </summary>
        public static int ReadStatusTimeout
        {
            get
            {
                int timeout = 1000;
                string timeoutStr = ConfigurationManager.AppSettings["ReadStatusTimeout"];
                if (!string.IsNullOrEmpty(timeoutStr))
                {
                    int R;
                    if (int.TryParse(timeoutStr, out R))
                        timeout = R;
                }
                return timeout;
            }
        }
        /// <summary>
        /// 通道数（默认为4）
        /// </summary>
        public static int ChannelCount
        {
            get
            {
                int count = 4;
                string channels = ConfigurationManager.AppSettings["ChannelCount"];
                if (!string.IsNullOrEmpty(channels))
                {
                    int R;
                    if (int.TryParse(channels, out R))
                        count = R;
                }
                return count;
            }
        }
        /// <summary>
        /// 单片机的写命令头
        /// </summary>
        public static string sHead
        {
            get
            {
                string shead = ConfigurationManager.AppSettings["sHead"];
                if (!string.IsNullOrEmpty(shead))
                    return shead;
                return "3A";
            }
        }
        /// <summary>
        /// 单片机的读命令头
        /// </summary>
        public static string rHead
        {
            get
            {
                string rhead = ConfigurationManager.AppSettings["rHead"];
                if (!string.IsNullOrEmpty(rhead))
                    return rhead;
                return "A3";
            }
        }
        /// <summary>
        /// 单片机的命令尾
        /// </summary>
        public static string tail
        {
            get
            {
                string tailStr = ConfigurationManager.AppSettings["tail"];
                if (!string.IsNullOrEmpty(tailStr))
                    return tailStr;
                return "0D 0A";
            }
        }
        /// <summary>
        /// 单片机的控制命令
        /// </summary>
        public static string sCmd
        {
            get
            {
                string scmd = ConfigurationManager.AppSettings["sCmd"];
                if (!string.IsNullOrEmpty(scmd))
                    return scmd;
                return "88";
            }
        }
        /// <summary>
        /// 单片机的获取状态命令
        /// </summary>
        public static string gCmd
        {
            get
            {
                string gcmd = ConfigurationManager.AppSettings["gCmd"];
                if (!string.IsNullOrEmpty(gcmd))
                    return gcmd;
                return "99";
            }
        }
        /// <summary>
        /// 闭合第1通道
        /// </summary>
        public static string setK1
        {
            get
            {
                string setK1Str = ConfigurationManager.AppSettings["setK1"];
                if (!string.IsNullOrEmpty(setK1Str))
                    return setK1Str;
                return "01 01 FF";
            }
        }
        /// <summary>
        /// 闭合第2通道
        /// </summary>
        public static string setK2
        {
            get
            {
                string setK2Str = ConfigurationManager.AppSettings["setK2"];
                if (!string.IsNullOrEmpty(setK2Str))
                    return setK2Str;
                return "01 02 FF";
            }
        }
        /// <summary>
        /// 闭合第3通道
        /// </summary>
        public static string setK3
        {
            get
            {
                string setK3Str = ConfigurationManager.AppSettings["setK3"];
                if (!string.IsNullOrEmpty(setK3Str))
                    return setK3Str;
                return "01 03 FF";
            }
        }
        /// <summary>
        /// 闭合第4通道
        /// </summary>
        public static string setK4
        {
            get
            {
                string setK4Str = ConfigurationManager.AppSettings["setK4"];
                if (!string.IsNullOrEmpty(setK4Str))
                    return setK4Str;
                return "01 04 FF";
            }
        }
        /// <summary>
        /// 闭合第5通道
        /// </summary>
        public static string setK5
        {
            get
            {
                string setK5Str = ConfigurationManager.AppSettings["setK5"];
                if (!string.IsNullOrEmpty(setK5Str))
                    return setK5Str;
                return "01 05 FF";
            }
        }
        /// <summary>
        /// 闭合第6通道
        /// </summary>
        public static string setK6
        {
            get
            {
                string setK6Str = ConfigurationManager.AppSettings["setK6"];
                if (!string.IsNullOrEmpty(setK6Str))
                    return setK6Str;
                return "01 06 FF";
            }
        }
        /// <summary>
        /// 闭合第7通道
        /// </summary>
        public static string setK7
        {
            get
            {
                string setK7Str = ConfigurationManager.AppSettings["setK7"];
                if (!string.IsNullOrEmpty(setK7Str))
                    return setK7Str;
                return "01 07 FF";
            }
        }
        /// <summary>
        /// 闭合第8通道
        /// </summary>
        public static string setK8
        {
            get
            {
                string setK8Str = ConfigurationManager.AppSettings["setK8"];
                if (!string.IsNullOrEmpty(setK8Str))
                    return setK8Str;
                return "01 08 FF";
            }
        }
        /// <summary>
        /// 闭合所有通道
        /// </summary>
        public static string setAll
        {
            get
            {
                string setAllStr = ConfigurationManager.AppSettings["setAll"];
                if (!string.IsNullOrEmpty(setAllStr))
                    return setAllStr;
                return "01 00 FF";
            }
        }
        /// <summary>
        /// 断开第1通道
        /// </summary>
        public static string resetK1
        {
            get
            {
                string resetK1Str = ConfigurationManager.AppSettings["resetK1"];
                if (!string.IsNullOrEmpty(resetK1Str))
                    return resetK1Str;
                return "01 01 00";
            }
        }
        /// <summary>
        /// 断开第2通道
        /// </summary>
        public static string resetK2
        {
            get
            {
                string resetK2Str = ConfigurationManager.AppSettings["resetK2"];
                if (!string.IsNullOrEmpty(resetK2Str))
                    return resetK2Str;
                return "01 02 00";
            }
        }
        /// <summary>
        /// 断开第3通道
        /// </summary>
        public static string resetK3
        {
            get
            {
                string resetK3Str = ConfigurationManager.AppSettings["resetK3"];
                if (!string.IsNullOrEmpty(resetK3Str))
                    return resetK3Str;
                return "01 03 00";
            }
        }
        /// <summary>
        /// 断开第4通道
        /// </summary>
        public static string resetK4
        {
            get
            {
                string resetK4Str = ConfigurationManager.AppSettings["resetK4"];
                if (!string.IsNullOrEmpty(resetK4Str))
                    return resetK4Str;
                return "01 04 00";
            }
        }
        /// <summary>
        /// 断开第5通道
        /// </summary>
        public static string resetK5
        {
            get
            {
                string resetK5Str = ConfigurationManager.AppSettings["resetK5"];
                if (!string.IsNullOrEmpty(resetK5Str))
                    return resetK5Str;
                return "01 05 00";
            }
        }
        /// <summary>
        /// 断开第6通道
        /// </summary>
        public static string resetK6
        {
            get
            {
                string resetK6Str = ConfigurationManager.AppSettings["resetK6"];
                if (!string.IsNullOrEmpty(resetK6Str))
                    return resetK6Str;
                return "01 06 00";
            }
        }
        /// <summary>
        /// 断开第7通道
        /// </summary>
        public static string resetK7
        {
            get
            {
                string resetK7Str = ConfigurationManager.AppSettings["resetK7"];
                if (!string.IsNullOrEmpty(resetK7Str))
                    return resetK7Str;
                return "01 07 00";
            }
        }
        /// <summary>
        /// 断开第8通道
        /// </summary>
        public static string resetK8
        {
            get
            {
                string resetK8Str = ConfigurationManager.AppSettings["resetK8"];
                if (!string.IsNullOrEmpty(resetK8Str))
                    return resetK8Str;
                return "01 08 00";
            }
        }
        /// <summary>
        /// 断开所有通道
        /// </summary>
        public static string resetAll
        {
            get
            {
                string resetAllStr = ConfigurationManager.AppSettings["resetAll"];
                if (!string.IsNullOrEmpty(resetAllStr))
                    return resetAllStr;
                return "01 00 00";
            }
        }
        /// <summary>
        /// 获取第1通道的状态
        /// </summary>
        public static string getK1
        {
            get
            {
                string getK1Str = ConfigurationManager.AppSettings["getK1"];
                if (!string.IsNullOrEmpty(getK1Str))
                    return getK1Str;
                return "01 01";
            }
        }
        /// <summary>
        /// 获取第2通道的状态
        /// </summary>
        public static string getK2
        {
            get
            {
                string getK2Str = ConfigurationManager.AppSettings["getK2"];
                if (!string.IsNullOrEmpty(getK2Str))
                    return getK2Str;
                return "01 02";
            }
        }
        /// <summary>
        /// 获取第3通道的状态
        /// </summary>
        public static string getK3
        {
            get
            {
                string getK3Str = ConfigurationManager.AppSettings["getK3"];
                if (!string.IsNullOrEmpty(getK3Str))
                    return getK3Str;
                return "01 03";
            }
        }
        /// <summary>
        /// 获取第4通道的状态
        /// </summary>
        public static string getK4
        {
            get
            {
                string getK4Str = ConfigurationManager.AppSettings["getK4"];
                if (!string.IsNullOrEmpty(getK4Str))
                    return getK4Str;
                return "01 04";
            }
        }
        /// <summary>
        /// 获取第5通道的状态
        /// </summary>
        public static string getK5
        {
            get
            {
                string getK5Str = ConfigurationManager.AppSettings["getK5"];
                if (!string.IsNullOrEmpty(getK5Str))
                    return getK5Str;
                return "01 05";
            }
        }
        /// <summary>
        /// 获取第6通道的状态
        /// </summary>
        public static string getK6
        {
            get
            {
                string getK6Str = ConfigurationManager.AppSettings["getK6"];
                if (!string.IsNullOrEmpty(getK6Str))
                    return getK6Str;
                return "01 06";
            }
        }
        /// <summary>
        /// 获取第7通道的状态
        /// </summary>
        public static string getK7
        {
            get
            {
                string getK7Str = ConfigurationManager.AppSettings["getK7"];
                if (!string.IsNullOrEmpty(getK7Str))
                    return getK7Str;
                return "01 07";
            }
        }
        /// <summary>
        /// 获取第8通道的状态
        /// </summary>
        public static string getK8
        {
            get
            {
                string getK8Str = ConfigurationManager.AppSettings["getK8"];
                if (!string.IsNullOrEmpty(getK8Str))
                    return getK8Str;
                return "01 08";
            }
        }
        /// <summary>
        /// 获取所有通道的状态
        /// </summary>
        public static string getAll
        {
            get
            {
                string getAllStr = ConfigurationManager.AppSettings["getAll"];
                if (!string.IsNullOrEmpty(getAllStr))
                    return getAllStr;
                return "01 00";
            }
        }
        /// <summary>
        /// 串口号（默认为1，注意：为数字，不带COM这三个字符）
        /// </summary>
        public static string ComNum
        {
            get
            {
                string comNum = ConfigurationManager.AppSettings["ComNum"];
                if (!string.IsNullOrEmpty(comNum))
                    return comNum;
                return "1";
            }
        }
        /// <summary>
        /// 波特率（默认为9600）
        /// </summary>
        public static string BaudRate
        {
            get
            {
                string baudRate = ConfigurationManager.AppSettings["BaudRate"];
                if (!string.IsNullOrEmpty(baudRate))
                    return baudRate;
                return "9600";
            }
        }
        /// <summary>
        /// 数据位（默认为8）
        /// </summary>
        public static string DataBits
        {
            get
            {
                string dataBits = ConfigurationManager.AppSettings["DataBits"];
                if (!string.IsNullOrEmpty(dataBits))
                    return dataBits;
                return "8";
            }
        }
        /// <summary>
        /// 停止位（默认为One）
        /// </summary>
        public static string StopBits
        {
            get
            {
                string stopBits = ConfigurationManager.AppSettings["StopBits"];
                if (!string.IsNullOrEmpty(stopBits))
                    return stopBits;
                return "One";
            }
        }
        /// <summary>
        /// 校验方式（默认为None）
        /// </summary>
        public static string Parity
        {
            get
            {
                string parity = ConfigurationManager.AppSettings["Parity"];
                if (!string.IsNullOrEmpty(parity))
                    return parity;
                return "None";
            }
        }
    }
}
