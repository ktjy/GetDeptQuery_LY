using GetDeptQuery_LY.Entity.RegisterConfirmInfo.YQ;
using GetDeptQuery_LY.Entity.RegisterDeptInfo.HIS;
using GetDeptQuery_LY.Entity.RegisterDeptInfo.YQ;
using GetDeptQuery_LY.Entity.RegisterDoctorInfo.HIS;
using GetDeptQuery_LY.Entity.RegisterDoctorInfo.YQ;
using GetDeptQuery_LY.Entity.RegisterDoctorPartTimeInfo.YQ;
using GetDeptQuery_LY.Entity.RegisterDoctorScheduleInfo.YQ;
using GetDeptQuery_LY.Entity.RegisterNumType.YQ;
using Pnb50NetClient.Command;
using Pnb50NetUtilty;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;
using YQHelperInterface;
using YQHelperInterface.Helper;

namespace GetDeptQuery_LY.Action
{
    // Token: 0x0200003B RID: 59
    public class RegisterAction
    {
        /// <summary>
        /// 挂号获取科室
        /// </summary>
        /// <param name="InXml">入参</param>
        /// <returns>出参</returns>
        public string MOP_OutpRegisterDeptQuery_LY(string InXml)
        {
            Log.AddTrace("-------------------------- MOP_OutpRegisterDeptQuery 方法开始 ---------------------------------");
            Log.AddTrace("-------------------------- 源启入参：" + InXml + " ---------------------------------");
            GetDeptQuery_LY.Entity.RegisterDeptInfo.YQ.request request = XmlSerializerHelper.XmlDeserialize<GetDeptQuery_LY.Entity.RegisterDeptInfo.YQ.request>(InXml);
            GetDeptQuery_LY.Entity.RegisterDeptInfo.YQ.response response = new GetDeptQuery_LY.Entity.RegisterDeptInfo.YQ.response();
            string text0 = "select t.dept_code,t.dept_name,t.parent_id,t.location,t.ORDERNUM from powerrst.HOSPITAL_DEPT t ";
            RequestRegDept requestRegDept = new RequestRegDept();
            requestRegDept.TypeCodeList = "01,02,03,04,05,06,07,08,09,10,11,12,13,14,15,16,17,18";
            DateTime now = DateTime.Now;
            requestRegDept.Date = request.DepartType;
            string text = XmlSerializerHelper.XmlSerializer<RequestRegDept>(requestRegDept);
            if (request.Level == "0")
            {
                text0 += "where t.dept_level=1 order by t.parent_id,t.ordernum";
                Log.AddTrace("----查询一级科室进入sqltext：" + text0 + "--------");
                GetDeptQuery_LY.Entity.RegisterDeptInfo.YQ.data_row[] array = this.cmmondSrv.GetListEntity<GetDeptQuery_LY.Entity.RegisterDeptInfo.YQ.data_row>(text0).ToArray();
                if (array != null && array.Length != 0)
                {
                    response.returnresult.returncode = 1;
                    response.returnresult.errormsg = "获取科室信息成功";
                    response.data = array;
                }
                else
                {
                    response.returnresult.returncode = -1;
                    response.returnresult.errormsg = "获取科室信息失败";
                }
                Log.AddTrace("----查询一级科室结束：" + response.returnresult.errormsg.Trim() + "--------");
            }
            else if (request.Level == "1")
            {
                Log.AddTrace("--------------------------  his接口（008）入参：" + text + " ---------------------------------");
                string text3 = HisAction.Process("008", text);
                Log.AddTrace("--------------------------  his接口（008）出参：" + text3 + " ---------------------------------");
                StringBuilder stringBuilder2 = new StringBuilder(text3);
                stringBuilder2.Replace("</Err>", "</Err><DeptLists>");
                stringBuilder2.Replace("</ReponseRegDept>", "</DeptLists></ReponseRegDept>");
                text3 = stringBuilder2.ToString();
                ReponseRegDept reponseRegDept = XmlSerializerHelper.XmlDeserialize<ReponseRegDept>(text3);
                text0 += "where t.dept_level=2 and t.parent_id=? order by t.parent_id,t.ordernum";
                this.cmmondSrv.AddParameter(request.DepartCode);
                Log.AddTrace("---- 查询二级科室进入sqltext：" + text0 + " -------");
                GetDeptQuery_LY.Entity.RegisterDeptInfo.YQ.data_row[] array = this.cmmondSrv.GetListEntity<GetDeptQuery_LY.Entity.RegisterDeptInfo.YQ.data_row>(text0).ToArray();
                if (reponseRegDept.Result == 1 && array.Length > 0)
                {
                    List<GetDeptQuery_LY.Entity.RegisterDeptInfo.YQ.data_row> list2 = new List<GetDeptQuery_LY.Entity.RegisterDeptInfo.YQ.data_row>();
                    List<GetDeptQuery_LY.Entity.RegisterDeptInfo.YQ.data_row> list3 = new List<GetDeptQuery_LY.Entity.RegisterDeptInfo.YQ.data_row>();

                    foreach (DeptList deptList in reponseRegDept.DeptLists)
                    {
                        list2.Add(new GetDeptQuery_LY.Entity.RegisterDeptInfo.YQ.data_row
                        {
                            DEPT_CODE = deptList.ID,
                            DEPT_NAME = deptList.Name,
                            LOCATION = deptList.Address
                        });
                    }
                    Dictionary<string, string> map = new Dictionary<string, string>();
                    for (int k = 0; k < array.Length; k++)
                    {
                        map.Add(array[k].DEPT_CODE, "");
                    }

                    for (int m = 0; m < list2.ToArray().Length; m++)
                    {
                        //数组一已经存在的直值是否包含指定的键值，包含的才取出来
                        if (map.ContainsKey(list2.ToArray()[m].DEPT_CODE))
                        {
                            list3.Add(list2.ToArray()[m]);
                        }
                    }
                    response.data = list3.ToArray();
                    response.returnresult.returncode = reponseRegDept.Result;
                    response.returnresult.errormsg = reponseRegDept.Err;

                }
                else
                {
                    response.returnresult.returncode = -1;
                    response.returnresult.errormsg = reponseRegDept.Err;
                }
            }
            string text4 = XmlSerializerHelper.XmlSerializer<GetDeptQuery_LY.Entity.RegisterDeptInfo.YQ.response>(response);
            //Log.AddTrace("-------------------------- 源启出参：" + text4 + " ---------------------------------");
            Log.AddTrace("-------------------------- MOP_OutpRegisterDeptQuery 方法结束 ---------------------------------");
            return text4;
        }

