using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HiddenVillage.Data
{
    [Serializable()]
    public class TileObject
    {
        public int row;
        public int column;
        public string tileid;
        public string tileStatusId;

        public TileObject() { }
        public TileObject(int row, int column, string tileid, string tileStatusId)
        {
            this.row = row;
            this.column = column;
            this.tileid = tileid;
            this.tileStatusId = tileStatusId;
        }

    }
}
