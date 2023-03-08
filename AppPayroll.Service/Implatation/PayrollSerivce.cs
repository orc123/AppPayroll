using AppPayroll.Model.Payrolls;
using AppPayroll.Service.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AppPayroll.Service.Implatation;

public class PayrollSerivce : IPayrollSerivce
{
    private readonly IConfiguration _configuration;
    public PayrollSerivce(IConfiguration configuration)
    {
        this._configuration = configuration;
    }
    public async Task<ResultModel<PayrollResponseDto>> SaralyPayrollAsync(PayrollRequestDto request)
    {
        PayrollResponseDto result = null;
        var validate = ValidateModel(request);
        if (validate != null)
        {
            return new ResultModel<PayrollResponseDto>(validate);
        }

        double grossSalary = 0;
        double netSalary = 0;

        if (request.Type == Model.Enums.TypePayroll.GrossToNet)
        {
            result = GrossToNet(grossSalary, request);
        }
        else
        {
            result = NetToGross(netSalary, request);
        }

        return new ResultModel<PayrollResponseDto>(result);
    }
    private PayrollResponseDto GrossToNet(double grossSalary, PayrollRequestDto request)
    {
        int FamilyAllowances = _configuration["familyAllowances"] == null ? 11000000 : int.Parse(_configuration["familyAllowances"]);
        int dependentsFamily = _configuration["dependentsFamily"] == null ? 44000000 : int.Parse(_configuration["dependentsFamily"]);

        double socialInsurance = _configuration["socialInsurance"] == null ? 8 : double.Parse(_configuration["socialInsurance"]);
        double healthInsurance = _configuration["healthInsurance"] == null ? 1.5 : double.Parse(_configuration["healthInsurance"]);
        double unemploymentInsurance = _configuration["unemploymentInsurance"] == null ? 1 : double.Parse(_configuration["unemploymentInsurance"]);

        PayrollResponseDto result = new PayrollResponseDto()
        {
            IncomeBeforeTax = new(),
            IncomeTaxes = new(),
            SalaryGross = new(),
            NetSalary = 0
        };
        grossSalary = (request.TypeOfInsurance == Model.Enums.TypeOfInsurance.Other && request.OtherSalary.HasValue) ?
               request.OtherSalary.Value : request.Wage.Value;

        result.SalaryGross.SalaryGrossProterty = grossSalary;
        result.SalaryGross.SocialInsurance = grossSalary * socialInsurance / 100;
        result.SalaryGross.HealthInsurance = grossSalary * healthInsurance / 100;
        result.SalaryGross.UnemploymentInsurance = grossSalary * unemploymentInsurance / 100;

        result.IncomeBeforeTax.IncomeBeforeTaxProterty =
            request.Wage.Value - (result.SalaryGross.SocialInsurance + result.SalaryGross.HealthInsurance
            + result.SalaryGross.UnemploymentInsurance);

        result.IncomeBeforeTax.FamilyAllowances = FamilyAllowances;
        result.IncomeBeforeTax.DependentsFamily = request.NumberOfDependents.Value * dependentsFamily;

        result.IncomeTaxes.IncomeTaxesProperty =
            result.IncomeBeforeTax.IncomeBeforeTaxProterty - result.IncomeBeforeTax.FamilyAllowances - result.IncomeBeforeTax.DependentsFamily
             > 0 ? result.IncomeBeforeTax.IncomeBeforeTaxProterty - result.IncomeBeforeTax.FamilyAllowances - result.IncomeBeforeTax.DependentsFamily : 0;

        result.IncomeTaxes.TaxPayment = PersonalIncomeTax(result.IncomeTaxes.IncomeTaxesProperty);

        result.NetSalary = result.IncomeBeforeTax.IncomeBeforeTaxProterty
             - result.IncomeTaxes.PersonalIncomeTax;

        return result;
    }

    private PayrollResponseDto NetToGross(double netSalary, PayrollRequestDto request)
    {
        PayrollResponseDto result = new PayrollResponseDto()
        {
            IncomeBeforeTax = new(),
            IncomeTaxes = new(),
            SalaryGross = new(),
            NetSalary = 0
        };
        return result;
    }

