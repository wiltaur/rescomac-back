namespace rescomac_back.repository.Dto
{
    public class ApiResponse<T>
    {
        public ApiResponse(T data)
        {
            Data = data;
            IsSuccess = true;
            ReturnMessage = "";
        }
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string ReturnMessage { get; set; }
    }
}