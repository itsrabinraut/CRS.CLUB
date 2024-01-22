using System;

namespace CRS.CLUB.BUSINESS.LogManagement.ErrorLogManagement
{
    public interface IErrorLogManagementBusiness
    {
        string LogError(Exception Exception, string Page);
    }
}
