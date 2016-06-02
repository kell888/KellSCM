using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;

namespace KellSCM
{
    /// <summary>
    /// 串口读到数据的事件参数类
    /// </summary>
    public class ReadDataArgs : EventArgs
    {
        /// <summary>
        /// 串口号
        /// </summary>
        public string ComName { get; private set; }
        /// <summary>
        /// 读到的实际数据
        /// </summary>
        public byte[] Data { get; private set; }
        /// <summary>
        /// 读到的开关状态（当通道号为0时，数组内所有的元素都有效，否则只有第一个元素有效）
        /// </summary>
        public bool[] SwicthStatus { get; private set; }
        /// <summary>
        /// 通道号（大于0时表示某一个通道，为0时表示全部通道）
        /// </summary>
        public int ChannelNum { get; private set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="comName"></param>
        /// <param name="data"></param>
        /// <param name="switchStatus"></param>
        /// <param name="channelNum"></param>
        public ReadDataArgs(string comName, byte[] data, bool[] switchStatus, int channelNum)
        {
            this.ComName = comName;
            this.Data = data;
            this.SwicthStatus = switchStatus;
            this.ChannelNum = channelNum;
        }
        /// <summary>
        /// 预览信息
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            List<string> switchs = new List<string>();
            foreach (bool b in this.SwicthStatus)
            {
                switchs.Add(b ? "开" : "关");
            }
            return this.ComName + "：" + Environment.NewLine + "通道：" + this.ChannelNum + Environment.NewLine + "状态：" + string.Join(",", switchs.ToArray());
        }
    }
    /// <summary>
    /// 串口读到数据的委托
    /// </summary>
    /// <returns></returns>
    public delegate void ReadedHandler(object sender, ReadDataArgs e);
    /// <summary>
    /// 单片机控制器类（支持4通道和8通道两种单片机）
    /// </summary>
    public class Controller
    {
        byte sHead = Convert.ToByte(Const.sHead, 16);
        byte rHead = Convert.ToByte(Const.rHead, 16);
        byte[] tail = ComUtility.StrHexToBin(Const.tail);
        byte sCmd = Convert.ToByte(Const.sCmd, 16);
        byte gCmd = Convert.ToByte(Const.gCmd, 16);
        byte[] setK1 = ComUtility.StrHexToBin(Const.setK1);
        byte[] setK2 = ComUtility.StrHexToBin(Const.setK2);
        byte[] setK3 = ComUtility.StrHexToBin(Const.setK3);
        byte[] setK4 = ComUtility.StrHexToBin(Const.setK4);
        byte[] setK5 = ComUtility.StrHexToBin(Const.setK5);
        byte[] setK6 = ComUtility.StrHexToBin(Const.setK6);
        byte[] setK7 = ComUtility.StrHexToBin(Const.setK7);
        byte[] setK8 = ComUtility.StrHexToBin(Const.setK8);
        byte[] setAll = ComUtility.StrHexToBin(Const.setAll);
        byte[] resetK1 = ComUtility.StrHexToBin(Const.resetK1);
        byte[] resetK2 = ComUtility.StrHexToBin(Const.resetK2);
        byte[] resetK3 = ComUtility.StrHexToBin(Const.resetK3);
        byte[] resetK4 = ComUtility.StrHexToBin(Const.resetK4);
        byte[] resetK5 = ComUtility.StrHexToBin(Const.resetK5);
        byte[] resetK6 = ComUtility.StrHexToBin(Const.resetK6);
        byte[] resetK7 = ComUtility.StrHexToBin(Const.resetK7);
        byte[] resetK8 = ComUtility.StrHexToBin(Const.resetK8);
        byte[] resetAll = ComUtility.StrHexToBin(Const.resetAll);
        byte[] getK1 = ComUtility.StrHexToBin(Const.getK1);
        byte[] getK2 = ComUtility.StrHexToBin(Const.getK2);
        byte[] getK3 = ComUtility.StrHexToBin(Const.getK3);
        byte[] getK4 = ComUtility.StrHexToBin(Const.getK4);
        byte[] getK5 = ComUtility.StrHexToBin(Const.getK5);
        byte[] getK6 = ComUtility.StrHexToBin(Const.getK6);
        byte[] getK7 = ComUtility.StrHexToBin(Const.getK7);
        byte[] getK8 = ComUtility.StrHexToBin(Const.getK8);
        byte[] getAll = ComUtility.StrHexToBin(Const.getAll);
        volatile int readed1 = -1, readed2 = -1, readed3 = -1, readed4 = -1, readed5 = -1, readed6 = -1, readed7 = -1, readed8 = -1;
        System.IO.Ports.SerialPort sp;

