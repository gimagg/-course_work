using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    interface Teacher_interface
    {
        int Regectration(string name, string password, string subject);
        void AddLecture(string title, string text);
    }
}
