using DataManagement.Domain.Enums;

namespace DataManagement.Domain.Abstractions.Result
{
	public sealed record Error(ErrorCodes Code, string? Description = null)
    {
        public static readonly Error None = new(ErrorCodes.None);

        public static implicit operator Result(Error error) => Result.Failure(error);
    }
}
