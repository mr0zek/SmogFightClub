﻿namespace SFC.Tests.Tools.Api
{
    public class PostUserModel
    {
        public PostUserModel(string email)
        {
            Email = email;
        }

        public string Email { get; set; }
    }
}