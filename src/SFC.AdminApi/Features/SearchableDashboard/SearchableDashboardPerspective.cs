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
        @"insert into SearchableDashboard.SearchableDashboard(loginName, alertsCount)
          values(@loginName, @alertsCount)",
        new
        {
          loginName = searchableDashboardEntry.LoginName.ToString(),
          alertsCount = searchableDashboardEntry.AlertsCount
        });
    }

    public SearchableDashboardEntry Get(LoginName loginName)
    {
      return _connection.QueryFirst<SearchableDashboardEntry>(
        @"select id, loginName, alertsCount from SearchableDashboard.SearchableDashboard where loginName = @loginName", new {loginName = loginName.ToString()});
    }

    public void Update(SearchableDashboardEntry searchableDashboardEntry)
    {
      _connection.Execute(
        @"update SearchableDashboard.SearchableDashboard 
          set alertsCount = @alertsCount
          where loginName = @loginName",
          new 
          {             
            loginName = searchableDashboardEntry.LoginName.ToString(), 
            alertsCount = searchableDashboardEntry.AlertsCount });
    }

    public SearchableDashboardResult Search(SearchableDashboardQueryModel query)
    {
      return new SearchableDashboardResult(_connection.Query<SearchableDashboardEntry>(
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