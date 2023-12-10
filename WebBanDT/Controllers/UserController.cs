using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebBanDT.Models;

namespace WebBanDT.Controllers
{
    public class UserController : Controller
    {
        MyData data = new MyData();
        // GET: User
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(FormCollection collection, KhachHang kh)
        {
            var regexPassword = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            var hoten = collection["hoten"];
            var tendangnhap = collection["tendangnhap"];
            var matkhau = collection["matkhau"];
            var MatKhauXacNhan = collection["MatKhauXacNhan"];
            var email = collection["email"];
            var diachi = collection["diachi"];
            var dienthoai = collection["dienthoai"];
            var ngaysinh = string.Format("{0:d/M/yyyy}", collection["ngaysinh"]);
            if(dienthoai.Length != 10 && dienthoai.Length != 11)
            {
                ViewData["SDT"] = "Số điện thoại phải có 10 hoặc 11 chữ số";
            }
            if (!regexPassword.IsMatch(matkhau))
            {
                ViewData["matkhau"] = "Mật khẩu không hợp lệ!";
            }
            else
            {
                if (String.IsNullOrEmpty(MatKhauXacNhan))
                {
                    ViewData["NhapMKXN"] = "Phải nhập mật khẩu xác nhận!";
                }
                else
                {
                    if (!matkhau.Equals(MatKhauXacNhan))
                    {
                        ViewData["MatKhauGiongNhau"] = "Mật khẩu và mật khầu xác nhận phài giống nhau";
                    }
                    else
                    {
                        kh.hoten = hoten;
                        kh.tendangnhap = tendangnhap;
                        kh.matkhau = matkhau;
                        kh.email = email;
                        kh.diachi = diachi;
                        kh.dienthoai = dienthoai;
                        kh.ngaysinh = DateTime.Parse(ngaysinh);
                        kh.MaQuyen = 1;
                        data.KhachHangs.Add(kh);
                        data.SaveChanges();
                        return RedirectToAction("DangNhap");
                    }
                }
            }
            return this.DangKy();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var tendangnhap = collection["tendangnhap"];
            var matkhau = collection["matkhau"];
            KhachHang kh = data.KhachHangs.SingleOrDefault(n => n.tendangnhap == tendangnhap && n.matkhau == matkhau);
            if (kh != null)
            {
                Session["TaiKhoan"] = kh;
                if (!kh.Quyen.TenQuyen.Equals("Admin"))
                {
                    ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                    return RedirectToAction("Index", "Admin");
                }
            }
                
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khấu không đúng";
                return this.DangNhap();
            }
        }
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
        
        public ActionResult QuenMK()
        {
            return View();
        }
        [HttpPost]
        public ActionResult QuenMK(FormCollection collection)
        {
            var tendangnhap = collection["tendangnhap"];
            KhachHang kh = data.KhachHangs.SingleOrDefault(n => n.tendangnhap == tendangnhap);
            if (kh != null)
            {
                Session["TKXN"] = kh;
                return RedirectToAction("DoiMK");
            }
            ViewBag.TaiKhoan = "Tài khoản không tồn tại";
            return View();
        }

        public ActionResult DoiMK()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DoiMK(FormCollection collection)
        {
            KhachHang kh = (KhachHang)Session["TKXN"];
            var regexPassword = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
            var matkhau = collection["matkhau"];
            var MatKhauXacNhan = collection["MatKhauXacNhan"];
            if (!regexPassword.IsMatch(matkhau))
            {
                ViewData["matkhau"] = "Mật khẩu không hợp lệ! Mật khẩu phải dài tối thiểu 8 kí tự, có 1 chữ thường, 1 chữ hoa, 1 số và 1 kí tự đặc biết";
            }
            else
            {
                if (String.IsNullOrEmpty(MatKhauXacNhan))
                {
                    ViewData["NhapMKXN"] = "Phải nhập mật khẩu xác nhận!";
                }
                else
                {
                    if (!matkhau.Equals(MatKhauXacNhan))
                    {
                        ViewData["MatKhauGiongNhau"] = "Mật khẩu và mật khầu xác nhận phài giống nhau";
                    }
                    else
                    {
                        kh.matkhau = matkhau;
                        data.KhachHangs.AddOrUpdate(kh);
                        data.SaveChanges();
                        ViewData["XacNhanDoiMK"] = "Mật khẩu đổi thành công.";
                        return RedirectToAction("DangNhap");
                    }
                }
            }
            return this.DoiMK();
        }
    }
}