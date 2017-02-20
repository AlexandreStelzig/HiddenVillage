using HiddenVillage.Enumeration;
using HiddenVillage.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HiddenVillage.UIHelper
{




    public class Transition
    {

        // alpha
        private float alpha;

        // delays
        private double upDelay, downDelay, pauseDelay;
        private double totalTime;

        // transition logic
        private bool goingUp;
        private bool pause;

        // textures
        private Texture2D pixel;
        private Rectangle screen;

        // name
        public TransitionEnum transitionEnum { private set; get; }

        // instances
        private GameManager gameManager;

        public Transition(TransitionEnum transitionEnum, double delay, GameManager gameManager)
        {
            this.gameManager = gameManager;
            this.transitionEnum = transitionEnum;
            upDelay = downDelay = delay;
            pixel = gameManager.getContent().Load<Texture2D>("pixel/blackpixel");
            screen = new Rectangle(0, 0, gameManager.getGraphicsDevice().DisplayMode.Width, gameManager.getGraphicsDevice().DisplayMode.Height);
            pauseDelay = 0;
            reset();
        }

        public Transition(TransitionEnum transitionEnum, double upDelay, double downDelay, GameManager gameManager)
        {
            this.gameManager = gameManager;
            this.transitionEnum = transitionEnum;
            this.downDelay = downDelay;
            this.upDelay = upDelay;
            pixel = gameManager.getContent().Load<Texture2D>("pixel/blackpixel");
            screen = new Rectangle(0, 0, gameManager.getGraphicsDevice().DisplayMode.Width, gameManager.getGraphicsDevice().DisplayMode.Height);
            pauseDelay = 0;
            reset();
        }
        public Transition(TransitionEnum transitionEnum, double upDelay, double pause, double downDelay, GameManager gameManager)
        {
            this.gameManager = gameManager;
            this.transitionEnum = transitionEnum;
            this.downDelay = downDelay;
            this.upDelay = upDelay;
            pixel = gameManager.getContent().Load<Texture2D>("pixel/blackpixel");
            screen = new Rectangle(0, 0, gameManager.getGraphicsDevice().DisplayMode.Width, gameManager.getGraphicsDevice().DisplayMode.Height);
            pauseDelay = pause;
            reset();
        }

        public void reset()
        {
            goingUp = true;
            totalTime = 0;
            // init alpha
            if (upDelay <= 0)
                alpha = 1f;
            else
                alpha = 0f;
            pause = false;
        }

        public void Update(double elapsedTime)
        {

            totalTime += elapsedTime;

            if (!pause)
            {
                if (goingUp)
                {
                    double timeRemaining = totalTime / upDelay;
                    alpha = (float)(1 * timeRemaining);

                    if (alpha >= 1f)
                    {
                        goingUpDone();
                        goingUp = false;
                        totalTime = 0;
                        pause = true;
                    }
                }
                else
                {
                    double timeRemaining = totalTime / downDelay;
                    alpha = (float)(1 - 1 * timeRemaining);

                    if (alpha <= 0f)
                    {
                        goingDownDone();
                        goingUp = true;
                        totalTime = 0;
                        pause = true;
                    }
                }
            }
            else
            {
                if (!goingUp)
                    alpha = 1f;
                else
                    alpha = 0f;

                if (totalTime >= pauseDelay)
                {
                    totalTime = 0;
                    pause = false;
                }
            }


        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(pixel, screen, Color.White * alpha);

        }


        private void goingUpDone()
        {
            gameManager.transitionGoingUpDone(this);
        }

        private void goingDownDone()
        {
            reset();
            gameManager.transitionGoingDownDone(this);
        }


    }
}
