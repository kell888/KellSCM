using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace KellSCM
{
    /// <summary>
    /// 日志静态类
    /// </summary>
    public static class Log
    {
        /// <summary>
        /// 日志级别
        /// </summary>
        public enum Level
        {
            /// <summary>
            /// 正常信息
            /// </summary>
            Info,
            /// <summary>
            /// 调试跟踪
            /// </summary>
            Trace,
            /// <summary>
            /// 程序及业务错误
            /// </summary>
            Error
        }
        static string path = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="module">来源</param>
        /// <param name="msg">日志内容</param>
        /// <param name="level">日志级别</param>
        public static void WriteLog(string module, string msg, Level level)
        {
            DateTime now = DateTime.Now;
            string p = path + level.ToString();
            if (!Directory.Exists(p))
                Directory.CreateDirectory(p);
            string m = "[" + now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + module + Environment.NewLine + msg + Environment.NewLine + Environment.NewLine;
            File.AppendAllText(p + "\\" + now.ToShortDateString() + ".log", m);
        }
    }
}
