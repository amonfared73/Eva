using Eva.Core.Domain.Enums;

namespace Eva.Core.Domain.BaseModels
{
    public record Error(ErrorType type, string description)
    {
        public static Error InvalidComment = new Error(ErrorType.Failure, "No post found for this comment");
        public static Error NotEnoughCharacters = new Error(ErrorType.Failure, "Comment does not have enough characters");
        public static Error PostNotFound = new Error(ErrorType.Failure, "Post not found");

        public static Error UserNotFound = new Error(ErrorType.Validation, "User not found");

        public static Error EmptyString = new Error(ErrorType.Validation, "Empty string");

        public static Error PermissionFetchError = new Error(ErrorType.Failure, "Permission fetch failure");

        public static Error EmptyPermissionCollection = new Error(ErrorType.Validation, "Empty permission collection");
    }
}
