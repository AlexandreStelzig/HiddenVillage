using HiddenVillage.Enumeration;
using HiddenVillage.Manager;
using HiddenVillage.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenVillage.Tile
{

    public class TileEntity
    {

        // display size of tiles
        public static readonly int TILE_SIZE = (int) (56 * GameManager.gameScale);

        // id
        public TileId tileId;
        public TileStatusId tileStatusId;

        // display
        public Texture2D texture;
        public Vector2 position;
        public Rectangle rectangle;

        private GameManager gameManager;

        public TileEntity(TileId tileId, TileStatusId tileStatusId, int posX, int posY, GameManager gameManager)
        {
            this.gameManager = gameManager;
            this.tileId = tileId;
            this.tileStatusId = tileStatusId;
            position = new Vector2(posX, posY);

            setTexture(tileId);



            rectangle = new Rectangle(posX * TILE_SIZE, posY * TILE_SIZE, TILE_SIZE, TILE_SIZE);
            
        }

        // To change when tile editor is implemented
        private void setTexture(TileId tileId)
        {
            if (tileId == TileId.WALL)
                this.texture = gameManager.getContent().Load<Texture2D>("tiles/redoutline");
            else if (tileId == TileId.GRASS)
                this.texture = gameManager.getContent().Load<Texture2D>("tiles/plain_grass");
            else if (tileId == TileId.PATH_NORMAL)
                this.texture = ResourceHelper.getInstance().getPathTiles(gameManager)[0];
            else if (tileId == TileId.PATH_CORNER_1)
                this.texture = ResourceHelper.getInstance().getPathTiles(gameManager)[1];
            else if (tileId == TileId.PATH_CORNER_2)
                this.texture = ResourceHelper.getInstance().getPathTiles(gameManager)[2];
            else if (tileId == TileId.PATH_CORNER_3)
                this.texture = ResourceHelper.getInstance().getPathTiles(gameManager)[4];
            else if (tileId == TileId.PATH_CORNER_4)
                this.texture = ResourceHelper.getInstance().getPathTiles(gameManager)[3];
            else if (tileId == TileId.PATH_LEFT)
                this.texture = ResourceHelper.getInstance().getPathTiles(gameManager)[7];
            else if (tileId == TileId.PATH_RIGHT)
                this.texture = ResourceHelper.getInstance().getPathTiles(gameManager)[6];
            else if (tileId == TileId.PATH_UP)
                this.texture = ResourceHelper.getInstance().getPathTiles(gameManager)[5];
            else if (tileId == TileId.PATH_DOWN)
                this.texture = ResourceHelper.getInstance().getPathTiles(gameManager)[8];




            else
                 this.texture = gameManager.getContent().Load<Texture2D>("tiles/blueoutline");
        }

        public void Update(double elapsedTime)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);

        }

    }
}
