/****************/

// Lê Thanh Mai 

// 202418940 

/****************/
using System;

/// <summary>Nhân viên hưởng lương cố định hằng tháng.</summary>
class SalariedEmployee : Employee
{
    double luong_thang, phu_cap;

    // Constructor rút gọn: dùng giá trị mặc định cho phòng ban và phụ cấp
    public SalariedEmployee(string ma, string ten, double luong_thang)
        : this(ma, ten, "Unassigned", luong_thang, 0) { }

    // Constructor đầy đủ: ủy quyền (base) phần kiểm tra chung lên Employee
    public SalariedEmployee(string ma, string ten, string phong_ban, double luong_thang, double phu_cap)
        : base(ma, ten, phong_ban)
    {
        if (luong_thang < 0) throw new ArgumentException("Lương tháng không được âm");
        if (phu_cap < 0) throw new ArgumentException("Phụ cấp không được âm");
        this.luong_thang = luong_thang;
        this.phu_cap = phu_cap;
    }

    public override double CalculateGrossPay() => luong_thang + phu_cap + thuong_thang;

    public override string GetEmployeeType() => "Nhân viên lương cố định";

    public override void DisplayPayrollInfo() =>
        Console.WriteLine($"[{GetEmployeeType()}] {ma_nhan_su} | {ho_ten} | {phong_ban} | " +
                           $"Lương: {luong_thang:N0} | Phụ cấp: {phu_cap:N0} | " +
                           $"Thưởng: {thuong_thang:N0} | Thực lãnh: {CalculateGrossPay():N0}");
}