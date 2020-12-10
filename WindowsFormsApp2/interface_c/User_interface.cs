using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    interface User_interface
    {
        int Regectration(string name, string password);
        (int lec_id, string subject, string text, string title, string username) GetLecture(string title);
        int GetProgress(int user_id);

    }
}
