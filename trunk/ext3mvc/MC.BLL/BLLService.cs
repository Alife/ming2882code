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
        #region Save & SaveAll　保存一个对象或一个对象集合
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
        #region Delete　删除一个对象
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
        #region Insert　插入一个对象
        public static int Insert(QueryInfo queryInfo)
        {
            int v = dao.Insert(queryInfo);
            //if (v > 0)
            //    log.Add(queryInfo);
            return v;
        }
        #endregion
        #region Update　更新一个对象
        public static int Update(QueryInfo queryInfo)
        {
            return dao.Update(queryInfo);
        }
        #endregion
        #region GetList  返回一个集合，集合中可以是不同的实体
        public static IList<T> GetList<T>(QueryInfo queryInfo) where T : Entity, new()
        {
            return dao.GetList<T>(queryInfo);
        }
        #endregion
        #region IList　返回一个记录集合，配置文件要写明返回一个集合
        public static IList GetList(QueryInfo queryInfo)
        {
            return dao.GetList(queryInfo);
        }
        #endregion
        #region GetListPage　返回一个记录集合，配置文件要写明返回一个分页集合
        public static PagedList<T> GetListPage<T>(QueryInfo queryInfo) where T : Entity, new()
        {
            return dao.GetListPage<T>(queryInfo);
        }
        #endregion
        #region 返回PagedTable，内容为分页的DataTable，不支持存储过程
        public static PagedTable GetListPage(QueryInfo queryInfo)
        {
            return dao.GetListPage(queryInfo);
        }
        #endregion
        #region GetListPages　返回一个记录IDictionary集合，配置文件要写明返回一个分页集合 //可以是存储过程
        public static PagedIList<T> GetIListPage<T>(QueryInfo queryInfo) where T : Entity, new()
        {
            return dao.GetIListPage<T>(queryInfo);
        }
        #endregion
        #region TotalCount　查询记录数，配置文件要写明返回一个整型
        public static int TotalCount<T>(QueryInfo queryInfo) where T : Entity, new()
        {
            return dao.TotalCount<T>(queryInfo);
        }
        #endregion
        #region GetItem 返回一条记录或值，配置文件要写明返回的类型
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
        #region 返回DataTable的查询//可以是存储过程包括返回值的、分页的
        public static DataTable GetDataTable(QueryInfo queryInfo)
        {
            return dao.GetDataTable(queryInfo);
        }
        #endregion
    }
}
