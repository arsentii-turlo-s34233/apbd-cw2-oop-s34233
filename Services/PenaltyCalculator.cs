namespace apbd_cw2_oop_s34233.Services;

public static class PenaltyCalculator
{
    private const decimal PenaltyPerDay = 5m;

    public static decimal CalculatePenalty(DateTime dueDate, DateTime returnDate)
    {
        if (returnDate <= dueDate)
            return 0m;
        int daysLate = (int)(returnDate - dueDate).TotalDays;
        return daysLate * PenaltyPerDay;
    }
}