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
using System.IO;

namespace Web.Controllers
{
    public class OrderController : BaseController
    {
        #region 枚举
        public ContentResult kitstate()
        {
            List<string[]> arrStr = GetEnumBLL.GetEnumOpt(typeof(KitState));
            GridJSONHelper jsonObject = new GridJSONHelper();
            jsonObject.success = true;
            foreach (string[] str in arrStr)
            {
                jsonObject.AddItem("displayText", str[0]);
                jsonObject.AddItem("displayValue", str[1]);
                jsonObject.ItemOk();
            }
            return Content(jsonObject.ToString());
        }
        public ContentResult kitphotostate()
        {
            List<string[]> arrStr = GetEnumBLL.GetEnumOpt(typeof(KitPhotoState));
            GridJSONHelper jsonObject = new GridJSONHelper();
            jsonObject.success = true;
            foreach (string[] str in arrStr)
            {
                jsonObject.AddItem("displayText", str[0]);
                jsonObject.AddItem("displayValue", str[1]);
                jsonObject.ItemOk();
            }
            return Content(jsonObject.ToString());
        }
        public ContentResult kitproofstate()
        {
            List<string[]> arrStr = GetEnumBLL.GetEnumOpt(typeof(KitProofState));
            GridJSONHelper jsonObject = new GridJSONHelper();
            jsonObject.success = true;
            foreach (string[] str in arrStr)
            {
                jsonObject.AddItem("displayText", str[0]);
                jsonObject.AddItem("displayValue", str[1]);
                jsonObject.ItemOk();
            }
            return Content(jsonObject.ToString());
        }
        public ContentResult KitTypeState()
        {
            List<string[]> arrStr = GetEnumBLL.GetEnumOpt(typeof(KitTypeState));
            GridJSONHelper jsonObject = new GridJSONHelper();
            jsonObject.success = true;
            foreach (string[] str in arrStr)
            {
                jsonObject.AddItem("displayText", str[0]);
                jsonObject.AddItem("displayValue", str[1]);
                jsonObject.ItemOk();
            }
            return Content(jsonObject.ToString());
        }
        public ContentResult balance()
        {
            List<string[]> arrStr = GetEnumBLL.GetEnumOpt(typeof(Balance));
            GridJSONHelper jsonObject = new GridJSONHelper();
            jsonObject.success = true;
            foreach (string[] str in arrStr)
            {
                jsonObject.AddItem("displayText", str[0]);
                jsonObject.AddItem("displayValue", str[1]);
                jsonObject.ItemOk();
            }
            return Content(jsonObject.ToString());
        }
        #endregion
        #region 单据列表
        #region 单据
        [AdminAuthorize("crankorder", "select")]
        public ContentResult kit(int start, int limit, string keyword, string custom, string state, string beginTime, string endTime)
        {
            string userID = sys_DataPermissionBLL.GetList(UserBase.ID, ResourceType.OrderQuery);
            int records = 0;
            DataTable list = d_KitBLL.GetList(start, limit, ref records, keyword, custom, userID, state, beginTime, endTime);
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
                    GetEnumBLL.GetEnumDescription(Enum.Parse(typeof(KitState), list.Rows[i]["State"].ToString()))) + " }";
            }
            json = String.Concat(arrJson) + " }";
            return Content(json);
        }
        #endregion
        #region 工作单
        [AdminAuthorize("workorder", "select")]
        public ContentResult workorder(int start, int limit, string keyword, string custom, string arter, string state,
            string proofState, string beginTime, string endTime)
        {
            string arterID = sys_DataPermissionBLL.GetList(UserBase.ID, ResourceType.WorkOrderQuery);
            return Content(getkitphoto(start, limit, keyword, custom, arter, string.Empty, arterID, state, proofState, beginTime, endTime, string.Empty, string.Empty));
        }
        #endregion
        #region 完成单
        [AdminAuthorize("finishorder", "select")]
        public ContentResult finishorder(int start, int limit, string keyword, string custom, string arter, string state,
            string proofState, string beginTime, string endTime)
        {
            string arterID = sys_DataPermissionBLL.GetList(UserBase.ID, ResourceType.WorkOrderQuery);
            return Content(getkitphoto(start, limit, keyword, custom, arter, string.Empty, arterID, state, proofState, string.Empty, string.Empty, beginTime, endTime));
        }
        #endregion
        #region 校图
        [AdminAuthorize("checkphoto", "select")]
        public ContentResult checkphoto(int start, int limit, string keyword, string proofState, string arter, string state)
        {
            string custom = string.Empty;
            string userid = string.Empty;
            if (UserBaseType.Type == (int)UserType.Artist)
            {
                userid = sys_DataPermissionBLL.GetList(UserBase.ID, ResourceType.Proofs);
                proofState = ((int)KitProofState.StartDeal).ToString();
                return Content(getkitphoto(start, limit, keyword, custom, arter, string.Empty, userid, state, proofState, string.Empty, string.Empty, string.Empty, string.Empty));
            }
            else if (UserBaseType.Type == (int)UserType.Custom)
            {
                custom = UserBase.UserCode;
                proofState = string.Format("{0},{1}", (int)KitProofState.UnProof, (int)KitProofState.Deal);
                return Content(getkitphoto(start, limit, keyword, custom, arter, string.Empty, string.Empty, state, proofState, string.Empty, string.Empty, string.Empty, string.Empty));
            }
            else if (UserBaseType.Type == (int)UserType.Admin)
            {
                var dataper = sys_DataPermissionBLL.GetItem(UserBase.ID, ResourceType.Proofs);
                if (dataper != null && dataper.Confine == (int)Confine.Global)
                {
                    userid = string.Empty;
                    if (string.IsNullOrEmpty(proofState))
                        proofState = string.Format("{0},{1},{2},{3}", (int)KitProofState.UnProof, (int)KitProofState.Deal, (int)KitProofState.Finish, (int)KitProofState.StartDeal);
                }
                else if (dataper != null && (dataper.Confine == (int)Confine.Company || dataper.Confine == (int)Confine.Dept))
                {
                    userid = sys_DataPermissionBLL.GetList(UserBase.ID, ResourceType.Proofs);
                    if (string.IsNullOrEmpty(proofState))
                        proofState = string.Format("{0},{1}", (int)KitProofState.UnProof, (int)KitProofState.Finish);
                    return Content(getkitphoto(start, limit, keyword, custom, arter, string.Empty, userid, state, proofState, string.Empty, string.Empty, string.Empty, string.Empty));
                }
                else
                {
                    userid = string.Empty;
                    if (string.IsNullOrEmpty(proofState))
                        proofState = string.Format("{0},{1},{2},{3}", (int)KitProofState.UnProof, (int)KitProofState.Deal, (int)KitProofState.Finish, (int)KitProofState.StartDeal);
                }
            }
            return Content(getkitphoto(start, limit, keyword, custom, arter, userid, string.Empty, state, proofState, string.Empty, string.Empty, string.Empty, string.Empty));
        }
        #endregion
        #region 共用
        private string getkitphoto(int start, int limit, string keyword, string custom, string arter, string userID, string arterID,
            string state, string proofState, string sendBeginTime, string sendEndTime, string finishBeginTime, string finishEndTime)
        {
            int records = 0;
            keyword = RBTW.Simplified2Traditional(keyword);
            DataTable list = d_KitWorkBLL.GetList(start, limit, ref records, keyword, custom, arter, userID, arterID, state, proofState, 
                sendBeginTime, sendEndTime, finishBeginTime, finishEndTime);
            JsonSerializerSettings jsonSs = new JsonSerializerSettings();
            jsonSs.Converters.Add(new JavaScriptDateTimeConverter());
            jsonSs.Converters.Add(new DataTableConverter());
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
                    GetEnumBLL.GetEnumDescription(Enum.Parse(typeof(KitPhotoState), list.Rows[i]["State"].ToString())));
                arrJson[i] += string.Format(",\"TypeText\":\"{0}\"",
                    GetEnumBLL.GetEnumDescription(Enum.Parse(typeof(KitTypeState), list.Rows[i]["Type"].ToString())));
                arrJson[i] += string.Format(",\"ProofStateText\":\"{0}\"", list.Rows[i]["ProofState"] == DBNull.Value ? "" :
                    GetEnumBLL.GetEnumDescription(Enum.Parse(typeof(KitProofState), list.Rows[i]["ProofState"].ToString()))) + " }";
            }
            json = String.Concat(arrJson) + " }";
            return json;
        }
        #endregion
        #endregion
        #region 增加单据
        [AdminAuthorize("crankorder", "add")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult kitadd(string classsends)
        {
            d_Kit kit = new d_Kit();
            TryUpdateModel(kit, Request.Form.AllKeys);
            if (string.IsNullOrEmpty(kit.Name))
                kit.Name = t_UserBLL.GetItem(kit.CustomID).TrueName;
            List<d_KitClass> classList = JsonConvert.DeserializeObject<List<d_KitClass>>(classsends);
            kit.IsValid = true;
            kit.State = (int)KitState.Stock;
            int kitid = d_KitBLL.Insert(kit);
            classList.ForEach(new Action<d_KitClass>(delegate(d_KitClass obj) { obj.KitID = kitid; }));
            d_KitClassBLL.Insert(classList);
            classList = d_KitClassBLL.GetList(kitid);
            List<d_KitChild> childList = new List<d_KitChild>();
            foreach (var obj in classList)
            {
                for (int i = 1; i <= obj.BoyNum + obj.GirlNum; i++)
                {
                    string code = i.ToString().PadLeft(2, '0');
                    childList.Add(new d_KitChild { Code = code, TrueName = code, KitClassID = obj.ID });
                }
            }
            d_KitChildBLL.Insert(childList);
            if (kitid > 0)
                return Json(new MessageBox(true, "增加成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "增加失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 修改单据
        [AdminAuthorize("crankorder", "edit")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult kitedit(int id, string classsends)
        {
            d_Kit kit = d_KitBLL.GetItem(id);
            TryUpdateModel(kit, Request.Form.AllKeys);
            if (string.IsNullOrEmpty(kit.Name))
                kit.Name = t_UserBLL.GetItem(kit.CustomID).TrueName;
            int v = d_KitBLL.Update(kit);
            List<d_KitClass> classList = JsonConvert.DeserializeObject<List<d_KitClass>>(classsends);
            classList.ForEach(new Action<d_KitClass>(delegate(d_KitClass obj) { obj.KitID = id; }));
            d_KitClassBLL.Insert(classList);
            List<d_KitChild> childList = new List<d_KitChild>();
            foreach (var obj in classList)
            {
                if (obj.ID == 0)
                {
                    for (int i = 0; i < obj.BoyNum + obj.GirlNum; i++)
                    {
                        string code = i.ToString().PadLeft(2, '0');
                        childList.Add(new d_KitChild { Code = code, TrueName = code, KitClassID = obj.ID });
                    }
                }
            }
            d_KitChildBLL.Insert(childList);
            if (v > 0)
                return Json(new MessageBox(true, "修改成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "修改失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 删除单据
        [AdminAuthorize("crankorder", "del")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult deletekit()
        {
            List<string> ids = Request["id"].Split(',').ToList();
            if (d_KitBLL.Delete(ids) > 0)
                return Json(new MessageBox(true, "删除成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "删除失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 工作单管理
        #region 增加工作单
        [AdminAuthorize("workorder", "add")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult kitworkadd()
        {
            d_KitWork item = new d_KitWork();
            TryUpdateModel(item, Request.Form.AllKeys);
            d_Kit kit = d_KitBLL.GetItem(item.KitID);
            kit.State = (int)KitState.Process;
            d_KitBLL.Update(kit);
            item.State = (int)KitPhotoState.Process;
            if (d_KitWorkBLL.Insert(item) > 0)
                return Json(new MessageBox(true, "增加成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "增加失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 修改工作单
        [AdminAuthorize("workorder", "edit")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult kitworkedit(int id, bool? isCooperate)
        {
            d_KitWork item = d_KitWorkBLL.GetItem(id);
            TryUpdateModel(item, Request.Form.AllKeys);
            if (item.State != (int)KitPhotoState.MonthEnd)
                item.TotolMonthID = null;
            if (d_KitWorkBLL.Update(item) > 0)
            {
                if (!isCooperate.HasValue)
                    d_KitPhotoBLL.Update(item.ID, item.ArterID);
                return Json(new MessageBox(true, "修改成功"), JsonRequestBehavior.AllowGet);
            }
            return Json(new MessageBox(false, "修改失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 删除单据
        [AdminAuthorize("workorder", "del")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult deletekitwork()
        {
            List<string> ids = Request["id"].Split(',').ToList();
            if (d_KitWorkBLL.Delete(ids) > 0)
                return Json(new MessageBox(true, "删除成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "删除失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 工作单制程完成
        public JsonResult kitworkend(int id, DateTime beginTime, DateTime finishTime, bool? isproof)
        {
            var item = d_KitWorkBLL.GetItem(id);
            var list = d_KitPhotoBLL.GetList(id);
            if (list.Count > 0)
            {
                item.State = (int)KitPhotoState.End;
                if (isproof.HasValue && isproof.Value)
                    item.ProofState = (int)KitProofState.UnProof;
                else
                    item.ProofState = (int)KitProofState.NoProof;
                item.BeginTime = beginTime;
                item.FinishTime = finishTime;
                item.ProofTime = finishTime;
                if (d_KitWorkBLL.Update(item) > 0)
                    return Json(new MessageBox(true, "制程完成"), JsonRequestBehavior.AllowGet);
                return Json(new MessageBox(false, "制程失败"), JsonRequestBehavior.AllowGet);
            }
            return Json(new MessageBox(false, "失败，没有工作单明细"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 工作单明显查询
        [AdminAuthorize("workorder", "select")]
        public ContentResult kitphoto(int id)
        {
            var list = d_KitPhotoBLL.GetList(id);
            decimal photoNum = 0;
            decimal amount = 0;
            JsonSerializerSettings jsonSs = new JsonSerializerSettings();
            string json = JsonConvert.SerializeObject(list, Formatting.Indented, jsonSs);
            string[] arrJson = json.Split('}');
            for (int i = 0; i < arrJson.Length - 1; i++)
            {
                var kitphototype = d_KitPhotoTypeBLL.GetItem(list[i].KitPhotoTypeID);
                arrJson[i] += string.Format(",\"KitPhotoType\":\"{0}\"", kitphototype.Name);
                var user = t_UserBLL.GetItem(list[i].ArterID);
                arrJson[i] += string.Format(",\"Arter\":\"{0}({1})\"", user.TrueName, user.UserCode) + " }";
                if (kitphototype.Category == (int)KitPhotoType.Cover || kitphototype.Category == (int)KitPhotoType.Head)
                    photoNum += (decimal)((decimal)list[i].PhotoNum / 2);
                else if (kitphototype.Category == (int)KitPhotoType.Classmates && kitphototype.ID != 3)
                    photoNum += (decimal)((decimal)(list[i].PeopleNum + list[i].TeacherNum) / 2);
                else if (kitphototype.Category == (int)KitPhotoType.Classmates && kitphototype.ID == 3)
                    photoNum += (decimal)((decimal)(list[i].PeopleNum * 2 + list[i].TeacherNum) / 2);
                else
                    photoNum += list[i].PhotoNum;
                amount += list[i].Amount;
                if (i == arrJson.Length - 2)
                    arrJson[i] += ",{" + string.Format("\"Arter\":\"<div style=text-align:right;color:red>统计：</div>\",\"PhotoNum\":\"<span style=color:red>{0}(P)</span>\",\"Amount\":\"<span style=color:red>{1}</span>\""
                        , photoNum, amount) + "}";
            }
            json = String.Concat(arrJson);
            return Content(json);
        }
        #endregion
        #region 增加工作单明显
        [AdminAuthorize("workorder", "add")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult kitphotoadd(int kitworkid, int? arterid, int kitworkarterID)
        {
            d_KitPhoto item = new d_KitPhoto();
            TryUpdateModel(item, Request.Form.AllKeys);
            item.KitWorkID = kitworkid;
            if (arterid.HasValue)
                item.ArterID = arterid.Value;
            else
                item.ArterID = kitworkarterID;
            d_KitPhotoType kpt = d_KitPhotoTypeBLL.GetItem(item.KitPhotoTypeID);
            string formula = kpt.Formula;
            formula = formula.Replace("Price", item.ArtistPrice.ToString());
            formula = formula.Replace("PhotoNum", item.PhotoNum.ToString());
            formula = formula.Replace("PeopleNum", item.PeopleNum.ToString());
            formula = formula.Replace("TeacherNum", item.TeacherNum.ToString());
            formula = string.Format("return {0};", formula);
            Expression exp = new Expression(formula);
            item.Amount = (decimal)exp.Compute(0);
            formula = kpt.Formula;
            formula = formula.Replace("Price", kpt.Price.ToString());
            formula = formula.Replace("PhotoNum", item.PhotoNum.ToString());
            formula = formula.Replace("PeopleNum", item.PeopleNum.ToString());
            formula = formula.Replace("TeacherNum", item.TeacherNum.ToString());
            formula = string.Format("return {0};", formula);
            exp = new Expression(formula);
            item.Amt = (decimal)exp.Compute(0);
            if (d_KitPhotoBLL.Insert(item) > 0)
            {
                //var user = t_UserBLL.GetItem(kitworkarterID);
                //int photonum = d_KitPhotoBLL.GetList(kitworkid).Sum(p => p.PhotoNum);
                ////int photonum = item.PhotoNum;
                //double day = (double)photonum / (double)user.DayNum;
                //d_KitWork kitWorkItem = null;
                //var kitwork = d_KitWorkBLL.GetItemByFinish(kitworkid, kitworkarterID);
                //if (kitwork.ID == kitworkid)
                //    kitWorkItem = kitwork;
                //else
                //    kitWorkItem = d_KitWorkBLL.GetItem(kitworkid);
                //if (kitwork.FinishTime.HasValue)
                //    kitWorkItem.PlanTime = kitwork.FinishTime.Value.AddDays(day);
                //else
                //    kitWorkItem.PlanTime = DateTime.Now.AddDays(day);
                //d_KitWorkBLL.Update(kitWorkItem);
                return Json(new MessageBox(true, "增加成功"), JsonRequestBehavior.AllowGet);
            }
            return Json(new MessageBox(false, "增加失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 修改工作单明显
        [AdminAuthorize("workorder", "edit")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult kitphotoedit(int id, int arterid)
        {
            d_KitPhoto item = d_KitPhotoBLL.GetItem(id);
            int kitworkid = item.KitWorkID;
            TryUpdateModel(item, Request.Form.AllKeys);
            d_KitPhotoType kpt = d_KitPhotoTypeBLL.GetItem(item.KitPhotoTypeID);
            string formula = kpt.Formula;
            formula = formula.Replace("Price", item.ArtistPrice.ToString());
            formula = formula.Replace("PhotoNum", item.PhotoNum.ToString());
            formula = formula.Replace("PeopleNum", item.PeopleNum.ToString());
            formula = formula.Replace("TeacherNum", item.TeacherNum.ToString());
            formula = string.Format("return {0};", formula);
            Expression exp = new Expression(formula);
            item.Amount = (decimal)exp.Compute(0);
            formula = kpt.Formula;
            formula = formula.Replace("Price", kpt.Price.ToString());
            formula = formula.Replace("PhotoNum", item.PhotoNum.ToString());
            formula = formula.Replace("PeopleNum", item.PeopleNum.ToString());
            formula = formula.Replace("TeacherNum", item.TeacherNum.ToString());
            formula = string.Format("return {0};", formula);
            exp = new Expression(formula);
            item.Amt = (decimal)exp.Compute(0);
            if (d_KitPhotoBLL.Update(item) > 0)
            {
                //var user = t_UserBLL.GetItem(arterid);
                //int photonum = d_KitPhotoBLL.GetList(kitworkid).Sum(p => p.PhotoNum);
                //double day = (double)photonum / (double)user.DayNum;
                //d_KitWork kitWorkItem = null;
                //var kitwork = d_KitWorkBLL.GetItemByFinish(kitworkid, arterid);
                //if (kitwork.ID == kitworkid)
                //    kitWorkItem = kitwork;
                //else
                //    kitWorkItem = d_KitWorkBLL.GetItem(kitworkid);
                //if (kitwork.FinishTime.HasValue)
                //    kitWorkItem.PlanTime = kitwork.FinishTime.Value.AddDays(day);
                //else
                //    kitWorkItem.PlanTime = DateTime.Now.AddDays(day);
                //d_KitWorkBLL.Update(kitWorkItem);
                int arternum = d_KitPhotoBLL.GetList(kitworkid).GroupBy(p => new { p.ArterID }).Count();
                var kitWorkItem = d_KitWorkBLL.GetItem(kitworkid);
                if (arternum > 1) kitWorkItem.IsCooperate = true;
                else kitWorkItem.IsCooperate = false;
                d_KitWorkBLL.Update(kitWorkItem);
                return Json(new MessageBox(true, "修改成功"), JsonRequestBehavior.AllowGet);
            }
            return Json(new MessageBox(false, "修改失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 删除工作单明显
        [AdminAuthorize("workorder", "del")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult kitphotodelete()
        {
            List<string> ids = Request["id"].Split(',').ToList();
            if (d_KitPhotoBLL.Delete(ids) > 0)
                return Json(new MessageBox(true, "删除成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "删除失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        #region 查询班级
        [AdminAuthorize]
        public JsonResult getclass(int id)
        {
            return Json(d_KitClassBLL.GetList(id), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 删除班级
        [AdminAuthorize("crankorder,leakorder", "del,del")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult classdelete()
        {
            List<string> ids = Request["id"].Split(',').ToList();
            if (d_KitClassBLL.Delete(ids) > 0)
                return Json(new MessageBox(true, "删除成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "删除失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 小朋友管理
        #region 查询小朋友
        [AdminAuthorize("crankorder,leakorder", "select,select")]
        public JsonResult getchild(int id)
        {
            return Json(d_KitChildBLL.GetList(id), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 增加小朋友
        [AdminAuthorize("crankorder,leakorder", "edit,edit")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult childadd(string kitChildSends)
        {
            List<d_KitChild> list = JsonConvert.DeserializeObject<List<d_KitChild>>(kitChildSends);
            int v = d_KitChildBLL.Insert(list);
            if (v > 0)
                return Json(new MessageBox(true, "保存成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "保存失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 删除小朋友
        [AdminAuthorize("crankorder,leakorder", "del,del")]
        public JsonResult childdelete(int id)
        {
            List<string> ids = Request["id"].Split(',').ToList();
            if (d_KitChildBLL.Delete(ids) > 0)
                return Json(new MessageBox(true, "删除成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "删除失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        #region 小朋友服装服装管理
        #region 查询小朋友服装
        [AdminAuthorize("crankorder,leakorder", "select,select")]
        public JsonResult getCostume(int id)
        {
            return Json(d_KitCostumeBLL.GetList(id), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 增加小朋友服装
        [AdminAuthorize("crankorder,leakorder", "edit,edit")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Costumeadd(string kitCostumeSends)
        {
            List<d_KitCostume> list = JsonConvert.DeserializeObject<List<d_KitCostume>>(kitCostumeSends);
            int v = d_KitCostumeBLL.Insert(list);
            if (v > 0)
                return Json(new MessageBox(true, "保存成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "保存失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 删除小朋友服装
        [AdminAuthorize("crankorder,leakorder", "del,del")]
        public JsonResult Costumedelete(int id)
        {
            List<string> ids = Request["id"].Split(',').ToList();
            if (d_KitCostumeBLL.Delete(ids) > 0)
                return Json(new MessageBox(true, "删除成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "删除失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        #region 已上传
        [AdminAuthorize("finishorder", "uploaded")]
        public JsonResult uploaded(string uploadFile)
        {
            List<string> ids = Request.Form["id"].Split(',').ToList();
            if (d_KitWorkBLL.Update(ids, uploadFile) > 0)
                return Json(new MessageBox(true, "成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 更換結算期
        [AdminAuthorize("finishorder", "month")]
        public JsonResult gettotolmonth()
        {
            return Json(d_TotolMonthBLL.GetList(), JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("finishorder", "month")]
        public JsonResult editmonth(int id, int totolMonthID)
        {
            var kitwork = d_KitWorkBLL.GetItem(id);
            kitwork.TotolMonthID = totolMonthID;
            if (d_KitWorkBLL.Update(kitwork) > 0)
                return Json(new MessageBox(true, "结算成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "结算失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 结算
        [AdminAuthorize("finishorder", "month")]
        public JsonResult totolmonth()
        {
            List<string> ids = Request.Form["id"].Split(',').ToList();
            d_TotolMonth totolMonth = d_TotolMonthBLL.GetItem();
            if (totolMonth == null)
            {
                totolMonth = new d_TotolMonth();
                var serialModel = sys_SerialNumberBLL.GetItem((int)SerialNember.Balance);
                totolMonth.BeginTime = serialModel.CurrentDate;
                totolMonth.EndTime = DateTime.Now;
                totolMonth.State = (int)Balance.Normal;
                totolMonth.ID = d_TotolMonthBLL.Insert(totolMonth);
                totolMonth.OrderName = totolMonth.ID.ToString() + "期";
                d_TotolMonthBLL.Update(totolMonth);
                serialModel.CurrentDate = totolMonth.EndTime;
                sys_SerialNumberBLL.Update(serialModel);
            }
            if (d_KitWorkBLL.Update(ids, totolMonth.ID) > 0)
            {
                return Json(new MessageBox(true, "结算成功"), JsonRequestBehavior.AllowGet);
            }
            return Json(new MessageBox(false, "结算失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 合并工作单
        public JsonResult coalition(string workname)
        {
            List<string> ids = Request.Form["id"].Split(',').ToList();
            string fid = ids[0];
            if (ids.Count() > 1)
            {
                ids = ids.Where(p => p != fid).ToList();
                d_KitWorkBLL.Update(ids, fid, workname);
                return Json(new MessageBox(true, "合并成功"), JsonRequestBehavior.AllowGet);
            }
            return Json(new MessageBox(false, "请选择多个同档的记录"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 重算
        [AdminAuthorize("finishorder", "month")]
        public JsonResult retotol()
        {
            List<string> ids = Request.Form["id"].Split(',').ToList();
            foreach (string id in ids)
            {
                List<d_KitPhoto> list = d_KitPhotoBLL.GetList(int.Parse(id));
                foreach(var item in list)
                {
                    //if (item.ArtistPrice == (decimal)0.4)
                    //{
                        d_KitPhotoType kpt = d_KitPhotoTypeBLL.GetItem(item.KitPhotoTypeID);
                        string formula = kpt.Formula;
                        formula = formula.Replace("Price", kpt.ArtPrice.ToString());
                        formula = formula.Replace("PhotoNum", item.PhotoNum.ToString());
                        formula = formula.Replace("PeopleNum", item.PeopleNum.ToString());
                        formula = formula.Replace("TeacherNum", item.TeacherNum.ToString());
                        formula = string.Format("return {0};", formula);
                        Expression exp = new Expression(formula);
                        item.Amount = (decimal)exp.Compute(0);
                        item.ArtistPrice = kpt.ArtPrice;
                        formula = kpt.Formula;
                        formula = formula.Replace("Price", kpt.Price.ToString());
                        formula = formula.Replace("PhotoNum", item.PhotoNum.ToString());
                        formula = formula.Replace("PeopleNum", item.PeopleNum.ToString());
                        formula = formula.Replace("TeacherNum", item.TeacherNum.ToString());
                        formula = string.Format("return {0};", formula);
                        exp = new Expression(formula);
                        item.Amt = (decimal)exp.Compute(0);
                        d_KitPhotoBLL.Update(item);
                    //}
                }
            }
            return Json(new MessageBox(true, "修改成功"), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
