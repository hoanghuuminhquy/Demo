using CaptchaMvc.HtmlHelpers;
using QuanLyBanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyBanHang.Controllers
{
    public class HomeController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        public ActionResult Index()
        {
            ViewBag.x = db.LoaiSanPhams;
            return View(db.SanPhams.Where(n => n.DaXoa == false));
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DangKy(ThanhVien tv, FormCollection f)
        {
            
            //Kiểm tra Captcha hợp lệ
            if (this.IsCaptchaValid("Captcha is not valid"))
            {
                if (ModelState.IsValid)
                {
                    ViewBag.ThongBao = "Thêm thành công";
                    
                    db.ThanhViens.Add(tv);
                    db.SaveChanges();

                }
                else
                {
                    ViewBag.ThongBao = "Thêm thất bại";
                    
                }

                return View();
            }
            ViewBag.ThongBao = "Sai mã Captcha";
            return View();
        }

        [HttpGet]
        public ActionResult DangNhap()
        {
           
            return View();
        }

        [HttpPost]
        public ActionResult DangNhap(ThanhVien tv)
        {
            string Taikhoan = tv.TaiKhoan.ToString();
            string Matkhau = tv.MatKhau.ToString();
            tv = db.ThanhViens.SingleOrDefault( n => n.TaiKhoan==Taikhoan && n.MatKhau == Matkhau);
            if(tv != null)
            {
                Session["Taikhoan"] = tv;
                //return Content("<script>window.location.reload();</script>");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.loi = "Tài khoản hoặc mật khẩu không đúng!";
                return View();
            }
            //return RedirectToAction("Index", "Home", "Tài khoản hoặc mật khẩu không đúng.");
            //return RedirectToAction("DangNhap", "Home", "Tài khoản hoặc mật khẩu không đúng.");

        }

        public ActionResult DangXuat()
        {
            Session["Taikhoan"] = null;
            return RedirectToAction("Index");
        }
    }
}