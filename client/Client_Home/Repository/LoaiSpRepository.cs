using Client_Home.Models;

namespace Client_Home.Repository
{
    public class LoaiSpRepository : ILoaiSpRepository
    {
        private readonly Client_Home.Data.ConveniencestoreContext _context;
        public LoaiSpRepository(Client_Home.Data.ConveniencestoreContext context)
        {
            _context = context;
        }

        public Category Add(Category loaiSp)
        {
            _context.Categories.Add(loaiSp);
            _context.SaveChanges();
            return loaiSp;
        }

        public Category Delete(string maloaiSp)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Category> GetAllLoaiSp()
        {
            return _context.Categories;
        }

        public Category GetLoaiSp(string maloaiSp)
        {
            return _context.Categories.Find(maloaiSp);
        }

        public Category Update(Category loaiSp)
        {
            _context.Update(loaiSp);
            _context.SaveChanges();
            return loaiSp;
        }
    }
}
