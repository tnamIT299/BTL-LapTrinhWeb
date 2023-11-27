using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Client_Home.Areas.Admin.DTO.Employees

{
    public interface IAddEmployFromExcel
    {
        string DoucumentUpload(IFormFile formFile);
        DataTable EmployeeDataTable(string path);
        void ImportEmployee(DataTable employee);
    }
}
