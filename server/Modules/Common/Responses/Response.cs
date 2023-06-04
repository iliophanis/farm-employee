namespace server.Modules.Common.Responses;
public class Response<T>
{
    public T Data { get; set; }
    public Pagination Pagination { get; set; }
    public List<string> Actions { get; set; }

    public Response()
    {
        Actions = new List<string>();
    }

    public Response<T> WithData(T data)
    {
        this.Data = data;
        return this;
    }

    public Response<T> WithActions(IEnumerable<string> actions)
    {
        this.Actions.AddRange(actions);
        return this;
    }
}