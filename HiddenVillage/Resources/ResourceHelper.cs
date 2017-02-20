using HiddenVillage.ContentHelper;
using HiddenVillage.Manager;
using HiddenVillage.Tile;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenVillage.Resources
{

    public class ResourceHelper
    {

        // path spreed sheat
        private List<Texture2D> pathSS;

        private static ResourceHelper instance;

        public static ResourceHelper getInstance()
        {
            if (instance == null)
                instance = new ResourceHelper();
            return instance;
        }

        private ResourceHelper()
        {
        }

        // To change when tile editor is implemented
        public List<Texture2D> getPathTiles(GameManager gameManager)
        {

            if (pathSS == null)
                pathSS = ImageHelper.getSubImages(gameManager, "tiles/path_spreedsheat", 0, 1, 0, 9, 16, 16);

            return pathSS;
        }


    }
}
