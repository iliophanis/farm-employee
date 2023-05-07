namespace server.Modules.Common.Responses
{
    public class CommandResponse<T>
    {
        public T Data { get; set; }

        public CommandResponse<T> WithData(T data)
        {
            this.Data = data;
            return this;
        }
    }
}