﻿using Client_Home.Models;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.OleDb;
using System.Runtime.InteropServices;

namespace Client_Home.Areas.Admin.DTO.Employees

{
    public class AddEmployFromExcel : IAddEmployFromExcel
    {
        private IConfiguration configuration;
        private IWebHostEnvironment webHostEnvironment;

        public AddEmployFromExcel(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            this.configuration = configuration;
            this.webHostEnvironment = webHostEnvironment;
        }
        public string DoucumentUpload(IFormFile formFile)
        {
            string uploadPath = webHostEnvironment.WebRootPath;
            string dest_path = Path.Combine(uploadPath, "uploaded_doc");
            if (!Directory.Exists(dest_path))
            {
                Directory.CreateDirectory(dest_path);
            }
            string sourceFile = Path.GetFileName(formFile.FileName);
            string path = Path.Combine(dest_path, sourceFile);
            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                formFile.CopyTo(fileStream);
            }
            return path;
        }

        public DataTable EmployeeDataTable(string path)
        {
            var conStr = configuration.GetConnectionString("excelconnection");
            DataTable dataTable = new DataTable();

            conStr = string.Format(conStr, path);

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                using (OleDbConnection excelconn = new OleDbConnection(conStr))
                {
                    excelconn.Open();

                    DataTable excelschema = excelconn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                    if (excelschema != null && excelschema.Rows.Count > 0)
                    {
                        string sheetname = excelschema.Rows[0]["TABLE_NAME"].ToString();
                        excelconn.Close();

                        excelconn.Open();
                        using (OleDbCommand cmd = new OleDbCommand())
                        {
                            cmd.Connection = excelconn;

                            // Đảm bảo tên bảng được bao quanh bởi dấu ngoặc vuông
                            cmd.CommandText = "SELECT * FROM [" + sheetname + "]";
                            using (OleDbDataAdapter adapterexcel = new OleDbDataAdapter(cmd))
                            {
                                adapterexcel.Fill(dataTable);
                            }
                        }
                        excelconn.Close();
                    }
                }
            }
            return dataTable;
        }

        public void ImportEmployee(DataTable employee)
        {
            var sqlconn = configuration.GetConnectionString("dbCONVENIENCESTORE");

            using (SqlConnection scon = new SqlConnection(sqlconn))
            {
                scon.Open();

                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(scon))
                {
                    sqlBulkCopy.DestinationTableName = "Employees";
                    foreach (DataColumn column in employee.Columns)
                    {
                        // Ánh xạ cột
                        sqlBulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                    }
                    sqlBulkCopy.WriteToServer(employee);
                }

                // Đóng kết nối sau khi đã sử dụng
                scon.Close();
            }
        }
    }
}
