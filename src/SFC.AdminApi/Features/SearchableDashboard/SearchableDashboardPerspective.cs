using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using SFC.Infrastructure.Interfaces;
using SFC.SharedKernel;

namespace SFC.AdminApi.Features.SearchableDashboard
{
  class SearchableDashboardPerspective : IWriteDashboardPerspective, ISearchabelDashboardPerspective
  {
    private readonly IDbConnection _connection;

    public SearchableDashboardPerspective(ConnectionString connectionString)
    {
      _connection = new SqlConnection(connectionString.ToString());
    }

    public async Task Add(SearchableDashboardEntry searchableDashboardEntry)
    {
      await _connection.ExecuteAsync(
        @"insert into SearchableDashboard.SearchableDashboard(loginName, alertsCount)
          values(@loginName, @alertsCount)",
        new
        {
          loginName = searchableDashboardEntry?.LoginName?.ToString(),
          alertsCount = searchableDashboardEntry?.AlertsCount
        });
    }

    public async Task<SearchableDashboardEntry> Get(LoginName loginName)
    {
      return await _connection.QueryFirstAsync<SearchableDashboardEntry>(
        @"select id, loginName, alertsCount from SearchableDashboard.SearchableDashboard where loginName = @loginName", new {loginName = loginName.ToString()});
    }

    public async Task Update(SearchableDashboardEntry searchableDashboardEntry)
    {
      await _connection.ExecuteAsync(
        @"update SearchableDashboard.SearchableDashboard 
          set alertsCount = @alertsCount
          where loginName = @loginName",
          new 
          {             
            loginName = searchableDashboardEntry?.LoginName?.ToString(), 
            alertsCount = searchableDashboardEntry?.AlertsCount });
    }

    public async Task<SearchableDashboardResult> Search(SearchableDashboardQueryModel query)
    {
      return new SearchableDashboardResult(await _connection.QueryAsync<SearchableDashboardEntry>(
        @"select id, loginName, alertsCount  
          from SearchableDashboard.SearchableDashboard
          where alertsCount between @alertsMin and @alertsMax
          order by id 
          offset @skip rows 
          fetch next @take rows only", new
        { 
          skip = query.Skip, 
          take = query.Take, 
          alertsMin = query.AlertsMin, 
          alertsMax = query.AlertsMax }));
    }
  }
}