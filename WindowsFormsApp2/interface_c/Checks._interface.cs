using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp2
{
    interface Checks_interface
    {
        int CreateQwe(int lect_id, int value, string text);
//        Checks[] add_obj_to_arr(Checks[] array, int id, int lect_id, string text, int value);
        Checks[] arrayCheck(int lect_id);
        (int, int, string, int) SelectInfoCheck(int id);

    }
}
