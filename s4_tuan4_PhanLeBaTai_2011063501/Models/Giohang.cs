using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace s4_tuan4_PhanLeBaTai_2011063501.Models
{
    public class Giohang
    {
        myDataDataContext data = new myDataDataContext();
        public int masach { get; set; }
        [Display(Name = "Tên Sách")]
        public String tensach { get; set; }
        [Display(Name = "Ảnh bìa")]
        public String hinh { get; set; }
        [Display(Name = "Giá bán")]

        public Double giaban { get; set; }
        [Display(Name = "Số lượng")]
        public int isoluong { get; set; }
        public Double dThanhTien { get { return isoluong * giaban; } }
        public Giohang(int id)
        {
            masach = id;
            Sach sach = data.Saches.Single(n => n.masach == masach);
            tensach = sach.tensach;
            hinh = sach.hinh;
            giaban = double.Parse(sach.giaban.ToString());
            isoluong = 1;
        }
    }
}