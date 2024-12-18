namespace Coupon.Core.BaseResult
{
    public  class ResultViewModel
    {
        public ResultViewModel(string message= "", bool isSuccess =false)
        {
            Message = message;
            IsSuccess = isSuccess;
        }

        public  string Message { get; private set; } = default!;

        public  bool IsSuccess { get; private set; }


        public static ResultViewModel Success(string message) => new ResultViewModel(message, true);
        public static ResultViewModel Failure(string message) => new ResultViewModel(message, false);
    }

    public  class ResultViewModel<T> : ResultViewModel
    {
        public ResultViewModel(T? data = default, bool isSuccess = true, string message = "")
        : base(message, isSuccess)
        {
            Data = data;
        }
        public T? Data { get; }
        public static ResultViewModel<T> Success(T? data, string message) => new ResultViewModel<T>(data, true, message);
        public static ResultViewModel<T> Failure(T? data, string message) => new ResultViewModel<T>(data, false, message);


    }
}
