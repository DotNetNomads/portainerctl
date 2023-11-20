using System.Collections.Generic;
using System.Linq;
using PortainerClient.Api.Base;
using PortainerClient.Api.Model;

namespace PortainerClient.Api;

/// <summary>
/// Different APIs of Portainer
/// </summary>
public class PortainerApiService : BaseApiService
{
    /// <summary>
    /// Get memberships of user
    /// </summary>
    /// <param name="userId">User identifier</param>
    /// <returns>List of memberships</returns>
    public IEnumerable<Membership> GetMemberships(int userId)
    {
        var teams = Get<List<Team>>("teams");
        return Get<List<Membership>>($"users/{userId}/memberships").Select(m =>
        {
            m.TeamName = teams.First(t => t.Id == m.TeamId).Name;
            return m;
        });
    }
}
