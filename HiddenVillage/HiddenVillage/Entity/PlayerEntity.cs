using HiddenVillage.ContentHelper;
using HiddenVillage.Enumeration;
using HiddenVillage.InputHelper;
using HiddenVillage.Manager;
using HiddenVillage.Tile;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenVillage.Entity
{

    public class PlayerEntity : MovableEntityObject
    {


        // movement

        private enum PlayerFacing
        {
            FACING_UP, FACING_DOWN, FACING_LEFT, FACING_RIGHT
        }

        private enum PlayerNextMovement
        {
            UP, DOWN, LEFT, RIGHT, NONE
        }
        private List<PlayerNextMovement> movementStack;

        // input delay for movement
        private double inputElapsedTime;
        private double movementDelay = 0.30;

        private readonly double walking_speed = 0.30;
        private readonly double running_speed = 0.15;


        // animation
        public Vector2 location;
        private List<Texture2D> iddleAnimationSubImages;

        private EntityAnimation movingRightAnimation;
        private EntityAnimation movingLeftAnimation;
        private EntityAnimation movingUpAnimation;
        private EntityAnimation movingDownAnimation;

        private PlayerFacing playerFacing;

        // keyboard input
        private bool keyUp, keyDown, keyLeft, keyRight;

        // Singleton
        private static PlayerEntity instance;

        public static PlayerEntity getInstance(GameManager gameManager)
        {
            if (instance == null)
                instance = new PlayerEntity(gameManager);
            return instance;
        }


        private PlayerEntity(GameManager gameManager) : base(EntityId.PLAYER, gameManager.getContent().Load<Texture2D>("character/player"), gameManager)
        {
            currentTile = new Vector2(4, 4);
            nextTile = currentTile;
            keyUp = keyDown = keyLeft = keyRight = false;

            location = new Vector2(currentTile.X * TileEntity.TILE_SIZE, currentTile.Y * TileEntity.TILE_SIZE);

            movementStack = new List<PlayerNextMovement>();


            iddleAnimationSubImages = ImageHelper.getSubImages(gameManager, "character/playerOne", 0, 1, 0, 4, 33, 48);

            movingRightAnimation = new EntityAnimation(ImageHelper.getSubImages(gameManager, "character/playerOne", 2, 3, 0, 4, 32, 48), (float)movementDelay / 2);
            movingLeftAnimation = new EntityAnimation(ImageHelper.getSubImages(gameManager, "character/playerOne", 1, 2, 0, 4, 32, 48), (float)movementDelay / 2);
            movingUpAnimation = new EntityAnimation(ImageHelper.getSubImages(gameManager, "character/playerOne", 3, 4, 0, 4, 32, 48), (float)movementDelay / 2);
            movingDownAnimation = new EntityAnimation(ImageHelper.getSubImages(gameManager, "character/playerOne", 0, 1, 0, 4, 32, 48), (float)movementDelay / 2);

            texture = movingDownAnimation.getAnimation();
            playerFacing = PlayerFacing.FACING_DOWN;

            float height = texture.Bounds.Height * scale;
            float width = texture.Bounds.Width * scale;
            drawingOffset = new Vector2((TileEntity.TILE_SIZE - width) / 2, (TileEntity.TILE_SIZE - height));

        }


        public override void Update(double elapsedTime)
        {

            // is moving
            if (inputElapsedTime <= movementDelay && currentTile != nextTile)
            {

                inputElapsedTime += elapsedTime;
                moveCharacter(inputElapsedTime / movementDelay, elapsedTime);

            }
            // not moving = find next position
            else
            {
                if (KeyboardInput.Instance.getPressedState(KeyboadInputEnum.ACTION))
                {
                    selectTile();
                }
                else if (KeyboardInput.Instance.getPressedState(KeyboadInputEnum.CONTROL))
                {
                    changedDirection();
                }
                else
                {
                    getNextAction();
                }


            }
        }

        private void changedDirection()
        {
            KeyboardInput currentState = KeyboardInput.Instance;

            if (currentState.getPressedState(KeyboadInputEnum.RIGHT))
            {
                playerFacing = PlayerFacing.FACING_RIGHT;
                texture = movingRightAnimation.getAnimation();

            }
            if (currentState.getPressedState(KeyboadInputEnum.LEFT))
            {
                playerFacing = PlayerFacing.FACING_LEFT;
                texture = movingLeftAnimation.getAnimation();
            }
            if (currentState.getPressedState(KeyboadInputEnum.UP))
            {
                playerFacing = PlayerFacing.FACING_UP;
                texture = movingUpAnimation.getAnimation();
            }

            if (currentState.getPressedState(KeyboadInputEnum.DOWN))
            {
                playerFacing = PlayerFacing.FACING_DOWN;
                texture = movingDownAnimation.getAnimation();
            }


        }

        private void selectTile()
        {
            int tileSelectedPositionX = (int)currentTile.X;
            int tileSelectedPositionY = (int)currentTile.Y;

            if (playerFacing == PlayerFacing.FACING_RIGHT)
            {
                tileSelectedPositionX += 1;
            }
            else if (playerFacing == PlayerFacing.FACING_LEFT)
            {
                tileSelectedPositionX -= 1;
            }
            else if (playerFacing == PlayerFacing.FACING_DOWN)
            {
                tileSelectedPositionY += 1;
            }
            else if (playerFacing == PlayerFacing.FACING_UP)
            {
                tileSelectedPositionY -= 1;
            }

            TileEntity selectedTile = currentMap.getTileAtPosition(tileSelectedPositionX, tileSelectedPositionY);

            if (selectedTile != null)
                selectedTile.texture = gameManager.getContent().Load<Texture2D>("pixel/blackpixel");

        }

        private void getNextAction()
        {
            // get next position if delay done
            getKeyboardInput();


            getNextPosition();
            collisionDetection();

            if (nextTile != currentTile)
            {
                inputElapsedTime = 0;
            }
            else
            {
                movingDownAnimation.setCurrentAnimation(0);
                movingUpAnimation.setCurrentAnimation(0);
                movingRightAnimation.setCurrentAnimation(0);
                movingLeftAnimation.setCurrentAnimation(0);
            }


        }

        private void moveCharacter(double movementElapsed, double elapsedTime)
        {

            if (movementStack.Count > 0)
            {

                PlayerNextMovement nextDirection = getNextMovementPlayer();
                // continue next or last direction
                if (nextDirection == PlayerNextMovement.RIGHT)
                {

                    movingRightAnimation.Update(elapsedTime);

                    location.X = (float)((nextTile.X - 1) * TileEntity.TILE_SIZE + TileEntity.TILE_SIZE * (movementElapsed));

                    if (location.X > nextTile.X * TileEntity.TILE_SIZE)
                    {
                        location.X = nextTile.X * TileEntity.TILE_SIZE;
                        currentTile = nextTile;
                        getNextAction();

                        nextDirection = getNextMovementPlayer();
                        if (nextDirection != PlayerNextMovement.RIGHT)
                            movingRightAnimation.setCurrentAnimation(0);

                    }

                    texture = movingRightAnimation.getAnimation();
                }
                else if (nextDirection == PlayerNextMovement.LEFT)
                {
                    movingLeftAnimation.Update(elapsedTime);


                    //texture = iddleAnimationSubImages[1];

                    location.X = (float)((nextTile.X + 1) * TileEntity.TILE_SIZE - TileEntity.TILE_SIZE * (movementElapsed));

                    if (location.X < nextTile.X * TileEntity.TILE_SIZE)
                    {
                        location.X = nextTile.X * TileEntity.TILE_SIZE;
                        currentTile = nextTile;
                        getNextAction();

                        nextDirection = getNextMovementPlayer();
                        if (nextDirection != PlayerNextMovement.LEFT)
                            movingLeftAnimation.setCurrentAnimation(0);
                    }

                    texture = movingLeftAnimation.getAnimation();
                }
                else if (nextDirection == PlayerNextMovement.UP)
                {
                    movingUpAnimation.Update(elapsedTime);


                    //texture = iddleAnimationSubImages[3];

                    location.Y = (float)((nextTile.Y + 1) * TileEntity.TILE_SIZE - TileEntity.TILE_SIZE * (movementElapsed));

                    if (location.Y < nextTile.Y * TileEntity.TILE_SIZE)
                    {
                        location.Y = nextTile.Y * TileEntity.TILE_SIZE;
                        currentTile = nextTile;
                        getNextAction();

                        nextDirection = getNextMovementPlayer();
                        if (nextDirection != PlayerNextMovement.UP)
                            movingUpAnimation.setCurrentAnimation(0);
                    }

                    texture = movingUpAnimation.getAnimation();
                }
                else if (nextDirection == PlayerNextMovement.DOWN)
                {
                    movingDownAnimation.Update(elapsedTime);


                    //texture = iddleAnimationSubImages[0];

                    location.Y = (float)((nextTile.Y - 1) * TileEntity.TILE_SIZE + TileEntity.TILE_SIZE * (movementElapsed));

                    if (location.Y > nextTile.Y * TileEntity.TILE_SIZE)
                    {
                        location.Y = nextTile.Y * TileEntity.TILE_SIZE;
                        currentTile = nextTile;
                        getNextAction();

                        nextDirection = getNextMovementPlayer();
                        if (nextDirection != PlayerNextMovement.DOWN)
                            movingDownAnimation.setCurrentAnimation(0);
                    }

                    texture = movingDownAnimation.getAnimation();

                }

            }

        }

        private void getKeyboardInput()
        {
            KeyboardInput currentState = KeyboardInput.Instance;


            if (currentState.getPressedState(KeyboadInputEnum.RIGHT))
            {
                keyRight = true;

            }
            else
            {
                keyRight = false;
                if (movementStack.Contains(PlayerNextMovement.RIGHT))
                    movementStack.Remove(PlayerNextMovement.RIGHT);
            }
            if (currentState.getPressedState(KeyboadInputEnum.LEFT))
            {
                keyLeft = true;
            }
            else
            {
                keyLeft = false;
                if (movementStack.Contains(PlayerNextMovement.LEFT))
                    movementStack.Remove(PlayerNextMovement.LEFT);
            }
            if (currentState.getPressedState(KeyboadInputEnum.UP))
            {
                keyUp = true;
            }
            else
            {
                keyUp = false;
                if (movementStack.Contains(PlayerNextMovement.UP))
                    movementStack.Remove(PlayerNextMovement.UP);
            }
            if (currentState.getPressedState(KeyboadInputEnum.DOWN))
            {
                keyDown = true;
            }
            else
            {
                keyDown = false;
                if (movementStack.Contains(PlayerNextMovement.DOWN))
                    movementStack.Remove(PlayerNextMovement.DOWN);
            }
            

            if (currentState.getPressedState(KeyboadInputEnum.SHIFT))
            {

                if (movementDelay != running_speed)
                {
                    movementDelay = running_speed;
                    movingRightAnimation.setDelay((float)movementDelay/2);
                    movingLeftAnimation.setDelay((float)movementDelay / 2);
                    movingUpAnimation.setDelay((float)movementDelay / 2);
                    movingDownAnimation.setDelay((float)movementDelay / 2);
                }

            }
            else
            {
                if (movementDelay != walking_speed)
                {
                    movementDelay = walking_speed;
                    movingRightAnimation.setDelay((float)movementDelay / 2);
                    movingLeftAnimation.setDelay((float)movementDelay / 2);
                    movingUpAnimation.setDelay((float)movementDelay / 2);
                    movingDownAnimation.setDelay((float)movementDelay / 2);
                }
                
            }


        }


        private void getNextPosition()
        {

            // no input
            if (!keyDown && !keyUp && !keyRight && !keyLeft)
            {
                nextTile = currentTile;
                return;
            }


            // new direction?
            if (keyRight && !movementStack.Contains(PlayerNextMovement.RIGHT) && !movementStack.Contains(PlayerNextMovement.LEFT))
            {
                movementStack.Add(PlayerNextMovement.RIGHT);
            }
            else if (keyLeft && !movementStack.Contains(PlayerNextMovement.LEFT) && !movementStack.Contains(PlayerNextMovement.RIGHT))
            {
                movementStack.Add(PlayerNextMovement.LEFT);
            }
            else if (keyUp && !movementStack.Contains(PlayerNextMovement.UP))
            {
                movementStack.Add(PlayerNextMovement.UP);
            }
            else if (keyDown && !movementStack.Contains(PlayerNextMovement.DOWN))
            {
                movementStack.Add(PlayerNextMovement.DOWN);
            }

            PlayerNextMovement nextDirection = getNextMovementPlayer();

            // continue next or last direction
            if (nextDirection == PlayerNextMovement.RIGHT)
            {
                texture = movingRightAnimation.getAnimation();
                nextTile.X += 1;
                playerFacing = PlayerFacing.FACING_RIGHT;
            }
            else if (nextDirection == PlayerNextMovement.LEFT)
            {
                texture = movingLeftAnimation.getAnimation();
                nextTile.X -= 1;
                playerFacing = PlayerFacing.FACING_LEFT;
            }
            else if (nextDirection == PlayerNextMovement.UP)
            {
                texture = movingUpAnimation.getAnimation();
                nextTile.Y -= 1;
                playerFacing = PlayerFacing.FACING_UP;
            }
            else if (nextDirection == PlayerNextMovement.DOWN)
            {
                texture = movingDownAnimation.getAnimation();
                nextTile.Y += 1;
                playerFacing = PlayerFacing.FACING_DOWN;
            }

        }


        private void collisionDetection()
        {

            // boundaries
            if (nextTile.X < 0)
            {
                nextTile.X = currentTile.X;
            }
            else if (nextTile.X > currentMap.getTileMapWidthTile() - 1)
            {
                nextTile.X = currentTile.X;
            }
            if (nextTile.Y < 0)
            {
                nextTile.Y = currentTile.Y;
            }
            else if (nextTile.Y > currentMap.getTileMapHeightTile() - 1)
            {
                nextTile.Y = currentTile.Y;
            }


            // tile map
            if (currentMap.getTileAtPosition((int)nextTile.X, (int)nextTile.Y).tileStatusId == TileStatusId.SOLID)
            {
                nextTile = currentTile;
            }

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, location + drawingOffset, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);

            //Rectangle temp = new Rectangle((int) position.X, (int)( position.Y + (dimensions.Y * collisionHeight)), (int)dimensions.X, (int) (dimensions.Y - (dimensions.Y * collisionHeight)));
            //spriteBatch.Draw(texture, temp, Color.White);

        }

        public void setTileMap(TileMap tileMap)
        {
            currentMap = tileMap;
        }

        private PlayerNextMovement getNextMovementPlayer()
        {

            if (movementStack.Count > 0)
            {
                return movementStack[movementStack.Count - 1];
            }
            else
            {
                return PlayerNextMovement.NONE;
            }
        }

    }
}
