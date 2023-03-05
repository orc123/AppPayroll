using AppPayroll.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace AppPayroll.Model.Payrolls;

public class PayrollRequestDto
{
    public double? Wage { get; set; }

    public DateApply? DateApply { get; set; }
    public int? NumberOfDependents { get; set; }
    public TypeOfInsurance? TypeOfInsurance { get; set; }
    public int? OtherSalary { get; set; }

    public Zone? Zone { get; set;}
    public TypePayroll? Type { get; set; }

}
