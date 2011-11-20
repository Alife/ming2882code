using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MC.DAO;
using MC.Model;

namespace MC.BLL
{
    public class BLLService
    {
        private static IDao _dao = null;
        //private static sys_LogManager _log = null;
        //public static sys_LogManager log
        //{
        //    get
        //    {
        //        if (_log == null)
        //            _log = new sys_LogManager();
        //        return _log;
        //    }
        //}
        public static IDao dao
        {
            get
            {
                if (_dao == null)
                    _dao = new DaoImpl();
                return _dao;
            }
        }
        #region Save & SaveAll������һ�������һ�����󼯺�
        public static int Save(Entity objEntity)
        {
            int v = dao.Save(objEntity);
            //if (v > 0)
            //    log.Save<Entity>(objEntity);
            return v;
        }
        public static int Save<T>(IList<T> lstEntity) where T : Entity, new()
        {
            lstEntity = dao.Save<T>(lstEntity);
            int v = lstEntity.Count;
            //if (v > 0)
            //    log.Save<T>(lstEntity);
            return v;
        }
        #endregion
        #region Delete��ɾ��һ������
        public static int Delete(Entity objEntity)
        {
            int v = dao.Delete(objEntity);
            //if (v > 0)
            //    log.Save<Entity>(objEntity);
            return v;
        }
        public static int Delete(QueryInfo queryInfo)
        {
            int v = dao.Delete(queryInfo);
            //if (v > 0)
            //    log.Delete(queryInfo);
            return v;
        }
        #endregion
        #region Insert������һ������
        public static int Insert(QueryInfo queryInfo)
        {
            int v = dao.Insert(queryInfo);
            //if (v > 0)
            //    log.Add(queryInfo);
            return v;
        }
        #endregion
        #region Update������һ������
        public static int Update(QueryInfo queryInfo)
        {
            return dao.Update(queryInfo);
        }
        #endregion
        #region GetList  ����һ�����ϣ������п����ǲ�ͬ��ʵ��
        public static IList<T> GetList<T>(QueryInfo queryInfo) where T : Entity, new()
        {
            return dao.GetList<T>(queryInfo);
        }
        #endregion
        #region IList������һ����¼���ϣ������ļ�Ҫд������һ������
        public static IList GetList(QueryInfo queryInfo)
        {
            return dao.GetList(queryInfo);
        }
        #endregion
        #region GetListPage������һ����¼���ϣ������ļ�Ҫд������һ����ҳ����
        public static PagedList<T> GetListPage<T>(QueryInfo queryInfo) where T : Entity, new()
        {
            return dao.GetListPage<T>(queryInfo);
        }
        #endregion
        #region ����PagedTable������Ϊ��ҳ��DataTable����֧�ִ洢����
        public static PagedTable GetListPage(QueryInfo queryInfo)
        {
            return dao.GetListPage(queryInfo);
        }
        #endregion
        #region GetListPages������һ����¼IDictionary���ϣ������ļ�Ҫд������һ����ҳ���� //�����Ǵ洢����
        public static PagedIList<T> GetIListPage<T>(QueryInfo queryInfo) where T : Entity, new()
        {
            return dao.GetIListPage<T>(queryInfo);
        }
        #endregion
        #region TotalCount����ѯ��¼���������ļ�Ҫд������һ������
        public static int TotalCount<T>(QueryInfo queryInfo) where T : Entity, new()
        {
            return dao.TotalCount<T>(queryInfo);
        }
        #endregion
        #region GetItem ����һ����¼��ֵ�������ļ�Ҫд�����ص�����
        public static T GetItem<T>(QueryInfo queryInfo) where T : Entity, new()
        {
            return dao.GetItem<T>(queryInfo);
        }
        public static T GetItem<T>(T objEntity) where T : Entity, new()
        {
            return dao.GetItem<T>(objEntity);
        }
        public static T GetItem<T>(object sPK) where T : Entity, new()
        {
            return dao.GetItem<T>(sPK);
        }
        #endregion
        #region ����DataTable�Ĳ�ѯ//�����Ǵ洢���̰�������ֵ�ġ���ҳ��
        public static DataTable GetDataTable(QueryInfo queryInfo)
        {
            return dao.GetDataTable(queryInfo);
        }
        #endregion
    }
}
