using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using MyBatis.DataMapper.Session;
using MyBatis.DataMapper.Session.Transaction;
using MyBatis.Common;
using MyBatis.Common.Logging;
using MC.Model;

namespace MC.DAO
{
    public class DaoImpl : IBatiseHelper, IDao
    {
        public DaoImpl() { }

        #region ������־
        /// <summary>
        /// ������־
        /// </summary>
        /// <param name="xmlID"></param>
        /// <param name="_ErrorLog"></param>
        public void ErrorLog(string xmlID, string errorLog, IDictionary iDictionary)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("XML ID=" + xmlID.PadRight(8));
            sb.Append(" message=" + errorLog);
            try
            {
                string user = System.Web.HttpContext.Current.User.Identity.Name;
                if (string.IsNullOrEmpty(user)) user = "�ο�";
                sb.Append("\r\n" + user + "----------------");
            }
            catch
            {
                sb.Append("\r\n�ο�----------------");
            }
            _logger.Error(sb.ToString());
        }
        public void ErrorLog(string xmlID, string errorLog)
        {
            ErrorLog(xmlID, errorLog, new QueryInfo().Parameters);
        }
        #endregion

        #region ��ѯ����ָ���ֶ�
        public object QueryForObject(QueryInfo queryInfo)
        {
            string xmlID = sPreFix + queryInfo.MappingName + (!string.IsNullOrEmpty(queryInfo.XmlID) ? "." + queryInfo.XmlID : ".Load");
            object obj = null;
            try
            {
                obj = dataMapper.QueryForObject(xmlID, queryInfo.Parameters);
            }
            catch (Exception e)
            {
                ErrorLog(xmlID, e.Message, queryInfo.Parameters);
            }
            return obj;
        }
        #endregion

        #region TotalCount����ѯ��¼���������ļ�Ҫд������һ������

        public int TotalCount(string sTableName, IDictionary iDictionary, string xmlID)
        {
            xmlID = sPreFix + sTableName + (!string.IsNullOrEmpty(xmlID) ? "." + xmlID : ".Count");
            int i = 0;
            try
            {
                i = Convert.ToInt32(dataMapper.QueryForObject(xmlID, iDictionary));
            }
            catch (Exception e)
            {
                ErrorLog(xmlID, e.Message, iDictionary);
            }
            return i;
        }

        /// <summary>
        /// ��ѯ��¼���������ļ�Ҫд������һ������
        /// </summary>
        /// <param name="sTableName"></param>
        /// <param name="eParameters"></param>
        /// <returns></returns>
        public int TotalCount<T>(QueryInfo queryInfo) where T : Entity, new()
        {
            if (queryInfo == null) queryInfo = new QueryInfo();
            if (queryInfo.MappingName == null || queryInfo.MappingName.Length == 0)
            {
                T obj = new T();
                queryInfo.MappingName = obj.GetTableName();
            }
            int iCount = TotalCount(queryInfo.MappingName, queryInfo.Parameters, queryInfo.XmlID);
            return iCount;
        }
        #endregion

        #region Find������һ����¼���ϣ������ļ�Ҫд������һ������  //�����Ǵ洢����

