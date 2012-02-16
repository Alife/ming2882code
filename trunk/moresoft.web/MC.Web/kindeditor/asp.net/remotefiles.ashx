<%@ webhandler Language="C#" class="remotefiles" %>
using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Net;
using LitJson;
public class remotefiles : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    private string savePath = "/content/images/upload/image/";
    /// <summary>
    /// 通过mime类型获取图片后缀
    /// </summary>
    /// <param name="MIME">mime类型</param>
    /// <returns>图片后缀</returns>
    private string GetExt(string MIME)
    {
        switch (MIME.ToLower().Trim())
        {
            case "image/gif": return "gif";
            case "image/jpeg": return "jpg";
            case "image/png": return "png";
            case "application/x-ms-bmp":
            case "image/nbmp": return "bmp";
            default: return null;
        }
    }
    /// <summary>
    /// 将下载的文件流写入硬盘
    /// </summary>
    /// <param name="FullPath">本地文件的路径</param>
    /// <param name="ns">文件流</param>
    private void WriteToHDD(string FullPath, Stream ns)
    {
        FileStream writer = new FileStream(FullPath, FileMode.OpenOrCreate, FileAccess.Write);
        int bufferSize = 512, readSize = 0;
        byte[] buffer = new byte[bufferSize];
        readSize = ns.Read(buffer, 0, bufferSize);
        while (readSize > 0)
        {
            writer.Write(buffer, 0, readSize);
            readSize = ns.Read(buffer, 0, bufferSize);
        }
        writer.Flush();
        writer.Close();
    }
    /// <summary>
    /// 下载网络图片
    /// </summary>
    /// <param name="url">图片网络地址</param>
    /// <param name="folder">保存到本地图片的时间文件夹</param>
    /// <param name="i">第几个文件</param>
    /// <param name="context">上下文对象</param>
    /// <returns>如果是图片则下载并返回生成的文件名，否则返回null</returns>
    private string DownLoadFile(string url, string folder, int i, HttpContext context)
    {
        HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
        HttpWebResponse response = (HttpWebResponse)req.GetResponse();
        string ext = GetExt(response.ContentType)/*根据响应头获取后缀*/, fileName = null;
        try
        {
            if (ext != null)//是图片文件
            {
                fileName = DateTime.Now.ToString("yyyyMMddHHmmss") + i.ToString() + "." + ext;
                Stream ns = response.GetResponseStream();
                WriteToHDD(context.Server.MapPath(savePath + folder + "/" + fileName), ns);//注意修改保存图片的路径
                ns.Close();
            }
            response.Close();
        }
        catch
        {
            response.Close();
        }
        return fileName;
    }
    public void ProcessRequest(HttpContext context)
    {
        string js = "";
        string files = HttpUtility.UrlDecode(context.Request.Form["files"]);
        if (string.IsNullOrEmpty(files))
        {
            remotemsg hash = new remotemsg();
            hash.success = false;
            hash.msg = "没有远程图片需要上传！";
            js = JsonMapper.ToJson(hash);
        }
        else
        {
            remotemsg hash = new remotemsg();
            string[] arrFiles = files.Split('|');
            string func = context.Request.QueryString["func"], folder = DateTime.Now.ToString("yyyyMMdd"), fileName = "";
            if (!Directory.Exists(context.Server.MapPath(savePath + folder)))
                Directory.CreateDirectory(context.Server.MapPath(savePath + folder));
            for (int i = 0; i < arrFiles.Length; i++)
            {
                fileName = DownLoadFile(arrFiles[i], folder, i, context);
                if (fileName == null) hash.items.Add(fileName);
                else hash.items.Add(savePath + folder + "/" + fileName);
            }
            hash.success = true;
            js = JsonMapper.ToJson(hash);
        }
        context.Response.Charset = "utf-8";
        context.Response.Write(js);
        context.Response.End();
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}
public class remotemsg
{
    public bool success { get; set; }
    public string msg { get; set; }
    public List<string> items { get; set; }
    public remotemsg() { items = new List<string>(); }
}