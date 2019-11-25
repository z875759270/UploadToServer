using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Net;
using Microsoft.Win32;
using System.Diagnostics;

namespace Daaa
{
    public partial class Frm_Vr : Form
    {
        public Frm_Vr()
        {
            InitializeComponent();
        }

        static WebServ.ServiceClient ws = new WebServ.ServiceClient();
        int folderNum = ws.GetFolderNum();
        string path = "";
        string newPath = "";

        private void Frm_Vr_Load(object sender, EventArgs e)
        {
            cboType.Items.Add(new ComboxItem("田园", "tianyuan"));
            cboType.Items.Add(new ComboxItem("地中海", "dizhonghai"));
            cboType.Items.Add(new ComboxItem("公主风", "gongzhu"));
            cboType.Items.Add(new ComboxItem("卡通", "katong"));
            cboType.Items.Add(new ComboxItem("东南亚", "dongnanya"));
            cboType.Items.Add(new ComboxItem("美式", "meishi"));
            cboType.Items.Add(new ComboxItem("日式", "rishi"));
            cboType.Items.Add(new ComboxItem("ins风", "ins"));
            cboType.Items.Add(new ComboxItem("学院", "xueyuan"));
            cboType.Items.Add(new ComboxItem("古典民族风", "gudian"));
        }

        /// <summary>
        /// 保存至数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.ToString();
            string type = ((ComboxItem)cboType.SelectedItem).Values;
            int res = ws.InsertVRIntoDataBase(name, "quanjing/qj" + folderNum + "/index.html", "quanjing/qj" + folderNum + "/images/preview_nodeimage_node1.jpg", type, "skrrr");
            if (res > 0)
            {
                if (path != "")
                {
                    //显示进度
                    label3.Visible = false;
                    lblSize.Visible = true;
                    lblSpeed.Visible = true;
                    lblState.Visible = true;
                    lblTime.Visible = true;
                    progressBar1.Visible = true;

                    //对文件夹进行改名、压缩
                    string[] temp = path.Split('\\');
                    temp[temp.Length - 1] = "qj" + folderNum;
                    newPath = "";
                    for (int i = 0; i < temp.Length; i++)
                    {
                        if (i != temp.Length - 1)
                            newPath += temp[i] + "\\";
                        else
                            newPath += temp[i];
                    }
                    Directory.Move(path, newPath);
                    CompressRar(newPath + "\\", "D:\\" + "qj" + folderNum + ".zip");

                    //上传至服务器
                    int res2 = Upload_Request2("http://101.37.245.109/save.aspx", "D:\\" + "qj" + folderNum + ".zip", "qj" + folderNum + ".rar", progressBar1);
                    if (res == 1)
                    {
                        MessageBox.Show("全景录入成功!请前往录入相应的商品");
                        File.Delete("D:\\" + "qj" + folderNum + ".zip");
                        Data.qjNum = "qj" + folderNum;
                        Frm_Good frm_Good = new Frm_Good();
                        frm_Good.Show();
                    }
                    else
                        MessageBox.Show("上传至服务器失败！");
                }
                else
                    MessageBox.Show("请将文件拖入下方区域！！");
            }
            else
                MessageBox.Show("录入失败！");

        }

        /// <summary>
        /// 拖进时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        /// <summary>
        /// 拖放后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                label3.Text = "";
                path = "";
                newPath = "";
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                List<string> filesList = files.ToList();