        public IList<T> GetList<T>(QueryInfo queryInfo) where T : Entity, new()
        {
            if (queryInfo == null) queryInfo = new QueryInfo();

            if (queryInfo.MappingName == null || queryInfo.MappingName.Length == 0)
            {
                //T obj = System.Activator.CreateInstance<T>();
                T obj = new T();
                queryInfo.MappingName = obj.GetTableName();
            }
            #region order by
            if (queryInfo.Orderby != null && queryInfo.Orderby.Count > 0)
            {
                string orderBy = string.Empty;
                foreach (object obj in queryInfo.Orderby.Keys)
                {
                    if (obj.ToString().Trim().Length == 0) continue;
                    orderBy += obj.ToString();
                    if (queryInfo.Orderby[obj] == null || queryInfo.Orderby[obj].Equals("asc"))
                        orderBy += " ,";
                    else
                        orderBy += string.Format(" {0} " + " ,", queryInfo.Orderby[obj]);
                }
                if (orderBy.Trim().Length > 0)
                {
                    orderBy = "order by " + orderBy.Substring(0, orderBy.Length - 1);
                    if (queryInfo.Parameters.Contains("OrderBy"))
                        queryInfo.Parameters["OrderBy"] = orderBy;
                    else
                        queryInfo.Parameters.Add("OrderBy", orderBy);
                }

            }
            #endregion
            IList<T> lstEntity = null;
            string xmlID = sPreFix + queryInfo.MappingName + (!string.IsNullOrEmpty(queryInfo.XmlID) ? "." + queryInfo.XmlID : ".Load");
            try
            {
                lstEntity = dataMapper.QueryForList<T>(xmlID, queryInfo.Parameters);
            }
            catch (Exception e)
            {
                ErrorLog(xmlID, e.Message + "\r\n" + e.StackTrace);
            }
            return lstEntity;
        }
        #endregion

        #region IList  ����һ�����ϣ������п����ǲ�ͬ��ʵ��
        /// <summary>
        /// ����һ�����ϣ������п����ǲ�ͬ��ʵ��
        /// </summary>
        /// <param name="queryInfo"></param>
        /// <returns></returns>
        public IList GetList(QueryInfo queryInfo)
        {
            string xmlID = sPreFix + queryInfo.MappingName + (!string.IsNullOrEmpty(queryInfo.XmlID) ? "." + queryInfo.XmlID : ".LoadList");
            IList list = null;
            try
            {
                if (queryInfo.MapQueryValue != null)
                    list = dataMapper.QueryForList(xmlID, queryInfo.MapQueryValue);
                else
                    list = dataMapper.QueryForList(xmlID, queryInfo.Parameters);
            }
            catch (Exception e)
            {
                ErrorLog(xmlID, e.Message + "\r\n" + e.StackTrace);
            }
            return list;
        }
        #endregion

        #region GetListPage������һ����¼���ϣ������ļ�Ҫд������һ����ҳ���� //�����Ǵ洢����

        public T GetListPage<T>(QueryInfo queryInfo) where T : EntityList, new()
        {
            if (queryInfo == null) queryInfo = new QueryInfo();

            if (queryInfo.MappingName == null || queryInfo.MappingName.Length == 0)
            {
                //T obj = System.Activator.CreateInstance<T>();
                T obj = new T();
                queryInfo.MappingName = obj.GetType().Name.Replace("List", "");
            }
            #region order by
            if (queryInfo.Orderby != null && queryInfo.Orderby.Count > 0)
            {
                string orderBy = string.Empty;
                foreach (object obj in queryInfo.Orderby.Keys)
                {
                    if (obj.ToString().Trim().Length == 0) continue;
                    orderBy += obj.ToString();
                    if (queryInfo.Orderby[obj] == null || queryInfo.Orderby[obj].Equals("asc"))
                        orderBy += " ,";
                    else
                        orderBy += string.Format(" {0} " + " ,", queryInfo.Orderby[obj]);
                }
                if (orderBy.Trim().Length > 0)
                {
                    orderBy = "order by " + orderBy.Substring(0, orderBy.Length - 1);
                    //orderBy = orderBy.Substring(0, orderBy.Length - 1);
                    if (queryInfo.Parameters.Contains("OrderBy"))
                        queryInfo.Parameters["OrderBy"] = orderBy;
                    else
                        queryInfo.Parameters.Add("OrderBy", orderBy);
                }
            }
            #endregion
            T lstEntity = new T();
            string xmlID = sPreFix + queryInfo.MappingName + (!string.IsNullOrEmpty(queryInfo.XmlID) ? "." + queryInfo.XmlID : ".LoadPageList");
            try
            {
                lstEntity.records = TotalCount(queryInfo.MappingName, queryInfo.Parameters, queryInfo.XmlPageCountID);
                if (lstEntity.records > 0)
                    lstEntity.data = dataMapper.QueryForList<Entity>(xmlID, queryInfo.Parameters);
                if (lstEntity.data == null) lstEntity.data = new List<Entity>();
            }
            catch (Exception e)
            {
                ErrorLog(xmlID, e.Message + "\r\n" + e.StackTrace);
            }
            return lstEntity;
        }
        #endregion

