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
        protected ushort ID
        {
            get
            {
                return ID;
            }
            set
            {
                if (value >= 0)
                    ID = value;
            }
        }
        protected string Name
        {
            get
            {
                return Name;
            }

            set
            {
                if (value.Length <= 32)
                    Name = value;
            }
        }

        private string Password { get; set; }

        byte Statement
        {
            get
            {
                return Statement;
            }
            set
            {
                switch (value)
                {
                    default:
                        {
                            Statement = Convert.ToByte(AccountStatements.ACCOUNT_AVAILABLE);
                            break;
                        }
                    case 1:
                        {
                            Statement = Convert.ToByte(AccountStatements.ACCOUNT_INACTIVE);
                            break;
                        }
                    case 2:
                        {
                            Statement = Convert.ToByte(AccountStatements.ACCOUNT_AFK);
                            break;
                        }
                    case 3:
                        {
                            Statement = Convert.ToByte(AccountStatements.ACCOUNT_BLOCKED);
                            break;
                        }
                    case 4:
                        {
                            Statement = Convert.ToByte(AccountStatements.ACCOUNT_ACTIVE);
                            break;
                        }
                }
            }
        }

        protected bool IsAvailable { get; set; }

        public Account(ushort id, string name, string password, bool isAvailable)
        {
            ID = id;
            Name = name;
            IsAvailable = isAvailable;
            Statement = 0;

            var PasswordGenerator = new PasswordGenerator();
            Password = PasswordGenerator.Generate();
        }
    }
}
