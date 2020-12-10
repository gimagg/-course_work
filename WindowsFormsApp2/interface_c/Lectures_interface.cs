using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    interface Lectures_interface
    {
        int GetLecId(string before);
        void AddLecture(string text, int id_user, string title, string before);
        Lectures[] add_obj_to_arr(Lectures[] array, int id, string before, string title);
        Lectures[] arrayLecture();
        Lectures[] arrayLecture(int before);
        void AppProgress(int user_id, int lecture);
        int GetProgress(int user_id);
    }
}
