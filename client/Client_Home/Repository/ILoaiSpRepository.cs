using Client_Home.Models;
namespace Client_Home.Repository
{
    public interface ILoaiSpRepository
    {
        Category Add(Category loaiSp);
        Category Update(Category loaiSp);
        Category Delete(string maloaiSp);
        Category GetLoaiSp(string maloaiSp);
        IEnumerable<Category> GetAllLoaiSp();
    }
}
