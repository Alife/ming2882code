using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using MC.Model;

namespace MC.DAO
{
    /// <summary>
    /// 此类负责提供IBatis工程Query2类的泛化封装,供Business层调用
    /// </summary>
    public interface IDao
    {
        #region 查询返回指定字段
        object QueryForObject(QueryInfo queryInfo);
        #endregion

        #region TotalCount　查询记录数，配置文件要写明返回一个整型

        int TotalCount(string sTableName, IDictionary htParameters, string xmlID);

        int TotalCount<T>(QueryInfo queryInfo) where T : Entity, new();

        #endregion

        #region IList　返回一个记录集合，配置文件要写明返回一个集合
        IList<T> GetList<T>(QueryInfo queryInfo) where T : Entity, new();
        #endregion

        #region GetList  返回一个集合，集合中可以是不同的实体
        IList GetList(QueryInfo queryInfo);
        #endregion

        #region GetListPage　返回一个记录集合，配置文件要写明返回一个分页集合
        PagedList<T> GetListPage<T>(QueryInfo queryInfo) where T : Entity, new();
        #endregion

        #region GetListPages　返回一个记录IList集合，配置文件要写明返回一个分页集合 //可以是存储过程
        PagedIList<T> GetIListPage<T>(QueryInfo queryInfo) where T : Entity, new();
        #endregion

        #region GetItem 返回一条记录或值，配置文件要写明返回的类型
        T GetItem<T>(QueryInfo queryInfo) where T : Entity, new();
        T GetItem<T>(object sPK) where T : Entity, new();
        T GetItem<T>(T objEntity) where T : Entity, new();
        #endregion

        #region Insert　插入一个对象
        int Insert(Entity eEntity);
        int Insert(QueryInfo queryInfo);
        #endregion

        #region Update　更新一个对象
        int Update(Entity eEntity);
        int Update(QueryInfo queryInfo);
        #endregion

        #region Delete　删除一个对象
        int Delete(Entity eEntity);
        int Delete(QueryInfo queryInfo);
        #endregion

        #region Save & SaveAll　保存一个对象或一个对象集合
        int Save(Entity eEntity);
        IList<T> Save<T>(IList<T> lstEntity) where T : Entity, new();
        #endregion

        #region 返回DataTable的查询//可以是存储过程包括返回值的、分页的
        /// <summary>
        /// 返回QueryForDataTable的查询
        /// </summary>
        /// <param name="statementName">查询对应的配置语句ID</param>
        /// <param name="paramObject">查询参数对象</param>
        /// <returns></returns>
        DataTable GetDataTable(QueryInfo queryInfo);
        #endregion

        #region 返回分页DataTable的查询，不支持存储过程
        DataTable GetDataTable(QueryInfo queryInfo, ref int records);
        #endregion

        #region 返回IDictionary，内容为分页的DataTable，不支持存储过程
        IDictionary GetListPage(QueryInfo queryInfo);
        #endregion
    }
}
