using HiddenVillage.Enumeration;
using HiddenVillage.InputHelper;
using HiddenVillage.Manager;
using HiddenVillage.UIHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenVillage.State
{
    public class MenuState : GameState
    {

        // menu buttons
        private List<GameButton> menuButtons;
        private int currentButtonSelected;

        // input delay
        private double inputElapsedTime;
        private double inputElpaseWait = 0.15;

        // texture
        private Texture2D background;
        private Rectangle bgRectangle;


        public MenuState(GameManager gameManager) : base(gameManager)
        {
            background = gameManager.getContent().Load<Texture2D>("pixel/blackpixel");
            bgRectangle = new Rectangle(0, 0, gameManager.getGraphicsDevice().DisplayMode.Width, gameManager.getGraphicsDevice().DisplayMode.Height);

            menuButtons = new List<GameButton>();
            menuButtons.Add(new GameButton(this, GameButtonEnum.START, gameManager.getContent().Load<Texture2D>("buttons/startbutton"), 
                WindowHelper.getCoordForMiddleHorizontal(gameManager, 150), 200, 150, 50, 1.10));
            menuButtons.Add(new GameButton(this, GameButtonEnum.EXIT, gameManager.getContent().Load<Texture2D>("buttons/exitbutton"), 
                WindowHelper.getCoordForMiddleHorizontal(gameManager, 150), 300, 150, 50, 1.10));

            currentButtonSelected = 0;
            menuButtons[0].setButtonHovering(true);

            inputElapsedTime = 0;

        }


        public override void Update(double elapsedTime)
        {

            if (inputElapsedTime < inputElpaseWait)
            {
                inputElapsedTime += elapsedTime;
            }
            else
            {
                if (KeyboardInput.Instance.getPressedState(KeyboadInputEnum.DOWN))
                {
                    currentButtonSelected += 1;

                    if (currentButtonSelected >= menuButtons.Count)
                    {
                        currentButtonSelected = 0;
                    }

                    menuButtons[currentButtonSelected].setButtonHovering(true);

                    for (int count = 0; count < menuButtons.Count; count++)
                    {
                        if (count != currentButtonSelected)
                        {
                            menuButtons[count].setButtonHovering(false);
                        }
                    }
                    inputElapsedTime = 0;
                }
                else if (KeyboardInput.Instance.getPressedState(KeyboadInputEnum.UP))
                {
                    currentButtonSelected -= 1;

                    if (currentButtonSelected < 0)
                    {
                        currentButtonSelected = menuButtons.Count - 1;
                    }

                    menuButtons[currentButtonSelected].setButtonHovering(true);

                    for (int count = 0; count < menuButtons.Count; count++)
                    {
                        if (count != currentButtonSelected)
                        {
                            menuButtons[count].setButtonHovering(false);
                        }
                    }
                    inputElapsedTime = 0;
                }
            }
            if ((KeyboardInput.Instance.getPressedState(KeyboadInputEnum.ENTER)))
            {
                buttonClicked();

            }



        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, bgRectangle, Color.White);

            foreach (var button in menuButtons)
                button.Draw(spriteBatch);


        }

        public void buttonClicked()
        {
            if (menuButtons[currentButtonSelected].buttonName == GameButtonEnum.START)
            {
                gameManager.startStateTransition(TransitionEnum.STATE);
            }

            if (menuButtons[currentButtonSelected].buttonName == GameButtonEnum.EXIT)
            {
                gameManager.exitGame();
            }
        }

    }
}
