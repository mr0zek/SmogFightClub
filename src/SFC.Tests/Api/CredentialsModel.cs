﻿namespace SFC.Tests.Api
{
    public class CredentialsModel
    {
        public CredentialsModel(string loginName, string password)
        {
            LoginName = loginName;
            Password = password;
        }

        public string Password { get; set; }
        public string LoginName { get; }
    }
}