        public string MOP_OutpReserverDeptQuery_LY(string InXml)
        {
            Log.AddTrace("-------------------------- MOP_OutpRegisterDeptQuery 方法开始 ---------------------------------");
            Log.AddTrace("-------------------------- 源启入参：" + InXml + " ---------------------------------");
            GetDeptQuery_LY.Entity.RegisterDeptInfo.YQ.request request = XmlSerializerHelper.XmlDeserialize<GetDeptQuery_LY.Entity.RegisterDeptInfo.YQ.request>(InXml);
            GetDeptQuery_LY.Entity.RegisterDeptInfo.YQ.response response = new GetDeptQuery_LY.Entity.RegisterDeptInfo.YQ.response();
            List<Entity.RegisterDeptInfo.YQ.data_row> list = new List<Entity.RegisterDeptInfo.YQ.data_row>();
            string text0 = "select t.dept_code,t.dept_name,t.parent_id,t.location,t.ORDERNUM from powerrst.HOSPITAL_DEPT t ";
            RequestRegDept requestRegDept = new RequestRegDept();
            requestRegDept.TypeCodeList = "01,02,03,04,05,06,07,08,09,10,11,12,13,14,15,16,17,18";
            requestRegDept.Date = DateTime.Now.ToString("yyyy-MM-dd");
            requestRegDept.DateEnd = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");
            ReponseRegDept reponseRegDept = new ReponseRegDept();
            if (request.Level == "0")
            {
                text0 += "where t.dept_level=1 order by t.parent_id,t.ordernum";
                Log.AddTrace("----查询一级科室进入sqltext：" + text0 + "--------");
                GetDeptQuery_LY.Entity.RegisterDeptInfo.YQ.data_row[] array = this.cmmondSrv.GetListEntity<GetDeptQuery_LY.Entity.RegisterDeptInfo.YQ.data_row>(text0).ToArray();
                if (array != null && array.Length != 0)
                {
                    response.returnresult.returncode = 1;
                    response.returnresult.errormsg = "获取科室信息成功";
                    response.data = array;
                }
                else
                {
                    response.returnresult.returncode = -1;
                    response.returnresult.errormsg = "获取科室信息失败";
                }
                Log.AddTrace("----查询一级科室结束：" + response.returnresult.errormsg.Trim() + "--------");
            }
            else if (request.Level == "1")
            {
                string text2 = XmlSerializerHelper.XmlSerializer<RequestRegDept>(new RequestRegDept
                {
                    TypeCodeList = requestRegDept.TypeCodeList,
                    Date = requestRegDept.Date,
                    DateEnd = requestRegDept.DateEnd
                });
                Log.AddTrace("--------------------------  his接口（008）入参：" + text2.Trim() + " ---------------------------------");
                string text3 = HisAction.Process("008", text2);
                Log.AddTrace("--------------------------  his接口（008）出参：" + text3.Trim() + " ---------------------------------");
                StringBuilder stringBuilder2 = new StringBuilder(text3);
                stringBuilder2.Replace("</Err>", "</Err><DeptLists>");
                stringBuilder2.Replace("</ReponseRegDept>", "</DeptLists></ReponseRegDept>");
                text3 = stringBuilder2.ToString();
                reponseRegDept = XmlSerializerHelper.XmlDeserialize<ReponseRegDept>(text3);
                text0 += "where t.dept_level=2 and t.parent_id=? order by t.parent_id,t.ordernum";
                this.cmmondSrv.AddParameter(request.DepartCode);
                Log.AddTrace("---- 查询二级科室进入sqltext：" + text0 + " -------");
                GetDeptQuery_LY.Entity.RegisterDeptInfo.YQ.data_row[] array = this.cmmondSrv.GetListEntity<GetDeptQuery_LY.Entity.RegisterDeptInfo.YQ.data_row>(text0).ToArray();
                if (reponseRegDept.Result == 1 && array.Length > 0)
                {
                    List<GetDeptQuery_LY.Entity.RegisterDeptInfo.YQ.data_row> list2 = new List<GetDeptQuery_LY.Entity.RegisterDeptInfo.YQ.data_row>();
                    List<GetDeptQuery_LY.Entity.RegisterDeptInfo.YQ.data_row> list3 = new List<GetDeptQuery_LY.Entity.RegisterDeptInfo.YQ.data_row>();

                    foreach (DeptList deptList in reponseRegDept.DeptLists)
                    {
                        list2.Add(new GetDeptQuery_LY.Entity.RegisterDeptInfo.YQ.data_row
                        {
                            DEPT_CODE = deptList.ID,
                            DEPT_NAME = deptList.Name,
                            LOCATION = deptList.Address
                        });
                    }
                    Dictionary<string, string> map = new Dictionary<string, string>();
                    for (int k = 0; k < array.Length; k++)
                    {
                        map.Add(array[k].DEPT_CODE, "");
                    }

                    for (int m = 0; m < list2.ToArray().Length; m++)
                    {
                        //数组一已经存在的直值是否包含指定的键值，包含的才取出来
                        if (map.ContainsKey(list2.ToArray()[m].DEPT_CODE))
                        {
                            list2.ToArray()[m].PARENT_ID = request.DepartCode;
                            list3.Add(list2.ToArray()[m]);
                        }
                    }
                    response.data = list3.ToArray();
                    response.returnresult.returncode = reponseRegDept.Result;
                    response.returnresult.errormsg = reponseRegDept.Err;

                }
                else
                {
                    response.returnresult.returncode = -1;
                    response.returnresult.errormsg = reponseRegDept.Err;
                }
            }
            string text4 = XmlSerializerHelper.XmlSerializer<GetDeptQuery_LY.Entity.RegisterDeptInfo.YQ.response>(response);
            //Log.AddTrace("-------------------------- 源启出参：" + text4 + " ---------------------------------");
            Log.AddTrace("-------------------------- MOP_OutpRegisterDeptQuery 方法结束 ---------------------------------");
            return text4;
        }

