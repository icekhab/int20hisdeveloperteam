using System.Collections.Generic;
using System.Net;
using System.Text;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model.Attachments;
using VkNet.Model.RequestParams;

namespace Int20h.Service
{
    public class VkService
    {
        private readonly VkApi _api;

        public VkService()
        {
            _api = new VkApi();
        }

        public bool Authorize(ulong appId, string email, string password, Settings settings)
        {
            _api.Authorize(new ApiAuthParams
            {
                ApplicationId = appId,
                Login = email,
                Password = password,
                Settings = settings
            });
            return _api.IsAuthorized;
        }

        public IEnumerable<Photo> UploadImageInGroup(long albumId, long groupId, string fileName)
        {
            // Получить адрес сервера для загрузки.
            var uploadServer = _api.Photo.GetUploadServer(albumId, groupId);
            // Загрузить файл.
            var wc = new WebClient();
            var responseFile = Encoding.ASCII.GetString(wc.UploadFile(uploadServer.UploadUrl, fileName));
            // Сохранить загруженный файл
            var photos = _api.Photo.Save(new PhotoSaveParams
            {
                SaveFileResponse = responseFile,
                AlbumId = albumId,
                GroupId = groupId
            });
            return photos;
        }

        public void WallPost(string message, IEnumerable<MediaAttachment> media, long ownerId, bool isGroup = false)
        {
            var post = _api.Wall.Post(new WallPostParams
            {
                OwnerId = !isGroup ? ownerId : ownerId * -1,
                FromGroup = true,
                Message = message,
                Attachments = media
            });
            
        }
    }
}
