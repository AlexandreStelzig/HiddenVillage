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
    public abstract class MovableEntityObject : EntityObject
    {

        // movements
        protected Vector2 nextTile;
        protected bool movingUp, movingDown, movingLeft, movingRight;

        // tile map
        public TileMap currentMap;


        public MovableEntityObject(EntityId entityId, Texture2D texture, GameManager gameManager) : base(entityId, texture, gameManager)
        {
            movingUp = movingDown = movingLeft = movingRight = false;
        }


    }
}