                filesList.ForEach(u => path = u.ToString());
                label3.Text = path;
            }
        }

        /// <summary>   
        /// 将本地文件上传到指定的服务器(HttpWebRequest方法)   
        /// </summary>   
        /// <param name="address">文件上传到的服务器</param>   
        /// <param name="fileNamePath">要上传的本地文件（全路径）</param>   
        /// <param name="saveName">文件上传后的名称</param>   
        /// <param name="progressBar">上传进度条</param>   
        /// <returns>成功返回1，失败返回0</returns>   
        private int Upload_Request2(string address, string fileNamePath, string saveName, ProgressBar progressBar)
        {
            int returnValue = 0;     // 要上传的文件   
            FileStream fs = new FileStream(fileNamePath, FileMode.Open, FileAccess.Read);
            BinaryReader r = new BinaryReader(fs);     //时间戳   
            string strBoundary = "----------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + strBoundary + "\r\n");     //请求头部信息   
            StringBuilder sb = new StringBuilder();
            sb.Append("--");
            sb.Append(strBoundary);
            sb.Append("\r\n");
            sb.Append("Content-Disposition: form-data; name=\"");
            sb.Append("file");
            sb.Append("\"; filename=\"");
            sb.Append(saveName);
            sb.Append("\";");
            sb.Append("\r\n");
            sb.Append("Content-Type: ");
            sb.Append("application/octet-stream");
            sb.Append("\r\n");
            sb.Append("\r\n");
            string strPostHeader = sb.ToString();
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(strPostHeader);     // 根据uri创建HttpWebRequest对象   
            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(address));
            httpReq.Method = "POST";     //对发送的数据不使用缓存   
            httpReq.AllowWriteStreamBuffering = false;     //设置获得响应的超时时间（300秒）   
            httpReq.Timeout = 300000;
            httpReq.ContentType = "multipart/form-data; boundary=" + strBoundary;
            long length = fs.Length + postHeaderBytes.Length + boundaryBytes.Length;
            long fileLength = fs.Length;
            httpReq.ContentLength = length;
            try
            {
                progressBar.Maximum = int.MaxValue;
                progressBar.Minimum = 0;
                progressBar.Value = 0;
                //每次上传4k  
                int bufferLength = 4096;
                byte[] buffer = new byte[bufferLength]; //已上传的字节数   
                long offset = 0;         //开始上传时间   
                DateTime startTime = DateTime.Now;
                int size = r.Read(buffer, 0, bufferLength);
                Stream postStream = httpReq.GetRequestStream();         //发送请求头部消息   
                postStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
                while (size > 0)
                {
                    postStream.Write(buffer, 0, size);
                    offset += size;
                    progressBar.Value = (int)(offset * (int.MaxValue / length));
                    TimeSpan span = DateTime.Now - startTime;
                    double second = span.TotalSeconds;
                    lblTime.Text = "已用时：" + second.ToString("F2") + "秒";
                    if (second > 0.001)
                    {
                        lblSpeed.Text = "平均速度：" + (offset / 1024 / second).ToString("0.00") + "KB/秒";
                    }
                    else
                    {
                        lblSpeed.Text = " 正在连接…";
                    }
                    lblState.Text = "已上传：" + (offset * 100.0 / length).ToString("F2") + "%";
                    lblSize.Text = (offset / 1048576.0).ToString("F2") + "M/" + (fileLength / 1048576.0).ToString("F2") + "M";
                    Application.DoEvents();
                    size = r.Read(buffer, 0, bufferLength);
                }
                //添加尾部的时间戳   
                postStream.Write(boundaryBytes, 0, boundaryBytes.Length);
                postStream.Close();         //获取服务器端的响应   
                WebResponse webRespon = httpReq.GetResponse();
                Stream s = webRespon.GetResponseStream();
                //读取服务器端返回的消息  
                StreamReader sr = new StreamReader(s);
                string sReturnString = sr.ReadLine();
                s.Close();
                sr.Close();
                if (sReturnString == "Success")
                {
                    returnValue = 1;
                }
                else if (sReturnString == "Error")
                {
                    returnValue = 0;
                }
            }
            catch
            {
                returnValue = 0;
            }
            finally
            {
                fs.Close();
                r.Close();
            }
            return returnValue;
        }

        /// <summary>
        /// 将目录和文件压缩为rar格式并保存到指定的目录
        /// </summary>
        /// <param name="soruceDir">要压缩的文件夹目录</param>
        /// <param name="rarFileName">压缩后的rar保存路径</param>
        public static void CompressRar(string soruceDir, string rarFileName)
        {
            string regKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\WinRAR.exe";
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(regKey);
            string winrarPath = registryKey.GetValue("").ToString();
            registryKey.Close();
            string winrarDir = System.IO.Path.GetDirectoryName(winrarPath);
            //String commandOptions = string.Format("a -ep1 {0} {1}", rarFileName, soruceDir);

            StringBuilder sb = new StringBuilder(" a -ep1 ");
            sb.Append(rarFileName);
            sb.Append(" ");
            foreach (string str in Directory.GetDirectories(soruceDir))
            {
                sb.Append(str);
                sb.Append(" ");
            }
            foreach (string str in Directory.GetFiles(soruceDir))
            {
                sb.Append(str);
                sb.Append(" ");
            }

            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = Path.Combine(winrarDir, "rar.exe");
            processStartInfo.Arguments = sb.ToString();
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            Process process = new Process();
            process.StartInfo = processStartInfo;
            process.Start();
            process.WaitForExit();
            process.Close();
        }
    }


}
