/****************/

// Lê Thanh Mai 

// 202418940 

/****************/ 
using System;
using System.Text;

class Program
{
    static void Thu(string nhan, Action hanh_dong)
    {
        try { hanh_dong(); }
        catch (Exception loi) { Console.WriteLine($"  [{nhan}] Lỗi bắt được: {loi.Message}"); }
    }

    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        Console.WriteLine("=== Dữ liệu kiểm thử phần C ===");

        // E001 - lương cố định. AddBonus(amount): overload 1 chỉ có 1 tham số double
        var e001 = new SalariedEmployee("E001", "Nguyễn Minh An", "Đào tạo", 15_000_000, 2_000_000);
        e001.AddBonus(1_000_000);
        e001.DisplayPayrollInfo(); // mong đợi 18,000,000

        // E002 - theo giờ, không vượt ngưỡng. AddBonus(amount) - overload 1
        var e002 = new HourlyEmployee("E002", "Trần Thu Bình", "Hỗ trợ", 100_000, 150);
        e002.AddBonus(500_000);
        e002.DisplayPayrollInfo(); // mong đợi 15,500,000

        // E003 - theo giờ, vượt ngưỡng 160, không thưởng
        var e003 = new HourlyEmployee("E003", "Lê Hoàng Chi", "Hỗ trợ", 100_000, 170);
        e003.DisplayPayrollInfo(); // mong đợi 17,500,000

        // E004 - kinh doanh. AddBonus(rate, referenceAmount, reason) - overload 3
        var e004 = new SalesEmployee("E004", "Phạm Quốc Dũng", "Kinh doanh", 8_000_000, 200_000_000, 0.05);
        e004.AddBonus(0.02, 50_000_000, "Thưởng doanh số quý");
        e004.DisplayPayrollInfo(); // mong đợi 19,000,000

        var bang_luong = new Payroll("2026-09");
        bang_luong.AddEmployee(e001);
        bang_luong.AddEmployee(e002);
        bang_luong.AddEmployee(e003);
        bang_luong.AddEmployee(e004);

        Console.WriteLine();
        bang_luong.DisplayPayroll(); // mong đợi tổng 70,000,000

        Console.WriteLine($"Tổng phòng Hỗ trợ: {bang_luong.CalculatePayrollByDepartment("Hỗ trợ"):N0}"); // 33,000,000
        Console.WriteLine($"Người lương cao nhất: {bang_luong.FindHighestPaidEmployee().EmployeeId}"); // E004

        Console.WriteLine("\n=== Trường hợp biên và kiểm thử lỗi ===");

        // 1. Mã nhân sự rỗng
        Thu("1. Mã rỗng", () => new SalariedEmployee("", "Ten", "Phong", 1_000_000, 0));

        // 2. Họ tên rỗng
        Thu("2. Tên rỗng", () => new HourlyEmployee("E05", "", "Phong", 100_000, 100));

        // 3. Phòng ban rỗng (dùng constructor đầy đủ)
        Thu("3. Phòng ban rỗng", () => new SalesEmployee("E06", "Ten", "", 1_000_000, 0, 0.1));

        // 4. Thưởng âm/0 - AddBonus(amount) phải dương
        Thu("4. Thưởng không dương", () => e001.AddBonus(-500));

        // 5. AddBonus(amount, reason) với reason rỗng
        Thu("5. Lý do rỗng", () => e001.AddBonus(100_000, ""));

        // 6. AddBonus theo tỷ lệ với rate = 0 (phải > 0)
        Thu("6. Tỷ lệ = 0", () => e004.AddBonus(0, 1_000_000, "Thử"));

        // 7. AddBonus theo tỷ lệ với rate > 0.5
        Thu("7. Tỷ lệ > 0.5", () => e004.AddBonus(0.6, 1_000_000, "Thử"));

        // 8. AddBonus theo tỷ lệ với referenceAmount <= 0
        Thu("8. Giá trị tham chiếu <= 0", () => e004.AddBonus(0.1, 0, "Thử"));

        // 9. Số giờ làm vượt quá 250 (ngoài khoảng hợp lệ)
        Thu("9. Giờ làm > 250", () => new HourlyEmployee("E07", "Ten", "Phong", 100_000, 260));

        // 10. Số giờ làm âm
        Thu("10. Giờ làm âm", () => new HourlyEmployee("E08", "Ten", "Phong", 100_000, -5));

        // 11. Tỷ lệ hoa hồng vượt quá 0.3
        Thu("11. Hoa hồng > 0.3", () => new SalesEmployee("E09", "Ten", "Phong", 1_000_000, 0, 0.5));

        // 12. Lương tháng âm
        Thu("12. Lương âm", () => new SalariedEmployee("E10", "Ten", "Phong", -1_000_000, 0));

        // 13. Thêm nhân sự trùng mã vào Payroll
        Console.WriteLine("13. Thêm trùng mã E001: " + bang_luong.AddEmployee(
            new SalariedEmployee("E001", "Người khác", "Khác", 1_000_000, 0)));

        // 14. Tìm nhân sự không tồn tại
        Console.WriteLine("14. Tìm mã không tồn tại: " + (bang_luong.FindEmployee("E999") == null ? "null" : "tìm thấy"));

        // 15. Payroll rỗng
        var bang_luong_rong = new Payroll("2026-10");
        Console.WriteLine("15. Người lương cao nhất (rỗng): " +
            (bang_luong_rong.FindHighestPaidEmployee() == null ? "null" : "có"));
        bang_luong_rong.DisplayPayroll();

        // 16. Cập nhật doanh số âm
        Thu("16. Doanh số âm", () => e004.UpdateSalesRevenue(-1));

        Console.WriteLine("\n=== Đặt lại thưởng khi sang kỳ lương mới ===");
        e001.ResetBonus();
        Console.WriteLine($"Thưởng của E001 sau khi reset: {e001.MonthlyBonus:N0}");
    }
}