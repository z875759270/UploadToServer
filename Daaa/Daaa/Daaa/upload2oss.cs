﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Aliyun.OSS;
using Aliyun.OSS.Common;
using System.Threading;

namespace Daaa
{
    public partial class upload2oss : Form
    {
        protected string path = "";

        static WebServ.ServiceClient ws = new WebServ.ServiceClient();
        int folderNum = ws.GetFolderNum();

        public upload2oss()
        {
            InitializeComponent();
        }

        private void upload2oss_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (CheckDir(path))
            {
                lblError.Text += CreateAspx(path) != -1 ? "" : "生成Aspx文件失败！\n";
                if (lblError.Text == "")
                {
                    lblError.Text += UploadImgToOSS(path) != -1 ? "" : "上传图片至OSS失败！\n";
                }
                if (lblError.Text == "")
                {
                    lblError.Text += UploadJsToOSS(path) != -1 ? "" : "上传JS至OSS失败！\n";
                }
            }
            else
                MessageBox.Show("放入的文件夹格式有误，详细信息请阅读下方错误信息");
            //PutObjectProgress();
        }


        /// <summary>
        /// 上传文件至OSS
        /// </summary>
        /// <param name="objectName"></param>
        /// <param name="localFilename"></param>
        /// <returns>1：上传成功 -1：上传失败</returns>
        public int PutObjectProgress(string bucketName, string objectName, string localFilename)
        {
            var endpoint = "oss-cn-hangzhou.aliyuncs.com";
            var accessKeyId = "LTAI6znxLB8LTGZA";
            var accessKeySecret = "tThQgoJqcr5PdmHelU9Waomqw96CIf";
            bucketName = "twtwelkin";
            // 创建OssClient实例。
            var client = new OssClient(endpoint, accessKeyId, accessKeySecret);
            // 带进度条的上传。
            try
            {
                using (var fs = File.Open(localFilename, FileMode.Open))
                {
                    var putObjectRequest = new PutObjectRequest(bucketName, objectName, fs);
                    putObjectRequest.StreamTransferProgress += streamProgressCallback;
                    client.PutObject(putObjectRequest);
                }
                //MessageBox.Show(string.Format("文件:{0} 上传成功", objectName));
                return 1;
            }
            catch (OssException ex)
            {
                MessageBox.Show(string.Format("Failed with error code: {0}; Error info: {1}. \nRequestID: {2}\tHostID: {3}",
                    ex.ErrorCode, ex.Message, ex.RequestId, ex.HostId));
                return -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Failed with error info: {0}", ex.Message));
                return -1;
            }
        }

        /// <summary>
        /// 获取上传进度
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void streamProgressCallback(object sender, StreamTransferProgressArgs args)
        {

            Console.WriteLine("上传进度: {0}%, 文件大小:{1}, 已上传:{2} ",
                args.TransferredBytes * 100 / args.TotalBytes, args.TotalBytes, args.TransferredBytes);

            /*long offset = 0;
            while (args.TransferredBytes * 100 / args.TotalBytes < 100)
            {
                offset += args.TotalBytes;
                lblSize.Text += args.TotalBytes;
                lblJindu.Text += args.TransferredBytes;
                progressBar1.Maximum = 100;
                progressBar1.Minimum = 0;
                progressBar1.Value = 0;
                progressBar1.Value = (int)(args.TransferredBytes * 100 / args.TotalBytes);
            }*/
        }

        private void panel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        private void panel1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                lblPath.Text = "";
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                List<string> filesList = files.ToList();

                filesList.ForEach(u => path = u.ToString());
                lblPath.Text = path;
            }
        }

        #region 对文件夹操作

        /// <summary>
        /// 生成aspx文件并删除index.html文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns>1：生成成功 -1：生成失败</returns>
        public int CreateAspx(string pathh)
        {
            /*生成aspx文件
                 * FileStream fs = new FileStream(path+"/index.aspx", FileMode.OpenOrCreate);
                StreamWriter sw = new StreamWriter(fs);
                sw.WriteLine("");
                sw.Close();*/
            //复制aspx文件
            if (File.Exists(GetRootPath() + @"aspx\index.txt") && File.Exists(GetRootPath() + @"aspx\index.aspx.txt"))//必须判断要复制的文件是否存在
            {
                File.Copy(GetRootPath() + @"aspx\index.txt", pathh + "/index.aspx", true);//三个参数分别是源文件路径，存储路径，若存储路径有相同文件是否替换
                File.Copy(GetRootPath() + @"aspx\index.aspx.txt", pathh + "/index.aspx.cs", true);
                //删除index.html文件
                if (File.Exists(pathh + "/index.html"))
                {
                    File.Delete(pathh + "/index.html");
                }
                return 1;
            }
            else
                return -1;
        }

        /// <summary>
        /// 上传图片至OSS
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private int UploadImgToOSS(string pathh)
        {
            string imgPath = pathh + "/images";
            DirectoryInfo dinfo = new DirectoryInfo(imgPath);
            FileInfo[] finfo = dinfo.GetFiles("*.png");
            int res = 1;
            for (int i = 0; i < finfo.Length; i++)
            {
                res = PutObjectProgress("twtwelkin", "twtweb/qj" + folderNum + "/images/" + finfo[i].Name, finfo[i].FullName);
                if (res == -1)
                    return -1;
            }
            return 1;
        }

        /// <summary>
        /// 上传js文件至OSS
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public int UploadJsToOSS(string pathh)
        {
            string imgPath = pathh;
            DirectoryInfo dinfo = new DirectoryInfo(imgPath);
            FileInfo[] finfo = dinfo.GetFiles("*.js");
            int res = 1;
            for (int i = 0; i < finfo.Length; i++)
            {
                res = PutObjectProgress("twtwelkin", "twtweb/qj" + folderNum + "/" + finfo[i].Name, finfo[i].FullName);
                if (res == -1)
                    return -1;
            }
            return 1;
        }

        #endregion

        #region 验证

        /// <summary>
        /// 检测放入的文件夹是否符合规格
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <returns>是否符合规格</returns>
        private bool CheckDir(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    List<string> nameList = new List<string> { };
                    nameList = myTools.GetDirAndFileName(path);
                    if (nameList != null)
                    {
                        if (nameList.Count == 6 || nameList.Count == 7)
                        {
                            lblError.Text = "";
                            lblError.Text += nameList.Contains("images") ? "" : "缺少images文件夹！\n";
                            lblError.Text += nameList.Contains("tiles") ? "" : "缺少tiles文件夹！\n";
                            lblError.Text += nameList.Contains("index.html") ? "" : "缺少index.html文件！\n";
                            lblError.Text += nameList.Contains("pano.xml") ? "" : "缺少pano.xml文件！\n";
                            lblError.Text += nameList.Contains("pano2vr_player.js") ? "" : "缺少pano2vr_player.js文件！\n";
                            lblError.Text += nameList.Contains("skin.js") ? "" : "缺少skin.js文件！\n";
                            if (lblError.Text == "")
                                return true;
                            else
                                return false;
                        }
                        else
                        {
                            lblError.Text = "文件个数不对";
                            return false;
                        }
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        #endregion

        #region 小工具
        /// <summary>
        /// 获取绝对路径
        /// </summary>
        /// <returns>返回路径</returns>
        public static string GetRootPath()
        {
            string res = "";
            string[] temp = Application.StartupPath.Split('\\');
            for (int i = 0; i < temp.Length - 2; i++)
            {
                res += temp[i] + "\\";
            }
            return res;
        }
        #endregion

    }
}
