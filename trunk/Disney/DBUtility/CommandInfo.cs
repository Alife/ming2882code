using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

namespace DBUtility
{
    public class CommandInfo
    {
        public string CommandText;
        public EffentNextType EffentNextType;
        public object OriginalData;
        public DbParameter[] Parameters;
        public object ShareObject;

        private event EventHandler _solicitationEvent;

        public event EventHandler SolicitationEvent;

        public CommandInfo()
        {
            this.ShareObject = null;
            this.OriginalData = null;
            this.EffentNextType = EffentNextType.None;
            this.CommandText = string.Empty;
        }

        public CommandInfo(string sqlText, DbParameter[] para)
        {
            this.ShareObject = null;
            this.OriginalData = null;
            this.EffentNextType = EffentNextType.None;
            this.CommandText = sqlText;
            this.Parameters = para;
        }

        public CommandInfo(string sqlText, DbParameter[] para, EffentNextType type)
        {
            this.ShareObject = null;
            this.OriginalData = null;
            this.EffentNextType = EffentNextType.None;
            this.CommandText = sqlText;
            this.Parameters = para;
            this.EffentNextType = type;
        }

        public void OnSolicitationEvent()
        {
            if (this._solicitationEvent != null)
            {
                this._solicitationEvent(this, new EventArgs());
            }
        }
    }
}
