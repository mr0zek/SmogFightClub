using SFC.SharedKernel;
using System;

namespace SFC.Alerts.Features.VerifySmogExceedence
{
  class Alert
  {
    public Alert(Guid id, string zipCode, string loginName) : this(id, (ZipCode)zipCode, (LoginName)loginName)
    {
    }
    public Alert(Guid id, ZipCode zipCode, LoginName loginName)
    {
      LoginName = loginName;
      Id = id;
      ZipCode = zipCode;
    }

    public LoginName LoginName { get; set; }

    public Guid Id { get; set; }

    public ZipCode ZipCode { get; set; }  
  }
}