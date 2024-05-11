using HTI.Core.Entities;
using HTI.Core.RepositoriesContract;
using HTI.Repository.Data;

public class GraduationRepository : IGraduationRepository
{
    private readonly StoreContext _context;

    public GraduationRepository(StoreContext context)
    {
        _context = context;
    }

    public async Task AddGraduationAsync(Team registration)
    {
        // Create a new Team entity
        var team = new Team
        {
            Id = registration.Id,
            HasTeam = registration.HasTeam,
            NumberOfStudents = registration.HasTeam ? registration.TeamMembers.Count : 1
        };

        // Add the team to the database
        _context.Teams.Add(team);
        await _context.SaveChangesAsync();

        // Create TeamMember entities for each team member
        foreach (var teamMember in registration.TeamMembers)
        {
            var teamMemberEntity = new TeamMember
            {
                TeamId = team.Id,
                Name = teamMember.Name,
                Role = teamMember.Role,
                Semester = teamMember.Semester
            };

            // Add the team member to the database
            _context.TeamMembers.Add(teamMemberEntity);
        }

        // Save changes to the database
        await _context.SaveChangesAsync();
    }
}