using AppPayroll.Model.Enums;

namespace AppPayroll.Service;

public class ResultModel<T>
{
    public ResultModel(T data)
    {
        Data = data;
    }
    public ResultModel(ErrorModel error)
    {
        Error = error;
    }
    public ResultModel(ErrorType type, string errorMessage)
    {
        Error = new ErrorModel(type, errorMessage);
    }
    public T Data { get; private set; }
    public ErrorModel Error { get; private set; }
}