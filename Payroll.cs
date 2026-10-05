/****************/

// Lê Thanh Mai 

// 202418940 

/****************/
using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Quản lý danh sách nhân sự cho một kỳ lương.
/// KHÔNG kế thừa Employee (Payroll không phải một loại nhân sự).
/// Mọi phép tổng hợp đều gọi CalculateGrossPay() qua kiểu Employee (đa hình),
/// không có if/else rẽ nhánh theo loại nhân viên.
/// </summary>
class Payroll
{
    string ky_luong;
    List<Employee> danh_sach = new List<Employee>();

    public Payroll(string ky_luong)
    {
        if (string.IsNullOrWhiteSpace(ky_luong))
            throw new ArgumentException("Kỳ lương không được rỗng");
        this.ky_luong = ky_luong;
    }

    public bool AddEmployee(Employee nv)
    {
        if (nv == null || danh_sach.Any(n => n.EmployeeId == nv.EmployeeId)) return false;
        danh_sach.Add(nv);
        return true;
    }

    public Employee FindEmployee(string ma) => danh_sach.FirstOrDefault(n => n.EmployeeId == ma);

    public double CalculateTotalPayroll() => danh_sach.Sum(n => n.CalculateGrossPay());

    public double CalculatePayrollByDepartment(string phong_ban) =>
        danh_sach.Where(n => n.Department == phong_ban).Sum(n => n.CalculateGrossPay());

    // Xử lý danh sách rỗng: trả về null thay vì ném lỗi
    public Employee FindHighestPaidEmployee() =>
        danh_sach.Count == 0 ? null : danh_sach.OrderByDescending(n => n.CalculateGrossPay()).First();

    public void DisplayPayroll()
    {
        Console.WriteLine($"=== Bảng lương kỳ {ky_luong} ===");
        if (danh_sach.Count == 0)
        {
            Console.WriteLine("  (Danh sách rỗng)");
            return;
        }
        foreach (var nv in danh_sach) nv.DisplayPayrollInfo(); // gọi đa hình
        Console.WriteLine($"  Tổng bảng lương: {CalculateTotalPayroll():N0}");
    }
}