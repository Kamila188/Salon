using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Магазин_Красавицы.Entities;

namespace Магазин_Красавицы.Utilities
{
    static class Transition
    {
        public static Frame MainFrame { get; set; }
        
        private static MagazineModel _context;
        public static MagazineModel Context
        {
            get
            {
                if (_context == null)
                    _context = new MagazineModel();

                return _context;
            }
        }
    }
}
