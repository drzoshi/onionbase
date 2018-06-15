using System;
using System.Collections.Generic;
using System.Text;

namespace Exo_Base.Core.Identity
{
    public enum SignInStatus
    {
        Success,
        LockedOut,
        RequiresVerification,
        RequiresTwoFactorAuthentication,
        Failure
    }
}
