using Eva.Core.Domain.BaseModels;
using Eva.Core.Domain.Models;
using Eva.Core.Domain.ViewModels;

namespace Eva.Core.ApplicationService.Validators
{
    public class CommentValidator
    {
        public EvaResult<CommentCreationViewModel> ValidateRelatedPost(CommentCreationViewModel comment)
        {
            return comment.PostId == 0 ?
                EvaResult<CommentCreationViewModel>.Success(comment) :
                EvaResult<CommentCreationViewModel>.Failure(Error.InvalidComment);
        }

        public EvaResult<CommentCreationViewModel> ValidateCharacterCount(CommentCreationViewModel comment)
        {
            int charCount = comment.Text.Length;
            return charCount > 5 ? 
                EvaResult<CommentCreationViewModel>.Success(comment) :
                EvaResult<CommentCreationViewModel>.Failure(Error.NotEnoughCharacters);
        }

        public EvaResult<(string content, int blogId)> GetPostContent(Post post)
        {
            return EvaResult<(string content, int blogId)>.Success((post.Content, post.BlogId));
        }
    }
}
