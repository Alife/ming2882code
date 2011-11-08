using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Collections;
using System.ComponentModel;

namespace MC.Model
{
    [Serializable]
    public partial class Entity
    {
        private Entity _oldEntity;

        public virtual void SetOldEntity(Entity o)
        {
            _oldEntity = o;
        }
        public Entity GetOldEntity()
        {
            return _oldEntity;
        }
        private string insert_XmlID;
        /// <summary>
        /// 设置增加对应的mapping中的ID
        /// </summary>
        /// <param name="xmlID"></param>
        public virtual void SetInsertXmlID(string xmlID) { insert_XmlID = GetType().ToString() + (!string.IsNullOrEmpty(xmlID) ? "." + xmlID : ".Insert"); }
        public virtual string GetInsertXmlID() { return insert_XmlID; }

        private string update_XmlID;
        /// <summary>
        /// 设置修改对应的mapping中的ID
        /// </summary>
        /// <param name="xmlID"></param>
        public virtual void SetUpdateXmlID(string xmlID) { update_XmlID = GetType().ToString() + (!string.IsNullOrEmpty(xmlID) ? "." + xmlID : ".Update"); }
        public virtual string GetUpdateXmlID() { return update_XmlID; }

        private string delete_XmlID;
        /// <summary>
        /// 设置删除对应的mapping中的ID
        /// </summary>
        /// <param name="xmlID"></param>
        public virtual void SetDeleteXmlID(string xmlID) { delete_XmlID = GetType().ToString() + (!string.IsNullOrEmpty(xmlID) ? "." + xmlID : ".Delete"); }
        public virtual string GetDeleteXmlID() { return delete_XmlID; }
        private string select_XmlID;
        /// <summary>
        /// 设置查询对应的mapping中的ID
        /// </summary>
        /// <param name="xmlID"></param>
        public virtual void SetSelectXmlID(string xmlID) { select_XmlID = GetType().ToString() + (!string.IsNullOrEmpty(xmlID) ? "." + xmlID : ".LoadItem"); }
        public virtual string GetSelectXmlID() { return select_XmlID; }

        public Entity()
        {
            SetState(EntityState.Added);
            SetInsertXmlID(string.Empty);
            SetUpdateXmlID(string.Empty);
            SetDeleteXmlID(string.Empty);
            SetSelectXmlID(string.Empty);
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public virtual string[] GetKeyCols()
        {
            return null;
        }
        /// <summary>
        /// 得到表名
        /// </summary>
        /// <returns></returns>
        public virtual string GetTableName()
        {
            return null;
        }
        public virtual void SetLoading()
        {
            this.SetState(EntityState.Unchanged);
        }

        #region State
        private EntityState _state = EntityState.Unchanged;
        
        public EntityState GetState()
        {
            return _state;
        }

        public virtual void SetState(EntityState es)
        {
            _state = es;
        }

        #endregion
    }
    [Serializable]
    public class PagedList<T> where T : Entity, new()
    {
        public PagedList(int _records, IList<Entity> _data)
        {
            records = _records;
            data = _data;
        }
        public int records { get; set; }
        public IList<Entity> data { get; set; }
    }
    [Serializable]
    public class PagedIList<T> where T : Entity, new()
    {
        public PagedIList(int _records, IList _data)
        {
            records = _records;
            data = _data;
        }
        public int records { get; set; }
        public IList data { get; set; }
    }
}
