using s4_tuan4_PhanLeBaTai_2011063501.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace s4_tuan4_PhanLeBaTai_2011063501.Controllers
{
    public class GioHangController : Controller
    {

        myDataDataContext data = new myDataDataContext();
        public List<Giohang> LayGioHang()
        {
            List<Giohang> lstGiohang = Session["GioHang"] as List<Giohang>;
            if(lstGiohang == null)
            {
                lstGiohang = new List<Giohang>();
                Session["GioHang"] = lstGiohang;
            }
            return lstGiohang;
        }

        // GET: GioHang
        public ActionResult ThemGioHang(int id,string strURL)
        {
            List<Giohang> lstGiohang= LayGioHang();
            Giohang sanpham = lstGiohang.Find(n => n.masach == id);
            if(sanpham == null)
            {
                sanpham = new Giohang(id);
                lstGiohang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.isoluong++;
                return Redirect(strURL);
            }
            return View();
        }

        private int tongsoluong()
        {
            int tsl = 0;
            List<Giohang> lstgiohang = Session["Giohang"] as List<Giohang>;
            if (lstgiohang != null)
            {
                tsl = lstgiohang.Sum(n => n.isoluong);
            }
            return tsl;
        }
        private int tongsoluongsanpham()
        {
            int tsl = 0;
            List<Giohang> lstgiohang = Session["Giohang"] as List<Giohang>;
            if (lstgiohang != null)
            {
                tsl = lstgiohang.Count;
            }
            return tsl;
        }
        private double tongtien()
        {
            double tt = 0;
            List<Giohang> lstgiohang = Session["Giohang"] as List<Giohang>;
            if (lstgiohang != null)
            {
                tt = lstgiohang.Sum(n => n.dThanhTien);
            }
            return tt;
        }
        public ActionResult giohang()
        {
            List<Giohang> lstgiohang = LayGioHang();
            ViewBag.tongsoluong = tongsoluong();
            ViewBag.tongtien = tongtien();
            ViewBag.tongsoluongsanpham = tongsoluongsanpham();
            return View(lstgiohang);
        }
        public ActionResult giohangpartical()
        {
            ViewBag.tongsoluong = tongsoluong();
            ViewBag.tongtien = tongtien();
            ViewBag.tongsoluongsanpham = tongsoluongsanpham();
            return PartialView();
        }
        public ActionResult xoagiohang(int id)
        {
            List<Giohang> lstgiohang = LayGioHang();
            Giohang sanpham = lstgiohang.SingleOrDefault(n => n.masach == id);
            if (sanpham != null)
            {
                lstgiohang.RemoveAll(n => n.masach == id);
                return RedirectToAction("Giohang");
            }
            return RedirectToAction("Giohang");
        }
        public ActionResult capnhatgiohang(int id, FormCollection cls)
        {
            List<Giohang> lstgiohang = LayGioHang();
            Giohang sanpham = lstgiohang.SingleOrDefault(n => n.masach == id);
            if (sanpham != null)
            {
                sanpham.isoluong = int.Parse(cls["txtSolg"].ToString());

            }
            return RedirectToAction("Giohang");
        }
        public ActionResult Xoatatcagiohang()
        {
            List<Giohang> lstgiohang = LayGioHang();
            lstgiohang.Clear();
            return RedirectToAction("Giohang");
        }
        [HttpGet]
        public ActionResult DatHang()
        {
            if (Session["TaiKhoan"] == null || Session["TaiKhoan"].ToString() == "")
            {
                return RedirectToAction("DangNhap", "NguoiDung");
            }
            if(Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Sach");
            }
            List<Giohang> lstgiohang = LayGioHang();
            ViewBag.tongsoluong = tongsoluong();
            ViewBag.tongtien = tongtien();
            ViewBag.tongsoluongsanpham = tongsoluongsanpham();
            return View(lstgiohang);
        }
        public ActionResult DatHang(FormCollection collection)
        {
            DonHang dh = new DonHang();
            KhachHang kh = (KhachHang)Session["TaiKhoan"];
            Sach s = new Sach();

            List<Giohang> gh = LayGioHang();
            var ngaygiao = String.Format("{0:MM/dd/yyyy}", collection["NgayGiao"]);
            dh.makh = kh.makh;
            dh.ngaydat = DateTime.Now;
            dh.ngaygiao = DateTime.Parse(ngaygiao);
            dh.giaohang = false;
            dh.thanhtoan = false;
            data.DonHangs.InsertOnSubmit(dh);
            data.SubmitChanges();
            foreach(var item in gh)
            {
                ChiTietDonHang ctdh =  new ChiTietDonHang();
                ctdh.madon = dh.madon;
                ctdh.masach = item.masach;
                ctdh.soluong = item.isoluong;
                ctdh.gia=(decimal)item.giaban;
                s = data.Saches.Single(n => n.masach == item.masach);
                s.soluongton -= ctdh.soluong;
                data.SubmitChanges();
                data.ChiTietDonHangs.InsertOnSubmit(ctdh);

            }
            data.SubmitChanges();
            Session["GioHang"] = null;
            return RedirectToAction("XacNhanDonHang", "GioHang");
        }
        public ActionResult XacNhanDonHang()
        {
            return View();
        }
    }

}