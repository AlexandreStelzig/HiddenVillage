using HiddenVillage.Enumeration;
using HiddenVillage.State;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HiddenVillage.UIHelper
{
    public class GameButton
    {

        private Texture2D normalTexture, hoverTexture;

        private Rectangle normalRectangle;
        private Rectangle hoverRectangle;

        private GameState gameState;
        public GameButtonEnum buttonName { private set; get; }

        private bool hovering;

        public GameButton(GameState gameState, GameButtonEnum buttonName, Texture2D normalTexture, Texture2D hoverTexture, int posX, int posY, int width, int height, double hoverScale)
        {
            this.gameState = gameState;
            this.buttonName = buttonName;
            this.normalTexture = normalTexture;
            this.hoverTexture = hoverTexture;

            normalRectangle = new Rectangle(posX, posY, width, height);
            hoverRectangle = new Rectangle((int)(posX - (width * hoverScale - width) / 2), (int)(posY - (height * hoverScale - height) / 2), (int)(width * hoverScale), (int)(height * hoverScale));


            hovering = false;
        }

        public GameButton(GameState gameState, GameButtonEnum buttonName, Texture2D normalTexture, int posX, int posY, int width, int height, double hoverScale)
        {
            this.gameState = gameState;
            this.buttonName = buttonName;
            this.normalTexture = normalTexture;
            this.hoverTexture = normalTexture;

            normalRectangle = new Rectangle(posX, posY, width, height);
            hoverRectangle = new Rectangle((int)(posX - (width * hoverScale - width) / 2), (int)(posY - (height * hoverScale - height) / 2), (int)(width * hoverScale), (int)(height * hoverScale));


            hovering = false;
        }

        public GameButton(GameState gameState, GameButtonEnum buttonName, Texture2D normalTexture, Texture2D hoverTexture, int posX, int posY, int width, int height)
        {
            this.gameState = gameState;
            this.buttonName = buttonName;
            this.normalTexture = normalTexture;
            this.hoverTexture = hoverTexture;

            normalRectangle = new Rectangle(posX, posY, width, height);
            hoverRectangle = normalRectangle;


            hovering = false;
        }

        public GameButton(GameState gameState, GameButtonEnum buttonName, Texture2D normalTexture, int posX, int posY, int width, int height)
        {
            this.gameState = gameState;
            this.buttonName = buttonName;
            this.normalTexture = normalTexture;
            this.hoverTexture = normalTexture;

            normalRectangle = new Rectangle(posX, posY, width, height);
            hoverRectangle = normalRectangle;

            hovering = false;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (!hovering)
                spriteBatch.Draw(normalTexture, normalRectangle, Color.White);
            else
                spriteBatch.Draw(hoverTexture, hoverRectangle, Color.White);
        }

        public void setButtonHovering(bool isHovering)
        {
            hovering = isHovering;
        }


    }
}
