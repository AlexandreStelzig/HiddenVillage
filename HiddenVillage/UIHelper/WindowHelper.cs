using HiddenVillage.Manager;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenVillage.UIHelper
{
    public static class WindowHelper
    {



        public static int getCoordForMiddleHorizontal(GameManager gameManager, int width)
        {

            int screenWidth = gameManager.getGraphicsDevice().Viewport.Width;


            return (screenWidth / 2) - (width / 2);

        }

        public static int getCoordForMiddleVertical(GameManager gameManager, int height)
        {

            int screenHeight = gameManager.getGraphicsDevice().Viewport.Height;
            
            return (screenHeight / 2) - (height / 2);

        }



    }
}
