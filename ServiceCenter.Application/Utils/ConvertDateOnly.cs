using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceCenter.Application.Utils;

public static class ConvertDateOnly
{
    public static (DateTime, DateTime) ToDateTime(DateOnly startDate, DateOnly endDate)
    {
        DateTime startDateTime = startDate.ToDateTime(new TimeOnly(0, 0));
        DateTime endDateTime = endDate.ToDateTime(new TimeOnly(23, 59, 59));

        return (startDateTime, endDateTime);
    }
}
