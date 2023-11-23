
using System.Data;

namespace Client_Home.Areas.Admin.DTO.Customers
{
    public interface  IAddFromExcel
    {
        string DoucumentUpload(IFormFile formFile);
        DataTable CustomerDataTable(string path);
        void ImportCustomer(DataTable customer);
    }
}
