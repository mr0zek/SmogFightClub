using System.Data;
using System.Data.SqlClient;
using Dapper;
using SFC.SharedKernel;

namespace SFC.AdminApi.Features.SearchableDashboard
{
  class DashboardPerspective : IWriteDashboardPerspective, ISearchabelDashboardPerspective
  {
    private readonly IDbConnection _connection;

    public DashboardPerspective(string connectionString)
    {
      _connection = new SqlConnection(connectionString);
    }

    public void Add(SearchableDashboardEntry searchableDashboardEntry)
    {
      _connection.Execute(
        @"insert into SearchableDashboard.SearchableDashboard(id, loginName, alertsCount)
          values(@loginName, @alertsCount)",
        new
        {
          loginName = searchableDashboardEntry.LoginName,
          alertsCount = searchableDashboardEntry.AlertsSentCount
        });
    }

    public SearchableDashboardEntry Get(LoginName loginName)
    {
      return _connection.QueryFirst<SearchableDashboardEntry>(
        @"select id, loginName, alertsCount where loginName = @loginName", new {loginName});
    }

    public void Update(SearchableDashboardEntry searchableDashboardEntry)
    {
      _connection.Execute(
        @"update SearchableDashboard.SearchableDashboard 
          set loginName = @loginName, alertsCount = @alertsCount)
          where id = @id",
        searchableDashboardEntry);
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