using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Daaa
{
    class myTools
    {
        /// <summary>
        /// 获取文件路径下的单层的所有文件名
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>如果路径存在，返回文件名集，否则返回null</returns>
        public static List<string> GetFileName(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo dInfo = new DirectoryInfo(path);
                List<FileInfo> fileInfos = dInfo.GetFiles().ToList();
                List<string> res = new List<string> { };
                fileInfos.ForEach(a => res.Add(a.Name));
                return res;
            }
            else
                return null;
        }

        /// <summary>
        /// 获取文件路径下的单层的所有文件名(包括子文件夹名)
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<string> GetDirAndFileName(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo dInfo = new DirectoryInfo(path);
                List<FileInfo> fileInfos = dInfo.GetFiles().ToList();
                List<DirectoryInfo> dInfos = dInfo.GetDirectories().ToList();
                List<string> res = new List<string> { };
                dInfos.ForEach(b => res.Add(b.Name));
                fileInfos.ForEach(a => res.Add(a.Name));
                
                return res;
            }
            else
                return null;
        }

        /// <summary>
        /// 获取文件路径下的单层的所有文件名
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>如果路径存在，返回文件名集，否则返回null</returns>
        public static List<string> GetFileFullName(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo dInfo = new DirectoryInfo(path);
                List<FileInfo> fileInfos = dInfo.GetFiles().ToList();
                List<string> res = new List<string> { };
                fileInfos.ForEach(a => res.Add(a.FullName));
                return res;
            }
            else
                return null;
        }

        /// <summary>
        /// 获取文件路径下的单层的所有文件名(包括子文件夹名)
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<string> GetDirAndFileFullName(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo dInfo = new DirectoryInfo(path);
                List<FileInfo> fileInfos = dInfo.GetFiles().ToList();
                List<DirectoryInfo> dInfos = dInfo.GetDirectories().ToList();
                List<string> res = new List<string> { };
                dInfos.ForEach(b => res.Add(b.FullName));
                fileInfos.ForEach(a => res.Add(a.FullName));
                return res;
            }
            else
                return null;
        }
    }
}
