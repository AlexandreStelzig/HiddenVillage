using HiddenVillage.InputHelper;
using HiddenVillage.State;
using HiddenVillage.UIHelper;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HiddenVillage.Enumeration;
using Microsoft.Xna.Framework;

namespace HiddenVillage.Manager
{
    public class GameManager
    {
        // game reference
        private Game1 game;

        // game scale
        public readonly static float gameScale = 1.5f;

        // states
        private GameState[] gameStates;
        private GameStateEnum currentState;

        // transition
        private bool transitionRunning;
        private TransitionEnum currentTransition;
        public Transition introTransition;
        public Transition stateTransition;

        // Camera
        private Camera camera;

        // pause function
        private bool isGamePaused;

        // fonts
        SpriteFont Font1;

        public GameManager(Game1 game)
        {
            this.game = game;
            gameStates = new GameState[16];
            changeState(GameStateEnum.MENU);

            camera = Camera.getInstance(this);

            isGamePaused = false;

            transitionRunning = true;
            currentTransition = TransitionEnum.MENU;
            introTransition = new Transition(TransitionEnum.MENU, 0, 0.1, .1, this);
            stateTransition = new Transition(TransitionEnum.STATE, 1, 1, 1, this);


            Font1 = getContent().Load<SpriteFont>("fonts/normal");

        }

        public void changeState(GameStateEnum state)
        {

            currentState = state;

            if (gameStates[(int)currentState] == null)
            {
                switch (state)
                {
                    case GameStateEnum.MENU:
                        gameStates[(int)currentState] = new MenuState(this);
                        break;
                    case GameStateEnum.START:
                        gameStates[(int)currentState] = new StartState(this);
                        break;
                    default:
                        break;
                }
            }
        }



        public void Update(double elapsedTime)
        {
            KeyboardInput.Instance.Update(elapsedTime);

            // update transition
            if (transitionRunning)
            {
                if (currentTransition == TransitionEnum.MENU)
                {
                    introTransition.Update(elapsedTime);
                }
                else
                {
                    stateTransition.Update(elapsedTime);
                }

            }
            // update current state
            else
            {
                // check for pause input
                if (currentState != GameStateEnum.MENU)
                {
                    if (KeyboardInput.Instance.getPressedState(KeyboadInputEnum.ESCAPE) && !KeyboardInput.Instance.getPrevisouState(KeyboadInputEnum.ESCAPE))
                    {
                        isGamePaused = !isGamePaused;
                    }

                }

                // update current state if not paused
                if (!isGamePaused)
                {
                    gameStates[(int)currentState].Update(elapsedTime);

                    if (currentState != GameStateEnum.MENU)
                        camera.Update(elapsedTime);
                }

            }


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // different drawing depending of states (camera reasons)
            if (currentState != GameStateEnum.MENU)
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone, null, camera.transform);
            else
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            // draw current state
            gameStates[(int)currentState].Draw(spriteBatch);

            // draw transitions
            if (transitionRunning)
            {
                if (currentTransition == TransitionEnum.MENU)
                {
                    introTransition.Draw(spriteBatch);
                }
                else
                {
                    stateTransition.Draw(spriteBatch);
                }
            }

            // draw pause menu
            if (isGamePaused)
            {
                string output = "Game paused";

                Vector2 FontPos = new Vector2(getGraphicsDevice().Viewport.Width / 2, (getGraphicsDevice().Viewport.Height / 2)) + camera.centre;

                Vector2 FontOrigin = Font1.MeasureString(output) / 2;
                spriteBatch.DrawString(Font1, output, FontPos, Color.Red,
                    0, FontOrigin, 2.0f, SpriteEffects.None, 0.5f);
            }
            
            // end of drawing
            spriteBatch.End();
        }


        // start of transition
        public void startStateTransition(TransitionEnum transition)
        {
            currentTransition = transition;
            transitionRunning = true;

        }

        // end of transition
        public void transitionGoingUpDone(Transition transitionSource)
        {
            if (transitionSource.transitionEnum == TransitionEnum.STATE)
            {
                changeState(GameStateEnum.START);
            }
        }

        public void transitionGoingDownDone(Transition transitionSource)
        {
            transitionRunning = false;
        }

        // get Content
        public ContentManager getContent()
        {
            return game.Content;
        }

        // get GraphicsDevice
        public GraphicsDevice getGraphicsDevice()
        {
            return game.GraphicsDevice;
        }

        // exits game
        public void exitGame()
        {
            game.exitGame();
        }

    }
}
