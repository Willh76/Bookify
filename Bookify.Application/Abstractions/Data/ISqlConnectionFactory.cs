using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Bookify.Application.Abstractions.Data;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}
