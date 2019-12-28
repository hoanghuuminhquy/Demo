using QuanLyBanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyBanHang.Controllers
{
    public class ThongKeController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        // GET: ThongKe
        public ActionResult Index()
        {
            ViewBag.TongDoanhThu = ThongKeDoanhThu();
            ViewBag.TongDDH = ThongKeDonHang();
            ViewBag.TongThanhVien = TongThanhVien();
            ViewBag.TongSP = TongSanPham();
            //ViewBag.TongDoanhThuTheoThang = ThongKeDoanhThuTheoThang(12, 2017);
            return View();
        }

        public decimal? ThongKeDoanhThu()
        {
            decimal? TongDoanhThu = db.ChiTietDonDatHangs.Sum(n => n.DonGia * n.SoLuong);
            return TongDoanhThu;
        }

        public double ThongKeDonHang()
        {
            //Đếm đơn đặt hàng
            double sl = db.DonDatHangs.Count();
            return sl;
        }

        public double TongThanhVien()
        {
            double slTV = db.ThanhViens.Count();
            return slTV;
        }

        public double TongSanPham()
        {
            double slsp = db.SanPhams.Count();
            return slsp;
        }
    }
}