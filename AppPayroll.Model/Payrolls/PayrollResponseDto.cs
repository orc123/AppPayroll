namespace AppPayroll.Model.Payrolls;

public class PayrollResponseDto
{
    // Lương GROSS	
    public SalaryGross SalaryGross { get; set; }

    // Thu nhập trước thuế
    public IncomeBeforeTax IncomeBeforeTax { get; set; }

    // Thu nhập chịu thuế	
    public IncomeTaxes IncomeTaxes { get; set; }

    // Lương NET
    public double NetSalary { get; set; }
}