        #region GetListPage������һ����¼IDictionary���ϣ������ļ�Ҫд������һ����ҳ���� //�����Ǵ洢����

        public IDictionary GetListPages<T>(QueryInfo queryInfo) where T : Entity, new()
        {
            if (queryInfo == null) queryInfo = new QueryInfo();

            if (queryInfo.MappingName == null || queryInfo.MappingName.Length == 0)
            {
                T obj = new T();
                queryInfo.MappingName = obj.GetTableName();
            }
            #region order by
            if (queryInfo.Orderby != null && queryInfo.Orderby.Count > 0)
            {
                string orderBy = string.Empty;
                foreach (object obj in queryInfo.Orderby.Keys)
                {
                    if (obj.ToString().Trim().Length == 0) continue;
                    orderBy += obj.ToString();
                    if (queryInfo.Orderby[obj] == null || queryInfo.Orderby[obj].Equals("asc"))
                        orderBy += " ,";
                    else
                        orderBy += string.Format(" {0} " + " ,", queryInfo.Orderby[obj]);
                }
                if (orderBy.Trim().Length > 0)
                {
                    orderBy = "order by " + orderBy.Substring(0, orderBy.Length - 1);
                    //orderBy = orderBy.Substring(0, orderBy.Length - 1);
                    if (queryInfo.Parameters.Contains("OrderBy"))
                        queryInfo.Parameters["OrderBy"] = orderBy;
                    else
                        queryInfo.Parameters.Add("OrderBy", orderBy);
                }
            }
            #endregion
            IDictionary lstEntity = new Hashtable();
            string xmlID = sPreFix + queryInfo.MappingName + (!string.IsNullOrEmpty(queryInfo.XmlID) ? "." + queryInfo.XmlID : ".LoadPageList");
            try
            {
                int total = TotalCount(queryInfo.MappingName, queryInfo.Parameters, queryInfo.XmlPageCountID);
                lstEntity.Add("total", total);
                if (total > 0)
                {
                    var data = dataMapper.QueryForList<Entity>(xmlID, queryInfo.Parameters);
                    lstEntity.Add("data", data);
                    if (data == null) lstEntity.Add("data", new List<Entity>());
                }
            }
            catch (Exception e)
            {
                ErrorLog(xmlID, e.Message + "\r\n" + e.StackTrace);
            }
            return lstEntity;
        }
        #endregion

        #region GetItem ����һ����¼��ֵ�������ļ�Ҫд�����ص�����
        /// <summary>
        /// ����һ����¼��ֵ�������ļ�Ҫд�����ص�����
        /// </summary>
        /// <param name="sTableName"></param>
        /// <param name="htParameters"></param>
        /// <returns></returns>
        public T GetItem<T>(QueryInfo queryInfo) where T : Entity, new()
        {
            if (queryInfo.MappingName == null || queryInfo.MappingName.Length == 0)
            {
                T obj = new T();
                queryInfo.MappingName = obj.GetTableName();
            }
            string xmlID = sPreFix + queryInfo.MappingName + (!string.IsNullOrEmpty(queryInfo.XmlID) ? "." + queryInfo.XmlID : ".Load");
            T entity = null;
            try
            {
                entity = dataMapper.QueryForObject<T>(xmlID, queryInfo.Parameters);
            }
            catch (Exception e)
            {
                ErrorLog(xmlID, e.Message + "\r\n" + e.StackTrace);
            }
            return entity;
        }
        /// <summary>
        /// ͨ��һ��ʵ���ѯ
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objEntity"></param>
        /// <returns></returns>
        public T GetItem<T>(T objEntity) where T : Entity, new()
        {
            string xmlID = objEntity.GetSelectXmlID();
            T entity = null;
            try
            {
                entity = dataMapper.QueryForObject<T>(xmlID, objEntity);
            }
            catch (Exception e)
            {
                ErrorLog(xmlID, e.Message + "\r\n" + e.StackTrace);
            }
            return entity;
        }

