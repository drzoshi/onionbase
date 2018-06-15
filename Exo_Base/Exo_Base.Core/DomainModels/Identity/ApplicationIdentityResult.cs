using System;
using System.Collections.Generic;
using System.Text;

namespace Exo_Base.Core.DomainModels.Identity
{
    public class ApplicationIdentityResult
    {
        public bool Succeeded { get; private set; }
        public IEnumerable<string> Errors { get; private set; }
        public ApplicationIdentityResult(IEnumerable<string> errors, bool succeeded)
        {
            Succeeded = succeeded;
            Errors = errors;
        }
    }
}
