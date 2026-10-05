/****************/

// Lê Thanh Mai 

// 202418940 

/****************/
/// <summary>
/// Một lần ghi nhận thưởng: số tiền và lý do.
/// Dùng List&lt;BonusRecord&gt; trong Employee thay vì 2 mảng song song (amounts[], reasons[]).
/// </summary>
class BonusRecord
{
    public double Amount { get; }
    public string Reason { get; }

    public BonusRecord(double amount, string reason)
    {
        Amount = amount;
        Reason = reason;
    }
}