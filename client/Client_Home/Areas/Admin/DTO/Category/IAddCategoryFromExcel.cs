using System.Data;

namespace Client_Home.Areas.Admin.DTO.Category
{
    public interface IAddCategoryFromExcel
    {
        string DoucumentUpload(IFormFile formFile);
        DataTable CategoryDataTable(string path);
        void ImportCategory(DataTable category);
    }
}