        public string MOP_RegisterGetDoctorSchedule(string InXml)
        {
            Log.AddTrace("-------------------------- MOP_OutpPartTimeQuery 方法开始 ---------------------------------");
            Log.AddTrace("-------------------------- 源启入参：" + InXml + " ---------------------------------");
            Entity.RegisterDoctorPartTimeInfo.YQ.request request = XmlSerializerHelper.XmlDeserialize<Entity.RegisterDoctorPartTimeInfo.YQ.request>(InXml);
            Entity.RegisterDoctorPartTimeInfo.YQ.response response = new Entity.RegisterDoctorPartTimeInfo.YQ.response();
            List<Doctor> list = new List<Doctor>();
            string text2 = XmlSerializerHelper.XmlSerializer<RequestRegDoct>(new RequestRegDoct
            {
                Date = DateTime.Now.ToString("yyyyMMdd"),
                //Date = "20191121",
                TypeCodeList = "01,02,03,04,05,06,07,08,09,10,11,12,13,14,15,16,17,18",
                DeptCode = request.deptcode,
                PactCode = "01"
            });
            Log.AddTrace("--------------------------  his接口（009）入参：" + text2 + " ---------------------------------");
            string text3 = HisAction.Process("009", text2);
            Log.AddTrace("--------------------------  his接口（009）出参：" + text3 + " ---------------------------------");
            StringBuilder stringBuilder = new StringBuilder(text3);
            stringBuilder.Replace("</Err>", "</Err><Doctors>");
            stringBuilder.Replace("</ReponseRegDoct>", "</Doctors></ReponseRegDoct>");
            text3 = stringBuilder.ToString();
            ReponseRegDoct reponseRegDoct = XmlSerializerHelper.XmlDeserialize<ReponseRegDoct>(text3);
            if (reponseRegDoct.Result == 1 && reponseRegDoct.Doctors != null && reponseRegDoct.Doctors.Length != 0)
            {
                list.AddRange(reponseRegDoct.Doctors);
            }
            if (list.Count > 0)
            {
                List<Entity.RegisterDoctorPartTimeInfo.YQ.data_row> list2 = new List<Entity.RegisterDoctorPartTimeInfo.YQ.data_row>();
                response.returnresult.returncode = 1;
                foreach (Doctor doctor in list)
                {

                    if (doctor.DoctorCode == request.doctorno && doctor.SchemaID == request.scheduleId && !string.IsNullOrEmpty(doctor.TimePoint))
                    {

                        string[] array = doctor.TimePoint.Split(new char[]
                        {
                            ','
                        });
                        for (int j = 0; j < array.Length; j++)
                        {
                            Entity.RegisterDoctorPartTimeInfo.YQ.data_row data_row = new Entity.RegisterDoctorPartTimeInfo.YQ.data_row();
                            data_row.DeptCode = request.deptcode;
                            data_row.DeptName = doctor.DeptName;
                            data_row.DoctorNo = doctor.DoctorCode;
                            data_row.DoctorName = doctor.DoctorName;
                            data_row.ScheduleId = doctor.SchemaID;//排班ID
                            data_row.PartTimeId = array[j].Split('|')[0];//分时ID
                            data_row.DoctorNo = doctor.DoctorCode;//医生编码
                            data_row.StartTime = array[j].Split('|')[1];//开始时间
                            data_row.EndTime = array[j].Split('|')[2];//结束时间
                            data_row.ClinicFee = Convert.ToDecimal(doctor.DiagFee);//诊察费
                            data_row.RegisterFee = Convert.ToDecimal(doctor.RegFee);//挂号费
                            data_row.Ext1 = doctor.BeginTime.Split(' ')[0];//就诊日期
                            data_row.Ext2 = doctor.IsAppend;//是否加号
                            DateTime dateTime = Convert.ToDateTime(data_row.EndTime);
                            int k = DateTime.Compare(dateTime, DateTime.Now);
                            if (k > 0) { list2.Add(data_row); }
                            //list2.Add(data_row);
                        }
                        //break;
                    }
                    if (doctor.DoctorCode == request.doctorno && doctor.SchemaID == request.scheduleId && doctor.IsAppend == "1")
                    {
                        Entity.RegisterDoctorPartTimeInfo.YQ.data_row data_row = new Entity.RegisterDoctorPartTimeInfo.YQ.data_row();
                        data_row.DeptCode = request.deptcode;
                        data_row.DeptName = doctor.DeptName;
                        data_row.DoctorNo = doctor.DoctorCode;
                        data_row.DoctorName = doctor.DoctorName;
                        data_row.ScheduleId = doctor.SchemaID;//排班ID
                        data_row.PartTimeId = "";//分时ID
                        data_row.DoctorNo = doctor.DoctorCode;//医生编码                       
                        switch (doctor.Noon)
                        {
                            case "1":
                                data_row.StartTime = "08:00";
                                data_row.EndTime = "12:00";
                                break;
                            case "2":
                                data_row.StartTime = "14:00";
                                data_row.EndTime = "18:00";
                                break;
                            case "3":
                                data_row.StartTime = "18:00";
                                data_row.EndTime = "23:00";
                                break;
                        }
                        //data_row.EndTime = "+";//结束时间
                        data_row.ClinicFee = Convert.ToDecimal(doctor.DiagFee);//诊察费
                        data_row.RegisterFee = Convert.ToDecimal(doctor.RegFee);//挂号费
                        data_row.Ext1 = doctor.BeginTime.Split(' ')[0];//就诊日期
                        data_row.Ext2 = doctor.IsAppend;//是否加号
                        data_row.Ext3 = doctor.IsAppend;//加号标识
                        list2.Add(data_row);
                    }
                }
                response.data = list2.ToArray();
            }
            else
            {
                response.returnresult.returncode = -1;
                response.returnresult.errormsg = "获取医生分时信息失败";
            }
            string text4 = XmlSerializerHelper.XmlSerializer<Entity.RegisterDoctorPartTimeInfo.YQ.response>(response);
            //Log.AddTrace("-------------------------- 源启出参：" + text4 + " ---------------------------------");
            Log.AddTrace("-------------------------- MOP_OutpPartTimeQuery 方法结束 ---------------------------------");
            return text4;
        }

