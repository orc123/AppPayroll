namespace AppPayroll.Servicve.Test;

[TestClass]
public class PayrollSerivceTest
{
    private readonly Random _rand = new Random();
    private readonly IPayrollSerivce _payrollService = new PayrollSerivce();

    #region TEST

    [TestMethod]
    public async Task SaralyPayrollAsync_ModelNull_Fail()
    {
        PayrollRequestDto? modelNull = null;
        var actualModel = await _payrollService.SaralyPayrollAsync(modelNull);


        Assert.IsNotNull(actualModel);
        Assert.IsNull(actualModel.Data);
        Assert.AreEqual(ErrorType.BAD_REQUEST, actualModel.Error.Type);
        Assert.AreEqual("Vui lòng điền đầy đủ thông tin yêu cầu", actualModel.Error.Message);
    }

    [TestMethod]
    public async Task SaralyPayrollAsync_WageNull_Fail()
    {
        PayrollRequestDto wageNull = InitPayrollRequestDto();
        wageNull.Wage = null;
        var actualModel = await _payrollService.SaralyPayrollAsync(wageNull);


        Assert.IsNotNull(actualModel);
        Assert.IsNull(actualModel.Data);
        Assert.AreEqual(ErrorType.BAD_REQUEST, actualModel.Error.Type);
        Assert.AreEqual("Mức lương không hợp lệ", actualModel.Error.Message);
    }

    [TestMethod]
    public async Task SaralyPayrollAsync_WageEqual0_Fail()
    {
        PayrollRequestDto wageNull = InitPayrollRequestDto();
        wageNull.Wage = 0;
        var actualModel = await _payrollService.SaralyPayrollAsync(wageNull);


        Assert.IsNotNull(actualModel);
        Assert.IsNull(actualModel.Data);
        Assert.AreEqual(ErrorType.BAD_REQUEST, actualModel.Error.Type);
        Assert.AreEqual("Mức lương không hợp lệ", actualModel.Error.Message);
    }

    [TestMethod]
    public async Task SaralyPayrollAsync_DateApplyNull_Fail()
    {
        PayrollRequestDto wageNull = InitPayrollRequestDto();
        wageNull.DateApply = null;
        var actualModel = await _payrollService.SaralyPayrollAsync(wageNull);


        Assert.IsNotNull(actualModel);
        Assert.IsNull(actualModel.Data);
        Assert.AreEqual(ErrorType.BAD_REQUEST, actualModel.Error.Type);
        Assert.AreEqual("Vui lòng chọn ngày áp dụng", actualModel.Error.Message);
    }

    [TestMethod]
    public async Task SaralyPayrollAsync_NumberOfDependentsNull_Fail()
    {
        PayrollRequestDto numberOfDependentsNull = InitPayrollRequestDto();
        numberOfDependentsNull.NumberOfDependents = null;
        var actualModel = await _payrollService.SaralyPayrollAsync(numberOfDependentsNull);


        Assert.IsNotNull(actualModel);
        Assert.IsNull(actualModel.Data);
        Assert.AreEqual(ErrorType.BAD_REQUEST, actualModel.Error.Type);
        Assert.AreEqual("Vui lòng chọn người phụ thuộc", actualModel.Error.Message);
    }

    [TestMethod]
    public async Task SaralyPayrollAsync_NumberOfDependentsNoEqual0_Fail()
    {
        PayrollRequestDto numberOfDependentsEqual0 = InitPayrollRequestDto();
        numberOfDependentsEqual0.NumberOfDependents = -1;
        var actualModel = await _payrollService.SaralyPayrollAsync(numberOfDependentsEqual0);


        Assert.IsNotNull(actualModel);
        Assert.IsNull(actualModel.Data);
        Assert.AreEqual(ErrorType.BAD_REQUEST, actualModel.Error.Type);
        Assert.AreEqual("Người phụ thuộc ít nhất phải bằng 0", actualModel.Error.Message);
    }

    [TestMethod]
    public async Task SaralyPayrollAsync_TypeOfInsuranceNull_Fail()
    {
        PayrollRequestDto typeOfInsuranceNull = InitPayrollRequestDto();
        typeOfInsuranceNull.TypeOfInsurance = null;
        var actualModel = await _payrollService.SaralyPayrollAsync(typeOfInsuranceNull);


        Assert.IsNotNull(actualModel);
        Assert.IsNull(actualModel.Data);
        Assert.AreEqual(ErrorType.BAD_REQUEST, actualModel.Error.Type);
        Assert.AreEqual("Vui lòng chọn loại bảo hiểm", actualModel.Error.Message);
    }

    [TestMethod]
    public async Task SaralyPayrollAsync_TypeOfInsuranceEqual2AndOtherSalaryNull_Fail()
    {
        PayrollRequestDto typeOfInsuranceEqual2AndOtherSalaryNull = InitPayrollRequestDto();
        typeOfInsuranceEqual2AndOtherSalaryNull.TypeOfInsurance = TypeOfInsurance.Other;
        typeOfInsuranceEqual2AndOtherSalaryNull.OtherSalary = null;
        var actualModel = await _payrollService.SaralyPayrollAsync(typeOfInsuranceEqual2AndOtherSalaryNull);


        Assert.IsNotNull(actualModel);
        Assert.IsNull(actualModel.Data);
        Assert.AreEqual(ErrorType.BAD_REQUEST, actualModel.Error.Type);
        Assert.AreEqual("Bạn chưa nhập số tiền đóng bảo hiểm", actualModel.Error.Message);
    }

    [TestMethod]
    public async Task SaralyPayrollAsync_ZoneNull_Fail()
    {
        PayrollRequestDto zoneNull = InitPayrollRequestDto();
        zoneNull.Zone = null;
        var actualModel = await _payrollService.SaralyPayrollAsync(zoneNull);


        Assert.IsNotNull(actualModel);
        Assert.IsNull(actualModel.Data);
        Assert.AreEqual(ErrorType.BAD_REQUEST, actualModel.Error.Type);
        Assert.AreEqual("Vui lòng chọn vùng", actualModel.Error.Message);
    }

    [TestMethod]
    public async Task SaralyPayrollAsync_TypeNull_Fail()
    {
        PayrollRequestDto typeNull = InitPayrollRequestDto();
        typeNull.Type = null;
        var actualModel = await _payrollService.SaralyPayrollAsync(typeNull);


        Assert.IsNotNull(actualModel);
        Assert.IsNull(actualModel.Data);
        Assert.AreEqual(ErrorType.BAD_REQUEST, actualModel.Error.Type);
        Assert.AreEqual("Vui lòng chọn loại", actualModel.Error.Message);
    }

    #endregion

    #region Init Data

    public PayrollRequestDto InitPayrollRequestDto()
    {
        return new PayrollRequestDto()
        {
            DateApply = DateApply.CURRENT,
            NumberOfDependents = 0,
            OtherSalary = 0,
            Type = Model.Enums.TypePayroll.NetToGross,
            TypeOfInsurance = Model.Enums.TypeOfInsurance.OnOfficialSalary,
            Wage = 11000000,
            Zone = Model.Enums.Zone.I
        };
    }

    #endregion

    #region Random func
    private int rand()
    {
        return _rand.Next(1, 1000000);
    }
    #endregion

    #region private

    private static string AppendSpaceToString(string str, int numberOfLength)
    {
        StringBuilder sb = new StringBuilder();
        if ((numberOfLength - str.Length) > 0)
        {
            sb.Append(' ', (numberOfLength - str.Length));
        }
        sb.Append(str);
        return sb.ToString();
    }
    #endregion
}