        /// <summary>
        /// ����һ����¼��ֵ�������ļ�Ҫд�����ص�����
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sPK"></param>
        /// <returns></returns>
        public T GetItem<T>(object sPK) where T : Entity, new()
        {
            T obj = new T();
            QueryInfo queryInfo = new QueryInfo();
            string[] arrPK = obj.GetKeyCols();
            queryInfo.Parameters.Add(arrPK[0], sPK);
            queryInfo.MappingName = obj.GetTableName();
            string xmlID = sPreFix + queryInfo.MappingName + (!string.IsNullOrEmpty(queryInfo.XmlID) ? "." + queryInfo.XmlID : ".Load");
            T entity = null;
            try
            {
                entity = dataMapper.QueryForObject<T>(xmlID, queryInfo.Parameters);
            }
            catch (Exception e)
            {
                ErrorLog(xmlID, e.Message + "\r\n" + e.StackTrace);
            }
            if (entity != null)
                entity.SetLoading();
            return entity;
        }
        #endregion

        #region Insert������һ������
        /// <summary>
        /// ����һ������
        /// </summary>
        /// <param name="objEntity"></param>
        /// <returns></returns>
        public int Insert(Entity objEntity)
        {
            string xmlID = objEntity.GetInsertXmlID();
            int i = 0;
            try
            {
                i = Convert.ToInt32(dataMapper.Insert(xmlID, objEntity));
            }
            catch (Exception e)
            {
                ErrorLog(xmlID, e.Message + "\r\n" + e.StackTrace);
            }
            return i;
        }

        public int Insert(QueryInfo queryInfo)
        {
            string xmlID = sPreFix + queryInfo.MappingName + (!string.IsNullOrEmpty(queryInfo.XmlID) ? "." + queryInfo.XmlID : ".Insert");
            int i = 0;
            try
            {
                i = Convert.ToInt32(dataMapper.Insert(xmlID, queryInfo.Parameters));
            }
            catch (Exception e)
            {
                ErrorLog(xmlID, e.Message + "\r\n" + e.StackTrace);
            }
            return i;
        }
        #endregion

        #region Update������һ������
        /// <summary>
        /// ����һ������
        /// </summary>
        /// <param name="objEntity"></param>
        /// <returns></returns>
        public int Update(Entity objEntity)
        {
            int i = 0;
            string xmlID = objEntity.GetUpdateXmlID();
            try
            {
                i = dataMapper.Update(xmlID, objEntity);
            }
            catch (Exception e)
            {
                ErrorLog(xmlID, e.Message + "\r\n" + e.StackTrace);
            }
            return i;
        }
        public int Update(QueryInfo queryInfo)
        {
            string xmlID = sPreFix + queryInfo.MappingName + (!string.IsNullOrEmpty(queryInfo.XmlID) ? "." + queryInfo.XmlID : ".Update");
            int i = 0;
            try
            {
                i = Convert.ToInt32(dataMapper.Update(xmlID, queryInfo.Parameters));
            }
            catch (Exception e)
            {
                ErrorLog(xmlID, e.Message + "\r\n" + e.StackTrace);
            }
            return i;
        }
        #endregion

        #region Delete��ɾ��һ������
        /// <summary>
        /// ����һ������
        /// </summary>
        /// <param name="objEntity"></param>
        /// <returns></returns>
        public int Delete(Entity objEntity)
        {
            int i = 0;
            string xmlID = objEntity.GetDeleteXmlID();
            try
            {
                i = dataMapper.Delete(xmlID, objEntity);
            }
            catch (Exception e)
            {
                ErrorLog(xmlID, e.Message + "\r\n" + e.StackTrace);
            }
            return i;
        }

