using System.Collections.Generic;
using System.Text;

namespace SFC.Users.Contract.Query
{
  public interface IUsersPerspective
  {
    User Get(string loginName);
  }
}