        /// <summary>
        /// 获取指定通道的状态（-1为未知，0为断开，1为闭合）
        /// </summary>
        /// <param name="channel">指定通道（取值范围：1-8）</param>
        /// <returns></returns>
        public int GetChannelStatus(int channel)
        {
            int status = -1;
            switch (channel)
            {
                case 1:
                    status = readed1;
                    break;
                case 2:
                    status = readed2;
                    break;
                case 3:
                    status = readed3;
                    break;
                case 4:
                    status = readed4;
                    break;
                case 5:
                    status = readed5;
                    break;
                case 6:
                    status = readed6;
                    break;
                case 7:
                    status = readed7;
                    break;
                case 8:
                    status = readed8;
                    break;
            }
            return status;
        }

        /// <summary>
        /// 串口读到有效数据的事件
        /// </summary>
        public event ReadedHandler Readed;

        private void OnReaded(ReadDataArgs e)
        {
            if (Readed != null)
                Readed(this, e);
        }

        /// <summary>
        /// 控制器构造函数
        /// </summary>
        public Controller()
        {
            sp = new System.IO.Ports.SerialPort();
            sp.PortName = "COM" + Const.ComNum;
            sp.BaudRate = Convert.ToInt32(Const.BaudRate);
            sp.DataBits = Convert.ToInt32(Const.DataBits);
            sp.StopBits = (StopBits)Enum.Parse(typeof(StopBits), Const.StopBits);
            sp.Parity = (Parity)Enum.Parse(typeof(Parity), Const.Parity);
            sp.ReadTimeout = Const.ReadStatusTimeout;
            sp.DataReceived += new SerialDataReceivedEventHandler(sp_DataReceived);
        }

