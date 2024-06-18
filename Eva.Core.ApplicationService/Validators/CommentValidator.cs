using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;

namespace Eva.Core.ApplicationService.Validators
{
    public class CommentValidator
    {
        public Result<CommentCreationViewModel> ValidateRelatedPost(CommentCreationViewModel comment)
        {
            return comment.PostId == 0 ?
                Result<CommentCreationViewModel>.Success(comment) :
                Result<CommentCreationViewModel>.Failure(Error.InvalidComment);
        }

        public Result<CommentCreationViewModel> ValidateCharacterCount(CommentCreationViewModel comment)
        {
            int charCount = comment.Text.Length;
            return charCount > 5 ? 
                Result<CommentCreationViewModel>.Success(comment) :
                Result<CommentCreationViewModel>.Failure(Error.NotEnoughCharacters);
        }

        public Result<(string content, int blogId)> GetPostContent(Post post)
        {
            return Result<(string content, int blogId)>.Success((post.Content, post.BlogId));
        }
    }
}
