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
        public T TryFunc<T>(string xmlID, Func<T> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                if (_logger.IsErrorEnabled)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine("XML ID=" + xmlID.PadRight(8));
                    sb.AppendLine(ex.Message); 
                    sb.AppendLine(ex.StackTrace);
                    string user = System.Web.HttpContext.Current.User.Identity.Name;
                    if (string.IsNullOrEmpty(user)) user = "�ο�";
                    sb.Append("\r\n" + user + "----------------");
                    _logger.Error(sb.ToString());
                }
                return default(T);
            }
        }
        #endregion

        #region ��ѯ����ָ���ֶ�
        public object QueryForObject(QueryInfo queryInfo)
        {
            string xmlID = sPreFix + queryInfo.MappingName + (!string.IsNullOrEmpty(queryInfo.XmlID) ? "." + queryInfo.XmlID : ".Load");
            return TryFunc(xmlID, () =>
            {
                return dataMapper.QueryForObject(xmlID, queryInfo.Parameters);
            });
        }
        #endregion

        #region TotalCount����ѯ��¼���������ļ�Ҫд������һ������

        public int TotalCount(string sTableName, IDictionary iDictionary, string xmlID)
        {
            xmlID = sPreFix + sTableName + (!string.IsNullOrEmpty(xmlID) ? "." + xmlID : ".Count");
            return TryFunc(xmlID, () =>
            {
                return Convert.ToInt32(dataMapper.QueryForObject(xmlID, iDictionary));
            });
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
            string xmlID = sPreFix + queryInfo.MappingName + (!string.IsNullOrEmpty(queryInfo.XmlID) ? "." + queryInfo.XmlID : ".Load");
            return TryFunc(xmlID, () =>
            {
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
                return dataMapper.QueryForList<T>(xmlID, queryInfo.Parameters);
            });
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
            return TryFunc(xmlID, () =>
            {
                IList list = null;
                if (queryInfo.MapQueryValue != null)
                    list = dataMapper.QueryForList(xmlID, queryInfo.MapQueryValue);
                else
                    list = dataMapper.QueryForList(xmlID, queryInfo.Parameters);
                return list;
            });
        }
        #endregion

        #region GetListPage������һ����¼���ϣ������ļ�Ҫд������һ����ҳ���� //�����Ǵ洢����
        #region old
        //public T GetListPage<T>(QueryInfo queryInfo) where T : EntityList, new()
        //{
        //    if (queryInfo == null) queryInfo = new QueryInfo();

        //    if (queryInfo.MappingName == null || queryInfo.MappingName.Length == 0)
        //    {
        //        //T obj = System.Activator.CreateInstance<T>();
        //        T obj = new T();
        //        queryInfo.MappingName = obj.GetType().Name.Replace("List", "");
        //    }
        //    #region order by
        //    if (queryInfo.Orderby != null && queryInfo.Orderby.Count > 0)
        //    {
        //        string orderBy = string.Empty;
        //        foreach (object obj in queryInfo.Orderby.Keys)
        //        {
        //            if (obj.ToString().Trim().Length == 0) continue;
        //            orderBy += obj.ToString();
        //            if (queryInfo.Orderby[obj] == null || queryInfo.Orderby[obj].Equals("asc"))
        //                orderBy += " ,";
        //            else
        //                orderBy += string.Format(" {0} " + " ,", queryInfo.Orderby[obj]);
        //        }
        //        if (orderBy.Trim().Length > 0)
        //        {
        //            orderBy = "order by " + orderBy.Substring(0, orderBy.Length - 1);
        //            //orderBy = orderBy.Substring(0, orderBy.Length - 1);
        //            if (queryInfo.Parameters.Contains("OrderBy"))
        //                queryInfo.Parameters["OrderBy"] = orderBy;
        //            else
        //                queryInfo.Parameters.Add("OrderBy", orderBy);
        //        }
        //    }
        //    #endregion
        //    T lstEntity = new T();
        //    string xmlID = sPreFix + queryInfo.MappingName + (!string.IsNullOrEmpty(queryInfo.XmlID) ? "." + queryInfo.XmlID : ".LoadPageList");
        //    try
        //    {
        //        lstEntity.records = TotalCount(queryInfo.MappingName, queryInfo.Parameters, queryInfo.XmlPageCountID);
        //        if (lstEntity.records > 0)
        //            lstEntity.data = dataMapper.QueryForList<Entity>(xmlID, queryInfo.Parameters);
        //        if (lstEntity.data == null) lstEntity.data = new List<Entity>();
        //    }
        //    catch (Exception e)
        //    {
        //        ErrorLog(xmlID, e.Message + "\r\n" + e.StackTrace);
        //    }
        //    return lstEntity;
        //}
        #endregion
        public PagedList<T> GetListPage<T>(QueryInfo queryInfo) where T : Entity, new()
        {
            if (queryInfo == null) queryInfo = new QueryInfo();
            if (queryInfo.MappingName == null || queryInfo.MappingName.Length == 0)
            {
                T obj = new T();
                queryInfo.MappingName = obj.GetType().Name;
            }
            string xmlID = sPreFix + queryInfo.MappingName + (!string.IsNullOrEmpty(queryInfo.XmlID) ? "." + queryInfo.XmlID : ".LoadPageList");
            return TryFunc(xmlID, () =>
            {
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
                int records = TotalCount(queryInfo.MappingName, queryInfo.Parameters, queryInfo.XmlPageCountID);
                IList<Entity> list = new List<Entity>();
                if (records > 0)
                    list = dataMapper.QueryForList<Entity>(xmlID, queryInfo.Parameters);
                return new PagedList<T>(records, list);
            });
        }
        #endregion

        #region GetListPage������һ����¼IList���ϣ������ļ�Ҫд������һ����ҳ���� //�����Ǵ洢���� //�����������
        #region old
        //public IDictionary GetListPages<T>(QueryInfo queryInfo) where T : Entity, new()
        //{
        //    if (queryInfo == null) queryInfo = new QueryInfo();

        //    if (queryInfo.MappingName == null || queryInfo.MappingName.Length == 0)
        //    {
        //        T obj = new T();
        //        queryInfo.MappingName = obj.GetTableName();
        //    }
        //    #region order by
        //    if (queryInfo.Orderby != null && queryInfo.Orderby.Count > 0)
        //    {
        //        string orderBy = string.Empty;
        //        foreach (object obj in queryInfo.Orderby.Keys)
        //        {
        //            if (obj.ToString().Trim().Length == 0) continue;
        //            orderBy += obj.ToString();
        //            if (queryInfo.Orderby[obj] == null || queryInfo.Orderby[obj].Equals("asc"))
        //                orderBy += " ,";
        //            else
        //                orderBy += string.Format(" {0} " + " ,", queryInfo.Orderby[obj]);
        //        }
        //        if (orderBy.Trim().Length > 0)
        //        {
        //            orderBy = "order by " + orderBy.Substring(0, orderBy.Length - 1);
        //            //orderBy = orderBy.Substring(0, orderBy.Length - 1);
        //            if (queryInfo.Parameters.Contains("OrderBy"))
        //                queryInfo.Parameters["OrderBy"] = orderBy;
        //            else
        //                queryInfo.Parameters.Add("OrderBy", orderBy);
        //        }
        //    }
        //    #endregion
        //    IDictionary lstEntity = new Hashtable();
        //    string xmlID = sPreFix + queryInfo.MappingName + (!string.IsNullOrEmpty(queryInfo.XmlID) ? "." + queryInfo.XmlID : ".LoadPageList");
        //    try
        //    {
        //        int records = TotalCount(queryInfo.MappingName, queryInfo.Parameters, queryInfo.XmlPageCountID);
        //        lstEntity.Add("records", records);
        //        if (records > 0)
        //        {
        //            var data = dataMapper.QueryForList<Entity>(xmlID, queryInfo.Parameters);
        //            lstEntity.Add("data", data);
        //            if (data == null) lstEntity.Add("data", new List<Entity>());
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        ErrorLog(xmlID, e.Message + "\r\n" + e.StackTrace);
        //    }
        //    return lstEntity;
        //}
        #endregion
        public PagedIList<T> GetIListPage<T>(QueryInfo queryInfo) where T : Entity, new()
        {
            if (queryInfo == null) queryInfo = new QueryInfo();
            if (queryInfo.MappingName == null || queryInfo.MappingName.Length == 0)
            {
                T obj = new T();
                queryInfo.MappingName = obj.GetTableName();
            }
            IDictionary lstEntity = new Hashtable();
            string xmlID = sPreFix + queryInfo.MappingName + (!string.IsNullOrEmpty(queryInfo.XmlID) ? "." + queryInfo.XmlID : ".LoadPageList");
            return TryFunc(xmlID, () =>
            {
                int records = TotalCount(queryInfo.MappingName, queryInfo.Parameters, queryInfo.XmlPageCountID);
                IList list = new List<Entity>();
                if (records > 0)
                    list = dataMapper.QueryForList(xmlID, queryInfo.Parameters);
                return new PagedIList<T>(records, list);
            });
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
            return TryFunc(xmlID, () =>
            {
                return dataMapper.QueryForObject<T>(xmlID, queryInfo.Parameters);
            });
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
            return TryFunc(xmlID, () =>
            {
                return dataMapper.QueryForObject<T>(xmlID, objEntity);
            });
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
            return TryFunc(xmlID, () =>
            {
                return dataMapper.QueryForObject<T>(xmlID, queryInfo.Parameters);
            });
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
            return TryFunc(xmlID, () =>
            {
                return Convert.ToInt32(dataMapper.Insert(xmlID, objEntity));
            });
        }

        public int Insert(QueryInfo queryInfo)
        {
            string xmlID = sPreFix + queryInfo.MappingName + (!string.IsNullOrEmpty(queryInfo.XmlID) ? "." + queryInfo.XmlID : ".Insert");
            return TryFunc(xmlID, () =>
            {
                return Convert.ToInt32(dataMapper.Insert(xmlID, queryInfo.Parameters));
            });
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
            string xmlID = objEntity.GetUpdateXmlID();
            return TryFunc(xmlID, () =>
            {
                return dataMapper.Update(xmlID, objEntity);
            });
        }
        public int Update(QueryInfo queryInfo)
        {
            string xmlID = sPreFix + queryInfo.MappingName + (!string.IsNullOrEmpty(queryInfo.XmlID) ? "." + queryInfo.XmlID : ".Update");
            return TryFunc(xmlID, () =>
            {
                return Convert.ToInt32(dataMapper.Update(xmlID, queryInfo.Parameters));
            });
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
            string xmlID = objEntity.GetDeleteXmlID();
            return TryFunc(xmlID, () =>
            {
                return dataMapper.Delete(xmlID, objEntity);
            });
        }

        public int Delete(QueryInfo queryInfo)
        {
            string xmlID = sPreFix + queryInfo.MappingName + (!string.IsNullOrEmpty(queryInfo.XmlID) ? "." + queryInfo.XmlID : ".Delete");
            return TryFunc(xmlID, () =>
            {
                return Convert.ToInt32(dataMapper.Delete(xmlID, queryInfo.Parameters));
            });
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
            string xmlID = sPreFix + queryInfo.MappingName + (!string.IsNullOrEmpty(queryInfo.XmlID) ? "." + queryInfo.XmlID : ".Load");
            return TryFunc(xmlID, () =>
            {
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
                return dataMapper.QueryForDataTable(xmlID, queryInfo.Parameters);
            });
        }
        #endregion

        #region ���ط�ҳDataTable�Ĳ�ѯ����֧�ִ洢����
        public PagedTable GetListPage(QueryInfo queryInfo)
        {
            if (queryInfo == null) queryInfo = new QueryInfo();
            string xmlID = sPreFix + queryInfo.MappingName + (!string.IsNullOrEmpty(queryInfo.XmlID) ? "." + queryInfo.XmlID : ".LoadPageListByTable");
            return TryFunc(xmlID, () =>
            {
                int records = 0;
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
                records = TotalCount(queryInfo.MappingName, queryInfo.Parameters, queryInfo.XmlPageCountID);
                if (records > 0)
                    dt = dataMapper.QueryForDataTable(xmlID, queryInfo.Parameters);
                return new PagedTable(records, dt);
            });
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
