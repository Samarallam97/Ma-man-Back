using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSchoolForKids.Core.Models;

public class ResetPasswordResponse
{
    public bool IsResetted { get; set; }

    public string Message { get; set; }
}
