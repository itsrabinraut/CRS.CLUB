using System;

namespace CRS.CLUB.REPOSITORY.LogManagement.ErrorLogManagement
{
    public interface IErrorLogManagementRepository
    {
        string LogError(Exception Exception, string Page);
    }
}
