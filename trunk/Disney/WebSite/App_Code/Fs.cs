using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Models;
using BLL;

public class GridItem<T>
{
    public int total
    {
        get;
        set;
    }
    public List<T> rows
    {
        get;
        set;
    }
}
public class TreeNodeItem
{
    public int id { get; set; }
    public string text { get; set; }
    public string iconCls { get; set; }
    public int href { get; set; }
    public bool leaf { get; set; }
    public List<TreeNodeItem> children { get; set; }
}
public class TreeArea : sys_Area
{
}
public class TreeAreaList
{
    public List<TreeArea> data { get; set; }
    public int records { get; set; }
}
public class DataTableClass
{
    public static DataTable GetPagedTable(DataTable dt, int PageIndex, int PageSize)
    {
        DataTable newdt = dt.Clone();
        //newdt.Clear();
        int rowbegin = PageIndex;
        int rowend = PageIndex + PageSize;

        if (rowbegin >= dt.Rows.Count)
            return newdt;

        if (rowend > dt.Rows.Count)
            rowend = dt.Rows.Count;
        for (int i = rowbegin; i <= rowend - 1; i++)
        {
            DataRow newdr = newdt.NewRow();
            DataRow dr = dt.Rows[i];
            foreach (DataColumn column in dt.Columns)
            {
                newdr[column.ColumnName] = dr[column.ColumnName];
            }
            newdt.Rows.Add(newdr);
        }

        return newdt;
    }
}