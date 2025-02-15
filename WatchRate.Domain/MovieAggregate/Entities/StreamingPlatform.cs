using WatchRate.Domain.Common.Models;
using WatchRate.Domain.StreamingAggregate.ValueObjects;

namespace WatchRate.Domain.StreamingAggregate;

public class StreamingPlatform : Entity<StreamingPlatformId>
{
    public string Name { get; set; }
    public string Url { get; set; }

    private StreamingPlatform() { }
    private StreamingPlatform(StreamingPlatformId streamingPlatformId, string name, string url)
        : base(streamingPlatformId)
    {
        Name = name;
        Url = url;
    }

    public static StreamingPlatform Create(string name, string url)
    {
        return new StreamingPlatform(StreamingPlatformId.CreateUnique(), name, url);
    }
}