        public int Delete(QueryInfo queryInfo)
        {
            string xmlID = sPreFix + queryInfo.MappingName + (!string.IsNullOrEmpty(queryInfo.XmlID) ? "." + queryInfo.XmlID : ".Delete");
            int i = 0;
            try
            {
                i = Convert.ToInt32(dataMapper.Delete(xmlID, queryInfo.Parameters));
            }
            catch (Exception e)
            {
                ErrorLog(xmlID, e.Message + "\r\n" + e.StackTrace);
            }
            return i;
        }
        #endregion

        #region Save & SaveAll������һ�������һ�����󼯺�
        /// <summary>
        /// ����һ�������һ�����󼯺�
        /// </summary>
        /// <param name="objEntity"></param>
        /// <returns></returns>
        public int Save(Entity objEntity)
        {
            int i = 0;
            switch (objEntity.GetState())
            {
                case EntityState.Added:
                    i = Insert(objEntity);
                    break;
                case EntityState.Modified:
                    #region ������־ �и��õĽ������
                    //Type objTye = objEntity.GetType();
                    //object id = objEntity.GetKeyCols()[0];
                    //PropertyInfo property = objTye.GetProperty(id.ToString());
                    //object o = property.GetValue(objEntity, null);
                    //QueryInfo queryInfo = new QueryInfo();
                    //queryInfo.MappingName = objEntity.GetTableName();
                    //queryInfo.Parameters.Add(id, o);
                    //objEntity.SetOldEntity(Load<Entity>(queryInfo));
                    #endregion
                    i = Update(objEntity);
                    break;
                case EntityState.Deleted:
                    i = Delete(objEntity);
                    break;
                default:
                    i = 0;
                    break;
            }
            return i;
        }
        /// <summary>
        /// ����һ�������һ�����󼯺�
        /// </summary>
        /// <param name="lstEntity"></param>
        /// <returns></returns>
        public IList<T> Save<T>(IList<T> lstEntity) where T : Entity, new()
        {
            IList<T> v = new List<T>();
            for (int i = 0; i < lstEntity.Count; i++)
            {
                Entity objEntity = lstEntity[i];
                if (Save(objEntity) > 0)
                    v.Add(lstEntity[i]);
            }
            return v;
        }
        #endregion

        #region ����DataTable�Ĳ�ѯ//�����Ǵ洢���̰�������ֵ�ġ���ҳ��
        /// <summary>
        /// ����DataTable�Ĳ�ѯ//�����Ǵ洢����
        /// </summary>
        /// <param name="statementName">��ѯ��Ӧ���������ID</param>
        /// <param name="paramObject">��ѯ��������</param>
        /// <returns></returns>
        public DataTable GetDataTable(QueryInfo queryInfo)
        {
            if (queryInfo == null) queryInfo = new QueryInfo();
            DataTable dt = new DataTable();
            #region order by
            if (queryInfo.Orderby != null && queryInfo.Orderby.Count > 0)
            {
                string orderBy = string.Empty;
                foreach (object obj in queryInfo.Orderby.Keys)
                {
                    if (obj.ToString().Trim().Length == 0) continue;
                    orderBy += obj.ToString();
                    if (queryInfo.Orderby[obj] == null || queryInfo.Orderby[obj].Equals("asc"))
                        orderBy += " ,";
                    else
                        orderBy += string.Format(" {0} " + " ,", queryInfo.Orderby[obj]);
                }
                if (orderBy.Trim().Length > 0)
                {
                    orderBy = "order by " + orderBy.Substring(0, orderBy.Length - 1);
                    if (queryInfo.Parameters.Contains("OrderBy"))
                        queryInfo.Parameters["OrderBy"] = orderBy;
                    else
                        queryInfo.Parameters.Add("OrderBy", orderBy);
                }
            }
            #endregion
            string xmlID = sPreFix + queryInfo.MappingName + (!string.IsNullOrEmpty(queryInfo.XmlID) ? "." + queryInfo.XmlID : ".Load");
            try
            {
                dt = dataMapper.QueryForDataTable(xmlID, queryInfo.Parameters);
            }
            catch (Exception e)
            {
                ErrorLog(xmlID, e.Message + "\r\n" + e.StackTrace);
            }
            return dt;
        }
        #endregion

