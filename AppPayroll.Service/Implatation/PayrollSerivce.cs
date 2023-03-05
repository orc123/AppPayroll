using AppPayroll.Model.Payrolls;
using AppPayroll.Service.Interfaces;

namespace AppPayroll.Service.Implatation;

public class PayrollSerivce : IPayrollSerivce
{
    public async Task<ResultModel<PayrollResponseDto>> SaralyPayrollAsync(PayrollRequestDto payrollRequestDto)
    {
        var validate = ValidateModel(payrollRequestDto);
        if (validate != null)
        {
            return new ResultModel<PayrollResponseDto>(validate);
        }

        return new ResultModel<PayrollResponseDto>(new PayrollResponseDto());
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
