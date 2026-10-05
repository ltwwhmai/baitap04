/****************/

// Lê Thanh Mai 

// 202418940 

/****************/
using System;
using System.Collections.Generic;

/// <summary>
/// Lớp cơ sở TRỪU TƯỢNG (abstract) cho mọi loại nhân sự.
/// Abstract vì "nhân viên chung" không có công thức tính lương cụ thể —
/// bắt buộc phải tạo đối tượng qua một lớp dẫn xuất thực sự (xem answers.md câu A.8.4).
/// </summary>
abstract class Employee
{
    protected string ma_nhan_su, ho_ten, phong_ban;
    protected double thuong_thang;
    protected List<BonusRecord> lich_su_thuong = new List<BonusRecord>();

    // Constructor rút gọn: phòng ban mặc định "Unassigned"
    public Employee(string ma_nhan_su, string ho_ten) : this(ma_nhan_su, ho_ten, "Unassigned") { }

    // Constructor đầy đủ: nơi duy nhất kiểm tra dữ liệu
    public Employee(string ma_nhan_su, string ho_ten, string phong_ban)
    {
        if (string.IsNullOrWhiteSpace(ma_nhan_su))
            throw new ArgumentException("Mã nhân sự không được rỗng");
        if (string.IsNullOrWhiteSpace(ho_ten))
            throw new ArgumentException("Họ tên không được rỗng");
        if (string.IsNullOrWhiteSpace(phong_ban))
            throw new ArgumentException("Phòng ban không được rỗng");

        this.ma_nhan_su = ma_nhan_su;
        this.ho_ten = ho_ten;
        this.phong_ban = phong_ban;
        this.thuong_thang = 0;
    }

    public string EmployeeId => ma_nhan_su;
    public string FullName => ho_ten;
    public string Department => phong_ban;
    public double MonthlyBonus => thuong_thang;

    // ----- Nạp chồng addBonus (3 phiên bản) -----

    // 1. Thưởng cố định
    public void AddBonus(double amount) => AddBonus(amount, (string)null);

    // 2. Thưởng cố định kèm lý do
    public void AddBonus(double amount, string reason)
    {
        if (amount <= 0)
            throw new ArgumentException("Số tiền thưởng phải dương");
        if (reason != null && string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("Lý do không được rỗng nếu có truyền vào");

        thuong_thang += amount;
        lich_su_thuong.Add(new BonusRecord(amount, reason ?? "(không ghi lý do)"));
    }

    // 3. Thưởng theo tỷ lệ của một giá trị tham chiếu, kèm lý do
    public void AddBonus(double rate, double referenceAmount, string reason)
    {
        if (rate <= 0 || rate > 0.5)
            throw new ArgumentException("Tỷ lệ thưởng phải trong khoảng (0, 0.5]");
        if (referenceAmount <= 0)
            throw new ArgumentException("Giá trị tham chiếu phải dương");
        if (string.IsNullOrWhiteSpace(reason))
            throw new ArgumentException("Lý do không được rỗng");

        double amount = rate * referenceAmount;
        thuong_thang += amount;
        lich_su_thuong.Add(new BonusRecord(amount, reason));
    }

    // Đặt lại thưởng khi bắt đầu kỳ lương mới
    public virtual void ResetBonus()
    {
        thuong_thang = 0;
        lich_su_thuong.Clear();
    }

    // ----- Hành vi buộc lớp con phải GHI ĐÈ (override, khác với nạp chồng ở trên) -----
    public abstract double CalculateGrossPay();
    public abstract string GetEmployeeType();

    public virtual void DisplayPayrollInfo() =>
        Console.WriteLine($"[{GetEmployeeType()}] {ma_nhan_su} | {ho_ten} | {phong_ban} | " +
                           $"Thưởng: {thuong_thang:N0} | Thực lãnh: {CalculateGrossPay():N0}");
}