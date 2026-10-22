using SnackisWeb.Models;

namespace SnackisWeb.Interfaces;

public interface IForumAPIService
{
    Task<List<ForumCategory>> GetForumCategories();
    Task<bool> UpdateForumCategory(UpdateForumCategory updateForumCategory);
    Task<bool> CreateForumCategory(CreateForumCategory createForumCategory);
    Task<bool> DeleteForumCategory(int id);

    Task<AccessTokenResponse?> Login(LoginRequest loginRequest);
    Task<bool> Register(RegisterRequest registerRequest);
    Task<User?> GetUser(string accessToken);
    Task<List<PublicUser>> GetForumUsers();
    Task<PublicUser> GetForumUser(string id);
    Task<bool> UpdateUser(UpdateUserRequest updateUserRequest);
    Task<User?> UpdateProfileImage(IFormFile image);

    Task<List<ForumPostReport>> GetForumPostReports();
    Task<ForumPostReport> GetForumPostReport(int id);
    Task<bool> CreateForumPostReport(CreateForumPostReport createForumPostReport);
    Task<bool> DeleteForumPostReport(int id);

    Task<ForumPost> GetForumPost(int id);
    Task<bool> UpdateForumPost(UpdatePostRequest updatePostRequest);
    Task<ForumPost> CreateForumPost(CreatePostRequest createPostRequest);
    Task<bool> DeleteForumPost(int id);

    Task<List<ForumMessage>> GetForumMessages();
    Task<ForumMessage> CreateForumMessage(CreateMessageRequest createMessageRequest);

    Task<List<ForumThread>> GetForumThreads();
    Task<ForumThread> GetForumThread(int id);
    Task<ForumThread?> CreateForumThread(CreateThreadRequest createForumThread);
    Task<bool> UpdateForumThread(UpdateThreadRequest updateForumThread);
    Task<bool> DeleteForumThread(int id);

}
