using AppPayroll.Model.Enums;

namespace AppPayroll.Service;

public class ErrorModel
{
    public ErrorModel()
    {

    }
    public ErrorModel(ErrorType type, string errorMessage)
    {
        Type = type;
        Message = errorMessage;
    }
    public ErrorType Type { get; private set; }
    public string Message { get; private set; }
}
