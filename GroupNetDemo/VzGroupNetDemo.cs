using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Threading;

namespace GroupNetDemo
{
    public partial class VzGroupNetDemo : Form
    {
        private const int MSG_PLATE_INFO = 0x901;
        private const int MSG_GROUP_PLATE_INFO = 0x902;
        private const int MSG_GROUP_IMAGE_INFO = 0x903;

        private int m_nPlayHandle = 0;
        private int m_nPlayHandle2 = 0;

        private string m_sAppPath;

        private Color m_originalColor;
        private bool m_bFirst;

        public IntPtr hwndMain;

        private VzClientSDK.VZLPRC_PLATE_INFO_CALLBACK m_PlateResultCB = null;
        private VzClientSDK.VZLPRC_PLATE_INFO_CALLBACK m_PlateResultCB2 = null;

        private int flag = 0;
        private VzClientSDK.VZLPRC_PLATE_GROUP_INFO_CALLBACK[] m_PlateGroupResult = new  VzClientSDK.VZLPRC_PLATE_GROUP_INFO_CALLBACK[10];

        public VzGroupNetDemo()
        {
            InitializeComponent();
        }

        //初始化
        private void VzGroupNetDemo_Load(object sender, EventArgs e)
        {
            VzClientSDK.VzLPRClient_Setup();

            //初始化"设备列表"
            this.listViewOpenDevice.Columns.Add("设备IP");
            this.listViewOpenDevice.Columns.Add("是否接受组网结果");
            this.listViewOpenDevice.View = System.Windows.Forms.View.Details;
            this.listViewOpenDevice.FullRowSelect = true;
            this.listViewOpenDevice.GridLines = true;
            this.listViewOpenDevice.Columns[0].Width = 90;
            this.listViewOpenDevice.Columns[1].Width = 120;


            //初始化"组网车牌识别结果"列表
            this.listView1.Columns.Add("设备IP");
            this.listView1.Columns.Add("车牌号");
            this.listView1.Columns.Add("入口时间");
            this.listView1.Columns.Add("出口时间");
            this.listView1.Columns.Add("时间间隔");
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            listView1.Columns[0].Width = 110;
            listView1.Columns[1].Width = 100;
            listView1.Columns[2].Width = 160;
            listView1.Columns[3].Width = 160;
            listView1.Columns[4].Width = 120;

            hwndMain = this.Handle;

            m_sAppPath = System.IO.Directory.GetCurrentDirectory();

            m_originalColor = pictureBox1.BackColor;
            m_bFirst = true;

        }

        //打开设备
        private void btnOpenDevice_Click(object sender, EventArgs e)
        {
            short nPort = Int16.Parse(textBoxPort.Text);

            //同一设备只能打开一次
            for(int i = 0;i < this.listViewOpenDevice.Items.Count;i++)
            {
                if (this.listViewOpenDevice.Items[i].Text == textBoxDeviceIP.Text)
                {
                    MessageBox.Show("该设备已打开","提示");
                    return;
                }
            }

            //打开设备
            int handle = VzClientSDK.VzLPRClient_Open(textBoxDeviceIP.Text, (ushort)nPort, textBoxUserName.Text, textBoxPwd.Text);
            if (handle == 0)
            {
                MessageBox.Show("打开设备失败");
                return;
            }

            //打开的设备添加到列表
            ListViewItem item = new ListViewItem(textBoxDeviceIP.Text);
            item.SubItems.Add("否");
            item.Tag = handle;
            
            this.listViewOpenDevice.Items.Add(item);
            
            //选中当前打开的设备
            int size = this.listViewOpenDevice.Items.Count;
            for (int i = 0; i < this.listViewOpenDevice.SelectedItems.Count; i++)
            {
                this.listViewOpenDevice.Items[this.listViewOpenDevice.SelectedItems[i].Index].Selected = false;// 当前选中的设备IP设置为不选中
            }
            this.listViewOpenDevice.Items[size-1].Selected = true;//设定刚打开的设备选中
        }
   

