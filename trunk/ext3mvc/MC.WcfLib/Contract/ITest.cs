using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Runtime.Serialization;

namespace MC.WcfLib.Contract
{
    [DataContractFormat]
    [ServiceContract]
    public interface ITest
    {
        [WebGet(UriTemplate = "/User/Get/{Name}/{Position}", BodyStyle = WebMessageBodyStyle.Bare)]
        [OperationContract]
        List<User> GetUser(string Name, string Position);

        //[WebInvoke(Method = "POST", UriTemplate = "/User/Create", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json, RequestFormat = WebMessageFormat.Json)]
        [WebInvoke(Method = "POST", UriTemplate = "/User/Create", BodyStyle = WebMessageBodyStyle.Bare, RequestFormat = WebMessageFormat.Json)]
        [OperationContract]
        Result CreateUser(User u);

        /// <summary>
        /// 展示一张图片
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [WebGet(UriTemplate = "ShowImage/{fileName}")]
        Stream ShowFile(string fileName);
        /// <summary>
        /// 下载一个文件
        /// </summary>
        /// <param name="excel"></param>
        [WebGet(UriTemplate = "Download/{fileName}")]
        void GetFile(string fileName);
        /// <summary>
        /// 上传一个文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        //[WebInvoke(UriTemplate = "AddFile/{fileName}", Method = "POST")]
        //bool AddFile(string fileName);
    }
    public class Test : ITest
    {
        public List<User> GetUser(string Name, string Position)
        {
            List<User> userList = List.Where(u => u.Name == Name && u.Position == Position).ToList();
            return userList;
        }
        public Result CreateUser(User u)
        {
            Result result = new Result();
            if (!List.Any(user => user.ID == u.ID))
                List.Add(u);
            result.Value = u.ID.ToString();
            return result;
        }
        private static List<User> List
        {
            get
            {
                List<User> list = new List<User>{
                    new User{
                        ID = 1,
                         Name = "haode",
                         Sex = 1,
                         Position = "china",
                         Email = "qq@qq.com"
                    }
                };
                return list;
            }
        }
        public Stream ShowFile(string fileName)
        {
            string folder = HostingEnvironment.MapPath("~/images/files");
            WebOperationContext.Current.OutgoingResponse.ContentType = GetContentType(fileName);
            var file = Path.Combine(folder, fileName);
            return File.OpenRead(file);
        }
        public void GetFile(string fileName)
        {
            string folder = System.Web.Hosting.HostingEnvironment.MapPath("~/images/files");
            var FullFileName = Path.Combine(folder, fileName);
            if (File.Exists(FullFileName))
            {
                FileInfo DownloadFile = new FileInfo(FullFileName);
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = false;
                HttpContext.Current.Response.ContentType = "application/octet-stream";
                HttpContext.Current.Response.AppendHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(DownloadFile.FullName, System.Text.Encoding.ASCII));
                HttpContext.Current.Response.AppendHeader("Content-Length", DownloadFile.Length.ToString());
                HttpContext.Current.Response.WriteFile(DownloadFile.FullName);

                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }

