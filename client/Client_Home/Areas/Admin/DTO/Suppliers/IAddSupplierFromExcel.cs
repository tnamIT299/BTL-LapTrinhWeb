using System.Data;

namespace Client_Home.Areas.Admin.DTO.Suppliers
{
    public interface IAddSupplierFromExcel
    {
        string DoucumentUpload(IFormFile formFile);
        DataTable SupplierDataTable(string path);
        void ImportSupplier(DataTable supplier);
    }
}
