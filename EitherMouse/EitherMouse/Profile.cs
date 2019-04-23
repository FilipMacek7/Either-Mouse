using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EitherMouse
{
    class Profile
    {
        public string Name { get; set; }
        public uint Speed { get; set; }
        public uint DoubleClick { get; set; }
        public uint Scroll { get; set; }

        public Profile(string name, uint speed, uint doubleclick, uint scroll)
        {
            Name = name;
            Speed = speed;
            DoubleClick = doubleclick;
            Scroll = scroll;
        }
    }
}
