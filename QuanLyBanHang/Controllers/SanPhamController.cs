using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using QuanLyBanHang.Models;

namespace QuanLyBanHang.Controllers
{
    public class SanPhamController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        // GET: SanPham
        public ActionResult Index()
        {
            return View(db.SanPhams.Where(n => n.DaXoa == false).OrderByDescending(n => n.MaSP));
        }

        public ActionResult Chitietsp(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Nếu không thì truy xuất csdl lấy ra sản phẩm tương ứng id
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == id && n.DaXoa == false);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }

        public ActionResult Danhmucsp(int? MaLoaiSP, int? page)
        {
            ViewBag.MaLoaiSP = MaLoaiSP;
            var lstSP = db.SanPhams.Where(n => n.DaXoa == false && n.MaLoaiSP == MaLoaiSP);
            if (lstSP.Count() == 0)
            {
                return HttpNotFound();
            }
            //Thực hiện chức năng phân trang
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            //Tạo biến số sp trên trang
            int PageSize = 8;
            //Tạo biến thứ 2 : Số trang hiện tại
            int PageNumber = (page ?? 1);
            ViewBag.MaLoaiSP = MaLoaiSP;
            return View(lstSP.OrderBy(n => n.MaSP).ToPagedList(PageNumber, PageSize));
        }
        public ActionResult SanPham(int? MaLoaiSP, int? page)
        {
            ViewBag.MaLoaiSP = MaLoaiSP;
            var lstSP = db.SanPhams.Where(n => n.DaXoa == false && n.MaLoaiSP == MaLoaiSP);
            if (lstSP.Count() == 0)
            {
                return HttpNotFound();
            }
            //Thực hiện chức năng phân trang
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            //Tạo biến số sp trên trang
            int PageSize = 8;
            //Tạo biến thứ 2 : Số trang hiện tại
            int PageNumber = (page ?? 1);
            ViewBag.MaLoaiSP = MaLoaiSP;
            return View(lstSP.OrderBy(n => n.MaSP).ToPagedList(PageNumber, PageSize));
        }
    }
}