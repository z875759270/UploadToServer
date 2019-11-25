using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Daaa
{
    public partial class Frm_Good : Form
    {
        public Frm_Good()
        {
            InitializeComponent();

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
            label1.Visible = false;
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop); // files保存了拖拽进控件的文件的路径集合
                List<string> filesList = files.ToList();
                filesList.ForEach(u => label1.Text = u.ToString());
                if (label1.Text != "" && label1.Text.EndsWith(".xml"))
                {
                    string path = label1.Text.ToString().Trim();
                    List<GoodData> resList = GetGoodList(path);
                    string str = "";
                    int page = resList.Count % 5 > 0 ? resList.Count / 5 + 1 : resList.Count / 5;
                    int x = 0;
                    int y = 0;
                    for (int i = 0; i < resList.Count; i++)
                    {

                        GroupBox gbo = new GroupBox();
                        gbo.Text = "商品" + i + "号";
                        gbo.Size = new Size(215, 161);
                        gbo.Location = new Point(5 + x * 215 + 6 * x, 5 + y * 164 + y * 6);


                        Label lblName = new Label();
                        lblName.Text = "商品名称：";
                        lblName.Location = new Point(26, 28);
                        lblName.Size = new Size(65, 12);
                        TextBox txtName = new TextBox();
                        txtName.Name = "txtName";
                        txtName.Text = "" + resList[i].Name;
                        txtName.Location = new Point(94, 23);

                        Label lblPrice = new Label();
                        lblPrice.Text = "商品价格：";
                        lblPrice.Location = new Point(26, 61);
                        lblPrice.Size = new Size(65, 12);
                        TextBox txtPrice = new TextBox();
                        txtPrice.Name = "txtPrice";
                        Random r = new Random();

                        txtPrice.Text = ""+r.Next(100,1000);
                        txtPrice.Location = new Point(94, 56);

                        Label lblType = new Label();
                        lblType.Text = "商品类别：";
                        lblType.Location = new Point(26, 93);
                        lblType.Size = new Size(65, 12);
                        ComboBox cboType = new ComboBox();
                        cboType.Name = "cboType";
                        cboType.Items.Add(new ComboxItem("沙发", "shafa"));
                        cboType.Items.Add(new ComboxItem("床", "chuang"));
                        cboType.Items.Add(new ComboxItem("桌子", "zhuozi"));
                        cboType.Items.Add(new ComboxItem("椅子", "yizi"));
                        cboType.Items.Add(new ComboxItem("窗帘", "chuanglian"));
                        cboType.Items.Add(new ComboxItem("地毯", "ditan"));
                        cboType.Items.Add(new ComboxItem("抱枕", "baozhen"));
                        cboType.Items.Add(new ComboxItem("装饰", "zhuangshi"));
                        cboType.Items.Add(new ComboxItem("墙贴", "qiangtie"));
                        cboType.Items.Add(new ComboxItem("相框", "xiangkuang"));
                        cboType.Items.Add(new ComboxItem("柜子", "guizi"));
                        cboType.Items.Add(new ComboxItem("灯", "deng"));
                        cboType.Items.Add(new ComboxItem("家电", "jiadian"));
                        cboType.Location = new Point(94, 88);
                        cboType.Size = new Size(100, 21);

                        Label lblLinkUrl = new Label();
                        lblLinkUrl.Text = "店铺链接：";
                        lblLinkUrl.Location = new Point(26, 127);
                        lblLinkUrl.Size = new Size(65, 12);
                        LinkLabel txtLinkUrl = new LinkLabel();
                        txtLinkUrl.Name = "txtLinkUrl";
                        txtLinkUrl.Text = "" + resList[i].Url;
                        txtLinkUrl.Location = new Point(94, 127);
                        txtLinkUrl.Click += TxtLinkUrl_Click;

                        gbo.Controls.Add(lblName);
                        gbo.Controls.Add(txtName);
                        gbo.Controls.Add(lblPrice);
                        gbo.Controls.Add(txtPrice);
                        gbo.Controls.Add(lblType);
                        gbo.Controls.Add(cboType);
                        gbo.Controls.Add(lblLinkUrl);
                        gbo.Controls.Add(txtLinkUrl);

                        this.Controls.Add(gbo);

                        x++;
                        if (x == 5)
                        {
                            x = 0;
                            if (y < page)
                                y++;
                        }



                        string vrdata = resList[i].Pan + "," + resList[i].Tilt;
                        //WebServ.ServiceClient ws = new WebServ.ServiceClient(); 
                        //ws.InsertIntoDataBase(resList[i].Name,);      //添加商品
                        str += "Name=" + resList[i].Name + " url=" + resList[i].Url + " vrdata=" + vrdata + "\n";
                    }
                    //richTextBox1.Text = str;
                }
                else
                    MessageBox.Show("请将对应xml文件拖入下方区域！");
            }

        }

        /// <summary>
        /// 链接网址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtLinkUrl_Click(object sender, EventArgs e)
        {
            LinkLabel linkLabel = (LinkLabel)sender;
            string url = linkLabel.Text.ToString();
            System.Diagnostics.Process.Start(url);
        }

        /// <summary>
        /// 解析XML获取商品列表
        /// </summary>
        /// <param name="path">XML文件路径</param>
        /// <returns>返回商品集合</returns>
        private List<GoodData> GetGoodList(string path)
        {
            if (path != "")
            {
                List<GoodData> resList = new List<GoodData> { };
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path);
                //获取根节点
                XmlNode root = xmlDoc.DocumentElement;
                //获取节点列表
                XmlNodeList items = root.ChildNodes;
                //遍历节点列表
                foreach (XmlNode item in items)
                {
                    //遍历item
                    if (item.Name == "hotspots")
                    {
                        foreach (XmlNode p in item)
                            if (p.Name == "hotspot")
                            {
                                XmlAttributeCollection atts = p.Attributes;
                                if (resList.Count != 0)
                                {
                                    if (!GoodNameIsRepeat(resList, atts["title"].Value))
                                    {
                                        if (atts["url"].Value != "")
                                        {
                                            GoodData goodData = new GoodData();
                                            goodData.Name = atts["title"].Value;
                                            goodData.Url = atts["url"].Value;
                                            goodData.Pan = atts["pan"].Value;
                                            goodData.Tilt = atts["tilt"].Value;
                                            goodData.Pic = atts["id"].Value.ToLower();
                                            resList.Add(goodData);
                                        }
                                    }
                                }
                                else
                                {
                                    GoodData goodData = new GoodData();
                                    goodData.Name = atts["title"].Value;
                                    goodData.Url = atts["url"].Value;
                                    goodData.Pan = atts["pan"].Value;
                                    goodData.Tilt = atts["tilt"].Value;
                                    goodData.Pic = atts["id"].Value.ToLower();
                                    resList.Add(goodData);
                                }

                            }
                    }
                }
                return resList;
            }
            else
                return null;
            
            
        }

        /// <summary>
        /// 判断商品是否重复
        /// </summary>
        /// <param name="goodDatas">商品集合</param>
        /// <param name="str">比对的文字</param>
        /// <returns>true:重复 false:不重复</returns>
        private bool GoodNameIsRepeat(List<GoodData> goodDatas, string str)
        {
            for (int i = 0; i < goodDatas.Count; i++)
            {
                if (goodDatas[i].Name == str)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 保存至数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            List<GoodData> goodDatas = GetGoodList(label1.Text.ToString());
            List<string> priceList = new List<string> { };
            List<string> typeList = new List<string> { };

            foreach (Control p in this.Controls)
            {
                foreach (Control a in p.Controls)
                {
                    if (a is TextBox || a is ComboBox)
                    {
                        if (a.Text != "")
                        {
                            if (a.Name.Equals("txtPrice"))
                            {
                                priceList.Add(a.Text.ToString());
                            }
                            if (a.Name.Equals("cboType"))
                            {
                                ComboBox cbo = (ComboBox)a;
                                typeList.Add(((ComboxItem)cbo.SelectedItem).Values);
                            }
                        }
                        else
                        {
                            MessageBox.Show("请确认是否填写完整！");
                            return;
                        }
                    }
                }
            }

            WebServ.ServiceClient ws = new WebServ.ServiceClient();
            if (goodDatas.Count == priceList.Count && goodDatas.Count == typeList.Count)
            {
                for (int i = 0; i < goodDatas.Count; i++)
                {
                    int res = ws.InsertGoodIntoDataBase(goodDatas[i].Name, Convert.ToInt32(priceList[i]), typeList[i], "quanjing/" + Data.qjNum + "/images/" + goodDatas[i].Pic + ".png", goodDatas[i].Url, "quanjing/" + Data.qjNum, goodDatas[i].Pan + "," + goodDatas[i].Tilt, "skrr");
                    if (res < 0)
                    {
                        MessageBox.Show("录入错误！");
                        break;
                    }   
                }
                MessageBox.Show("录入成功！");
            }
            else
                MessageBox.Show("录入错误！");

        }
    }

    //属性类
    class GoodData
    {
        string name;
        string url;
        string pan;
        string tilt;
        string pic;

        public GoodData()
        {

        }

        public GoodData(string name, string url, string pan, string tilt, string pic)
        {
            this.Name = name;
            this.Url = url;
            this.Pan = pan;
            this.Tilt = tilt;
            this.Pic = pic;
        }

        public string Name { get => name; set => name = value; }
        public string Url { get => url; set => url = value; }
        public string Pan { get => pan; set => pan = value; }
        public string Tilt { get => tilt; set => tilt = value; }
        public string Pic { get => pic; set => pic = value; }
    }

    //选项类
    public class ComboxItem
    {
        private string text;
        private string values;

        public string Text
        {
            get { return this.text; }
            set { this.text = value; }
        }

        public string Values
        {
            get { return this.values; }
            set { this.values = value; }
        }

        public ComboxItem(string _Text, string _Values)
        {
            Text = _Text;
            Values = _Values;
        }


        public override string ToString()
        {
            return Text;
        }
    }

}