        public string MOP_OutpRegisterLock(string InXml)
        {
            Log.AddTrace("-------------------------- MOP_OutpRegisterLock 方法开始 ---------------------------------");
            Entity.RegisterLock.YQ.request request = XmlSerializerHelper.XmlDeserialize<Entity.RegisterLock.YQ.request>(InXml);
            Entity.RegisterLock.YQ.response response = new Entity.RegisterLock.YQ.response();
            string Ybstring = "";
            if (request.LockType == "1")
            {
                if (string.IsNullOrEmpty(request.Ext5))
                {
                    Ybstring = request.Ext5;
                }
                else
                {
                    YBTranMsg ybcostCode = (YBTranMsg)HelperJson.GetObjectNew(request.Ext5, typeof(YBTranMsg));
                    ybcostCode.DJH = request.HisOrderNO;
                    ybcostCode.LSH = request.HisOrderNO;
                    ybcostCode.JBR = request.Ext3;
                    for (int i = 0; i < ybcostCode.items.Length; i++)
                    {
                        ybcostCode.items[i].LSH = request.HisOrderNO;
                        ybcostCode.items[i].JBR = request.Ext3;
                        ybcostCode.items[i].CD = "";
                    }
                    Ybstring = HelperJson.GetJsonStringNew(ybcostCode);
                }
            }
            if (string.IsNullOrEmpty(request.Ext4.Trim()))
            {
                response.returnresult.returncode = 1;
                response.returnresult.errormsg = "锁号成功";
                response.data.YBTranMsg = Ybstring;
                string text = XmlSerializerHelper.XmlSerializer(response);
                Log.AddTrace("-------------------------- MOP_OutpRegisterLock 方法结束 ---------------------------------");
                Log.AddTrace("-------------------------- 源启出参：" + text + " ---------------------------------");
                return text;
            }
            else
            {
                if (request.LockType == "-1") { request.LockType = "2"; }
                string text = XmlSerializerHelper.XmlSerializer(new Entity.RegisterLock.HIS.RequestReging
                {
                    CardNo = request.PatientID,
                    SchemaID = request.Ext2,
                    TimepointSortId = request.Ext4,
                    TypeCode = request.LockType,
                    UserID = request.Ext3,
                    FlowNo = request.HisOrderNO
                });
                Log.AddTrace("--------------------------  his接口（042）入参：" + text + " ---------------------------------");
                string text2 = HisAction.Process("042", text);
                Log.AddTrace("--------------------------  his接口（042）出参：" + text2 + " ---------------------------------");
                if (string.IsNullOrEmpty(text2.Trim()))
                {
                    response.returnresult.returncode = -1;
                    response.returnresult.errormsg = "锁号失败";
                }
                else
                {
                    Entity.RegisterLock.HIS.ResultInfo resultInfo = XmlSerializerHelper.XmlDeserialize<Entity.RegisterLock.HIS.ResultInfo>(text2);
                    response.returnresult.returncode = Convert.ToInt32(resultInfo.Result);
                    response.returnresult.errormsg = resultInfo.Err;
                    response.data.YBTranMsg = Ybstring;
                }
                string text3 = XmlSerializerHelper.XmlSerializer<Entity.RegisterLock.YQ.response>(response);
                Log.AddTrace("-------------------------- MOP_OutpRegisterLock 方法结束 ---------------------------------");
                //Log.AddTrace("-------------------------- 源启出参：" + text3 + " ---------------------------------");
                return text3;
            }
        }

