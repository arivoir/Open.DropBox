using Open.IO;
using Open.Net.Http;
using Open.OAuth2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Open.DropBox
{
    public class DropBoxClient : OAuth2Client
    {
        #region ** fields

        private string _accessToken = null;
        private const string _apiUri = "https://api.dropboxapi.com/2/";
        private const string _apiContentUri = "https://content.dropboxapi.com/2/";
        private const string _authorizeUri = "https://www.dropbox.com/oauth2/authorize";
        public const string DesktopCallbackUrl = "https://www.dropbox.com/2/oauth/oob";

        #endregion

        #region ** initialization

        public DropBoxClient(string accessToken)
        {
            _accessToken = accessToken;
        }

        #endregion

        #region ** authentication

        public static string GetRequestUrl(string clientId, string callbackUrl)
        {
            return OAuth2Client.GetRequestUrl(_authorizeUri, clientId, null, callbackUrl, response_type: "token");
        }

        #endregion

        #region ** public methods

        public async Task<Account> GetCurrentAccountAsync(CancellationToken cancellationToken)
        {
            var uri = BuildApiUri("users/get_current_account");
            var client = GetClient();
            var content = new StringContent("null");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(uri, content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<Account>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task<Space> GetSpaceUsage(CancellationToken cancellationToken)
        {
            var uri = BuildApiUri("users/get_space_usage");
            var client = GetClient();
            var content = new StringContent("null");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(uri, content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                var contentString = await response.Content.ReadAsStringAsync();
                return await response.Content.ReadJsonAsync<Space>();
            }
            else
            {
                var contentString = await response.Content.ReadAsStringAsync();
                throw await ProcessException(response);
            }
        }

        public async Task<ItemsList> ListFolderAsync(string path, CancellationToken cancellationToken)
        {
            var uri = BuildApiUri("files/list_folder");
            var client = GetClient();
            var @params = new ListFolderParams();
            @params.Path = path;
            var content = new StringContent(@params.SerializeJson());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(uri, content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<ItemsList>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }


        public async Task<ItemsList> ListFolderContinueAsync(string cursor, CancellationToken cancellationToken)
        {
            var uri = BuildApiUri("files/list_folder/continue");
            var client = GetClient();
            var @params = new CursorParams();
            @params.Cursor = cursor;
            var content = new StringContent(@params.SerializeJson());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = await client.PostAsync(uri, content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<ItemsList>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task<SearchList> SearchAsync(string query, string path = "", string mode = "filename", int start = 0, int maxResults = 1000, CancellationToken cancellationToken = default(CancellationToken))
        {
            var client = GetClient();
            var @params = new SearchParams();
            @params.Path = path;
            @params.Query = query;
            @params.Mode = mode;
            @params.Start = start;
            @params.MaxResults = maxResults;
            var content = new StringContent(@params.SerializeJson());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var uri = BuildApiUri("files/search");
            var response = await client.PostAsync(uri, content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<SearchList>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        /// <summary>
        /// Gets the thumbnail.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="size">The size.</param>
        public async Task<Stream> GetThumbnailAsync(string path, string size = null, string format = "jpeg", CancellationToken cancellationToken = default(CancellationToken))
        {
            var @params = new ThumbnailParams();
            @params.Path = path;
            @params.Size = size;
            @params.Format = format;
            var uri = BuildApiContentUri("files/get_thumbnail");
            var client = GetClient();
            client.DefaultRequestHeaders.Add("Dropbox-API-Arg", @params.SerializeJson(true));
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.SetEmptyContent();
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return new StreamWithLength(await response.Content.ReadAsStreamAsync(), response.Content.Headers.ContentLength);
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task<Item> MoveAsync(string from, string to, CancellationToken cancellationToken)
        {
            var @params = new CopyParams();
            @params.FromPath = from;
            @params.ToPath = to;
            var content = new StringContent(@params.SerializeJson());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var uri = BuildApiUri("files/move");
            var client = GetClient();
            var response = await client.PostAsync(uri, content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<Item>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task<Item> CopyAsync(string from, string to, CancellationToken cancellationToken)
        {
            var @params = new CopyParams();
            @params.FromPath = from;
            @params.ToPath = to;
            var content = new StringContent(@params.SerializeJson());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var uri = BuildApiUri("files/copy");
            var client = GetClient();
            var response = await client.PostAsync(uri, content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<Item>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task<Item> DeleteAsync(string path, CancellationToken cancellationToken)
        {
            var @params = new ItemParams();
            @params.Path = path;
            var content = new StringContent(@params.SerializeJson());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var uri = BuildApiUri("files/delete");
            var client = GetClient();
            var response = await client.PostAsync(uri, content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<Item>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task<Item> CreateFolderAsync(string path, CancellationToken cancellationToken)
        {
            var @params = new ItemParams();
            @params.Path = path;
            var content = new StringContent(@params.SerializeJson());
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var uri = BuildApiUri("files/create_folder");
            var client = GetClient();
            var response = await client.PostAsync(uri, content, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<Item>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task<Item> UploadFileAsync(string path, Stream fileStream, IProgress<StreamProgress> progress, bool autorename = false, CancellationToken cancellationToken = default(CancellationToken))
        {

            var @params = new UploadParams();
            @params.Path = path;
            @params.Autorename = autorename;
            @params.Mode = "add";
            var uri = BuildApiContentUri("files/upload");
            var client = GetClient();
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            var content = new StreamedContent(fileStream, progress, cancellationToken);
            client.DefaultRequestHeaders.Add("Dropbox-API-Arg", @params.SerializeJson(true));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            request.Content = content;
            var response = await client.SendAsync(request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<Item>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task<UploadSessionResult> UploadSessionStartAsync(Stream fileStream, IProgress<StreamProgress> progress, CancellationToken cancellationToken = default(CancellationToken))
        {

            var @params = new UploadSessionParams();
            @params.Close = false;
            var uri = BuildApiContentUri("files/upload_session/start");
            var client = GetClient();
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            var content = new StreamedContent(fileStream, progress, cancellationToken);
            client.DefaultRequestHeaders.Add("Dropbox-API-Arg", @params.SerializeJson(true));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            request.Content = content;
            var response = await client.SendAsync(request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<UploadSessionResult>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task UploadSessionAppendAsync(string sessionId, Stream fileStream, long offset, IProgress<StreamProgress> progress, CancellationToken cancellationToken = default(CancellationToken))
        {

            var @params = new UploadSessionParams();
            @params.Close = false;
            @params.Cursor = new UploadSessionCursor() { SessionId = sessionId, Offset = offset };
            var uri = BuildApiContentUri("files/upload_session/append_v2");
            var client = GetClient();
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            var content = new StreamedContent(fileStream, progress, cancellationToken);
            client.DefaultRequestHeaders.Add("Dropbox-API-Arg", @params.SerializeJson(true));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            request.Content = content;
            var response = await client.SendAsync(request, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                throw await ProcessException(response);
            }
        }

        public async Task<Item> UploadSessionFinishAsync(string sessionId, string path, Stream fileStream, long offset, IProgress<StreamProgress> progress, bool autorename = false, CancellationToken cancellationToken = default(CancellationToken))
        {

            var @params = new UploadSessionParams();
            @params.Cursor = new UploadSessionCursor() { SessionId = sessionId, Offset = offset };
            @params.Commit = new UploadParams()
            {
                Path = path,
                Autorename = autorename,
                Mode = "add",
            };
            var uri = BuildApiContentUri("files/upload_session/finish");
            var client = GetClient();
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            var content = new StreamedContent(fileStream, progress, cancellationToken);
            client.DefaultRequestHeaders.Add("Dropbox-API-Arg", @params.SerializeJson(true));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            request.Content = content;
            var response = await client.SendAsync(request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadJsonAsync<Item>();
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        public async Task<Stream> DownloadFileAsync(string path, CancellationToken cancellationToken)
        {
            var @params = new ItemParams();
            @params.Path = path;
            var uri = BuildApiContentUri("files/download");
            var client = GetClient();
            client.DefaultRequestHeaders.Add("Dropbox-API-Arg", @params.SerializeJson(true));
            var request = new HttpRequestMessage(HttpMethod.Post, uri);
            request.SetEmptyContent();
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return new StreamWithLength(await response.Content.ReadAsStreamAsync(), response.Content.Headers.ContentLength);
            }
            else
            {
                throw await ProcessException(response);
            }
        }

        #endregion

        #region ** private stuff

        private static Uri BuildApiUri(string basePath, string path = null, Dictionary<string, string> parameters = null)
        {
            return BuildUri(_apiUri, basePath, path, parameters);
        }

        private Uri BuildApiContentUri(string basePath, string path = null, Dictionary<string, string> parameters = null)
        {
            return BuildUri(_apiContentUri, basePath, path, parameters);
        }

        private static Uri BuildUri(string baseUri, string basePath, string path, Dictionary<string, string> parameters)
        {
            UriBuilder builder = new UriBuilder(baseUri + basePath);
            if (path != null)
            {
                builder.Path += GetValidPath(path);
            }
            builder.Query = (parameters != null && parameters.Count() > 0 ? string.Join("&", parameters.Select(pair => pair.Key + "=" + Uri.EscapeDataString(pair.Value)).ToArray()) : "");
            return builder.Uri;
        }

        public static string GetValidPath(string dirPath, string fileName)
        {
            var path = GetValidPath(dirPath);
            return path + (path.EndsWith("/", StringComparison.CurrentCulture) ? "" : "/") + fileName;
        }
        public static string GetValidPath(string dirPath)
        {
            var path = string.Join("/", dirPath.Split(new string[] { "\\", "/" }, StringSplitOptions.RemoveEmptyEntries)/*.Select(str => Extensions.EscapeUriString(str))*/.ToArray());
            return (string.IsNullOrWhiteSpace(path) ? "" : "/") + path;
        }

        private new HttpClient GetClient()
        {
            var client = new HttpClient(new DropBoxMessageHandler());
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
            client.Timeout = Timeout.InfiniteTimeSpan;
            return client;
        }

        private async Task<Exception> ProcessException(HttpResponseMessage response)
        {
            if (response.StatusCode == (HttpStatusCode)409 ||
                response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var error = await response.Content.ReadJsonAsync<Error>();
                return new DropboxException(response.StatusCode, error);
            }
            var str = await response.Content.ReadAsStringAsync();
            return new DropboxException(response.StatusCode, str);
        }

        #endregion
    }
}
