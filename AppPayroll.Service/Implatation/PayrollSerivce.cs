using AppPayroll.Model.Payrolls;
using AppPayroll.Service.Interfaces;

namespace AppPayroll.Service.Implatation;

public class PayrollSerivce : IPayrollSerivce
{
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
        result.SalaryGross.SocialInsurance = grossSalary * 8 / 100;
        result.SalaryGross.HealthInsurance = grossSalary * 1.5 / 100;
        result.SalaryGross.UnemploymentInsurance = grossSalary / 100;

        result.IncomeBeforeTax.IncomeBeforeTaxProterty =
            request.Wage.Value - (result.SalaryGross.SocialInsurance + result.SalaryGross.HealthInsurance
            + result.SalaryGross.UnemploymentInsurance);

        result.IncomeBeforeTax.PersonalSituation = 11000000;
        result.IncomeBeforeTax.DependentsFamily = request.NumberOfDependents.Value * 44000000;

        result.IncomeTaxes.IncomeTaxesProperty =
            result.IncomeBeforeTax.IncomeBeforeTaxProterty - result.IncomeBeforeTax.PersonalSituation - result.IncomeBeforeTax.DependentsFamily
             > 0 ? result.IncomeBeforeTax.IncomeBeforeTaxProterty - result.IncomeBeforeTax.PersonalSituation - result.IncomeBeforeTax.DependentsFamily : 0;

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
        double taxPaymentRemain = incomeTaxesProperty - 0;
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
