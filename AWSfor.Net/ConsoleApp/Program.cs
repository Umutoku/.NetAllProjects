using Amazon.S3;

var s3client = new AmazonS3Client(); // sayesinde s3 servisine erişim sağlanır

var buckets = await s3client.ListBucketsAsync(); // s3 servisindeki tüm bucket'ları listeler

foreach (var bucket in buckets.Buckets)
{
    Console.WriteLine(bucket.BucketName);
}