        public string MOP_OutpDoctorQuery_LY(string InXml)
        {
            Log.AddTrace("-------------------------- MOP_OutpDoctorQuery 方法开始 ---------------------------------");
            Log.AddTrace("-------------------------- 源启入参：" + InXml + " ---------------------------------");
            Entity.RegisterDoctorInfo.YQ.request request = XmlSerializerHelper.XmlDeserialize<Entity.RegisterDoctorInfo.YQ.request>(InXml);
            Entity.RegisterDoctorInfo.YQ.response response = new Entity.RegisterDoctorInfo.YQ.response();
            string text = XmlSerializerHelper.XmlSerializer<RequestRegDoct>(new RequestRegDoct
            {
                TypeCodeList = request.ParentDeptCode,
                DeptCode = request.DepartCode,
                Date = request.Ext1,
                PactCode = request.Ext2
            });
            Log.AddTrace("--------------------------  his接口（009）入参：" + text + " ---------------------------------");
            string text2 = HisAction.Process("009", text);
            Log.AddTrace("--------------------------  his接口（009）出参：" + text2 + " ---------------------------------");
            StringBuilder stringBuilder = new StringBuilder(text2);
            stringBuilder.Replace("</Err>", "</Err><Doctors>");
            stringBuilder.Replace("</ReponseRegDoct>", "</Doctors></ReponseRegDoct>");
            text2 = stringBuilder.ToString();
            ReponseRegDoct reponseRegDoct = XmlSerializerHelper.XmlDeserialize<ReponseRegDoct>(text2);
            if (reponseRegDoct.Result == 1)
            {
                List<Entity.RegisterDoctorInfo.YQ.data_row> list = new List<Entity.RegisterDoctorInfo.YQ.data_row>();
                response.returnresult.returncode = reponseRegDoct.Result;
                response.returnresult.errormsg = reponseRegDoct.Err;
                new List<string>();
                foreach (Doctor doctor in reponseRegDoct.Doctors)
                {
                    Entity.RegisterDoctorInfo.YQ.data_row data_row = new Entity.RegisterDoctorInfo.YQ.data_row();
                    data_row.DepartCode = doctor.DeptCode;
                    data_row.DeptName = doctor.DeptName;
                    data_row.DoctorCode = doctor.DoctorCode;
                    data_row.DoctorName = doctor.DoctorName;
                    data_row.DoctorTitle = doctor.ReglevelName;
                    data_row.TimeInterval = doctor.Noon;
                    data_row.RegisterFee = doctor.RegFee;
                    data_row.ClinicFee = doctor.DiagFee;
                    data_row.Ext1 = doctor.SchemaID;
                    data_row.RegisterCount = doctor.Reged;
                    data_row.Ext2 = doctor.Wait;
                    data_row.Ext3 = doctor.RegLimit;
                    data_row.Ext4 = doctor.Reged;
                    if (string.IsNullOrEmpty(doctor.YbInMsg))
                    {
                        data_row.Ext5 = doctor.YbInMsg;
                    }
                    else
                    {
                        YBTranMsg ybcostCode = (YBTranMsg)HelperJson.GetObjectNew(doctor.YbInMsg, typeof(YBTranMsg));
                        data_row.Ext5 = HelperJson.GetJsonStringNew(ybcostCode);
                    }
                    DoctorDes multiEntity = this.cmmondSrv.GetMultiEntity<DoctorDes>(string.Format("select t.doctor_no,t.doctor_des from powerrst.hospital_doctor t where t.doctor_no='{0}'", data_row.DoctorCode));
                    if (multiEntity != null)
                    {
                        data_row.DoctorDes = multiEntity.doctor_des;
                    }
                    list.Add(data_row);
                }
                list = list.OrderBy(a => a.DoctorCode).ThenBy(a => a.TimeInterval).ToList();
                response.data = list.ToArray();
            }
            else
            {
                response.returnresult.returncode = -1;
                response.returnresult.errormsg = reponseRegDoct.Err;
            }
            string text3 = XmlSerializerHelper.XmlSerializer<Entity.RegisterDoctorInfo.YQ.response>(response);
            //Log.AddTrace("-------------------------- 源启出参：" + text3 + " ---------------------------------");
            Log.AddTrace("-------------------------- MOP_OutpDoctorQuery 方法结束 ---------------------------------");
            return text3;
        }

