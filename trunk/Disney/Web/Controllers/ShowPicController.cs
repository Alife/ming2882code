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
    public class ShowPicController : BaseController
    {
        #region 校图开始
        [AdminAuthorize]
        public JsonResult firstcheck(int id)
        {
            d_KitWork kitwork = d_KitWorkBLL.GetItem(id);
            if (!kitwork.ProofBeginTime.HasValue && UserBaseType.Type == (int)UserType.Custom)
            {
                kitwork.ProofBeginTime = DateTime.Now;
                d_KitWorkBLL.Update(kitwork);
            }
            return Json(new MessageBox(true, "提交成功"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 查询档图片
        [AdminAuthorize("checkphoto", "select")]
        public JsonResult kitimg(int kitworkid, int kitid, int? classid, string folder)
        {
            d_KitWork kitwork = d_KitWorkBLL.GetItem(kitworkid);
            double intResult = 0;
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            if (kitwork.ProofTime.HasValue) intResult = (kitwork.ProofTime.Value - startTime).TotalSeconds;
            else intResult = (DateTime.Now - startTime).TotalSeconds;
            d_Kit kit = d_KitBLL.GetItem(kitid);
            d_KitClass cls = null;
            string filepath = "/images/kitimg/" + kit.Code + "/";
            if (classid.HasValue)
            {
                cls = d_KitClassBLL.GetItem(classid.Value);
                if (cls.Code.ToLower() != kit.Code.ToLower())
                    filepath += cls.Code + "/";
            }
            else
            {
                var clslist = d_KitClassBLL.GetList(kitid);
                if (clslist.Count > 0 && clslist[0].Code.ToLower() != kit.Code.ToLower())
                    filepath += clslist[0].Code + "/";
            }
            if (!string.IsNullOrEmpty(folder))
                filepath += folder + "/";
            else
            {
                //string[] arrfolder = Directory.GetDirectories(Server.MapPath(filepath));
                //string temp = arrfolder[0].Replace("\\", "/");
                //temp = temp.Substring(temp.LastIndexOf('/') + 1);
                //filepath += temp + "/";
            }
            string path = Server.MapPath(filepath);
            string[] arrfiles = Directory.GetFiles(path, "*.jpg");
            List<KitImg> list = new List<KitImg>();
            foreach (string item in arrfiles)
            {
                FileInfo fileinfo = new FileInfo(item);
                KitImg kitimg = new KitImg();
                kitimg.name = fileinfo.Name;
                kitimg.size = fileinfo.Length;
                kitimg.url = filepath + fileinfo.Name + "?" + intResult;
                if (classid.HasValue)
                {
                    var q = d_KitQuestionBLL.GetItem(kitworkid, classid.Value, null, fileinfo.Name);
                    if (q != null) kitimg.isaq = true;
                    else kitimg.isaq = false;
                }
                else kitimg.isaq = false;
                using (System.Drawing.Image img = System.Drawing.Image.FromStream(fileinfo.Open(FileMode.Open, FileAccess.Read, FileShare.ReadWrite)))
                {
                    kitimg.width = img.Width;
                    kitimg.height = img.Height;
                    img.Dispose();
                }
                list.Add(kitimg);
            }
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 查询班级
        [AdminAuthorize("checkphoto", "select")]
        public ContentResult getclass(int id)
        {
            d_Kit kit = d_KitBLL.GetItem(id);
            string filepath = "/images/kitimg/" + kit.Code + "/";
            GridJSONHelper jsonObject = new GridJSONHelper();
            jsonObject.success = true;
            var list = d_KitClassBLL.GetList(id);
            foreach (var item in list)
            {
                bool isHad = false;
                if (item.Code.ToLower() == kit.Code.ToLower())
                    isHad = Directory.Exists(Server.MapPath(filepath));
                else
                    isHad = Directory.Exists(Server.MapPath(filepath + item.Code));
                if (isHad)
                {
                    jsonObject.AddItem("ID", item.ID);
                    jsonObject.AddItem("Name", item.Name);
                    jsonObject.AddItem("Code", item.Code);
                    jsonObject.AddItem("KitID", item.KitID);
                    jsonObject.AddItem("BoyNum", item.BoyNum);
                    jsonObject.AddItem("GirlNum", item.GirlNum);
                    jsonObject.AddItem("Imprint", item.Imprint);
                    jsonObject.ItemOk();
                }
            }
            return Content(jsonObject.ToString());
        }
        #endregion
        #region 查询档下文件夹
        [AdminAuthorize("checkphoto", "select")]
        public ContentResult getclassnum(int kitid, int? classid)
        {
            d_Kit kit = d_KitBLL.GetItem(kitid);
            d_KitClass cls = null;
            string filepath = "/images/kitimg/" + kit.Code + "/";
            if (classid.HasValue)
            {
                cls = d_KitClassBLL.GetItem(classid.Value);
                if (cls.Code.ToLower() != kit.Code.ToLower())
                    filepath += cls.Code + "/";
            }
            else
            {
                var clslist = d_KitClassBLL.GetList(kitid);
                if (clslist.Count > 0)
                {
                    classid = clslist[0].ID;
                    if (clslist[0].Code.ToLower() != kit.Code.ToLower())
                        filepath += clslist[0].Code + "/";
                }
            }
            string path = Server.MapPath(filepath);
            GridJSONHelper jsonObject = new GridJSONHelper();
            jsonObject.success = true;
            string[] arrFolder = Directory.GetDirectories(path);
            foreach (string item in arrFolder)
            {
                string name = new FileInfo(item).Name;
                var child = d_KitChildBLL.GetItem(classid.Value, name);
                if (child != null)
                {
                    jsonObject.AddItem("KitChildID", child.ID);
                    jsonObject.AddItem("ChildName", child.TrueName);
                    jsonObject.AddItem("displayText", name);
                    jsonObject.AddItem("displayValue", name);
                    jsonObject.ItemOk();
                }
            }
            return Content(jsonObject.ToString());
        }
        #endregion
        #region 查看所有问题
        [AdminAuthorize]
        public ContentResult showquestion(int id)
        {
            var list = d_KitQuestionBLL.GetList(id); 
            JsonSerializerSettings jsonSs = new JsonSerializerSettings();
            jsonSs.Converters.Add(new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
            jsonSs.Converters.Add(new Newtonsoft.Json.Converters.DataTableConverter());
            string json = JsonConvert.SerializeObject(list, Formatting.None, jsonSs);
            string[] arrJson = json.Split('}');
            for (int i = 0; i < arrJson.Length - 1; i++)
            {
                arrJson[i] += string.Format(",\"QuestionTypeText\":\"{0}\"", list.Rows[i]["QuestionType"]!=DBNull.Value?
                    GetEnumBLL.GetEnumDescription(Enum.Parse(typeof(KitQuestionType), list.Rows[i]["QuestionType"].ToString())) : string.Empty);
                arrJson[i] += string.Format(",\"StateText\":\"{0}\"",
                    GetEnumBLL.GetEnumDescription(Enum.Parse(typeof(KitQuestionState), list.Rows[i]["State"].ToString()))) + " }";
            }
            json = String.Concat(arrJson);
            return Content(json);
        }
        #endregion
        #region 查询单一问题
        [AdminAuthorize]
        public ContentResult getquestion(int kitWorkID, int kitClassID, int? kitChildID, string fileName)
        {
            var item = d_KitQuestionBLL.GetItem(kitWorkID, kitClassID, kitChildID, fileName);
            if (item == null) item = new d_KitQuestion { CreateTime = DateTime.Now };
            string json = JsonConvert.SerializeObject(item, new JavaScriptDateTimeConverter());
            return Content(json);
        }
        #endregion
        #region 提交问题
        [AdminAuthorize("checkphoto,checkphoto,checkphoto", "unsolve,solve,deal")]
        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult savequestion(int id, int? questionType)
        {
            var user = UserBase;
            d_KitQuestion item = null;
            int v = 0;
            if (id > 0)
            {
                item = d_KitQuestionBLL.GetItem(id);
                TryUpdateModel(item, Request.Form.AllKeys);
                item.CreateTime = DateTime.Now;
                if (user.ID == 1 || UserBaseType.Type == 2)
                    item.State = (int)KitQuestionState.Deal;
                else
                    item.State = (int)KitQuestionState.UnSolve;
                item.QuestionType = questionType ?? 1;
                v = d_KitQuestionBLL.Update(item);
            }
            else
            {
                item = new d_KitQuestion();
                TryUpdateModel(item, Request.Form.AllKeys);
                item.CreateTime = DateTime.Now;
                item.UserID = user.ID;
                item.State = (int)KitQuestionState.UnSolve;
                item.QuestionType = questionType ?? 1;
                v = d_KitQuestionBLL.Insert(item);
            }
            if (v > 0)
                return Json(new MessageBox(true, "保存成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "保存失败"), JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("checkphoto", "solve")]
        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult saveotherquestion(int? questionType)
        {
            d_KitQuestion item = new d_KitQuestion();
            TryUpdateModel(item, Request.Form.AllKeys);
            item.CreateTime = DateTime.Now;
            item.UserID = UserBase.ID;
            item.State = (int)KitQuestionState.UnSolve;
            item.FileName = string.Empty;
            item.QuestionType = questionType;
            int v = d_KitQuestionBLL.Insert(item);
            if (v > 0)
                return Json(new MessageBox(true, "保存成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "保存失败"), JsonRequestBehavior.AllowGet);
        }
        [AdminAuthorize("checkphoto", "otheraq")]
        [ValidateInput(false)]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult saveotheraqquestion(int id)
        {
            #region 其它问题
            //d_Kit kit = d_KitBLL.GetItem(kitid);
            //d_KitQuestion item = new d_KitQuestion();
            //TryUpdateModel(item, Request.Form.AllKeys);
            //string path = Server.MapPath("/images/kitimg/" + kit.Code + "/");
            //string[] arrfiles = Directory.GetFiles(path, "*.jpg", SearchOption.AllDirectories);
            //string filepath = arrfiles.FirstOrDefault(p => p.Contains(filename));
            //if (!string.IsNullOrEmpty(filepath))
            //{
            //    string classCode = string.Empty, childCode = string.Empty;
            //    string[] codes = filepath.Replace(path.Replace("/", "\\"), string.Empty).Replace("\\", "/").Split('/');
            //    if (codes.Length == 3) { classCode = codes[0]; childCode = codes[1]; }
            //    else if (codes.Length >= 2) { classCode = kit.Code; childCode = codes[0]; }
            //    else classCode = kit.Code;
            //    item.KitClassID = d_KitClassBLL.GetItem(kitid, classCode).ID;
            //    if (childCode != string.Empty)
            //        item.KitChildID = d_KitChildBLL.GetItem(item.KitClassID, childCode).ID;
            //    item.CreateTime = DateTime.Now;
            //    item.UserID = UserBase.ID;
            //    item.State = (int)KitQuestionState.UnSolve;
            //    item.QuestionType = questionType ?? 1;
            //    int v = d_KitQuestionBLL.Insert(item);
            //    if (v > 0)
            //        return Json(new MessageBox(true, "保存成功"), JsonRequestBehavior.AllowGet);
            //    return Json(new MessageBox(false, "保存失败"), JsonRequestBehavior.AllowGet);
            //}
            //return Json(new MessageBox(false, "文件不存在"), JsonRequestBehavior.AllowGet);
            #endregion
            d_KitQuestion item = d_KitQuestionBLL.GetItem(id);
            TryUpdateModel(item, Request.Form.AllKeys);
            int v = d_KitQuestionBLL.Update(item);
            if (v > 0)
                return Json(new MessageBox(true, "保存成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "保存失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 修改校图状态
        #region 通过校图(单记录)
        [AdminAuthorize("checkphoto,checkphoto", "unsolve,solve")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult setkitProofState(int kitWorkID, int kitClassID, int kitChildID, string fileName)
        {
            d_KitQuestion item = d_KitQuestionBLL.GetItem(kitWorkID, kitClassID, kitChildID, fileName);
            int v = 0;
            List<string> list = new List<string>();
            if (item != null)
            {
                list.Add(item.ID.ToString());
                v = d_KitQuestionBLL.Delete(list);
            }
            if (v > 0 || item == null)
                return Json(new MessageBox(true, "通过校图"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "通过校图失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 问题处理
        #region 已处理
        [AdminAuthorize("checkphoto", "deal")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult kitquestiondeal(int kitWorkID)
        {
            List<string> ids = Request["id"].Split(',').ToList();
            int v = d_KitQuestionBLL.Update(ids, (int)KitQuestionState.Deal, kitWorkID);
            if (v > 0)
                return Json(new MessageBox(true, "提交成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "提交失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region  验证处理
        [AdminAuthorize("checkphoto", "deal")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult checkdeal(int id, int kitid)
        {
            List<string> files = new List<string>();
            List<string> ids = new List<string>();
            d_Kit kit = d_KitBLL.GetItem(kitid);
            string filepath = "/images/kitimg/" + kit.Code + "/";
            string[] arrfiles = Directory.GetFiles(Server.MapPath(filepath), "*.jpg", SearchOption.AllDirectories);
            List<d_KitQuestion> list = d_KitQuestionBLL.GetListByAll(id);
            foreach (var item in list)
            {
                if (arrfiles.FirstOrDefault(p => p.Contains(item.FileName)) != null)
                    ids.Add(item.ID.ToString());
            }
            int v = d_KitQuestionBLL.Update(ids, (int)KitQuestionState.Deal, id);
            if (v > 0)
                return Json(new MessageBox(true, "提交成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "提交失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 美工解释
        [AdminAuthorize("checkphoto", "deal")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult dealremark(int id, string remark)
        {
            d_KitQuestion item = d_KitQuestionBLL.GetItem(id);
            item.Remark = remark;
            item.State = (int)KitQuestionState.Deal;
            item.IntroTime = DateTime.Now;
            int v = d_KitQuestionBLL.Update(item);
            if (v > 0)
            {
                var allq = d_KitQuestionBLL.GetListByAll(item.KitWorkID);
                int dealnum = 0;
                foreach (var q in allq)
                {
                    if (q.State == (int)KitQuestionState.Deal)
                        dealnum++;
                    else
                        break;
                }
                if (dealnum == allq.Count)
                {
                    d_KitWork kitwork = d_KitWorkBLL.GetItem(item.KitWorkID);
                    kitwork.ProofState = (int)KitProofState.Deal;
                    kitwork.ProofTime = DateTime.Now;
                    d_KitWorkBLL.Update(kitwork);
                }
                return Json(new MessageBox(true, "提交成功"), JsonRequestBehavior.AllowGet);
            }
            return Json(new MessageBox(false, "提交失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 未解决
        [AdminAuthorize("checkphoto", "unsolve")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult kitquestionunsolve(int kitWorkID)
        {
            List<string> ids = Request["id"].Split(',').ToList();
            int v = d_KitQuestionBLL.Update(ids, (int)KitQuestionState.UnSolve, kitWorkID);
            if (v > 0)
                return Json(new MessageBox(true, "提交成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "提交失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 已解决
        [AdminAuthorize("checkphoto", "solve")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult kitquestionsolve(int kitWorkID)
        {
            List<string> ids = Request["id"].Split(',').ToList();
            int v = d_KitQuestionBLL.Update(ids, (int)KitQuestionState.Solve, kitWorkID);
            if (v > 0)
                return Json(new MessageBox(true, "提交成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "提交失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 删除问题
        [AdminAuthorize("checkphoto", "delete")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult kitquestiondel()
        {
            List<string> ids = Request["id"].Split(',').ToList();
            int v = d_KitQuestionBLL.Delete(ids);
            if (v > 0)
                return Json(new MessageBox(true, "删除成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "删除失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 生成返工工作单
        [AdminAuthorize("checkphoto", "patch")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult returnpatch(int kitworkid)
        {
            List<string> ids = Request["id"].Split(',').ToList();
            d_Kit kit = d_KitBLL.GetItem(kitworkid);
            d_KitPhoto kitphoto = new d_KitPhoto();
            kitphoto.ArterID = kit.UserID;
            int v = 1;
            if (v > 0)
                return Json(new MessageBox(true, "生成成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "生成失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        #region 结束校图
        [AdminAuthorize("checkphoto", "finish")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult kitfinish(int id)
        {
            var item = d_KitWorkBLL.GetItem(id);
            item.ProofState = (int)KitProofState.Finish;
            item.ProofEndTime = DateTime.Now;
            if (d_KitWorkBLL.Update(item) > 0)
                return Json(new MessageBox(true, "结束校图"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 童话世界确认可以开始修图了
        [AdminAuthorize("checkphoto", "startdeal")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult startdeal(int id)
        {
            var item = d_KitWorkBLL.GetItem(id);
            item.ProofState = (int)KitProofState.StartDeal;
            if (d_KitWorkBLL.Update(item) > 0)
                return Json(new MessageBox(true, "提交成功"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "提交失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        #region 校图确认管理
        #region 查询全部
        [AdminAuthorize("checkphoto", "select")]
        public ContentResult finishphoto(int start, int limit, string kitName)
        {
            string proofState = string.Empty;
            string custom = string.Empty;
            string userid = string.Empty;
            if (UserBaseType.Type == (int)UserType.Custom)
            {
                custom = UserBase.ID.ToString();
                return Content(getconfirms(start, limit, kitName, custom));
            }
            userid = sys_DataPermissionBLL.GetList(UserBase.ID, ResourceType.Proofs);
            return Content(getconfirms(start, limit, kitName, userid));
        }
        private string getconfirms(int start, int limit, string kitName, string userID)
        {
            int records = 0;
            DataTable list = d_ConfirmPhotoBLL.GetList(start, limit, ref records, kitName, userID);
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
            return json;
        }
        #endregion
        #region 确认查询(单记录)
        [AdminAuthorize("checkphoto", "solve")]
        public ContentResult getconfirm(int kitWorkID, int kitClassID)
        {
            var item = d_ConfirmPhotoBLL.GetItem(kitWorkID, kitClassID);
            if (item == null) return Content("");
            return Content(JsonConvert.SerializeObject(item, Formatting.None, new JavaScriptDateTimeConverter()));
        }
        #endregion
        #region 通过校图
        [AdminAuthorize("checkphoto", "solve")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult kitsolve(int kitworkid)
        {
            d_ConfirmPhoto item = new d_ConfirmPhoto();
            TryUpdateModel(item, Request.Form.AllKeys);
            item.UserID = UserBase.ID;
            var v = d_ConfirmPhotoBLL.Insert(item);
            if (v > 0)
            {
                var workkit = d_KitWorkBLL.GetItem(kitworkid);
                workkit.ProofState = (int)KitProofState.Proof;
                d_KitWorkBLL.Update(workkit);
                d_KitQuestionBLL.Delete(kitworkid);
                if (d_KitPhotoBLL.GetCount(workkit.KitID) >= 5)
                {
                    var kit = d_KitBLL.GetItem(workkit.KitID);
                    kit.State = (int)KitState.End;
                    d_KitBLL.Update(kit); 
                    string filepath = Server.MapPath("/images/kitimg/" + kit.Code + "/");
                    if (Directory.Exists(filepath))
                    {
                        try { Directory.Delete(filepath, true); }
                        catch { }
                    }
                }
                return Json(new MessageBox(true, "通过本档校图"), JsonRequestBehavior.AllowGet);
            }
            return Json(new MessageBox(false, "失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #endregion
        #region 重新校图
        [AdminAuthorize("checkphoto,finishphoto", "recheck,recheck")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult recheck(int id)
        {
            var workkit = d_KitWorkBLL.GetItem(id);
            workkit.ProofState = (int)KitProofState.UnProof;
            if (d_KitWorkBLL.Update(workkit) > 0)
                return Json(new MessageBox(true, "操作完成"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 不校图
        [AdminAuthorize("checkphoto", "recheck")]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult notcheck(int id)
        {
            var workkit = d_KitWorkBLL.GetItem(id);
            workkit.ProofState = (int)KitProofState.NoProof;
            if (d_KitWorkBLL.Update(workkit) > 0)
                return Json(new MessageBox(true, "操作完成"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 重新加载图片
        [AdminAuthorize]
        public JsonResult updateprooftime(int id)
        {
            d_KitWork kitwork = d_KitWorkBLL.GetItem(id);
            kitwork.ProofTime = DateTime.Now;
            int v = d_KitWorkBLL.Update(kitwork);
            if (v > 0)
                return Json(new MessageBox(true, "重新加载"), JsonRequestBehavior.AllowGet);
            return Json(new MessageBox(false, "加载失败"), JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region 删除档图
        [AdminAuthorize("checkphoto", "deal")]
        public JsonResult delkitphoto(int id)
        {
            d_Kit kit = d_KitBLL.GetItem(id);
            string filepath = Server.MapPath("/images/kitimg/" + kit.Code + "/");
            try
            {
                Directory.Delete(filepath, true);
                return Json(new MessageBox(true, "删除成功"), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json(new MessageBox(false, "删除失败"), JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region 枚举
        public ContentResult questiontype()
        {
            List<string[]> arrStr = GetEnumBLL.GetEnumOpt(typeof(KitQuestionType));
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
    }
}
