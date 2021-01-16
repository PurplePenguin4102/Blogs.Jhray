using System;
using System.Collections.Generic;
using System.Net;

namespace Blogs.Jhray.Persistence.Database.Entities
{
    public partial class Users
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string EncryptedPassword { get; set; }
        public string ResetPasswordToken { get; set; }
        public DateTime? ResetPasswordSentAt { get; set; }
        public DateTime? RememberCreatedAt { get; set; }
        public int SignInCount { get; set; }
        public DateTime? CurrentSignInAt { get; set; }
        public DateTime? LastSignInAt { get; set; }
        public IPAddress CurrentSignInIp { get; set; }
        public IPAddress LastSignInIp { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool? Admin { get; set; }
    }
}
