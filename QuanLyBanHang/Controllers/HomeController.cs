using CaptchaMvc.HtmlHelpers;
using QuanLyBanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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
        public ActionResult DangNhap(FormCollection f)
        {
            //string Taikhoan = tv.TaiKhoan.ToString();
            //string Matkhau = tv.MatKhau.ToString();
            //tv = db.ThanhViens.SingleOrDefault( n => n.TaiKhoan==Taikhoan && n.MatKhau == Matkhau);
            //if(tv != null)
            //{
            //    Session["Taikhoan"] = tv;
            //    //return Content("<script>window.location.reload();</script>");
            //    return RedirectToAction("Index", "Home");
            //}
            //else
            //{
            //    ViewBag.loi = "Tài khoản hoặc mật khẩu không đúng!";
            //    return View();
            //}
            //return RedirectToAction("Index", "Home", "Tài khoản hoặc mật khẩu không đúng.");
            //return RedirectToAction("DangNhap", "Home", "Tài khoản hoặc mật khẩu không đúng.");

            string taikhoan = f["TaiKhoan"].ToString();
            string matkhau = f["MatKhau"].ToString();
            ThanhVien tv = db.ThanhViens.SingleOrDefault(n => n.TaiKhoan == taikhoan && n.MatKhau == matkhau);
            if (tv != null)
            {
                //Láy ra List quyền của thành viên tương ứng với loại thành viên
                var lstQuyen = db.LoaiThanhVien_Quyen.Where(n => n.MaLoaiTV == tv.MaLoaiTV);
                //Duyệt list quyền
                string Quyen = "";
                foreach (var item in lstQuyen)
                {
                    Quyen += item.MaQuyen + ",";
                }
                // Cắt dấu ","
                Quyen = Quyen.Substring(0, Quyen.Length - 1);
                PhanQuyen(tv.TaiKhoan, Quyen);
                Session["TaiKhoan"] = tv;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.loi = "Tài khoản hoặc mật khẩu không đúng!";
                return View();
            }
        }
        public void PhanQuyen(string TaiKhoan, string Quyen)
        {
            FormsAuthentication.Initialize();
            var ticket = new FormsAuthenticationTicket(1,
                                          TaiKhoan, //user
                                          DateTime.Now, //Thời gian bắt đầu
                                          DateTime.Now.AddHours(3), //Thời gian kết thúc
                                          false, //Ghi nhớ?
                                          Quyen, // "DangKy,QuanLyDonHang,QuanLySanPham"
                                          FormsAuthentication.FormsCookiePath);

            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
            Response.Cookies.Add(cookie);
        }
        // Tạo trang ngăn chặn truy cập
        public ActionResult LoiPhanQuyen()
        {
            return View();
        }

        public ActionResult DangXuat()
        {
            Session["Taikhoan"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}