        void sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            System.Threading.Thread.Sleep(20);
            if (sp.BytesToRead >= 3)
            {
                try
                {
                    byte[] buf = new byte[sp.BytesToRead];
                    sp.Read(buf, 0, buf.Length);
                    if (buf.Length == 3 && buf[0] == rHead && buf[1] == tail[0] && buf[2] == tail[1])
                    {
                        Log.WriteLog("sp_DataReceived", "命令执行失败，发送的数据有误或者单片机硬件故障！", Log.Level.Error);
                    }
                    else
                    {
                        if (buf[0] == rHead)
                        {
                            bool[] ks = new bool[8];
                            int k = 0;
                            if (Const.ChannelCount == 4)
                            {
                                if ((buf[4] == tail[0]) && (buf[5] == tail[1]))
                                {
                                    k = (int)buf[2];
                                    switch (k)
                                    {
                                        case 1:
                                            ks[0] = buf[3] == 255;
                                            readed1 = ks[0] ? 1 : 0;
                                            break;
                                        case 2:
                                            ks[1] = buf[3] == 255;
                                            readed2 = ks[1] ? 1 : 0;
                                            break;
                                        case 3:
                                            ks[2] = buf[3] == 255;
                                            readed3 = ks[2] ? 1 : 0;
                                            break;
                                        case 4:
                                            ks[3] = buf[3] == 255;
                                            readed4 = ks[3] ? 1 : 0;
                                            break;
                                    }
                                }
                                else if (buf[6] == tail[0] && buf[7] == tail[1])
                                {
                                    ks[0] = buf[2] == 255;
                                    ks[1] = buf[3] == 255;
                                    ks[2] = buf[4] == 255;
                                    ks[3] = buf[5] == 255;
                                    readed1 = ks[0] ? 1 : 0;
                                    readed2 = ks[1] ? 1 : 0;
                                    readed3 = ks[2] ? 1 : 0;
                                    readed4 = ks[3] ? 1 : 0;
                                }
                                OnReaded(new ReadDataArgs("COM" + Const.ComNum, buf, ks, k));
                            }
                            else if (Const.ChannelCount == 8)
                            {
                                if ((buf[4] == tail[0]) && (buf[5] == tail[1]))
                                {
                                    k = (int)buf[2];
                                    switch (k)
                                    {
                                        case 1:
                                            ks[0] = buf[3] == 255;
                                            readed1 = ks[0] ? 1 : 0;
                                            break;
                                        case 2:
                                            ks[1] = buf[3] == 255;
                                            readed2 = ks[1] ? 1 : 0;
                                            break;
                                        case 3:
                                            ks[2] = buf[3] == 255;
                                            readed3 = ks[2] ? 1 : 0;
                                            break;
                                        case 4:
                                            ks[3] = buf[3] == 255;
                                            readed4 = ks[3] ? 1 : 0;
                                            break;
                                        case 5:
                                            ks[4] = buf[3] == 255;
                                            readed5 = ks[4] ? 1 : 0;
                                            break;
                                        case 6:
                                            ks[5] = buf[3] == 255;
                                            readed6 = ks[5] ? 1 : 0;
                                            break;
                                        case 7:
                                            ks[6] = buf[3] == 255;
                                            readed7 = ks[6] ? 1 : 0;
                                            break;
                                        case 8:
                                            ks[7] = buf[3] == 255;
                                            readed8 = ks[7] ? 1 : 0;
                                            break;
                                    }
                                }
                                else if (buf[10] == tail[0] && buf[11] == tail[1])
                                {
                                    ks[0] = buf[2] == 255;
                                    ks[1] = buf[3] == 255;
                                    ks[2] = buf[4] == 255;
                                    ks[3] = buf[5] == 255;
                                    ks[4] = buf[6] == 255;
                                    ks[5] = buf[7] == 255;
                                    ks[6] = buf[8] == 255;
                                    ks[7] = buf[9] == 255;
                                    readed1 = ks[0] ? 1 : 0;
                                    readed2 = ks[1] ? 1 : 0;
                                    readed3 = ks[2] ? 1 : 0;
                                    readed4 = ks[3] ? 1 : 0;
                                    readed5 = ks[4] ? 1 : 0;
                                    readed6 = ks[5] ? 1 : 0;
                                    readed7 = ks[6] ? 1 : 0;
                                    readed8 = ks[7] ? 1 : 0;
                                }
                                OnReaded(new ReadDataArgs("COM" + Const.ComNum, buf, ks, k));
                            }
                        }
                    }
                }
                catch (TimeoutException ex)
                {
                    Log.WriteLog("sp_DataReceived", "接收数据超时，请检查单片机线路和电源。" + Environment.NewLine + ex.ToString(), Log.Level.Error);
                }
                catch (IndexOutOfRangeException ex)
                {
                    Log.WriteLog("sp_DataReceived", "接收到数据，但是数据长度有误，请确认配置项是否配置正确（默认为4通道），当前ChannelCount=" + Const.ChannelCount + Environment.NewLine + ex.ToString(), Log.Level.Error);
                }
            }
        }