        //获取列表中选中IP的句柄
        private int GetSeleHandle()
        {
            int handle = 0;

            if (listViewOpenDevice.SelectedItems.Count > 0)
            {
                string sHandle = this.listViewOpenDevice.SelectedItems[0].Tag.ToString();
                handle = Int32.Parse(sHandle);
            }

            return handle;
        }

        //获取列表中选中项的设备IP
        private string GetSeleDeviceIP()
        {
            string strSeleIP = "";
            if (listViewOpenDevice.SelectedItems.Count > 0)
            {
               strSeleIP = this.listViewOpenDevice.SelectedItems[0].Text.ToString();
            }

            return strSeleIP;
        }


        //获取第一个视频窗口的句柄
        private int GetPicBox1Handle()
        {
            int handle = 0;
            if (pictureBox1.Tag != null)
            {
                string sHandle = pictureBox1.Tag.ToString();
                handle = Int32.Parse(sHandle);
            }

            return handle;
        }

        //获取第二个视频窗口的句柄
        private int GetPicBox2Handle()
        {
            int handle = 0;
            if (pictureBox2.Tag != null)
            {
                string sHandle = pictureBox2.Tag.ToString();
                handle = Int32.Parse(sHandle);
            }

            return handle;
        }

        //设置第一个视频窗口的属性
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Color c = Color.FromArgb(255, 220, 220, 220);
            pictureBox1.BackColor = c;//第一个选中为灰色
            pictureBox2.BackColor = m_originalColor;//第二个未选中为白色

            m_bFirst = true;

        }

        //设置第二个视频窗口的属性
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pictureBox1.BackColor = m_originalColor;//第一个未选中为白色

            Color c = Color.FromArgb(255, 220, 220, 220);
            pictureBox2.BackColor = c;//第二个选中为灰色

