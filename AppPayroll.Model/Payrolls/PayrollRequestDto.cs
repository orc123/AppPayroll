using AppPayroll.Model.Enums;
using System.ComponentModel.DataAnnotations;

namespace AppPayroll.Model.Payrolls;

public class PayrollRequestDto
{
    public int? Wage { get; set; }

    [Required]
    public DateTime? DateApply { get; set; }

    [Required]
    public int? NumberOfDependents { get; set; }

    [Required]
    public TypeOfInsurance? TypeOfInsurance { get; set; }
    public int? OtherSalary { get; set; }

    [Required]
    public Zone? Zone { get; set;}

    [Required]
    public TypePayroll? Type { get; set; }

}