        #region ���ط�ҳDataTable�Ĳ�ѯ����֧�ִ洢����
        public DataTable GetDataTable(QueryInfo queryInfo, ref int records)
        {
            if (queryInfo == null) queryInfo = new QueryInfo();
            records = 0;
            DataTable dt = new DataTable();
            #region order by
            if (queryInfo.Orderby != null && queryInfo.Orderby.Count > 0)
            {
                string orderBy = string.Empty;
                foreach (object obj in queryInfo.Orderby.Keys)
                {
                    if (obj.ToString().Trim().Length == 0) continue;
                    orderBy += obj.ToString();
                    if (queryInfo.Orderby[obj] == null || queryInfo.Orderby[obj].Equals("asc"))
                        orderBy += " ,";
                    else
                        orderBy += string.Format(" {0} " + " ,", queryInfo.Orderby[obj]);
                }
                if (orderBy.Trim().Length > 0)
                {
                    orderBy = "order by " + orderBy.Substring(0, orderBy.Length - 1);
                    if (queryInfo.Parameters.Contains("OrderBy"))
                        queryInfo.Parameters["OrderBy"] = orderBy;
                    else
                        queryInfo.Parameters.Add("OrderBy", orderBy);
                }
            }
            #endregion
            string xmlID = sPreFix + queryInfo.MappingName + (!string.IsNullOrEmpty(queryInfo.XmlID) ? "." + queryInfo.XmlID : ".LoadPageListByTable");
            try
            {
                records = TotalCount(queryInfo.MappingName, queryInfo.Parameters, queryInfo.XmlPageCountID);
                if (records > 0)
                    dt = dataMapper.QueryForDataTable(xmlID, queryInfo.Parameters);
            }
            catch (Exception e)
            {
                ErrorLog(xmlID, e.Message + "\r\n" + e.StackTrace);
            }
            return dt;
        }
        #endregion

        #region ����IDictionary������Ϊ��ҳ��DataTable����֧�ִ洢����
        public IDictionary GetListPage(QueryInfo queryInfo)
        {
            if (queryInfo == null) queryInfo = new QueryInfo();
            #region order by
            if (queryInfo.Orderby != null && queryInfo.Orderby.Count > 0)
            {
                string orderBy = string.Empty;
                foreach (object obj in queryInfo.Orderby.Keys)
                {
                    if (obj.ToString().Trim().Length == 0) continue;
                    orderBy += obj.ToString();
                    if (queryInfo.Orderby[obj] == null || queryInfo.Orderby[obj].Equals("asc"))
                        orderBy += " ,";
                    else
                        orderBy += string.Format(" {0} " + " ,", queryInfo.Orderby[obj]);
                }
                if (orderBy.Trim().Length > 0)
                {
                    orderBy = "order by " + orderBy.Substring(0, orderBy.Length - 1);
                    if (queryInfo.Parameters.Contains("OrderBy"))
                        queryInfo.Parameters["OrderBy"] = orderBy;
                    else
                        queryInfo.Parameters.Add("OrderBy", orderBy);
                }
            }
            #endregion
            IDictionary ht = new Hashtable();
            string xmlID = sPreFix + queryInfo.MappingName + (!string.IsNullOrEmpty(queryInfo.XmlID) ? "." + queryInfo.XmlID : ".LoadPageListByTable");
            try
            {
                int total = TotalCount(queryInfo.MappingName, queryInfo.Parameters, queryInfo.XmlPageCountID);
                ht.Add("total", total);
                if (total > 0)
                {
                    DataTable dt = dataMapper.QueryForDataTable(xmlID, queryInfo.Parameters);
                    if (dt != null) ht.Add("data", dt); else ht.Add("data", new DataTable());
                }
            }
            catch (Exception e)
            {
                ErrorLog(xmlID, e.Message + "\r\n" + e.StackTrace);
            }
            return ht;
        }
        #endregion

        #region ����
        public ITransaction ITransaction
        {
            get
            {
                ISession session = sessionFactory.OpenSession();
                return session.BeginTransaction();
            }
        }
        #endregion
    }
}
