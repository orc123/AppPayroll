namespace AppPayroll.Model.Payrolls;

public class SalaryGross
{
    public double SalaryGrossProterty { get; set; }
    // Bảo hiểm xã hội
    public double SocialInsurance { get; set; }

    // Baỏ hiểm y tế
    public double HealthInsurance { get; set; }

    // Bảo hiểm thất nghiệp
    public double UnemploymentInsurance { get; set; }

}