        protected static string GetContentType(string strFileName)
        {
            string strExtendName = strFileName.Substring(strFileName.LastIndexOf('.'));
            if (string.IsNullOrEmpty(strExtendName))
            {
                return "";
            }
            string strRet = "";
            switch (strExtendName)
            {
                case ".*": strRet = "application/octet-stream"; break;
                case ".001": strRet = "application/x-001"; break;
                case ".301": strRet = "application/x-301"; break;
                case ".323": strRet = "text/h323"; break;
                case ".906": strRet = "application/x-906"; break;
                case ".907": strRet = "drawing/907"; break;
                case ".a11": strRet = "application/x-a11"; break;
                case ".acp": strRet = "audio/x-mei-aac"; break;
                case ".ai": strRet = "application/postscript"; break;
                case ".aif": strRet = "audio/aiff"; break;
                case ".aifc": strRet = "audio/aiff"; break;
                case ".aiff": strRet = "audio/aiff"; break;
                case ".anv": strRet = "application/x-anv"; break;
                case ".asa": strRet = "text/asa"; break;
                case ".asf": strRet = "video/x-ms-asf"; break;
                case ".asp": strRet = "text/asp"; break;
                case ".asx": strRet = "video/x-ms-asf"; break;
                case ".au": strRet = "audio/basic"; break;
                case ".avi": strRet = "video/avi"; break;
                case ".awf": strRet = "application/vnd.adobe.workflow"; break;
                case ".biz": strRet = "text/xml"; break;
                case ".bmp": strRet = "application/x-bmp"; break;
                case ".bot": strRet = "application/x-bot"; break;
                case ".c4t": strRet = "application/x-c4t"; break;
                case ".c90": strRet = "application/x-c90"; break;
                case ".cal": strRet = "application/x-cals"; break;
                case ".cat": strRet = "application/vnd.ms-pki.seccat"; break;
                case ".cdf": strRet = "application/x-netcdf"; break;
                case ".cdr": strRet = "application/x-cdr"; break;
                case ".cel": strRet = "application/x-cel"; break;
                case ".cer": strRet = "application/x-x509-ca-cert"; break;
                case ".cg4": strRet = "application/x-g4"; break;
                case ".cgm": strRet = "application/x-cgm"; break;
                case ".cit": strRet = "application/x-cit"; break;
                case ".class": strRet = "java/*"; break;
                case ".cml": strRet = "text/xml"; break;
                case ".cmp": strRet = "application/x-cmp"; break;
                case ".cmx": strRet = "application/x-cmx"; break;
                case ".cot": strRet = "application/x-cot"; break;
                case ".crl": strRet = "application/pkix-crl"; break;
                case ".crt": strRet = "application/x-x509-ca-cert"; break;
                case ".csi": strRet = "application/x-csi"; break;
                case ".css": strRet = "text/css"; break;
                case ".cut": strRet = "application/x-cut"; break;
                case ".dbf": strRet = "application/x-dbf"; break;
                case ".dbm": strRet = "application/x-dbm"; break;
                case ".dbx": strRet = "application/x-dbx"; break;
                case ".dcd": strRet = "text/xml"; break;
                case ".dcx": strRet = "application/x-dcx"; break;
                case ".der": strRet = "application/x-x509-ca-cert"; break;
                case ".dgn": strRet = "application/x-dgn"; break;
                case ".dib": strRet = "application/x-dib"; break;
                case ".dll": strRet = "application/x-msdownload"; break;
                case ".doc": strRet = "application/msword"; break;
                case ".dot": strRet = "application/msword"; break;
                case ".drw": strRet = "application/x-drw"; break;
                case ".dtd": strRet = "text/xml"; break;
                //case ".dwf": strRet = "Model/vnd.dwf";break; 
                case ".dwf": strRet = "application/x-dwf"; break;
                case ".dwg": strRet = "application/x-dwg"; break;
                case ".dxb": strRet = "application/x-dxb"; break;
                case ".dxf": strRet = "application/x-dxf"; break;
                case ".edn": strRet = "application/vnd.adobe.edn"; break;
                case ".emf": strRet = "application/x-emf"; break;
                case ".eml": strRet = "message/rfc822"; break;
                case ".ent": strRet = "text/xml"; break;
                case ".epi": strRet = "application/x-epi"; break;
                case ".eps": strRet = "application/x-ps"; break;
                //case ".eps": strRet = "application/postscript";break; 
                case ".etd": strRet = "application/x-ebx"; break;
                case ".exe": strRet = "application/x-msdownload"; break;
                case ".fax": strRet = "image/fax"; break;
                case ".fdf": strRet = "application/vnd.fdf"; break;
                case ".fif": strRet = "application/fractals"; break;
                case ".fo": strRet = "text/xml"; break;
                case ".frm": strRet = "application/x-frm"; break;
                case ".g4": strRet = "application/x-g4"; break;
                case ".gbr": strRet = "application/x-gbr"; break;
                case ".gcd": strRet = "application/x-gcd"; break;
                case ".gif": strRet = "image/gif"; break;
                case ".gl2": strRet = "application/x-gl2"; break;
                case ".gp4": strRet = "application/x-gp4"; break;
                case ".hgl": strRet = "application/x-hgl"; break;
                case ".hmr": strRet = "application/x-hmr"; break;
                case ".hpg": strRet = "application/x-hpgl"; break;
                case ".hpl": strRet = "application/x-hpl"; break;
                case ".hqx": strRet = "application/mac-binhex40"; break;
                case ".hrf": strRet = "application/x-hrf"; break;
                case ".hta": strRet = "application/hta"; break;
                case ".htc": strRet = "text/x-component"; break;
                case ".htm": strRet = "text/html"; break;
                case ".html": strRet = "text/html"; break;
                case ".htt": strRet = "text/webviewhtml"; break;
                case ".htx": strRet = "text/html"; break;
                case ".icb": strRet = "application/x-icb"; break;
                case ".ico": strRet = "image/x-icon"; break;
                //case ".ico": strRet = "application/x-ico";break; 
                case ".iff": strRet = "application/x-iff"; break;
                case ".ig4": strRet = "application/x-g4"; break;
                case ".igs": strRet = "application/x-igs"; break;
                case ".iii": strRet = "application/x-iphone"; break;
                case ".img": strRet = "application/x-img"; break;
                case ".ins": strRet = "application/x-internet-signup"; break;
                case ".isp": strRet = "application/x-internet-signup"; break;
                case ".IVF": strRet = "video/x-ivf"; break;
                case ".java": strRet = "java/*"; break;
                case ".jfif": strRet = "image/jpeg"; break;
                case ".jpe": strRet = "image/jpeg"; break;
                //case ".jpe": strRet = "application/x-jpe";break; 
                case ".jpeg": strRet = "image/jpeg"; break;
                case ".jpg": strRet = "image/jpeg"; break;
                //case ".jpg": strRet = "application/x-jpg";break; 
                case ".js": strRet = "application/x-javascript"; break;
                case ".jsp": strRet = "text/html"; break;
                case ".la1": strRet = "audio/x-liquid-file"; break;
                case ".lar": strRet = "application/x-laplayer-reg"; break;
                case ".latex": strRet = "application/x-latex"; break;
                case ".lavs": strRet = "audio/x-liquid-secure"; break;
                case ".lbm": strRet = "application/x-lbm"; break;
                case ".lmsff": strRet = "audio/x-la-lms"; break;
                case ".ls": strRet = "application/x-javascript"; break;
                case ".ltr": strRet = "application/x-ltr"; break;
                case ".m1v": strRet = "video/x-mpeg"; break;
                case ".m2v": strRet = "video/x-mpeg"; break;
                case ".m3u": strRet = "audio/mpegurl"; break;
                case ".m4e": strRet = "video/mpeg4"; break;
                case ".mac": strRet = "application/x-mac"; break;
                case ".man": strRet = "application/x-troff-man"; break;
                case ".math": strRet = "text/xml"; break;
                case ".mdb": strRet = "application/msaccess"; break;
                //case ".mdb": strRet = "application/x-mdb";break; 
                case ".mfp": strRet = "application/x-shockwave-flash"; break;
                case ".mht": strRet = "message/rfc822"; break;
                case ".mhtml": strRet = "message/rfc822"; break;
                case ".mi": strRet = "application/x-mi"; break;
                case ".mid": strRet = "audio/mid"; break;
                case ".midi": strRet = "audio/mid"; break;
                case ".mil": strRet = "application/x-mil"; break;
                case ".mml": strRet = "text/xml"; break;
                case ".mnd": strRet = "audio/x-musicnet-download"; break;
                case ".mns": strRet = "audio/x-musicnet-stream"; break;
                case ".mocha": strRet = "application/x-javascript"; break;
                case ".movie": strRet = "video/x-sgi-movie"; break;
                case ".mp1": strRet = "audio/mp1"; break;
                case ".mp2": strRet = "audio/mp2"; break;
                case ".mp2v": strRet = "video/mpeg"; break;
                case ".mp3": strRet = "audio/mp3"; break;
                case ".mp4": strRet = "video/mpeg4"; break;
                case ".mpa": strRet = "video/x-mpg"; break;
                case ".mpd": strRet = "application/vnd.ms-project"; break;
                case ".mpe": strRet = "video/x-mpeg"; break;
                case ".mpeg": strRet = "video/mpg"; break;
                case ".mpg": strRet = "video/mpg"; break;
                case ".mpga": strRet = "audio/rn-mpeg"; break;
                case ".mpp": strRet = "application/vnd.ms-project"; break;
                case ".mps": strRet = "video/x-mpeg"; break;
                case ".mpt": strRet = "application/vnd.ms-project"; break;
                case ".mpv": strRet = "video/mpg"; break;
                case ".mpv2": strRet = "video/mpeg"; break;
                case ".mpw": strRet = "application/vnd.ms-project"; break;
                case ".mpx": strRet = "application/vnd.ms-project"; break;
                case ".mtx": strRet = "text/xml"; break;
                case ".mxp": strRet = "application/x-mmxp"; break;
                case ".net": strRet = "image/pnetvue"; break;
                case ".nrf": strRet = "application/x-nrf"; break;
                case ".nws": strRet = "message/rfc822"; break;
                case ".odc": strRet = "text/x-ms-odc"; break;
                case ".out": strRet = "application/x-out"; break;
                case ".p10": strRet = "application/pkcs10"; break;
                case ".p12": strRet = "application/x-pkcs12"; break;
                case ".p7b": strRet = "application/x-pkcs7-certificates"; break;
                case ".p7c": strRet = "application/pkcs7-mime"; break;
                case ".p7m": strRet = "application/pkcs7-mime"; break;
                case ".p7r": strRet = "application/x-pkcs7-certreqresp"; break;
                case ".p7s": strRet = "application/pkcs7-signature"; break;
                case ".pc5": strRet = "application/x-pc5"; break;
                case ".pci": strRet = "application/x-pci"; break;
                case ".pcl": strRet = "application/x-pcl"; break;
                case ".pcx": strRet = "application/x-pcx"; break;
                case ".pdf": strRet = "application/pdf"; break;
                case ".pdx": strRet = "application/vnd.adobe.pdx"; break;
                case ".pfx": strRet = "application/x-pkcs12"; break;
                case ".pgl": strRet = "application/x-pgl"; break;
                case ".pic": strRet = "application/x-pic"; break;
                case ".pko": strRet = "application/vnd.ms-pki.pko"; break;
                case ".pl": strRet = "application/x-perl"; break;
                case ".plg": strRet = "text/html"; break;
                case ".pls": strRet = "audio/scpls"; break;
                case ".plt": strRet = "application/x-plt"; break;
                case ".png": strRet = "image/png"; break;
                case ".pot": strRet = "application/vnd.ms-powerpoint"; break;
                case ".ppa": strRet = "application/vnd.ms-powerpoint"; break;
                case ".ppm": strRet = "application/x-ppm"; break;
                case ".pps": strRet = "application/vnd.ms-powerpoint"; break;
                case ".ppt": strRet = "application/vnd.ms-powerpoint"; break;
                case ".pr": strRet = "application/x-pr"; break;
                case ".prf": strRet = "application/pics-rules"; break;
                case ".prn": strRet = "application/x-prn"; break;
                case ".prt": strRet = "application/x-prt"; break;
                case ".ps": strRet = "application/x-ps"; break;
                case ".ptn": strRet = "application/x-ptn"; break;
                case ".pwz": strRet = "application/vnd.ms-powerpoint"; break;
                case ".r3t": strRet = "text/vnd.rn-realtext3d"; break;
                case ".ra": strRet = "audio/vnd.rn-realaudio"; break;
                case ".ram": strRet = "audio/x-pn-realaudio"; break;
                case ".ras": strRet = "application/x-ras"; break;
                case ".rat": strRet = "application/rat-file"; break;
                case ".rdf": strRet = "text/xml"; break;
                case ".rec": strRet = "application/vnd.rn-recording"; break;
                case ".red": strRet = "application/x-red"; break;
                case ".rgb": strRet = "application/x-rgb"; break;
                case ".rjs": strRet = "application/vnd.rn-realsystem-rjs"; break;
                case ".rjt": strRet = "application/vnd.rn-realsystem-rjt"; break;
                case ".rlc": strRet = "application/x-rlc"; break;
                case ".rle": strRet = "application/x-rle"; break;
                case ".rm": strRet = "application/vnd.rn-realmedia"; break;
                case ".rmf": strRet = "application/vnd.adobe.rmf"; break;
                case ".rmi": strRet = "audio/mid"; break;
                case ".rmj": strRet = "application/vnd.rn-realsystem-rmj"; break;
                case ".rmm": strRet = "audio/x-pn-realaudio"; break;
                case ".rmp": strRet = "application/vnd.rn-rn_music_package"; break;
                case ".rms": strRet = "application/vnd.rn-realmedia-secure"; break;
                case ".rmvb": strRet = "application/vnd.rn-realmedia-vbr"; break;
                case ".rmx": strRet = "application/vnd.rn-realsystem-rmx"; break;
                case ".rnx": strRet = "application/vnd.rn-realplayer"; break;
                case ".rp": strRet = "image/vnd.rn-realpix"; break;
                case ".rpm": strRet = "audio/x-pn-realaudio-plugin"; break;
                case ".rsml": strRet = "application/vnd.rn-rsml"; break;
                case ".rt": strRet = "text/vnd.rn-realtext"; break;
                case ".rtf": strRet = "application/x-rtf"; break;
                case ".rv": strRet = "video/vnd.rn-realvideo"; break;
                case ".sam": strRet = "application/x-sam"; break;
                case ".sat": strRet = "application/x-sat"; break;
                case ".sdp": strRet = "application/sdp"; break;
                case ".sdw": strRet = "application/x-sdw"; break;
                case ".sit": strRet = "application/x-stuffit"; break;
                case ".slb": strRet = "application/x-slb"; break;
                case ".sld": strRet = "application/x-sld"; break;
                case ".slk": strRet = "drawing/x-slk"; break;
                case ".smi": strRet = "application/smil"; break;
                case ".smil": strRet = "application/smil"; break;
                case ".smk": strRet = "application/x-smk"; break;
                case ".snd": strRet = "audio/basic"; break;
                case ".sol": strRet = "text/plain"; break;
                case ".sor": strRet = "text/plain"; break;
                case ".spc": strRet = "application/x-pkcs7-certificates"; break;
                case ".spl": strRet = "application/futuresplash"; break;
                case ".spp": strRet = "text/xml"; break;
                case ".ssm": strRet = "application/streamingmedia"; break;
                case ".sst": strRet = "application/vnd.ms-pki.certstore"; break;
                case ".stl": strRet = "application/vnd.ms-pki.stl"; break;
                case ".stm": strRet = "text/html"; break;
                case ".sty": strRet = "application/x-sty"; break;
                case ".svg": strRet = "text/xml"; break;
                case ".swf": strRet = "application/x-shockwave-flash"; break;
                case ".tdf": strRet = "application/x-tdf"; break;
                case ".tg4": strRet = "application/x-tg4"; break;
                case ".tga": strRet = "application/x-tga"; break;
                case ".tif": strRet = "image/tiff"; break;
                case ".tiff": strRet = "image/tiff"; break;
                case ".tld": strRet = "text/xml"; break;
                case ".top": strRet = "drawing/x-top"; break;
                case ".torrent": strRet = "application/x-bittorrent"; break;
                case ".tsd": strRet = "text/xml"; break;
                case ".txt": strRet = "text/plain"; break;
                case ".uin": strRet = "application/x-icq"; break;
                case ".uls": strRet = "text/iuls"; break;
                case ".vcf": strRet = "text/x-vcard"; break;
                case ".vda": strRet = "application/x-vda"; break;
                case ".vdx": strRet = "application/vnd.visio"; break;
                case ".vml": strRet = "text/xml"; break;
                case ".vpg": strRet = "application/x-vpeg005"; break;
                case ".vsd": strRet = "application/vnd.visio"; break;
                case ".vss": strRet = "application/vnd.visio"; break;
                case ".vst": strRet = "application/vnd.visio"; break;
                case ".vsw": strRet = "application/vnd.visio"; break;
                case ".vsx": strRet = "application/vnd.visio"; break;
                case ".vtx": strRet = "application/vnd.visio"; break;
                case ".vxml": strRet = "text/xml"; break;
                case ".wav": strRet = "audio/wav"; break;
                case ".wax": strRet = "audio/x-ms-wax"; break;
                case ".wb1": strRet = "application/x-wb1"; break;
                case ".wb2": strRet = "application/x-wb2"; break;
                case ".wb3": strRet = "application/x-wb3"; break;
                case ".wbmp": strRet = "image/vnd.wap.wbmp"; break;
                case ".wiz": strRet = "application/msword"; break;
                case ".wk3": strRet = "application/x-wk3"; break;
                case ".wk4": strRet = "application/x-wk4"; break;
                case ".wkq": strRet = "application/x-wkq"; break;
                case ".wks": strRet = "application/x-wks"; break;
                case ".wm": strRet = "video/x-ms-wm"; break;
                case ".wma": strRet = "audio/x-ms-wma"; break;
                case ".wmd": strRet = "application/x-ms-wmd"; break;
                case ".wmf": strRet = "application/x-wmf"; break;
                case ".wml": strRet = "text/vnd.wap.wml"; break;
                case ".wmv": strRet = "video/x-ms-wmv"; break;
                case ".wmx": strRet = "video/x-ms-wmx"; break;
                case ".wmz": strRet = "application/x-ms-wmz"; break;
                case ".wp6": strRet = "application/x-wp6"; break;
                case ".wpd": strRet = "application/x-wpd"; break;
                case ".wpg": strRet = "application/x-wpg"; break;
                case ".wpl": strRet = "application/vnd.ms-wpl"; break;
                case ".wq1": strRet = "application/x-wq1"; break;
                case ".wr1": strRet = "application/x-wr1"; break;
                case ".wri": strRet = "application/x-wri"; break;
                case ".wrk": strRet = "application/x-wrk"; break;
                case ".ws": strRet = "application/x-ws"; break;
                case ".ws2": strRet = "application/x-ws"; break;
                case ".wsc": strRet = "text/scriptlet"; break;
                case ".wsdl": strRet = "text/xml"; break;
                case ".wvx": strRet = "video/x-ms-wvx"; break;
                case ".xdp": strRet = "application/vnd.adobe.xdp"; break;
                case ".xdr": strRet = "text/xml"; break;
                case ".xfd": strRet = "application/vnd.adobe.xfd"; break;
                case ".xfdf": strRet = "application/vnd.adobe.xfdf"; break;
                case ".xhtml": strRet = "text/html"; break;
                case ".xls": strRet = "application/vnd.ms-excel"; break;
                case ".xlw": strRet = "application/x-xlw"; break;
                case ".xml": strRet = "text/xml"; break;
                case ".xpl": strRet = "audio/scpls"; break;
                case ".xq": strRet = "text/xml"; break;
                case ".xql": strRet = "text/xml"; break;
                case ".xquery": strRet = "text/xml"; break;
                case ".xsd": strRet = "text/xml"; break;
                case ".xsl": strRet = "text/xml"; break;
                case ".xslt": strRet = "text/xml"; break;
                case ".xwd": strRet = "application/x-xwd"; break;
                case ".x_b": strRet = "application/x-x_b"; break;
                case ".x_t": strRet = "application/x-x_t"; break;
            }
            return strRet;
        }        
    }
    [DataContract(Namespace = "http://rest-server/datacontract/user")]
    public class User
    {
        [DataMember]
        public long ID { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Sex { get; set; }
        [DataMember]
        public string Position { get; set; }
        [DataMember]
        public string Email { get; set; }
    }
    [DataContract(Namespace = "http://rest-server/datacontract/result")]
    public class Result
    {
        [DataMember]
        public string Value { get; set; }
    }
}
