using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLRapChieuPhim.Classes
{
    internal class ChairRow
    {
        public ChairRow() { }

        int idRow {  get; set; }
        string nameRow { get; set; }

        public ChairRow(int idRow, string nameRow)
        {
            this.idRow = idRow;
            this.nameRow = nameRow;
        }
    }
}
