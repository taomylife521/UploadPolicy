using ND.PolicyUploadService.DtoModel.CompleteUploadPolicy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ND.PolicyService.Core.PolicyCore
{
    public interface ICompleteUploadPolicy
    {
        CompleteUploadPolicyResponse SearchCompleteUploadPolicy();
    }
}
