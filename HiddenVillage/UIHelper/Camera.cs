using HiddenVillage.Entity;
using HiddenVillage.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenVillage.UIHelper
{
    class Camera
    {

        // Camera
        public Matrix transform;
        private Viewport view;
        public Vector2 centre;

        // game manager
        private GameManager gameManager;

        // screen size
        private float screenWidth;
        private float screenHeight;
        float height;
        float width;

        // player
        private PlayerEntity player;

        // Singleton
        private static Camera instance;

        public static Camera getInstance(GameManager gameManager)
        {
            if (instance == null)
                instance = new Camera(gameManager, gameManager.getGraphicsDevice().Viewport);
            return instance;
        }


        private Camera(GameManager gameManager, Viewport newView)
        {
            view = newView;
            this.gameManager = gameManager;
            centre = Vector2.Zero;

            player = PlayerEntity.getInstance(gameManager);
            height = player.texture.Bounds.Height * player.scale;
            width = player.texture.Bounds.Width * player.scale;

            screenWidth = gameManager.getGraphicsDevice().Viewport.Width;
            screenHeight = gameManager.getGraphicsDevice().Viewport.Height;

        }

        public void Update(double elapsedTime)
        {

            float playerX = player.location.X + (width / 2) - player.drawingOffset.X;
            float playerY = player.location.Y + (height / 2) - player.drawingOffset.Y;

            centre.X = playerX - screenWidth / 2;
            centre.Y = playerY - screenHeight / 2;

            // x boundaries
            if (playerX < screenWidth / 2)
            {
                centre.X = 0;
            }
            else if (playerX > player.currentMap.getTileMapWidthPixel() - screenWidth / 2)
            {
                if (player.currentMap.getTileMapWidthPixel() < screenWidth)
                {
                    centre.X = 0;
                }
                else
                {
                    centre.X = player.currentMap.getTileMapWidthPixel() - screenWidth;
                }

            }

            // y boundaries
            if (playerY < screenHeight / 2)
            {
                centre.Y = 0;
            }
            else if (playerY > player.currentMap.getTileMapHeightPixel() - screenHeight / 2)
            {

                if (player.currentMap.getTileMapHeightPixel() < screenHeight)
                {
                    centre.Y = 0;
                }
                else
                {
                    centre.Y = player.currentMap.getTileMapHeightPixel() - screenHeight;
                }
            }



            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) * Matrix.CreateTranslation(new Vector3(-centre.X, -centre.Y, 0));

        }




    }
}
