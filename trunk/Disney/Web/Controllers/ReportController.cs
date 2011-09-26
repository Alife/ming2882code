using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using Models.Enums;
using BLL;
using Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Data;
using System.Text;

namespace Web.Controllers
{
    public class ReportController : BaseController
    {
        #region 查询月结单
        [AdminAuthorize("totolorder,totolartermonth", "select,select")]
        public ContentResult totolorder(int start, int limit, string state, string beginTime, string endTime)
        {
            int records = 0;
            DataTable list = d_TotolMonthBLL.GetList(start, limit, ref records, 0, state, beginTime, endTime);
            JsonSerializerSettings jsonSs = new JsonSerializerSettings();
            jsonSs.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            jsonSs.Converters.Add(new Newtonsoft.Json.Converters.DataTableConverter());
            string json = JsonConvert.SerializeObject(list, Formatting.None, jsonSs);
            MessageBox msg = new MessageBox();
            msg.data = json;
            msg.records = records;
            msg.success = true;
            json = JsonConvert.SerializeObject(msg, Formatting.None);
            json = json.Replace("\\", "");
            json = json.Replace("\"[", "[");
            json = json.Replace("]\"", "]");
            string[] arrJson = json.Split('}');
            for (int i = 0; i < arrJson.Length - 2; i++)
            {
                arrJson[i] += string.Format(",\"StateText\":\"{0}\"",
                    GetEnumBLL.GetEnumDescription(Enum.Parse(typeof(Balance), list.Rows[i]["State"].ToString()))) + " }";
            }
            json = String.Concat(arrJson) + " }";
            return Content(json);
        }
        #endregion
        #region 美工工资
        [AdminAuthorize("artermonthreport", "select")]
        public ContentResult artermonthreport(int start, int limit, string state, string beginTime, string endTime)
        {
            int records = 0;
            int arterid = t_UserBLL.BaseUser.ID;
            DataTable list = d_TotolMonthBLL.GetList(start, limit, ref records, arterid, state, beginTime, endTime);
            JsonSerializerSettings jsonSs = new JsonSerializerSettings();
            jsonSs.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            jsonSs.Converters.Add(new Newtonsoft.Json.Converters.DataTableConverter());
            string json = JsonConvert.SerializeObject(list, Formatting.None, jsonSs);
            MessageBox msg = new MessageBox();
            msg.data = json;
            msg.records = records;
            msg.success = true;
            json = JsonConvert.SerializeObject(msg, Formatting.None);
            json = json.Replace("\\", "");
            json = json.Replace("\"[", "[");
            json = json.Replace("]\"", "]");
            string[] arrJson = json.Split('}');
            for (int i = 0; i < arrJson.Length - 2; i++)
            {
                arrJson[i] += string.Format(",\"StateText\":\"{0}\"",
                    GetEnumBLL.GetEnumDescription(Enum.Parse(typeof(Balance), list.Rows[i]["State"].ToString()))) + " }";
            }
            json = String.Concat(arrJson) + " }";
            return Content(json);
        }
        [AdminAuthorize("artermonthreport", "select")]
        public ContentResult artermonthreportdetail(int id, string arter, string beginTime, string endTime)
        {
            DataTable list = ReportBLL.totolorderdetail(id, arter, beginTime, endTime);
            JsonSerializerSettings jsonSs = new JsonSerializerSettings();
            jsonSs.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            jsonSs.Converters.Add(new Newtonsoft.Json.Converters.DataTableConverter());
            string json = JsonConvert.SerializeObject(list, Formatting.None, jsonSs);
            return Content(json);
        }
        #endregion
        #region 月结操作
        #region 月结
        [AdminAuthorize("totolorder", "month")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult op_month(int id)
        {
            d_TotolMonth item = d_TotolMonthBLL.GetItem(id);
            TryUpdateModel(item, Request.Form.AllKeys);
            item.State = (int)Balance.Bal;
            if (d_TotolMonthBLL.Update(item) > 0)
                return Json(new MessageBox(true, "月结成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "月结失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 美工月结
        [AdminAuthorize("totolorder", "artermonth")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult op_artermonth(int id)
        {
            d_TotolMonth item = d_TotolMonthBLL.GetItem(id);
            item.ArterBalanceTime = DateTime.Now;
            item.State = (int)Balance.ArtBal;
            if (d_TotolMonthBLL.Update(item) > 0)
                return Json(new MessageBox(true, "美工月结成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "美工月结失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        #region 查询月结单明显
        [AdminAuthorize("totolorder", "select")]
        public ContentResult totolorderdetail(int id, string arter, string beginTime, string endTime)
        {
            DataTable list = ReportBLL.totolorderdetail(id, arter, beginTime, endTime);
            JsonSerializerSettings jsonSs = new JsonSerializerSettings();
            jsonSs.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            jsonSs.Converters.Add(new Newtonsoft.Json.Converters.DataTableConverter());
            string json = JsonConvert.SerializeObject(list, Formatting.None, jsonSs);
            //string[] arrJson = json.Split('}');
            //for (int i = 0; i < arrJson.Length - 1; i++)
            //{
            //    decimal photoNum = 0;
            //    photoNum += (decimal)(Convert.ToDecimal(list.Rows[i]["Cover"].ToString()) / 2);
            //    photoNum += (decimal)(Convert.ToDecimal(list.Rows[i]["Head"].ToString()) / 2);
            //    photoNum += (decimal)((decimal)((int)(list.Rows[i]["ClassmatesPeopleNum"]) + (int)(list.Rows[i]["ClassmatesTeacher"])) / 2);
            //    photoNum += (decimal)((int)list.Rows[i]["Robe"] + (int)list.Rows[i]["GroupPhoto"] + (int)list.Rows[i]["Life"]);
            //    arrJson[i] += string.Format(",\"PhotoNum\":{0}", photoNum) + " }";
            //}
            //json = String.Concat(arrJson);
            return Content(json);
        }
        #region 已完成明显
        [AdminAuthorize("finishtotol", "select")]
        public ContentResult finishtotol(string arter, string beginTime, string endTime)
        {
            DataTable list = ReportBLL.finishtotol(arter, beginTime, endTime);
            JsonSerializerSettings jsonSs = new JsonSerializerSettings();
            jsonSs.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            jsonSs.Converters.Add(new Newtonsoft.Json.Converters.DataTableConverter());
            string json = JsonConvert.SerializeObject(list, Formatting.None, jsonSs);
            return Content(json);
        }
        #endregion
        [AdminAuthorize("totolorder", "select")]
        public FileContentResult exporttotolorderdetail(int id, string arter, string beginTime, string endTime)
        {
            d_TotolMonth totolMonth = d_TotolMonthBLL.GetItem(id);
            DataTable dt = ReportBLL.totolorderdetail(id, arter, beginTime, endTime);
            string str = string.Empty;
            str += "园所名称,人数,礼服,团照,生活照,同学录,老師,學生人數,封面,大头贴\r\n";
            decimal[] arr = new decimal[9];
            foreach (DataRow row in dt.Rows)
            {
                str += row["Custom"].ToString() + ",";
                str += row["Strength"].ToString() + ",";
                str += row["Robe"].ToString() + ",";
                str += row["GroupPhoto"].ToString() + ",";
                str += row["Life"].ToString() + ",";
                str += row["Classmates"].ToString() + ",";
                str += row["ClassmatesTeacher"].ToString() + ",";
                str += row["ClassmatesPeopleNum"].ToString() + ",";
                str += row["Cover"].ToString() + ",";
                str += row["Head"].ToString() + "\r\n";
                arr[0] += (int)row["Strength"];
                arr[1] += (int)row["Robe"];
                arr[2] += (int)row["GroupPhoto"];
                arr[3] += (int)row["Life"];
                arr[4] += (int)row["Classmates"];
                arr[5] += (int)row["ClassmatesTeacher"];
                arr[6] += (int)row["ClassmatesPeopleNum"];
                arr[7] += (int)row["Cover"];
                arr[8] += (int)row["Head"];
            }
            str += string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}\r\n", "总计", arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6], arr[7], arr[8]);
            str += "统计信息\r\n";
            var p1000_all = arr[1] + arr[2] + arr[3];
            var p1000 = p1000_all >= 1000 ? 1000 : p1000_all;
            var ps1000 = p1000_all > 1000 ? p1000_all - 1000 : 0;
            var pst1000 = ps1000 * (decimal)0.7;
            var class_p = arr[5] + arr[6]; 
            var class_totol = class_p * (decimal)0.5;
            var cover_p = arr[7]; 
            var cover_totol = cover_p * (decimal)0.5;
            var head_p = arr[8]; 
            var head_totol = head_p * (decimal)0.5;
            var totol = p1000 + pst1000 + class_totol + cover_totol + head_totol;
            str += string.Format("禮服/學士/團照/生活照/全家福/便服共:,{0}\r\n", p1000);
            str += string.Format("余禮服/學士/團照/生活照/全家福/便服:,{0}\r\n", pst1000);
            str += string.Format("同學錄=老師+學生數:,{0}\r\n", class_totol);
            str += string.Format("封面/年歷:,{0}\r\n", cover_totol);
            str += string.Format("大頭貼:,{0}\r\n", head_totol);
            str += string.Format("共計:,{0}", totol);
            byte[] buffer = System.Text.Encoding.GetEncoding("utf-8").GetBytes(str);
            string name = string.Empty;
            if (!string.IsNullOrEmpty(arter))
                name = t_UserBLL.GetItem(arter).TrueName;
            Response.AppendHeader("Content-Disposition", "attachment;filename=\"截止" + totolMonth.EndTime.ToShortDateString() + name + "结算报表.csv\"");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            byte[] outBuffer = new byte[buffer.Length + 3];
            outBuffer[0] = (byte)0xEF;//有BOM,解决乱码
            outBuffer[1] = (byte)0xBB;
            outBuffer[2] = (byte)0xBF;
            Array.Copy(buffer, 0, outBuffer, 3, buffer.Length);  
            return File(outBuffer, "application/ms-excel");
        }

        [AdminAuthorize("totolorder", "select")]
        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public FileContentResult exporttotolorderdetail1(int id, string exportContent)
        {
            d_TotolMonth totolMonth = d_TotolMonthBLL.GetItem(id);
            Response.AppendHeader("Content-Disposition", "attachment;filename=\"截止" + totolMonth.EndTime.ToShortDateString() + "结算报表.xls\"");
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("utf-8");
            return File(System.Text.Encoding.GetEncoding("utf-8").GetBytes(exportContent), "application/vnd.ms-excel");
        }
        #endregion
        #region 美工月结
        [AdminAuthorize("totolartermonth", "select")]
        public ContentResult artermonthtotol(int id, string arter, string beginTime, string endTime)
        {
            DataTable list = ReportBLL.ArterMonth(id, arter, beginTime, endTime);
            JsonSerializerSettings jsonSs = new JsonSerializerSettings();
            jsonSs.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            jsonSs.Converters.Add(new Newtonsoft.Json.Converters.DataTableConverter());
            string json = JsonConvert.SerializeObject(list, Formatting.None, jsonSs);
            return Content(json);
        }
        #endregion
        #region 美工月结明显
        [AdminAuthorize("totolartermonth", "select")]
        public ContentResult artermonth(int start, int limit, int id, string arter, string beginTime, string endTime)
        {
            int records = 0;
            DataTable list = d_KitPhotoBLL.GetList(start, limit, ref records, id, arter, beginTime, endTime);
            JsonSerializerSettings jsonSs = new JsonSerializerSettings();
            jsonSs.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            jsonSs.Converters.Add(new Newtonsoft.Json.Converters.DataTableConverter());
            string json = JsonConvert.SerializeObject(list, Formatting.None, jsonSs);
            MessageBox msg = new MessageBox();
            msg.data = json;
            msg.records = records;
            msg.success = true;
            json = JsonConvert.SerializeObject(msg, Formatting.None);
            json = json.Replace("\\", "");
            json = json.Replace("\"[", "[");
            json = json.Replace("]\"", "]");
            return Content(json);
        }
        #endregion
        #region 主页统计
        [AdminAuthorize]
        public JsonResult getmaintotol()
        {
            UserType userType = (UserType)UserBaseType.Type;
            string userID = string.Empty;
            int[] arr = new int[5];
            if (userType == UserType.Admin)
            {
                userID = sys_DataPermissionBLL.GetList(UserBase.ID, ResourceType.OrderQuery);
                arr[0] = ReportBLL.GetMainTotol("kit", userID);
                userID = sys_DataPermissionBLL.GetList(UserBase.ID, ResourceType.WorkOrderQuery);
                arr[1] = ReportBLL.GetMainTotol("kitend", userID);
                userID = sys_DataPermissionBLL.GetList(UserBase.ID, ResourceType.WorkOrderQuery);
                arr[2] = ReportBLL.GetMainTotol("worker", userID);
                userID = sys_DataPermissionBLL.GetList(UserBase.ID, ResourceType.Proofs);
                arr[3] = ReportBLL.GetMainTotol("editphoto", userID);
                arr[4] = ReportBLL.GetMainTotol("proof", userID);
            }
            if (userType == UserType.Artist)
            {
                arr[0] = 0; arr[1] = 0;
                userID = sys_DataPermissionBLL.GetList(UserBase.ID, ResourceType.WorkOrderQuery);
                arr[2] = ReportBLL.GetMainTotol("worker", userID);
                userID = sys_DataPermissionBLL.GetList(UserBase.ID, ResourceType.Proofs);
                arr[3] = ReportBLL.GetMainTotol("editphoto", userID);
                arr[4] = ReportBLL.GetMainTotol("proof", userID);
            }
            if (userType == UserType.Custom)
            {
                arr[0] = 0; arr[1] = 0; arr[2] = 0; arr[3] = 0;
                arr[4] = ReportBLL.GetMainTotol("proof", UserBase.ID.ToString());
            }
            var item = new { kit = arr[0], kitend = arr[1], worker = arr[2], editphoto = arr[3], proof = arr[4] };
            return Json(item, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 工作单统计
        [AdminAuthorize("workordertotol", "select")]
        public ContentResult workordertotol()
        {
            DataTable list = ReportBLL.workordertotol();
            JsonSerializerSettings jsonSs = new JsonSerializerSettings();
            jsonSs.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            jsonSs.Converters.Add(new Newtonsoft.Json.Converters.DataTableConverter());
            string json = JsonConvert.SerializeObject(list, Formatting.None, jsonSs);
            return Content(json);
        }
        #endregion
    }
}
