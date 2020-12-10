using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    interface Client_Interface
    {
     
     //   string Name();
     //   string Password();
     //   string Db_string();
       
        string GetHash(string input);
        bool CheckReg(string name);
        (int, int) LogIn(string name, string password);
        (int, string, string, string) SelectInfoUser(int user_id);
        int Regectration(string name, string password);

    }
}
