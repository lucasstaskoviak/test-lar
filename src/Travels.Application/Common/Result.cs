namespace Travels.Application.Common
{
    public class Result<T>
    {
        public bool IsSuccess { get; private set; }
        public bool IsFailure => !IsSuccess;
        public T Value { get; private set; }
        public string Error { get; private set; }

        // Construtor privado para evitar criação fora da classe
        private Result(bool isSuccess, T value, string error)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
        }

        // Método para criar um resultado de sucesso
        public static Result<T> Success(T value) =>
            new Result<T>(true, value, null);

        // Método para criar um resultado de falha
        public static Result<T> Failure(string error) =>
            new Result<T>(false, default(T), error);
    }
}
