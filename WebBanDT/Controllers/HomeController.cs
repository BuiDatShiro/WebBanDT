using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI;
using WebBanDT.Models;

namespace WebBanDT.Controllers
{
    public class HomeController : Controller
    {
        MyData data = new MyData();
        public ActionResult Index(int? page)
        {
            if (page == null) page = 1;
            var item = (from hh in data.SanPhams select hh).Where(m => m.Slgton > 0).OrderBy(m => m.MaSP);
            int pageSize = 4;
            int pageNum = page ?? 1;
            return View(item.ToPagedList(pageNum, pageSize));
        }

        public ActionResult Search(int? page, string Search)
        {
            ViewBag.KeyWord = Search;
            if (page == null) page = 1;
            var item = (from hh in data.SanPhams select hh).Where(m => m.Slgton > 0 && m.TenSP.Contains(Search)).OrderBy(m => m.MaSP); ;
            int pageSize = 4;
            int pageNum = page ?? 1;
            if (item.Count() < 1) ViewBag.NotExists = "Không có sản phẩm nào";
            return View(item.ToPagedList(pageNum, pageSize));
        }

        public ActionResult ThongTinSP(int? id)
        {
            var item = data.SanPhams.Where(m => m.MaSP == id && m.Slgton>0).First();
            return View(item);
        }

        public ActionResult DanhMuc()
        {
            var item = from dm in data.DanhMucs select dm;
            return View(item);
        }
        
        public ActionResult DanhMucSP(int? id,int? page)
        {
            if (page == null) page = 1;
            var item = data.SanPhams.Where(m=>m.MaDM==id && m.Slgton > 0).OrderBy(m=>m.MaSP);
            int pageSize = 4;
            int pageNum = page ?? 1;
            return View(item.ToPagedList(pageNum, pageSize));
        }

        public ActionResult XemLichSuDonHang()
        {
            KhachHang kh = (KhachHang)Session["TaiKhoan"];
            if (kh == null) return RedirectToAction("DangNhap","User");
            if(kh.MaQuyen == 2)
            {
                var lsdh = from dh in data.DonHangs select dh;
                return View(lsdh);
            }
            else
            {
                var lsdh = data.DonHangs.Where(m => m.KhachHang.makh == kh.makh);
                return View(lsdh);
            }
        }

        public ActionResult XemCTDH(int? id)
        {
            KhachHang kh = (KhachHang)Session["TaiKhoan"];
            if (kh == null) return RedirectToAction("DangNhap", "User");

            var ctdh = (from ct in data.ChiTietDonHangs where ct.madon == id select ct);
            return View(ctdh);
        }

        public ActionResult XacnhanDonhangThanhCong(int? id)
        {
            DonHang dh = data.DonHangs.Where(d=>d.madon == id).First();
            dh.giaohang = Convert.ToByte(1);
            data.DonHangs.AddOrUpdate(dh);
            data.SaveChanges();
            return RedirectToAction("XemLichSuDonHang");
        }

        public ActionResult XacnhanGiaohangThanhCong(int? id)
        {
            DonHang dh = data.DonHangs.Where(d => d.madon == id).First();
            dh.giaohang = Convert.ToByte(2);
            data.DonHangs.AddOrUpdate(dh); ;
            data.SaveChanges();
            return RedirectToAction("XemLichSuDonHang");

        }

        
        public ActionResult XacnhanHuyDonhang(int? id)
        {
            DonHang dh = data.DonHangs.Where(d => d.madon == id).First();
            dh.giaohang = Convert.ToByte(0);
            data.DonHangs.AddOrUpdate(dh);
            data.SaveChanges();
            return RedirectToAction("XemLichSuDonHang");
            
        }


    }


}