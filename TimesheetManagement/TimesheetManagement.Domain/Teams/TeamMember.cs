using TimesheetManagement.Domain.Common;

namespace TimesheetManagement.Domain.Teams;

public class TeamMember : Entity
{
    public Guid TeamId { get; private set; }
    public Guid UserId { get; private set; }

    private TeamMember() { }

    public TeamMember(Guid teamId, Guid userId)
    {
        TeamId = teamId;
        UserId = userId;
    }
}
