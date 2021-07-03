using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdminControls.Classes.Extras;

namespace AdminControls.Classes.Core
{
    enum AccountStatements
    {
        ACCOUNT_AVAILABLE = 1,
        ACCOUNT_INACTIVE,
        ACCOUNT_AFK,
        ACCOUNT_BLOCKED,
        ACCOUNT_ACTIVE
    }


    class Account
    {
        protected ushort Id
        {
            get => Id;
            set
            {
                if (value >= 0)
                    Id = value;
            }
        }
        protected string Username
        {
            get => Username;
            set
            {
                if (value.Length <= 32)
                    Username = value;
            }
        }

        private string Password { get; set; }

        AccountStatements Statement { get => Statement; set => ChangeStatement(value); }
        protected bool IsAvailable { get; set; }

        public Account(ushort Id, string Username, bool IsAvailable, AccountStatements Statement = AccountStatements.ACCOUNT_AVAILABLE)
        {
            this.Id = Id;
            this.Username = Username;
            this.IsAvailable = IsAvailable;
            this.Statement = Statement;


            var PasswordGenerator = new PasswordGenerator();
            Password = PasswordGenerator.Generate();
        }

        private void ChangeStatement(AccountStatements value)
        {
            switch (value)
            {
                case AccountStatements.ACCOUNT_AVAILABLE:
                    {
                        Statement = AccountStatements.ACCOUNT_AVAILABLE;
                        break;
                    }
                case AccountStatements.ACCOUNT_ACTIVE:
                    {
                        Statement = AccountStatements.ACCOUNT_ACTIVE;
                        break;
                    }
                case AccountStatements.ACCOUNT_AFK:
                    {
                        Statement = AccountStatements.ACCOUNT_AFK;
                        break;
                    }
                case AccountStatements.ACCOUNT_BLOCKED:
                    {
                        Statement = AccountStatements.ACCOUNT_BLOCKED;
                        break;
                    }
                case AccountStatements.ACCOUNT_INACTIVE:
                    {
                        Statement = AccountStatements.ACCOUNT_INACTIVE;
                        break;
                    }
                default:
                    Console.WriteLine("Not valid statement");
                    break;
            }
        }
    }
}
