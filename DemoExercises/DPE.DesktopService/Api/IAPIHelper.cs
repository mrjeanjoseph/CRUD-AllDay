﻿using DPE.DesktopUI.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace DPE.DesktopService.Api
{
    public interface IAPIHelper
    {
        HttpClient ApiClient { get; }
        void LogOffUser();
        Task<AuthenticatedUser> Authenticate(string username, string password);
        Task GetLoggedInUserInfo(string token);
    }
}