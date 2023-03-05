namespace AppPayroll.Model.Payrolls;

public class IncomeTaxes
{
    public double IncomeTaxesProperty { get; set; }
    public double PersonalIncomeTax
    {
        get
        {
            return TaxPayment[0] +
                 TaxPayment[1] +
                 TaxPayment[2] +
                 TaxPayment[3] +
                 TaxPayment[4] +
                 TaxPayment[5] +
                 TaxPayment[6];
        }
    }

    public double[] TaxPayment { get; set; } = new double[] { 0, 0, 0, 0, 0, 0, 0 };
}