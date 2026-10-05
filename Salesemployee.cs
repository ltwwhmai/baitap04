/****************/

// Lê Thanh Mai 

// 202418940 

/****************/
using System;

/// <summary>Nhân viên kinh doanh: lương cơ bản + hoa hồng theo doanh số.</summary>
class SalesEmployee : Employee
{
    double luong_co_ban, doanh_so, ty_le_hoa_hong;

    public SalesEmployee(string ma, string ten, double luong_co_ban)
        : this(ma, ten, "Unassigned", luong_co_ban, 0, 0) { }

    public SalesEmployee(string ma, string ten, string phong_ban, double luong_co_ban, double doanh_so, double ty_le_hoa_hong)
        : base(ma, ten, phong_ban)
    {
        if (luong_co_ban < 0) throw new ArgumentException("Lương cơ bản không được âm");
        if (doanh_so < 0) throw new ArgumentException("Doanh số không được âm");
        if (ty_le_hoa_hong < 0 || ty_le_hoa_hong > 0.3)
            throw new ArgumentException("Tỷ lệ hoa hồng phải từ 0 đến 0.3");
        this.luong_co_ban = luong_co_ban;
        this.doanh_so = doanh_so;
        this.ty_le_hoa_hong = ty_le_hoa_hong;
    }

    // Cập nhật doanh số có kiểm soát, không cho gán trực tiếp từ ngoài
    public void UpdateSalesRevenue(double doanh_so_moi)
    {
        if (doanh_so_moi < 0) throw new ArgumentException("Doanh số không được âm");
        doanh_so = doanh_so_moi;
    }

    public override double CalculateGrossPay() => luong_co_ban + doanh_so * ty_le_hoa_hong + thuong_thang;

    public override string GetEmployeeType() => "Nhân viên kinh doanh";

    public override void DisplayPayrollInfo() =>
        Console.WriteLine($"[{GetEmployeeType()}] {ma_nhan_su} | {ho_ten} | {phong_ban} | " +
                           $"Lương CB: {luong_co_ban:N0} | Doanh số: {doanh_so:N0} | " +
                           $"Hoa hồng: {ty_le_hoa_hong:P0} | Thưởng: {thuong_thang:N0} | Thực lãnh: {CalculateGrossPay():N0}");
}