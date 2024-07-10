namespace PingYourPackage.Domain
{
    public class OperationResult
    {
        public bool IsSuccess { get; private set; }
        public OperationResult(bool isSuccess) => IsSuccess = isSuccess;
    }
    public class OperationResult<TEntity    > : OperationResult
    {
        public TEntity Entity { get; set; }

        public OperationResult(bool isSuccess) : base(isSuccess) { }
    }
}