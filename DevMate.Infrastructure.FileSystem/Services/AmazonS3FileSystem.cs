using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using DevMate.Application.Abstractions.FileSystem;
using Microsoft.Extensions.Configuration;

namespace DevMate.Infrastructure.FileSystem.Services;

public class AmazonS3FileSystem : IFileSystem, IDisposable
{
    private readonly AmazonS3Client _amazonS3Client;

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
        await fileTransferUtility.UploadAsync(file, "devmate", fileName);
        
        string fileUrl = $"https://devmate.s3.{_amazonS3Client.Config.RegionEndpoint.SystemName}.amazonaws.com/{fileName}";
        
        return fileUrl;
    }

    public bool Exists(string fileName)
    {
        return true;
    }

    public void Dispose()
    {
        _amazonS3Client.Dispose();
    }
}