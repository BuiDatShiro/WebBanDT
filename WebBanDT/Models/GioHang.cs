using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace WebBanDT.Models
{
    public class GioHang
    {
        MyData data = new MyData();
        public int masp { get; set; }
        [Display(Name = "Tên Sản Phẩm")]
        public string tensp { get; set; }
        [Display(Name = "Ảnh bìa")]
        public string hinh { get; set; }
        [Display(Name = "Giá bán")]
        public Double giaban { get; set; }
        [Display(Name = "số lượng")]
        public int isoluong { get; set; }
        [Display(Name = "Thành tiền")]
        public Double dThanhtien
        {
            get { return isoluong * giaban; }
        }
        public GioHang(int id)
        {
            masp = id;
            SanPham sp = data.SanPham.Single(n => n.MaSP == masp);
            tensp = sp.TenSP;
            hinh = sp.HinhSP;
            giaban = double.Parse(sp.GiaSP.ToString());
            isoluong = 1;
        }

    }
}