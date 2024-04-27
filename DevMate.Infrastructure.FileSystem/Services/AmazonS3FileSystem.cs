using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using DevMate.Application.Abstractions.FileSystem;
using Microsoft.Extensions.Configuration;

namespace DevMate.Infrastructure.FileSystem.Services;

public class AmazonS3FileSystem : IFileSystem, IDisposable
{
    private readonly AmazonS3Client _amazonS3Client;
    private const string BucketName = "devmate";

    public AmazonS3FileSystem(IConfiguration configuration)
    {
        var credentials = new BasicAWSCredentials(
            configuration.GetSection("AppSettings:AWS:AccessKey").Value,
            configuration.GetSection("AppSettings:AWS:SecretKey").Value
        );

        _amazonS3Client = new AmazonS3Client(credentials, RegionEndpoint.EUNorth1);
    }

    public async Task<string> WriteFile(Stream file, string fileName)
    {
        using var fileTransferUtility = new TransferUtility(_amazonS3Client);
        await fileTransferUtility.UploadAsync(file, BucketName, fileName);

        string fileUrl =
            $"https://devmate.s3.{_amazonS3Client.Config.RegionEndpoint.SystemName}.amazonaws.com/{fileName}";

        return fileUrl;
    }

    public async Task<bool> Exists(string fileName)
    {
        try
        {
            var request = new GetObjectMetadataRequest
            {
                BucketName = BucketName,
                Key = fileName
            };

            await _amazonS3Client.GetObjectMetadataAsync(request);

            return true;
        }
        catch (AmazonS3Exception ex)
        {
            if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                return false;

            throw;
        }
    }

    public void Dispose()
    {
        _amazonS3Client.Dispose();
    }
}