    private double[] PersonalIncomeTax(double incomeTaxesProperty)
    {
        double[] taxPayment = new double[] { 0, 0, 0, 0, 0, 0, 0 };
        double taxPaymentRemain = 0;

        int level0 = _configuration["taxableRate:level0"] == null ? 0 : int.Parse(_configuration["taxableRate:level0"]);
        int level1 = _configuration["taxableRate:level1"] == null ? 5000000 : int.Parse(_configuration["taxableRate:level1"]);
        int level2 = _configuration["taxableRate:level2"] == null ? 10000000 : int.Parse(_configuration["taxableRate:level2"]);
        int level3 = _configuration["taxableRate:level3"] == null ? 18000000 : int.Parse(_configuration["taxableRate:level3"]);
        int level4 = _configuration["taxableRate:level4"] == null ? 32000000 : int.Parse(_configuration["taxableRate:level4"]);
        int level5 = _configuration["taxableRate:level5"] == null ? 52000000 : int.Parse(_configuration["taxableRate:level5"]);
        int level6 = _configuration["taxableRate:level6"] == null ? 80000000 : int.Parse(_configuration["taxableRate:level6"]);

        int rate0 = _configuration["taxableRate:rate0"] == null ? 5 : int.Parse(_configuration["taxableRate:rate0"]);
        int rate1 = _configuration["taxableRate:rate1"] == null ? 10 : int.Parse(_configuration["taxableRate:rate1"]);
        int rate2 = _configuration["taxableRate:rate2"] == null ? 15 : int.Parse(_configuration["taxableRate:rate2"]);
        int rate3 = _configuration["taxableRate:rate3"] == null ? 20 : int.Parse(_configuration["taxableRate:rate3"]);
        int rate4 = _configuration["taxableRate:rate4"] == null ? 25 : int.Parse(_configuration["taxableRate:rate4"]);
        int rate5 = _configuration["taxableRate:rate5"] == null ? 30 : int.Parse(_configuration["taxableRate:rate5"]);
        int rate6 = _configuration["taxableRate:rate6"] == null ? 35 : int.Parse(_configuration["taxableRate:rate6"]);

        if (incomeTaxesProperty == level0)
            return taxPayment;
        if (incomeTaxesProperty < 50000000)
        {
            return taxPayment;
        }


        return taxPayment;
    }


     private static ErrorModel? ValidateModel(PayrollRequestDto payrollRequestDto)
    {
        if (payrollRequestDto == null)
        {
            return new ErrorModel(Model.Enums.ErrorType.BAD_REQUEST, "Vui lòng điền đầy đủ thông tin yêu cầu");
        }
        if (payrollRequestDto.Wage == null)
        {
            return new ErrorModel(Model.Enums.ErrorType.BAD_REQUEST, "Mức lương không hợp lệ");
        }
        if (payrollRequestDto.Wage.Value <= 0)
        {
            return new ErrorModel(Model.Enums.ErrorType.BAD_REQUEST, "Mức lương không hợp lệ");
        }
        if (payrollRequestDto.DateApply == null)
        {
            return new ErrorModel(Model.Enums.ErrorType.BAD_REQUEST, "Vui lòng chọn ngày áp dụng");
        }
        if (payrollRequestDto.NumberOfDependents == null)
        {
            return new ErrorModel(Model.Enums.ErrorType.BAD_REQUEST, "Vui lòng chọn người phụ thuộc");
        }
        if (payrollRequestDto.NumberOfDependents.Value < 0)
        {
            return new ErrorModel(Model.Enums.ErrorType.BAD_REQUEST, "Người phụ thuộc ít nhất phải bằng 0");
        }
        if (payrollRequestDto.TypeOfInsurance == null)
        {
            return new ErrorModel(Model.Enums.ErrorType.BAD_REQUEST, "Vui lòng chọn loại bảo hiểm");
        }

        if (payrollRequestDto.TypeOfInsurance == Model.Enums.TypeOfInsurance.Other && payrollRequestDto.OtherSalary == null)
        {
            return new ErrorModel(Model.Enums.ErrorType.BAD_REQUEST, "Bạn chưa nhập số tiền đóng bảo hiểm");
        }
        if (payrollRequestDto.Zone == null)
        {
            return new ErrorModel(Model.Enums.ErrorType.BAD_REQUEST, "Vui lòng chọn vùng");
        }
        if (payrollRequestDto.Type == null)
        {
            return new ErrorModel(Model.Enums.ErrorType.BAD_REQUEST, "Vui lòng chọn loại");
        }
        return null;
    }

}
