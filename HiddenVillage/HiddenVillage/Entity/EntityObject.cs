using HiddenVillage.Enumeration;
using HiddenVillage.Manager;
using HiddenVillage.Tile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenVillage.Entity
{
    public abstract class EntityObject
    {

        // texture
        public Texture2D texture;
        public float scale;

        // position
        public Vector2 currentTile;
        public Vector2 drawingOffset;

        // manager
        protected GameManager gameManager;

        public EntityObject(EntityId entityId, Texture2D texture, GameManager gameManager)
        {

            this.texture = texture;
            this.gameManager = gameManager;
            scale = 2 * GameManager.gameScale;

            float height = texture.Bounds.Height * scale;
            float width = texture.Bounds.Width * scale;

            drawingOffset = new Vector2((TileEntity.TILE_SIZE - width) / 2, (TileEntity.TILE_SIZE - height));

        }


        public abstract void Update(double elapsedTime);
        public abstract void Draw(SpriteBatch spriteBatch);

    }
}
