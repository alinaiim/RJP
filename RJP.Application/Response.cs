namespace RJP.Application;

public class Response<T>
{
    public T Payload { get; set; }
    public string Message { get; set; }
    public bool Success { get; set; }
}
