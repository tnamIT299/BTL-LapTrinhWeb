using System.Data;

namespace Client_Home.Areas.Admin.DTO.Product
{
    public interface IAddProductFromExcel
    {
        string DoucumentUpload(IFormFile formFile);
        DataTable ProductDataTable(string path);
        void ImportProduct(DataTable product);
    }
}
