//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebBanDT.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class HoaDon
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoaDon()
        {
            this.ThanhToan = new HashSet<ThanhToan>();
        }
    
        public int MaHD { get; set; }
        public Nullable<System.DateTime> NgayLapHD { get; set; }
        public Nullable<int> ThanhTien { get; set; }
        public Nullable<int> MaDH { get; set; }
        public Nullable<int> MaHT { get; set; }
    
        public virtual DonHang DonHang { get; set; }
        public virtual HinhThucTT HinhThucTT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ThanhToan> ThanhToan { get; set; }
    }
}
