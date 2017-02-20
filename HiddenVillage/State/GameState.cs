using HiddenVillage.Manager;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenVillage.State
{
    public abstract class GameState
    {
        public GameManager gameManager { private set; get; }

        public GameState(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public abstract void Update(double elapsedTime);
        public abstract void Draw(SpriteBatch spriteBatch);

    }
}
