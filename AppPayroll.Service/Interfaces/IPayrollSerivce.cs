using AppPayroll.Model.Payrolls;

namespace AppPayroll.Service.Interfaces;

public interface IPayrollSerivce
{
    Task<ResultModel<PayrollResponseDto>> SaralyPayrollAsync(PayrollRequestDto payrollRequestDto);
}
