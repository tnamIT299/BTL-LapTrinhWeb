using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.OleDb;
using System.Runtime.InteropServices;

namespace Client_Home.Areas.Admin.DTO.Customers
{
    public class AddFromExcel : IAddFromExcel
    {
        private IConfiguration configuration;
        private IWebHostEnvironment webHostEnvironment;

        public AddFromExcel(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            this.configuration = configuration;
            this.webHostEnvironment = webHostEnvironment;
        }
        public DataTable CustomerDataTable(string path)
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

        public void ImportCustomer(DataTable customer)
        {
            var sqlconn = configuration.GetConnectionString("dbCONVENIENCESTORE");

            using (SqlConnection scon = new SqlConnection(sqlconn))
            {
                scon.Open();

                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(scon))
                {
                    sqlBulkCopy.DestinationTableName = "Customers";
                    foreach (DataColumn column in customer.Columns)
                    {
                        // Ánh xạ cột
                        sqlBulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                    }
                    sqlBulkCopy.WriteToServer(customer);
                }

                // Đóng kết nối sau khi đã sử dụng
                scon.Close();
            }
        }
    }
}