        /// <summary>
        /// 检查串口能否连接
        /// </summary>
        /// <returns></returns>
        public bool TestConnect()
        {
            try
            {
                if (sp.IsOpen)
                {
                    return true;
                }
                else
                {
                    sp.Open();
                    if (sp.IsOpen)
                    {
                        sp.Close();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {
                Log.WriteLog("TestConnect", "测试连接出错：" + Environment.NewLine + e.ToString(), Log.Level.Info);
            }
            return false;
        }

        /// <summary>
        /// 往串口发送命令数据
        /// </summary>
        /// <param name="channel">单片机的通道（0为全部通道，单个通道从1开始下去...）</param>
        /// <param name="set">true为开，false为关</param>
        /// <param name="read">是否为读命令（默认为false写）</param>
        /// <returns></returns>
        public bool Send(int channel, bool set, bool read = false)
        {
            bool flag = false;
            byte[] package = null;
            try
            {
                if (!sp.IsOpen)
                {
                    sp.Open();
                }
                if (sp.IsOpen)
                {
                    byte[] realData = null;
                    switch (channel)
                    {
                        case 1:
                            if (read)
                                realData = getK1;
                            else
                            {
                                if (set)
                                    realData = setK1;
                                else
                                    realData = resetK1;
                            }
                            break;
                        case 2:
                            if (read)
                                realData = getK2;
                            else
                            {
                                if (set)
                                    realData = setK2;
                                else
                                    realData = resetK2;
                            }
                            break;
                        case 3:
                            if (read)
                                realData = getK3;
                            else
                            {
                                if (set)
                                    realData = setK3;
                                else
                                    realData = resetK3;
                            }
                            break;
                        case 4:
                            if (read)
                                realData = getK4;
                            else
                            {
                                if (set)
                                    realData = setK4;
                                else
                                    realData = resetK4;
                            }
                            break;
                        case 5:
                            if (read)
                                realData = getK5;
                            else
                            {
                                if (set)
                                    realData = setK5;
                                else
                                    realData = resetK5;
                            }
                            break;
                        case 6:
                            if (read)
                                realData = getK6;
                            else
                            {
                                if (set)
                                    realData = setK6;
                                else
                                    realData = resetK6;
                            }
                            break;
                        case 7:
                            if (read)
                                realData = getK7;
                            else
                            {
                                if (set)
                                    realData = setK7;
                                else
                                    realData = resetK7;
                            }
                            break;
                        case 8:
                            if (read)
                                realData = getK8;
                            else
                            {
                                if (set)
                                    realData = setK8;
                                else
                                    realData = resetK8;
                            }
                            break;
                        case 0:
                            if (read)
                                realData = getAll;
                            else
                            {
                                if (set)
                                    realData = setAll;
                                else
                                    realData = resetAll;
                            }
                            break;
                    }
                    byte cmd = sCmd;
                    if (read) cmd = gCmd;
                    int dataIndex = 2;
                    int tailLen = tail.Length;//帧尾
                    int needLen = tailLen + dataIndex;
                    int length = (byte)realData.Length;
                    package = new byte[length + needLen];
                    package[0] = sHead;//帧头
                    package[1] = cmd;//命令号
                    for (int i = 0; i < realData.Length; i++)
                    {
                        package[dataIndex + i] = realData[i];
                    }
                    for (int i = 0; i < tailLen; i++)
                    {
                        package[dataIndex + i + length] = tail[i];
                    }
                    if (package != null)
                    {
                        if (read)
                        {
                            switch (channel)
                            {
                                case 1:
                                    readed1 = -1;
                                    sp.Write(package, 0, package.Length);
                                    while (readed1 == -1)
                                    {
                                        System.Threading.Thread.Sleep(10);
                                    }
                                    break;
                                case 2:
                                    readed2 = -1;
                                    sp.Write(package, 0, package.Length);
                                    while (readed2 == -1)
                                    {
                                        System.Threading.Thread.Sleep(10);
                                    }
                                    break;
                                case 3:
                                    readed3 = -1;
                                    sp.Write(package, 0, package.Length);
                                    while (readed3 == -1)
                                    {
                                        System.Threading.Thread.Sleep(10);
                                    }
                                    break;
                                case 4:
                                    readed4 = -1;
                                    sp.Write(package, 0, package.Length);
                                    while (readed4 == -1)
                                    {
                                        System.Threading.Thread.Sleep(10);
                                    }
                                    break;
                                case 5:
                                    readed5 = -1;
                                    sp.Write(package, 0, package.Length);
                                    while (readed5 == -1)
                                    {
                                        System.Threading.Thread.Sleep(10);
                                    }
                                    break;
                                case 6:
                                    readed6 = -1;
                                    sp.Write(package, 0, package.Length);
                                    while (readed6 == -1)
                                    {
                                        System.Threading.Thread.Sleep(10);
                                    }
                                    break;
                                case 7:
                                    readed7 = -1;
                                    sp.Write(package, 0, package.Length);
                                    while (readed7 == -1)
                                    {
                                        System.Threading.Thread.Sleep(10);
                                    }
                                    break;
                                case 8:
                                    readed8 = -1;
                                    sp.Write(package, 0, package.Length);
                                    while (readed8 == -1)
                                    {
                                        System.Threading.Thread.Sleep(10);
                                    }
                                    break;
                                case 0:
                                    readed1 = readed2 = readed3 = readed4 = readed5 = readed6= readed7 = readed8 = -1;
                                    sp.Write(package, 0, package.Length);
                                    while (readed1 == -1 || readed2 == -1 || readed3 == -1 || readed4 == -1 || readed5 == -1 || readed6 == -1 || readed7 == -1 || readed8 == -1)
                                    {
                                        System.Threading.Thread.Sleep(10);
                                    }
                                    break;
                            }
                            flag = true;
                        }
                        else
                        {
                            sp.Write(package, 0, package.Length);
                            flag = true;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Log.WriteLog("Send", "发送数据出错，请检查单片机线路和电源。" + Environment.NewLine + e.ToString(), Log.Level.Error);
            }
            return flag;
        }
    }
}
