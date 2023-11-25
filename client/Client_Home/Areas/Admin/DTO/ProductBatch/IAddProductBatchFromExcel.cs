using System.Data;

namespace Client_Home.Areas.Admin.DTO.ProductBatch
{
    public interface IAddProductBatchFromExcel
    {
        string DoucumentUpload(IFormFile formFile);
        DataTable ProductBatchDataTable(string path);
        void ImportProductBatch(DataTable productBatch);
    }
}
