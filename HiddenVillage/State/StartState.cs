using HiddenVillage.Manager;
using HiddenVillage.Tile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using HiddenVillage.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenVillage.State
{
    public class StartState : GameState
    {

        PlayerEntity player;
        TileMap tileMap;
        
        public StartState(GameManager gameManager) : base(gameManager)
        {
            player = PlayerEntity.getInstance(gameManager);
            tileMap = new TileMap(gameManager);
            player.setTileMap(tileMap);

        }



        public override void Update(double elapsedTime)
        {
            tileMap.Update(elapsedTime);
            player.Update(elapsedTime);


        }


        public override void Draw(SpriteBatch spriteBatch)
        {

            tileMap.DrawLayerOne(spriteBatch);
            player.Draw(spriteBatch);
            tileMap.DrawLayerTwo(spriteBatch);
        }

    }
}
