using n.work.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace n.work.Models
{
  public class VerificationResponse
  {
    public string AuthToken { get; set; }

    public Account Account { get; set; }

    public WorkerAccount WorkerAccount { get; set; }

    public VerificationResponse(Account Account,string AuthToken)
    {
      this.Account = Account;
      this.AuthToken = AuthToken;
    }

    public VerificationResponse(WorkerAccount WorkerAccount, string AuthToken)
    {
      this.WorkerAccount = WorkerAccount;
      this.AuthToken = AuthToken;
    }
  }
}
