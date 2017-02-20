using HiddenVillage.ContentHelper;
using HiddenVillage.Enumeration;
using HiddenVillage.Manager;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Serialization;
using HiddenVillage.Data;
using HiddenVillage.Entity;

namespace HiddenVillage.Tile
{
    public class TileMap
    {


        // map
        public TileEntity[,] tileMap { private set; get; }

        public List<TileEntity> tileMapMovableEntities { private set; get; }

        public List<TileEntity> tileMapLayerOne { private set; get; }
        public List<TileEntity> tileMapLayerTwo { private set; get; }
        int mapWidth, mapHeight;

        private float offsetX, offsetY;

        // instances
        private GameManager gameManager;
        private PlayerEntity player;

        public TileMap(GameManager gameManager)
        {
            this.gameManager = gameManager;
            player = PlayerEntity.getInstance(gameManager);

            TileObject[] tiles;
            tiles = gameManager.getContent().Load<TileObject[]>("tilemap/start_tilemap");

            mapWidth = tiles[tiles.Length-1].column + 1;
            mapHeight = tiles[tiles.Length-1].row + 1;

            offsetX = offsetY = 0;

            if (gameManager.getGraphicsDevice().Viewport.Width > mapWidth * TileEntity.TILE_SIZE)
            {
                offsetX = (gameManager.getGraphicsDevice().Viewport.Width - mapWidth * TileEntity.TILE_SIZE) / 2;
            }

            if (gameManager.getGraphicsDevice().Viewport.Height > mapHeight * TileEntity.TILE_SIZE)
            {
                offsetY = (gameManager.getGraphicsDevice().Viewport.Height - mapHeight * TileEntity.TILE_SIZE) / 2;
            }

            tileMap = new TileEntity[mapWidth, mapHeight];
            tileMapMovableEntities = new List<TileEntity>();
            tileMapLayerOne = new List<TileEntity>();
            tileMapLayerTwo = new List<TileEntity>();

            for (int count = 0; count < tiles.Length; count++)
            {
                TileObject currentTileObject = tiles[count];
                int tilePosX = currentTileObject.column;
                int tilePosY = currentTileObject.row;

                // TileId
                if (currentTileObject.tileStatusId.Equals("SOLID")) 
                {
                    tileMap[tilePosX, tilePosY] = new TileEntity(TileId.WALL, TileStatusId.SOLID, tilePosX, tilePosY, gameManager);
                }
                else
                {

                    // To change when tile editor is implemented
                    TileId currentTileId = TileId.GRASS;
                    if (currentTileObject.tileid.Equals("GRASS"))
                        currentTileId = TileId.GRASS;
                    else if (currentTileObject.tileid.Equals("PATH_NORMAL"))
                        currentTileId = TileId.PATH_NORMAL;
                    else if (currentTileObject.tileid.Equals("PATH_CORNER_1"))
                        currentTileId = TileId.PATH_CORNER_1;
                    else if (currentTileObject.tileid.Equals("PATH_CORNER_2"))
                        currentTileId = TileId.PATH_CORNER_2;
                    else if (currentTileObject.tileid.Equals("PATH_CORNER_3"))
                        currentTileId = TileId.PATH_CORNER_3;
                    else if (currentTileObject.tileid.Equals("PATH_CORNER_4"))
                        currentTileId = TileId.PATH_CORNER_4;
                    else if (currentTileObject.tileid.Equals("PATH_LEFT"))
                        currentTileId = TileId.PATH_LEFT;
                    else if (currentTileObject.tileid.Equals("PATH_RIGHT"))
                        currentTileId = TileId.PATH_RIGHT;
                    else if (currentTileObject.tileid.Equals("PATH_UP"))
                        currentTileId = TileId.PATH_UP;
                    else if (currentTileObject.tileid.Equals("PATH_DOWN"))
                        currentTileId = TileId.PATH_DOWN;



                    tileMap[tilePosX, tilePosY] = new TileEntity(currentTileId, TileStatusId.EMPTY, tilePosX, tilePosY, gameManager);
                }
            }

        }



        public void Update(double elapsedTime)
        {

            tileMapLayerOne.Clear();
            tileMapLayerTwo.Clear();

            foreach (TileEntity tile in tileMapMovableEntities)
            {

                if(player.currentTile.Y <= tile.position.Y)
                {
                    tileMapLayerTwo.Add(tile);
                }else
                {
                    tileMapLayerOne.Add(tile);
                }
            }
            
        }

        // !!!!!!!!!!!!!! change for loops parameters (n^2 = bad) !!!!!!!!!!!!!!
        public void DrawLayerOne(SpriteBatch spriteBatch)
        {

            for (int row = 0; row < mapWidth; row++)
            {
                for (int column = 0; column < mapHeight; column++)
                {
                    if (tileMap[row, column] != null)
                        tileMap[row, column].Draw(spriteBatch);
                }
            }

            foreach (TileEntity tile in tileMapLayerOne)
            {
                tile.Draw(spriteBatch);
            }


        }

        public void DrawLayerTwo(SpriteBatch spriteBatch)
        {

            foreach (TileEntity tile in tileMapLayerTwo)
            {
                tile.Draw(spriteBatch);
            }
        }



        public TileEntity getTileAtPosition(int posX, int posY)
        {

            if (posX > mapWidth || posX < 0 || posY < 0 || posY > mapHeight)
                return null;

            return tileMap[posX, posY];
        }


        public int getTileMapWidthPixel()
        {
            return mapWidth * TileEntity.TILE_SIZE;
        }

        public int getTileMapHeightPixel()
        {
            return mapHeight * TileEntity.TILE_SIZE;
        }

        public int getTileMapWidthTile()
        {
            return mapWidth;
        }

        public int getTileMapHeightTile()
        {
            return mapHeight;
        }

    }
}
