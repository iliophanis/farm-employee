namespace server.Modules.Common.Responses
{
    public class DataTableResponse<T>
    {
        public T Data { get; set; }
        public Pagination Pagination { get; set; }
        public DataTableResponse<T> WithData(T data)
        {
            this.Data = data;
            return this;
        }
    }
}