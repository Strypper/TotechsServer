using Azure.Storage;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Intranet;

public class AzureBlobStorageMediaService : IMediaService
{
    private readonly IOptionsMonitor<AzureStorageConfig> _storageConfig;
    private readonly StorageSharedKeyCredential _storageSharedKeyCredential;
    public AzureBlobStorageMediaService(IOptionsMonitor<AzureStorageConfig> azureStorageConfig,
                                        StorageSharedKeyCredential storageSharedKeyCredential)
    {
        _storageConfig = azureStorageConfig;
        _storageSharedKeyCredential = storageSharedKeyCredential;
    }

    public Task<List<string>> GetThumbNailUrls()
    {
        throw new NotImplementedException();
    }

    public bool IsImage(IFormFile file)
    {
        if (file.ContentType.Contains("image"))
        {
            return true;
        }

        string[] formats = new string[] { ".jpg", ".png", ".gif", ".jpeg" };

        return formats.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<Tuple<bool, string>> UploadAvatarToStorage(Stream fileStream,
                                                                 string fileName)
    {
        var blobUri = new Uri("https://" +
                              _storageConfig.CurrentValue.AccountName +
                              ".blob.core.windows.net/" +
                              _storageConfig.CurrentValue.AvatarContainer
                              + "/" + fileName);
        // Create the blob client.
        var blobClient = new BlobClient(blobUri, _storageSharedKeyCredential);

        // Upload the file
        await blobClient.UploadAsync(fileStream);

        return new Tuple<bool, string>(await Task.FromResult(true), blobClient.Uri.AbsoluteUri);
    }

    public Task<Tuple<bool, string>> UploadFileToStorage(Stream fileStream, string fileName)
    {
        throw new NotImplementedException();
    }
}