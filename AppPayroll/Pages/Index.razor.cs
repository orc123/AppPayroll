using AppPayroll.Model.Enums;
using AppPayroll.Model.Payrolls;
using AppPayroll.Service;
using Microsoft.AspNetCore.Components.Forms;
using System;

namespace AppPayroll.Pages;

public partial class Index
{
    public bool ShowResult = false;
    PayrollRequestDto payrollRequestDto = new();
    private ResultModel<PayrollResponseDto> result;

    private EditContext editContext;

    protected override void OnInitialized()
    {
        editContext = new EditContext(payrollRequestDto);
    }

    public async Task SubmitPayrollRequest(TypePayroll typePayroll)
    {
        payrollRequestDto.Type = typePayroll;

        result = await payrollService.SaralyPayrollAsync(payrollRequestDto);

        if (result.Data == null)
        {
            // xử lý thêm
        }
        else
        {
            ShowResult = true;
        }
    }
}