            m_bFirst = false;

        }

        //车牌识别结果回调
        private int OnPlateResult(int handle, IntPtr pUserData,
                                                         IntPtr pResult, uint uNumPlates,
                                                         VzClientSDK.VZ_LPRC_RESULT_TYPE eResultType,
                                                         IntPtr pImgFull,
                                                         IntPtr pImgPlateClip)
        {
            if (eResultType != VzClientSDK.VZ_LPRC_RESULT_TYPE.VZ_LPRC_RESULT_REALTIME)
            {
                VzClientSDK.TH_PlateResult result = (VzClientSDK.TH_PlateResult)Marshal.PtrToStructure(pResult, typeof(VzClientSDK.TH_PlateResult));
                string strLicense = new string(result.license);

                VzClientSDK.VZ_LPR_MSG_PLATE_INFO plateInfo = new VzClientSDK.VZ_LPR_MSG_PLATE_INFO();
                plateInfo.plate = strLicense;

                //设置视频截图保存图片的目录及格式
                DateTime nowTime = DateTime.Now;
                string sTime = string.Format("{0:yyyyMMddHHmmssffff}", nowTime);

                string strFilePath = m_sAppPath + "\\cap\\";
                if (!Directory.Exists(strFilePath))
                {
                    Directory.CreateDirectory(strFilePath);
                }

                string path = strFilePath + sTime + ".jpg";

                //将图像保存为JPEG到指定路径
                VzClientSDK.VzLPRClient_ImageSaveToJpeg(pImgFull, path, 100);
                plateInfo.img_path = path;

                int size = Marshal.SizeOf(plateInfo);
                IntPtr intptr = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(plateInfo, intptr, true);

                Win32API.PostMessage(hwndMain, MSG_PLATE_INFO, (int)intptr, handle);//将消息推送给窗体
            }

            return 0;

        }

        //输出功能
        private void btnOutput_Click(object sender, EventArgs e)
        {
            int lprHandle = GetSeleHandle();
            if (lprHandle == 0)
            {
                MessageBox.Show("请选择一台列表中的设备","提示");
                return;
            }

            if (lprHandle == GetPicBox1Handle() || lprHandle == GetPicBox2Handle())
            {
                MessageBox.Show("该设备已经输出在视频窗口中","提示");
                return;
            }

            //在第一个视频窗口播放视频
            if (m_bFirst)
            {
                pictureBox1.Image = null;//不显示图片

                if (m_nPlayHandle > 0)
                {
                    VzClientSDK.VzLPRClient_StopRealPlay(m_nPlayHandle);//停止当前播放的视频
                    m_nPlayHandle = 0;
                }

                int picHandle1 = GetPicBox1Handle();
                if (picHandle1 > 0)
                {
                    VzClientSDK.VzLPRClient_SetPlateInfoCallBack(picHandle1, null, IntPtr.Zero, 0);//清空上一个车牌识别结果
                }

                m_nPlayHandle = VzClientSDK.VzLPRClient_StartRealPlay(lprHandle, pictureBox1.Handle);//播放实时视频
                pictureBox1.Tag = lprHandle;

                //设置车牌识别结果回调
                m_PlateResultCB = new VzClientSDK.VZLPRC_PLATE_INFO_CALLBACK(OnPlateResult);
                VzClientSDK.VzLPRClient_SetPlateInfoCallBack(lprHandle, m_PlateResultCB, IntPtr.Zero, 1);

            }
            //在第二个视频窗口播放视频
            else
            {
                pictureBox2.Image = null;//不显示图片

                if (m_nPlayHandle2 > 0)
                {
                    VzClientSDK.VzLPRClient_StopRealPlay(m_nPlayHandle2);//停止当前播放的视频
                    m_nPlayHandle2 = 0;
                }

                int picHandle2 = GetPicBox2Handle();
                if (picHandle2 > 0)
                {
                    VzClientSDK.VzLPRClient_SetPlateInfoCallBack(picHandle2, null, IntPtr.Zero, 0);//清空上一个车牌识别结果
                }

                m_nPlayHandle2 = VzClientSDK.VzLPRClient_StartRealPlay(lprHandle, pictureBox2.Handle);//播放实时视频
                pictureBox2.Tag = lprHandle;

                //设置车牌识别结果回调
                m_PlateResultCB2 = new VzClientSDK.VZLPRC_PLATE_INFO_CALLBACK(OnPlateResult);
                VzClientSDK.VzLPRClient_SetPlateInfoCallBack(lprHandle, m_PlateResultCB2, IntPtr.Zero, 1);
            }
        }


        //显示图片、车牌识别结果、时间和组网车牌识别结果
        protected override void DefWndProc(ref Message m)
        {
            IntPtr intptr;
            VzClientSDK.VZ_LPR_MSG_PLATE_INFO plateInfo;
            VzClientSDK.TH_GroupPlateResult groupPlateInfo;
            VzClientSDK.TH_GroupPlateResult groupImgeInfo;

            int handle = 0;
            switch (m.Msg)
            {
                //显示图片与车牌识别结果、时间
                case MSG_PLATE_INFO:

                    intptr = (IntPtr)m.WParam.ToInt32();
                    handle = m.LParam.ToInt32();

                    if (intptr != null)
                    {
                        plateInfo = (VzClientSDK.VZ_LPR_MSG_PLATE_INFO)Marshal.PtrToStructure(intptr, typeof(VzClientSDK.VZ_LPR_MSG_PLATE_INFO));

                        //在第一个图片窗口以及文本框中显示
                        if (handle == GetPicBox1Handle())
                        {
                            //显示车牌识别结果、时间
                            if (plateInfo.plate != "")
                            {
                                string strShowInfo =  plateInfo.plate;

                                lblPlateResult1.Text = strShowInfo;
                            }

                            //显示图片
                            //if (plateInfo.img_path != "")
                            //{
                            //    pictureBox3.Image = Image.FromFile(plateInfo.img_path);
                            //}
                        }

                        //在第二个图片窗口以及文本框中显示
                        else
                        {
                            //显示车牌识别结果、时间
                            if (plateInfo.plate != "")
                            {
                                string strShowInfo2 = plateInfo.plate  ;

                                lblPlateResult2.Text = strShowInfo2;

                            }

                            //显示图片
                            //if (plateInfo.img_path != "")
                            //{
                            //    pictureBox4.Image = Image.FromFile(plateInfo.img_path);
                            //}
                        }

                        Marshal.FreeHGlobal(intptr);

                    }
                    break;

                //显示组网车牌识别结果
                case MSG_GROUP_PLATE_INFO:

                    intptr = (IntPtr)m.WParam.ToInt32();
                    handle = m.LParam.ToInt32();

                    if (intptr != null)
                    {
                        groupPlateInfo = (VzClientSDK.TH_GroupPlateResult)Marshal.PtrToStructure(intptr,typeof(VzClientSDK.TH_GroupPlateResult));

                        ListViewItem itemGroupResult = new ListViewItem();

                        //显示设备IP
                        string strGroupDeviceIP = "";
                        for (int i = 0; i < this.listViewOpenDevice.Items.Count; i++)
                        {
                            int lvwHandle = 0;
                            string strhandle = this.listViewOpenDevice.Items[i].Tag.ToString();
                            lvwHandle = Int32.Parse(strhandle);
                                 
                            if (lvwHandle == handle)
                            {
                                strGroupDeviceIP = this.listViewOpenDevice.Items[i].Text;                      
                            }
                        }

                        if (strGroupDeviceIP != "")
                        {
                            itemGroupResult.SubItems[0].Text = strGroupDeviceIP;
                        }

                        //显示车牌
                        string strGroupLicense = groupPlateInfo.groupLicense.ToString();
                        itemGroupResult.SubItems.Add(strGroupLicense);

                        //显示入口时间
                        string strEntryDateTime = groupPlateInfo.entryDateTime.ToString();
                        itemGroupResult.SubItems.Add(strEntryDateTime);

                        //显示出口时间
                        string strExitDateTime = groupPlateInfo.exitDateTime.ToString();
                        itemGroupResult.SubItems.Add(strExitDateTime);

                        //显示时间间隔
                        string strTimeInterval = groupPlateInfo.timeInterval.ToString();
                        itemGroupResult.SubItems.Add(strTimeInterval);

                        this.listView1.Items.Insert(0,itemGroupResult);

                        //超过10行，则删除最后一行
                        int countPlateMsg = this.listView1.Items.Count;
                        int flagItem = 10;
                        if (countPlateMsg > flagItem)
                        {
                            this.listView1.Items[countPlateMsg - 1].Remove();
                        }

                        Marshal.FreeHGlobal(intptr);
                          
                    }
                    break;
                case MSG_GROUP_IMAGE_INFO:

                    intptr = (IntPtr)m.WParam.ToInt32();
                    int isEntry = m.LParam.ToInt32();

                    if (intptr != null)
                    {
                        groupImgeInfo = (VzClientSDK.TH_GroupPlateResult)Marshal.PtrToStructure(intptr, typeof(VzClientSDK.TH_GroupPlateResult));
                        if (isEntry ==1)
                        {
                            pictureBox3.Image = Image.FromFile(groupImgeInfo.img_path);
                        }
                        else
                        {
                            pictureBox4.Image = Image.FromFile(groupImgeInfo.img_path);
                        }
                        
                        Marshal.FreeHGlobal(intptr);
                    }
                    break;
                    
               default:
                    base.DefWndProc(ref m);
                    break;
            }
        }

        //"手动识别"功能
        private void btnManual_Click(object sender, EventArgs e)
        {
            int lprHandle = GetSeleHandle();
            if (lprHandle == 0)
            {
                MessageBox.Show("请选择一台列表中的设备");
                return;
            }
            if (lprHandle > 0)
            {
                VzClientSDK.VzLPRClient_ForceTrigger(lprHandle);
            }

        }

        //转换为当前日期时间
        private string date1970ConvertCurrentDate(UInt64 TotalSeconds)
        {
            string dateFormat = "yyyy/MM/dd HH:mm:ss";
            DateTime primDateDT = new DateTime(1970, 1, 1, 8, 0, 0);
            DateTime currentDateDT = primDateDT.AddSeconds(TotalSeconds);
            string strCurDate = currentDateDT.ToString(dateFormat);

            return strCurDate;
        }

        //时间间隔转换
        private string timeIntervalConvert(Int32 timeInterval)
        {
            TimeSpan ts = new TimeSpan(0,0,timeInterval);
            string strTime = "";

            if (ts.Hours > 0)
            {
                strTime = ts.Hours.ToString() + "小时" + ts.Minutes.ToString() + "分" + ts.Seconds.ToString() + "秒";
            }
            else if (ts.Hours == 0 && ts.Minutes > 0)
            {
                strTime = ts.Minutes.ToString() + "分" + ts.Seconds.ToString() + "秒";
            }
            else if(ts.Minutes == 0 && ts.Seconds > 0)
            {
                strTime = ts.Seconds.ToString() + "秒";
            }
            else if (ts.Seconds == 0)
            {
                strTime = "0秒";
            }

            return strTime;

        }

        //开线程获取组网图片
        private void  LoadGroupImg(int handle, int id, string sn, int isEntry)
        {
            Thread thread = new Thread(() => LoadGroupImgThread(handle, id, sn, isEntry));
            thread.Start();
        }

        //获取组网图片线程
        private void LoadGroupImgThread(int handle, int id, string sn, int isEntry)
        {
            VzClientSDK.TH_GroupPlateResult groupImageResult = new VzClientSDK.TH_GroupPlateResult();
           
            int nSize = 1280 * 720;
            byte[] picdata = new byte[1280 * 720];
            GCHandle hObject = GCHandle.Alloc(picdata, GCHandleType.Pinned);//为指定的对象分配指定类型的句柄，允许使用固定对象的地址
            IntPtr pObject = hObject.AddrOfPinnedObject();//检索对象的地址

            int ret = VzClientSDK.VzLPRClient_LoadGroupFullImageById(handle, id, sn, pObject, ref nSize);

            DateTime now = DateTime.Now;
            string sTime = string.Format("{0:yyyyMMddHHmmssffff}", now);
            string szImgDir = "";
            string strFilePath = m_sAppPath + "\\组网";
            if (!Directory.Exists(strFilePath))
            {
                Directory.CreateDirectory(strFilePath);
            }

            if(isEntry == 1)
            {
                szImgDir = strFilePath + "\\入口";
                if (!Directory.Exists(szImgDir))
                {
                    Directory.CreateDirectory(szImgDir);
                }
            }
            else
            {
                szImgDir = strFilePath + "\\出口";
                if (!Directory.Exists(szImgDir))
                {
                    Directory.CreateDirectory(szImgDir);
                }
            }

            string path = szImgDir + "\\"+ sTime + ".jpg";

            if (nSize > 0 && nSize < 1280*720)
            {
                FileStream aFile = new FileStream(path, FileMode.Create);
                aFile.Seek(0, SeekOrigin.Begin);
                aFile.Write(picdata, 0, picdata.Length);
                aFile.Close();
            }

            groupImageResult.img_path = path;

            int size = Marshal.SizeOf(groupImageResult);
            IntPtr intptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(groupImageResult, intptr, true);

            Win32API.PostMessage(hwndMain, MSG_GROUP_IMAGE_INFO, (int)intptr, isEntry);//将消息推送给窗体

            if (hObject.IsAllocated)
                hObject.Free();    

        }

        //设置组网识别结果的回调函数
        private int OnPlateGroupInfo(int handle, IntPtr pUserData, 
                                     int exitEntryInfo, 
                                     IntPtr exitIvsInfo,
                                     IntPtr entryIvsInfo, 
                                     IntPtr exitDGInfo, 
                                     IntPtr entryDGInfo)
        {
            VzClientSDK.TH_PlateResult entryInfo = new VzClientSDK.TH_PlateResult();
            VzClientSDK.TH_PlateResult exitInfo = new VzClientSDK.TH_PlateResult();
            VzClientSDK.IVS_DG_DEVICE_INFO entryImageInfo = new VzClientSDK.IVS_DG_DEVICE_INFO();
            VzClientSDK.IVS_DG_DEVICE_INFO exitImageInfo = new VzClientSDK.IVS_DG_DEVICE_INFO();

            if (exitIvsInfo != null)
            {
                entryInfo = (VzClientSDK.TH_PlateResult)Marshal.PtrToStructure(entryIvsInfo, typeof(VzClientSDK.TH_PlateResult));
            }

            if (entryIvsInfo != null)
            {
                exitInfo = (VzClientSDK.TH_PlateResult)Marshal.PtrToStructure(exitIvsInfo, typeof(VzClientSDK.TH_PlateResult));
            }

            if (entryDGInfo != null)
            {
                entryImageInfo = (VzClientSDK.IVS_DG_DEVICE_INFO)Marshal.PtrToStructure(entryDGInfo, typeof(VzClientSDK.IVS_DG_DEVICE_INFO));
            }

            if (exitDGInfo != null)
            {
                exitImageInfo = (VzClientSDK.IVS_DG_DEVICE_INFO)Marshal.PtrToStructure(exitDGInfo, typeof(VzClientSDK.IVS_DG_DEVICE_INFO));
            }

            VzClientSDK.TH_GroupPlateResult groupPlateResult = new VzClientSDK.TH_GroupPlateResult();       
           
            //获取入口设备组网车牌识别结果
            if(exitEntryInfo == 0)
            {

                //车牌号码
                string strEntryLicense = new string(entryInfo.license);
                groupPlateResult.groupLicense = strEntryLicense;

                //入口时间
                UInt64 entryTotalSeconds1 = entryInfo.tvPTS.uTVSec;
                string strEntryDateTime1 = date1970ConvertCurrentDate(entryTotalSeconds1);
                groupPlateResult.entryDateTime = strEntryDateTime1;
                
            }

            //获取出口设备组网车牌结果
            else if (exitEntryInfo == 1)
            {
   
                //车牌号码
                string strExitLicense2 = new string(entryInfo.license);
                groupPlateResult.groupLicense = strExitLicense2;

                //入口时间
                UInt64 entryTotalSeconds2 = entryInfo.tvPTS.uTVSec;
                string EntryDateTime2 = date1970ConvertCurrentDate(entryTotalSeconds2);
                groupPlateResult.entryDateTime = EntryDateTime2;
   
                //出口时间
                UInt64 exitTotalSeconds2 = exitInfo.tvPTS.uTVSec;
                string strExitDateTime2 = date1970ConvertCurrentDate(exitTotalSeconds2);
                groupPlateResult.exitDateTime = strExitDateTime2;
               
                //时间间隔
                Int32 timeInterval = (Int32)(exitTotalSeconds2 - entryTotalSeconds2);
                string strTimeInterval = timeIntervalConvert(timeInterval);
                groupPlateResult.timeInterval = strTimeInterval;
              
            }

            //入口图片
            if (entryInfo.uId > 0 && entryImageInfo.vzSN.ToString() != "")
            {
                string strEntrySN = new string(entryImageInfo.vzSN);
                char[] chrEntrySN = new char[17];
                strEntrySN.CopyTo(0, chrEntrySN, 0, 17);
                string strNewEntrySN = new string(chrEntrySN);

                LoadGroupImg(handle, (int)entryInfo.uId, strNewEntrySN, 1);
            }

            //出口图片
            if (exitInfo.uId > 0 && exitImageInfo.vzSN.ToString() != "")
            {
                string strExitSN = new string(exitImageInfo.vzSN);
                char[] chrExitSN = new char[17];
                strExitSN.CopyTo(0, chrExitSN, 0, 17);
                string strNewExitSN = new string(chrExitSN);

                LoadGroupImg(handle, (int)exitInfo.uId, strNewExitSN, 0);

            }

            int size = Marshal.SizeOf(groupPlateResult);
            IntPtr intptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(groupPlateResult, intptr, true);

            Win32API.PostMessage(hwndMain, MSG_GROUP_PLATE_INFO, (int)intptr, handle);//将消息推送给窗体

            return 0;
        }

        //开启接收组网结果
        private void btnGroupOpen_Click(object sender, EventArgs e)
        {
            int lprHandle = GetSeleHandle();
            if (lprHandle == 0)
            {
                MessageBox.Show("请选择一台列表中的设备");
                return;
            }

            string strDeviceIP = GetSeleDeviceIP();
            int retSetDG = VzClientSDK.VzLPRClient_SetDGResultEnable(lprHandle, true);
            if (retSetDG != 0)
            {
                MessageBox.Show(strDeviceIP +"开启接收组网结果失败!","提示");
                return;
            }

            this.listViewOpenDevice.SelectedItems[0].SubItems[1].Text = "是";
            //MessageBox.Show(strDeviceIP + "开启接收组网结果成功!", "提示");

            //设置识别结果的回调函数（组网）
            if (flag < 10)
            {
                m_PlateGroupResult[flag] = new VzClientSDK.VZLPRC_PLATE_GROUP_INFO_CALLBACK(OnPlateGroupInfo);
                VzClientSDK.VzLPRClient_SetPlateGroupInfoCallBack(lprHandle, m_PlateGroupResult[flag], IntPtr.Zero);
                flag++;
            }
            else
            {
                MessageBox.Show("开启接收组网结果的设备已达到10个，请关闭一些设备!");
                return;
            }
        }

        //关闭第一个窗口
        private delegate void Pic1CloseThread();
        public void Pic1Close()
        {
            Pic1CloseThread Pic1CloseDelegate = delegate()
            {
                pictureBox1.Image = null;
                pictureBox1.Refresh();
                pictureBox1.Tag = 0;
            };
            pictureBox1.Invoke(Pic1CloseDelegate);
        }

        //关闭第二个窗口
        private delegate void Pic2CloseThread();
        public void Pic2Close()
        {
            Pic2CloseThread Pic2CloseDelegate = delegate()
            {
                pictureBox2.Image = null;
                pictureBox2.Refresh();
                pictureBox2.Tag = 0;
            };
            pictureBox2.Invoke(Pic2CloseDelegate);
        }

        //关闭第三个窗口
        private delegate void Pic3CloseThread();
        public void Pic3Close()
        {
            Pic2CloseThread Pic3CloseDelegate = delegate()
            {
                pictureBox3.Image = null;
                pictureBox3.Refresh();             
            };
            pictureBox3.Invoke(Pic3CloseDelegate);
        }

        //关闭第四个窗口
        private delegate void Pic4CloseThread();
        public void Pic4Close()
        {
            Pic1CloseThread Pic4CloseDelegate = delegate()
            {
                pictureBox4.Image = null;
                pictureBox4.Refresh();
            };

            pictureBox4.Invoke(Pic4CloseDelegate);
        }

        //关闭设备
        private void btnCloseDevice_Click(object sender, EventArgs e)
        {
            int lprHandle = GetSeleHandle();
            if (lprHandle == 0)
            {
                MessageBox.Show("请选择一台列表中的设备");
                return;
            }

            int index = 0;
            if (lprHandle == GetPicBox1Handle())
            {
                if (m_nPlayHandle > 0)
                {
                    int ret = VzClientSDK.VzLPRClient_StopRealPlay(m_nPlayHandle);
                    m_nPlayHandle = 0;
                }

                Pic1Close();
                Pic3Close();
                lblPlateResult1.Text = "";
            }
            else
            {
                if (m_nPlayHandle2 > 0)
                {
                    int ret = VzClientSDK.VzLPRClient_StopRealPlay(m_nPlayHandle2);
                    m_nPlayHandle2 = 0;
                }

                Pic2Close();
                Pic4Close();
                lblPlateResult2.Text = "";
            }

            VzClientSDK.VzLPRClient_Close(lprHandle);

            //删除列表中当前选中项
            index = this.listViewOpenDevice.SelectedItems[0].Index;
            listViewOpenDevice.Items[index].Remove();
        }

        //停止组网车牌识别结果
        private void btnGroupClose_Click(object sender, EventArgs e)
        {
            int lprHandle = GetSeleHandle();
            if (lprHandle == 0)
            {
                MessageBox.Show("请选择一台列表中的设备");
                return;
            }

            string strDeviceIP = GetSeleDeviceIP();
            int retSetDG = VzClientSDK.VzLPRClient_SetDGResultEnable(lprHandle, false);//关闭“接收组网识别结果"
            if (retSetDG != 0)
            {
                MessageBox.Show(strDeviceIP + "停止接收组网结果失败!", "提示");
                return;
            }

            this.listViewOpenDevice.SelectedItems[0].SubItems[1].Text = "否";
            //MessageBox.Show(strDeviceIP + "停止接收组网结果成功!", "提示");

            VzClientSDK.VzLPRClient_SetPlateGroupInfoCallBack(lprHandle, null, IntPtr.Zero);
            
        }

        private void btnstopplay_Click(object sender, EventArgs e)
        {
            if (m_bFirst)
            {
                if (m_nPlayHandle > 0)
                {

                    VzClientSDK.VzLPRClient_StopRealPlay(m_nPlayHandle);
                    m_nPlayHandle = 0;
                }

                int lprHandle = GetPicBox1Handle();
                if (lprHandle > 0)
                {
                    VzClientSDK.VzLPRClient_SetPlateInfoCallBack(lprHandle, null, IntPtr.Zero, 0);
                }

                Pic1Close();
                Pic3Close();

            }
            else
            {
                if (m_nPlayHandle2> 0)
                {

                    VzClientSDK.VzLPRClient_StopRealPlay(m_nPlayHandle2);
                    m_nPlayHandle2 = 0;
                }

                int lprHandle2 = GetPicBox2Handle();
                if (lprHandle2 > 0)
                {
                    VzClientSDK.VzLPRClient_SetPlateInfoCallBack(lprHandle2, null, IntPtr.Zero, 0);
                }

                Pic2Close();
                Pic4Close();
            }
        }

        //关闭窗口
        private void VzGroupNetDemo_FormClosed(object sender, FormClosedEventArgs e)
        {
            VzClientSDK.VzLPRClient_Cleanup();
        }
        
    }
    
     public class Win32API
    {
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("User32.dll", EntryPoint = "FindWindowEx")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpClassName, string lpWindowName);

        /// <summary>
        /// 自定义的结构
        /// </summary>
        public struct My_lParam
        {
            public int i;
            public string s;
        }
        /// <summary>
        /// 使用COPYDATASTRUCT来传递字符串
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct COPYDATASTRUCT
        {
            public IntPtr dwData;
            public int cbData;
            [MarshalAs(UnmanagedType.LPStr)]
            public string lpData;
        }
        //消息发送API
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(
            IntPtr hWnd,        // 信息发往的窗口的句柄
           int Msg,            // 消息ID
            int wParam,         // 参数1
            int lParam          //参数2
        );


        //消息发送API
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(
            IntPtr hWnd,        // 信息发往的窗口的句柄
           int Msg,            // 消息ID
            int wParam,         // 参数1
            ref My_lParam lParam //参数2
        );

        //消息发送API
        [DllImport("User32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessage(
            IntPtr hWnd,        // 信息发往的窗口的句柄
           int Msg,            // 消息ID
            int wParam,         // 参数1
            ref  COPYDATASTRUCT lParam  //参数2
        );

        //消息发送API
        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(
            IntPtr hWnd,        // 信息发往的窗口的句柄
           int Msg,            // 消息ID
            int wParam,         // 参数1
            int lParam            // 参数2
        );



        //消息发送API
        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(
            IntPtr hWnd,        // 信息发往的窗口的句柄
           int Msg,            // 消息ID
            int wParam,         // 参数1
            ref My_lParam lParam //参数2
        );

        //异步消息发送API
        [DllImport("User32.dll", EntryPoint = "PostMessage")]
        public static extern int PostMessage(
            IntPtr hWnd,        // 信息发往的窗口的句柄
           int Msg,            // 消息ID
            int wParam,         // 参数1
            ref  COPYDATASTRUCT lParam  // 参数2
        );

    }
}
