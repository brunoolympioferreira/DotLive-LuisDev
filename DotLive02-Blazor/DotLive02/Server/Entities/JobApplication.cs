namespace DotLive02.Server.Entities;

public class JobApplication
{
    public JobApplication(Guid id, string userId, Guid jobId)
    {
        Id = id;
        UserId = userId;
        JobId = jobId;

        AplliedAt = DateTime.Now;
    }

    public Guid Id { get; private set; }
    public string UserId { get; private set; }
    public Guid JobId { get; private set; }
    public Job Job { get; private set; }
    public DateTime AplliedAt { get; private set; }
}
