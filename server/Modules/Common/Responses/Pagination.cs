namespace server.Modules.Common.Responses
{
    public class Pagination
    {
        public int Skip { get; set; }
        public int Take { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages => Take == 0 ? 0 : (int)Math.Ceiling((TotalRecords / (double)Take));
    }
}