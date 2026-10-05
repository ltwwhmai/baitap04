/****************/

// Lê Thanh Mai 

// 202418940 

/****************/
using System;

/// <summary>Nhân viên hưởng lương theo giờ làm, có tính giờ tăng ca sau ngưỡng 160.</summary>
class HourlyEmployee : Employee
{
    const double nguong_gio_thuong = 160;
    const double he_so_tang_ca = 1.5;

    double don_gia_gio, so_gio_lam;

    public HourlyEmployee(string ma, string ten, double don_gia_gio, double so_gio_lam)
        : this(ma, ten, "Unassigned", don_gia_gio, so_gio_lam) { }

    public HourlyEmployee(string ma, string ten, string phong_ban, double don_gia_gio, double so_gio_lam)
        : base(ma, ten, phong_ban)
    {
        if (don_gia_gio < 0) throw new ArgumentException("Đơn giá giờ không được âm");
        if (so_gio_lam < 0 || so_gio_lam > 250) throw new ArgumentException("Số giờ làm phải từ 0 đến 250");
        this.don_gia_gio = don_gia_gio;
        this.so_gio_lam = so_gio_lam;
    }

    // Tính từ trạng thái hiện có, không lưu riêng tiền làm thêm thành một trường
    double LuongCoBan =>
        so_gio_lam <= nguong_gio_thuong
            ? so_gio_lam * don_gia_gio
            : nguong_gio_thuong * don_gia_gio + (so_gio_lam - nguong_gio_thuong) * don_gia_gio * he_so_tang_ca;

    public override double CalculateGrossPay() => LuongCoBan + thuong_thang;

    public override string GetEmployeeType() => "Nhân viên theo giờ";

    public override void DisplayPayrollInfo() =>
        Console.WriteLine($"[{GetEmployeeType()}] {ma_nhan_su} | {ho_ten} | {phong_ban} | " +
                           $"Giờ làm: {so_gio_lam} | Lương cơ bản: {LuongCoBan:N0} | " +
                           $"Thưởng: {thuong_thang:N0} | Thực lãnh: {CalculateGrossPay():N0}");
}