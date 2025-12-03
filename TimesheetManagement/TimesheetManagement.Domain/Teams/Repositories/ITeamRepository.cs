namespace TimesheetManagement.Domain.Teams.Repositories;
public interface ITeamRepository
{
    Task<Team?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    Task<bool> NameExistsAsync(string name, CancellationToken cancellationToken = default);
    Task AddAsync(Team team, CancellationToken cancellationToken = default);
    Task UpdateAsync(Team team, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Team>> GetAllAsync(CancellationToken cancellationToken = default);
}
