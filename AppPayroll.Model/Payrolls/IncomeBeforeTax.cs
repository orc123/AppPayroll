namespace AppPayroll.Model.Payrolls;

public class IncomeBeforeTax
{
    public double IncomeBeforeTaxProterty { get; set; }
    // Giảm trừ gia cảnh bản thân 
    public int PersonalSituation { get; set; }

    // Giảm trừ gia cảnh người phụ thuộc
    public int DependentsFamily { get; set; }
}
