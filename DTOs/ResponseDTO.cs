using StudentExamSystem.Enums;

namespace StudentExamSystem.DTOs
{
    public class ResponseDTO<T>
    {
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public ErrorCode ErrorCode { get; set; }
        public static ResponseDTO<T> Success(T data, string message = "")
        {
            return new ResponseDTO<T>
            {
                Data = data,
                IsSuccess = true,
                Message = message,
                ErrorCode = ErrorCode.None,
            };
        }

        public static ResponseDTO<T> Error(ErrorCode errorCode, string message = "")
        {
            return new ResponseDTO<T>
            {
                Data = default,
                IsSuccess = false,
                Message = message,
                ErrorCode = errorCode,
            };
        }
    }
}
