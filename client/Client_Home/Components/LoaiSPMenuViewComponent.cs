using Client_Home.Models;
using Client_Home.Repository;
using Microsoft.AspNetCore.Mvc;
using Client_Home.Repository;
namespace Client_Home.Components
{
    public class LoaiSPMenuViewComponent : ViewComponent
    {
        private readonly ILoaiSpRepository _loaiSp;
        public LoaiSPMenuViewComponent(ILoaiSpRepository loaiSpRepository)
        {
            _loaiSp = loaiSpRepository;
        }

        public IViewComponentResult Invoke()
        {
            var loaiSp = _loaiSp.GetAllLoaiSp().OrderBy(x => x.CategoryName);
            return View(loaiSp);
        }
    }
}
