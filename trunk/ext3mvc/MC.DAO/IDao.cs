using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using MC.Model;

namespace MC.DAO
{
    /// <summary>
    /// ���ฺ���ṩIBatis����Query2��ķ�����װ,��Business�����
    /// </summary>
    public interface IDao
    {
        #region ��ѯ����ָ���ֶ�
        object QueryForObject(QueryInfo queryInfo);
        #endregion

        #region SelectCount����ѯ��¼���������ļ�Ҫд������һ������

        int SelectCount(string sTableName, IDictionary htParameters, string xmlID);

        int SelectCount<T>(QueryInfo queryInfo) where T : Entity, new();

        #endregion

        #region IList������һ����¼���ϣ������ļ�Ҫд������һ������
        IList<T> Find<T>(QueryInfo queryInfo) where T : Entity, new();
        #endregion

        #region Find  ����һ�����ϣ������п����ǲ�ͬ��ʵ��
        IList FindList(QueryInfo queryInfo);
        #endregion

        #region Find������һ����¼���ϣ������ļ�Ҫд������һ����ҳ����
        T FindList<T>(QueryInfo queryInfo) where T : EntityList, new();
        #endregion

        #region GetList������һ����¼IDictionary���ϣ������ļ�Ҫд������һ����ҳ���� //�����Ǵ洢����
        IDictionary GetList<T>(QueryInfo queryInfo) where T : Entity, new();
        #endregion

        #region GetItem ����һ����¼��ֵ�������ļ�Ҫд�����ص�����
        T GetItem<T>(QueryInfo queryInfo) where T : Entity, new();
        T GetItem<T>(object sPK) where T : Entity, new();
        T GetItem<T>(T objEntity) where T : Entity, new();
        #endregion

        #region Insert������һ������
        int Insert(Entity eEntity);
        int Insert(QueryInfo queryInfo);
        #endregion

        #region Update������һ������
        int Update(Entity eEntity);
        int Update(QueryInfo queryInfo);
        #endregion

        #region Delete��ɾ��һ������
        int Delete(Entity eEntity);
        int Delete(QueryInfo queryInfo);
        #endregion

        #region Save & SaveAll������һ�������һ�����󼯺�
        int Save(Entity eEntity);
        IList<T> Save<T>(IList<T> lstEntity) where T : Entity, new();
        #endregion

        #region ����DataTable�Ĳ�ѯ//�����Ǵ洢���̰�������ֵ�ġ���ҳ��
        /// <summary>
        /// ����QueryForDataTable�Ĳ�ѯ
        /// </summary>
        /// <param name="statementName">��ѯ��Ӧ���������ID</param>
        /// <param name="paramObject">��ѯ��������</param>
        /// <returns></returns>
        DataTable GetDataTable(QueryInfo queryInfo);
        #endregion

        #region ���ط�ҳDataTable�Ĳ�ѯ����֧�ִ洢����
        DataTable GetDataTable(QueryInfo queryInfo, ref int records);
        #endregion

        #region ����IDictionary������Ϊ��ҳ��DataTable����֧�ִ洢����
        IDictionary GetList(QueryInfo queryInfo);
        #endregion
    }
}
