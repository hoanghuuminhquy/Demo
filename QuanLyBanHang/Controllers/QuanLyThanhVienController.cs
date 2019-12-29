using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using QuanLyBanHang.Models;

namespace QuanLyBanHang.Controllers
{
    public class QuanLyThanhVienController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        [Authorize(Roles = "QLThanhVien")]
        // GET: QuanLyThanhVien
        public ActionResult Index()
        {
            return View(db.ThanhViens.OrderByDescending(n => n.MaThanhVien));
        }
    }
}