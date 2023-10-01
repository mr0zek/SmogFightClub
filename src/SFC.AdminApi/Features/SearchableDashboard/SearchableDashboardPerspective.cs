using System.Data;
using System.Data.SqlClient;
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

    public void Add(SearchableDashboardEntry searchableDashboardEntry)
    {
      _connection.Execute(
        @"insert into SearchableDashboard.SearchableDashboard(loginName, alertCount)
          values(@loginName, @alertCount)",
        new
        {
          loginName = searchableDashboardEntry.LoginName.ToString(),
          alertCount = searchableDashboardEntry.AlertsSentCount
        });
    }

    public SearchableDashboardEntry Get(LoginName loginName)
    {
      return _connection.QueryFirst<SearchableDashboardEntry>(
        @"select id, loginName, alertCount from SearchableDashboard.SearchableDashboard where loginName = @loginName", new {loginName = loginName.ToString()});
    }

    public void Update(SearchableDashboardEntry searchableDashboardEntry)
    {
      _connection.Execute(
        @"update SearchableDashboard.SearchableDashboard 
          set alertCount = @alertCount
          where loginName = @loginName",
          new 
          {             
            loginName = searchableDashboardEntry.LoginName.ToString(), 
            alertCount = searchableDashboardEntry.AlertsSentCount });
    }

    public SearchableDashboardResult Search(SearchableDashboardQueryModel query)
    {
      return new SearchableDashboardResult(_connection.Query<SearchableDashboardEntry>(
        @"select id, loginName 
          from SearchableDashboard.SearchableDashboard
          where alertCount > @alertsCountGreaterThan
          order by id 
          offset @skip rows 
          fetch next @take rows only", new { query.Skip, query.Take, query.AlertsCountGreaterThan }));
    }
  }
}