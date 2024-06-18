using Eva.Core.Domain.Enums;

namespace Eva.Core.Domain.BaseModels
{
    public record Error(ErrorType type, string description)
    {
        public static Error InvalidComment = new Error(ErrorType.Failure, "No post found for this comment");
        public static Error NotEnoughCharacters = new Error(ErrorType.Failure, "Comment does not have enough characters");
        public static Error PostNotFound = new Error(ErrorType.Failure, "Post not found");
    }
}
