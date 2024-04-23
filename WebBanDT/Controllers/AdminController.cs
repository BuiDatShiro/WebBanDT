using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using WebBanDT.Models;

namespace WebBanDT.Controllers
{
    public class AdminController : Controller
    {
        MyData data = new MyData();
        
        public ActionResult Index()
        {
            KhachHang kh = (KhachHang)Session["TaiKhoan"];
            if(kh == null)  return RedirectToAction("DangNhap","User");
            var item = from sp in data.SanPham select sp;
            return View(item);
        }

        public ActionResult ThemSP()
        {
            ViewBag.MaDM = new SelectList(data.DanhMuc.ToList(), "MaDM", "TenDM");
            return View();
        }
        [HttpPost]
        public ActionResult ThemSP(FormCollection collection, SanPham sp)
        {

            var S_TenSP = collection["TenSP"];
            var S_GiaSP = Convert.ToDecimal(collection["GiaSP"]);
            var S_Slgton = Convert.ToInt32(collection["Slgton"]);
            var S_HinhSP = collection["HinhSP"];
            var S_NgayNhap = Convert.ToDateTime(collection["NgayNhap"]);
            if (string.IsNullOrEmpty(S_TenSP))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                sp.TenSP = S_TenSP.ToString();
                sp.HinhSP = S_HinhSP.ToString();
                sp.GiaSP = (int?)S_GiaSP;
                sp.NgayNhap = S_NgayNhap;
                sp.Slgton = S_Slgton;
                data.SanPham.Add(sp);
                data.SaveChanges();
                return RedirectToAction("Index");
            }
            return this.ThemSP();
        }
        public ActionResult SuaSP(int? id)
        {
            ViewBag.MaDM = new SelectList(data.DanhMuc.ToList(), "MaDM", "TenDM");
            var S_sanpham = data.SanPham.First(m => m.MaSP == id);
            return View(S_sanpham);
        }
        [HttpPost]
        public ActionResult SuaSP(int id, FormCollection collection)
        {
            var S_sanpham = data.SanPham.First(m => m.MaSP == id);
            var S_TenSP = collection["TenSP"];
            var S_HinhSP = collection["HinhSP"];
            var S_GiaSP = Convert.ToDecimal(collection["GiaSP"]);
            var S_NgayNhap = Convert.ToDateTime(collection["NgayNhap"]);
            var S_Slgton = Convert.ToInt32(collection["SlgTon"]);
            S_sanpham.MaSP = id;
            if (string.IsNullOrEmpty(S_TenSP))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                S_sanpham.TenSP = S_TenSP;
                S_sanpham.HinhSP = S_HinhSP;
                S_sanpham.GiaSP = (int?)S_GiaSP;
                S_sanpham.NgayNhap = S_NgayNhap;
                S_sanpham.Slgton = S_Slgton;
                UpdateModel(S_sanpham);
                data.SaveChanges();
                return RedirectToAction("Index");
            }
            return this.SuaSP(id);

        }
        public string ProcessUpload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return "";
            }
            file.SaveAs(Server.MapPath("~/Content/images/" + file.FileName));
            return "/Content/images/" + file.FileName;
        }
        public ActionResult XoaSP(int? id)
        {
            var D_sp = data.SanPham.First(m => m.MaSP == id);
            return View(D_sp);
        }
        [HttpPost]
        public ActionResult XoaSP(int id, FormCollection collection)
        {
            var D_sp = data.SanPham.Where(m => m.MaSP == id).First();
            data.SanPham.Remove(D_sp);
            data.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult ThongTinSP(int? id)
        {
            var item = data.SanPham.Where(m => m.MaSP == id && m.Slgton > 0).First();
            return View(item);
        }
    }

}