/*-----------------------------------------------
// Copyright (C) 2018 南京戎光软件科技有限公司  版权所有。
// 文件名称：    ExcelHelper
// 功能描述：    
// 创建标识：    panshuai 2018-07-07 10:53:37
// 修改标识：    panshuai 2018-07-07 10:53:37
// 修改描述:     
-----------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Cells;
using System.Data;
using System.IO;

namespace DeviceConnector.Helper
{
    public class ExcelHelper
    {
        /// <summary>
        /// Excel导入为DataTable
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <param name="columnNames">列名</param>
        /// <param name="sheetNum">导入工作簿序号</param>
        /// <param name="startRow">起始行</param>
        /// <param name="startCol">起始列</param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string filePath, string[] columnNames, int sheetNum = 0,
            int startRow = 0, int startCol = 0)
        {
            DataTable dt = new DataTable();
            try
            {
                Workbook book = new Workbook(filePath);
                Worksheet sheet = book.Worksheets[sheetNum];
                Cells cells = sheet.Cells;
                dt = cells.ExportDataTableAsString(startRow, startCol, cells.MaxDataRow - startRow + 1,
                    cells.MaxDataColumn - startCol + 1, false);
                int num = columnNames.Length > dt.Columns.Count ? dt.Columns.Count : columnNames.Length;
                for (int i = 0; i < num; i++)
                {
                    dt.Columns[i].ColumnName = columnNames[i];
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// Excel导入为DataTable
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string filePath)
        {
            DataTable dt = new DataTable();
            try
            {
                Workbook book = new Workbook(filePath);
                Worksheet sheet = book.Worksheets[0];
                Cells cells = sheet.Cells;
                dt = cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// Excel导入为DataSet
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static DataSet ExcelToDataSet(string filePath)
        {
            DataSet ds = new DataSet();
            try
            {
                Workbook book = new Workbook(filePath);
                foreach (Worksheet sheet in book.Worksheets)
                {
                    DataTable dt = new DataTable();
                    Cells cells = sheet.Cells;
                    dt = cells.ExportDataTableAsString(0, 0, cells.MaxDataRow + 1, cells.MaxDataColumn + 1, false);
                    ds.Tables.Add(dt);
                }
            }
            catch (Exception ex)
            {
            }
            return ds;
        }

        /// <summary>
        /// DataTable导出为Excel
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="filePath"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static bool DataTableToExcel(DataTable dt, string filePath, string title = "")
        {
            bool result = false;
            try
            {
                Workbook book = new Workbook();
                Worksheet sheet = book.Worksheets[0];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sheet.Cells[0, i].PutValue(dt.Columns[i].ColumnName);
                }
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        sheet.Cells[i, j].PutValue(dt.Rows[i][j].ToString());
                    }
                }

                sheet.AutoFitColumns();
                sheet.AutoFitRows();
                book.Save(filePath);
                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// DataSet导出为Excel
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="filePath"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static bool DataSetToExcel(DataSet ds, string filePath, string fileName = "报表", string[] sheetName = null,
            string[,] colName = null)
        {
            bool result = false;
            try
            {
                Workbook book = new Workbook();

                for (int k = 0; k < ds.Tables.Count; k++)
                {
                    DataTable dt = ds.Tables[k];
                    Worksheet sheet = book.Worksheets[k];
                    if (sheetName != null)
                    {
                        sheet.Name = sheetName[k];
                    }

                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (colName != null)
                        {
                            sheet.Cells[0, i].PutValue(colName[k, i]);
                        }
                        else
                        {
                            sheet.Cells[0, i].PutValue(dt.Columns[i].ColumnName);
                        }
                    }
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            sheet.Cells[i + 1, j].PutValue(dt.Rows[i][j].ToString());
                        }
                    }
                    sheet.AutoFitColumns();
                    sheet.AutoFitRows();
                    book.Worksheets.Add();
                }
                //保存Excel文件
                string fileToSave = Path.Combine(filePath, fileName);
                book.Save(fileToSave);

                result = true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
    }
}