        public string AddHisRegisterCostSettlement(string InXml)
        {
            Log.AddTrace("-------------------------- 医保挂号开始 ---------------------------------");
            Log.AddTrace("-------------------------- 医保挂号接口入参：" + InXml + " ---------------------------------");
            Entity.RegisterConfirmInfo.YQ.request request = XmlSerializerHelper.XmlDeserialize<Entity.RegisterConfirmInfo.YQ.request>(InXml);
            Entity.RegisterConfirmInfo.YQ.response response = new Entity.RegisterConfirmInfo.YQ.response();
            Entity.RegisterConfirmInfo.HIS.RequestReg requestReg = new Entity.RegisterConfirmInfo.HIS.RequestReg();
            Entity.RegisterConfirmInfo.HIS.YBOutMsg yBOutMsg = new Entity.RegisterConfirmInfo.HIS.YBOutMsg();
            yBOutMsg.BCXJZF = request.BCXJZF;
            yBOutMsg.BCZHZF = request.BCZHZF;
            yBOutMsg.BGRQ = request.BGRQ;
            yBOutMsg.BNBCBXZCLJ = request.BNBCBXZCLJ;
            yBOutMsg.BNBCYLBXZCLJ = request.BNDBJZZCLJ;
            yBOutMsg.BNDEBFJRBCYLLJ = request.BNDEBFJRBCYLLJ;
            yBOutMsg.BNDEBCYLBXZCLJ = request.BNDEBCYLBXZCLJ;
            yBOutMsg.BNGWYBZZCLJ = request.BNGWYBZZCLJ;
            yBOutMsg.BNJRJZJFYLJ = request.BNJRJZJFYLJ;
            yBOutMsg.BNJRTCFYLJ = request.BNJRTCFYLJ;
            yBOutMsg.BNMZMXBDBZCLJ = request.BNMZMXBDBZCLJ;
            yBOutMsg.BNMZMXBFHJBYLFYLJ = request.BNMZMXBFHJBYLFYLJ;
            yBOutMsg.BNMZMXBFHJBYLFYLJ_1 = request.BNMZMXBFHJBYLFYLJ_1;
            yBOutMsg.BNMZMXBQFBZZFLJ = request.BNMZMXBQFBZZFLJ;
            yBOutMsg.BNMZMXBTCZCLJ = request.BNMZMXBTCZCLJ;
            yBOutMsg.BNMZMXBTJTZZFLJ = request.BNMZMXBTJTZZFLJ;
            yBOutMsg.BNMZMXBYLYPZFLJ = request.BNMZMXBYLYPZFLJ;
            yBOutMsg.BNTCBCYLBXZCLJ = request.BNTCBCYLBXZCLJ;
            yBOutMsg.BNTCBFJRBCYLLJ = request.BNTCBFJRBCYLLJ;
            yBOutMsg.BNTCZCLJ = request.BNTCZCLJ;
            yBOutMsg.BNTSMZTCZCLJ = request.BNTSMZTCZCLJ;
            yBOutMsg.BNTSMZDEJZZCLJ = request.BNTSMZDEJZZCLJ;
            yBOutMsg.BNXJZCLJ = request.BNXJZCLJ;
            yBOutMsg.BNZFFYLJ_1 = request.BNZFFYLJ_1;
            yBOutMsg.BNZFFYLJ_2 = request.BNZFFYLJ_2;
            yBOutMsg.BNZFYLJ = request.BNZFYLJ;
            yBOutMsg.BNZHYE = request.BNZHYE;
            yBOutMsg.BNZLFYLJ = request.BNZLFYLJ;
            yBOutMsg.BNZHZCLJ = request.BNZHZCLJ;
            yBOutMsg.BNZYCS = request.BNZYCS;
            yBOutMsg.BNZYFHJBYLFYLJ_1 = request.BNZYFHJBYLFYLJ_1;
            yBOutMsg.BNZYFHJBYLFYLJ_2 = request.BNZYFHJBYLFYLJ_2;
            yBOutMsg.BNZYTJTZZFLJ = request.BNZYTJTZZFLJ;
            yBOutMsg.BNZYYLYPZFLJ = request.BNZYYLYPZFLJ;
            yBOutMsg.CCZLFAZFFY = request.CCZLFAZFFY;
            yBOutMsg.CFDXZF = request.CFDXZF;
            yBOutMsg.CSD = request.CSD;
            yBOutMsg.CSRQ = request.CSRQ;
            yBOutMsg.DBJZJJZF = request.DBJZJJZF;
            yBOutMsg.DWBH = request.DWBH;
            yBOutMsg.FHJBYLFY = request.FHJBYLFY;
            yBOutMsg.GMSFHM = request.GMSFHM;
            yBOutMsg.GRBH = request.GRBH;
            yBOutMsg.GRSF = request.GRSF;
            yBOutMsg.GWYBS = request.GWYBS;
            yBOutMsg.GWYBZZF = request.GWYBZZF;
            yBOutMsg.JBXXYL1 = request.JBXXYL1;
            yBOutMsg.JBXXYL2 = request.JBXXYL2;
            yBOutMsg.JRJZJFY = request.JRJZJFY;
            yBOutMsg.JRTCFY = request.JRTCFY;
            yBOutMsg.MMYDEYE = request.MMYDEYE;
            yBOutMsg.JZJZF = request.JZJZF;
            yBOutMsg.MZ = request.MZ;
            yBOutMsg.MZMXB1BM = request.MZMXB1BM;
            yBOutMsg.MZMXB1DBJZZCLJ = request.MZMXB1DBJZZCLJ;
            yBOutMsg.MZMXB1TCZCLJ = request.MZMXB1TCZCLJ;
            yBOutMsg.MZMXB2BM = request.MZMXB2BM;
            yBOutMsg.MZMXB2DBJZZCLJ = request.MZMXB2DBJZZCLJ;
            yBOutMsg.MZMXB2TCZCLJ = request.MZMXB2TCZCLJ;
            yBOutMsg.MZMXB3BM = request.MZMXB3BM;
            yBOutMsg.MZMXB3DBJZZCLJ = request.MZMXB3DBJZZCLJ;
            yBOutMsg.MZMXB3TCZCLJ = request.MZMXB3TCZCLJ;
            yBOutMsg.MZMXB4BM = request.MZMXB4BM;
            yBOutMsg.MZMXB4DBJZZCLJ = request.MZMXB4DBJZZCLJ;
            yBOutMsg.MZMXB4TCZCLJ = request.MZMXB4TCZCLJ;
            yBOutMsg.MZMXB5BM = request.MZMXB5BM;
            yBOutMsg.MZMXB5DBJZZCLJ = request.MZMXB5DBJZZCLJ;
            yBOutMsg.MZMXB5TCZCLJ = request.MZMXB5TCZCLJ;
            yBOutMsg.NZGPOXM = request.NZGPOXM;
            yBOutMsg.QFBZZF = request.QFBZZF;
            yBOutMsg.QYBCJJZF = request.QYBCJJZF;
            yBOutMsg.SBKH = request.SBKH;
            yBOutMsg.SCCYJBBM = request.SCCYJBBM;
            yBOutMsg.SCCYRQ = request.SCCYRQ;
            yBOutMsg.SCCYYDJ = request.SCCYYDJ;
            yBOutMsg.SCCYYLLB = request.SCCYYLLB;
            yBOutMsg.SCCYYY = request.SCCYYY;
            yBOutMsg.SCCYYYBM = request.SCCYYYBM;
            yBOutMsg.SCDELJ = request.SCDELJ;
            yBOutMsg.SCFHJBYLFY = request.SCFHJBYLFY;
            yBOutMsg.SCJRTCFY = request.SCJRTCFY;
            yBOutMsg.SCQFX = request.SCQFX;
            yBOutMsg.SCQFXSJZF = request.SCQFXSJZF;
            yBOutMsg.SCTCJZLJ = request.SCTCJZLJ;
            yBOutMsg.SSQBH = request.SSQBH;
            yBOutMsg.TCFDZF = request.TCFDZF;
            yBOutMsg.TCQH = request.TCQH;
            yBOutMsg.TCZFJE = request.TCZFJE;
            yBOutMsg.TJGBGWYBZ = request.TJGBGWYBZ;
            yBOutMsg.TJTZZL = request.TJTZZL;
            yBOutMsg.WHCD = request.WHCD;
            yBOutMsg.XM = request.XM;
            yBOutMsg.XB = request.XB;
            yBOutMsg.XMZLFY = request.XMZLFY;
            yBOutMsg.XZZT = request.XZZT;
            yBOutMsg.XZZWJB = request.XZZWJB;
            yBOutMsg.YBQFBZE = request.YBQFBZE;
            yBOutMsg.YDRYBS = request.YDRYBS;
            yBOutMsg.YDRYBZ = request.YDRYBZ;
            yBOutMsg.YL2 = request.YL2;
            yBOutMsg.YLFZE = request.YLFZE;
            yBOutMsg.YLRYLB = request.YLRYLB;
            yBOutMsg.YPZLFY = request.YPZLFY;
            yBOutMsg.ZFFY = request.ZFFY;
            yBOutMsg.ZHND = request.ZHND;
            yBOutMsg.ZHXXYL1 = request.ZHXXYL1;
            yBOutMsg.ZHXXYL2 = request.ZHXXYL2;
            yBOutMsg.ZYCS = request.ZYCS;
            yBOutMsg.ZYZT = request.ZYZT;
            yBOutMsg.Birthday = request.Birthday;
            yBOutMsg.PatientID = request.PatientID;
            yBOutMsg.PatientName = request.PatientName;
            yBOutMsg.PatientSex = request.PatientSex;
            yBOutMsg.DJH = request.DJH;
            yBOutMsg.LSH = request.LSH;
            yBOutMsg.YLLB = request.YLLB;
            yBOutMsg.GHRQ = request.GHRQ;
            yBOutMsg.KSMC = request.KSMC;
            yBOutMsg.JBR = request.JBR;
            yBOutMsg.YWZQH = request.YWZQH;
            yBOutMsg.YYJYLSH = request.YYJYLS;
            yBOutMsg.YBCardOutStr = request.YBCardOutStr;
            yBOutMsg.YBCostOutStr = request.YBCostOutStr;
            string[] YBarray = request.YBCostOutStr.Split(new char[] { '|' });
            yBOutMsg.ZFFY = YBarray[7];
            string YbOutStr = HelperJson.GetJsonStringNew(yBOutMsg);
            string[] array = request.TimeInterval.Split(new char[] { '|' });
            requestReg.CardNo = request.PatientID;
            requestReg.SchemaID = array[1];
            requestReg.UserID = array[2];
            requestReg.FlowNo = request.PowerTranID;
            requestReg.PactCode = "05";
            requestReg.PayType = request.PayType;
            requestReg.PowerPayChannel = request.PowerPayChannel;
            requestReg.TimePointId = array[3];
            requestReg.YBOutMsg = YbOutStr;
            string text = this.ObjectToXML(requestReg);
            Log.AddTrace("--------------------------  his接口（010）入参：" + text + " ---------------------------------");
            string text2 = HisAction.Process("010", text);
            Log.AddTrace("--------------------------  his接口（010）出参：" + text2 + " ---------------------------------");
            if (!string.IsNullOrEmpty(text2))
            {
                Entity.RegisterConfirmInfo.HIS.ReponseReg reponseReg = XmlSerializerHelper.XmlDeserialize<Entity.RegisterConfirmInfo.HIS.ReponseReg>(text2);
                if (reponseReg.Result == 1)
                {
                    response.returnresult.returncode = reponseReg.Result;
                    response.returnresult.errormsg = reponseReg.Err;
                    response.data.QueueCount = reponseReg.SeeNO;
                    response.data.Location = reponseReg.SeeAddress;
                    response.data.OrderNo = request.PowerTranID;
                    response.data.Ext1 = reponseReg.SeePointTime;
                    response.data.Ext2 = reponseReg.ClinicCode;
                }
                else
                {
                    response.returnresult.returncode = -1;
                    response.returnresult.errormsg = reponseReg.Err;
                }
            }
            else
            {
                response.returnresult.returncode = -1;
                response.returnresult.errormsg = "医保挂号失败";
            }
            string OutXml = XmlSerializerHelper.XmlSerializer<Entity.RegisterConfirmInfo.YQ.response>(response);
            //Log.AddTrace("-------------------------- 医保挂号接口出参：" + OutXml + " ---------------------------------");
            Log.AddTrace("-------------------------- 医保挂号结束 ---------------------------------");
            return OutXml;
        }


        public string ObjectToXML(object obj)
        {
            string result;
            using (StringWriter stringWriter = new StringWriter())
            {
                Type type = obj.GetType();
                XmlSerializer xmlSerializer = new XmlSerializer(obj.GetType());
                xmlSerializer.Serialize(stringWriter, obj);
                stringWriter.Close();
                result = stringWriter.ToString();
            }
            return result;
        }


        private SQLCommandService cmmondSrv = new SQLCommandService();

    }
}
