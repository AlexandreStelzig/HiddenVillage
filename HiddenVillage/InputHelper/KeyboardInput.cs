using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenVillage.InputHelper
{
    public enum KeyboadInputEnum
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        SHIFT,
        ENTER,
        ESCAPE,
        ACTION,
        CONTROL
    }

    class KeyboardInput
    {

        private KeyboardState currentState;
        private KeyboardState previousState;

        private static KeyboardInput instance;

        private KeyboardInput()
        {
            currentState = Keyboard.GetState();
            previousState = currentState;
        }


        public static KeyboardInput Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new KeyboardInput();
                }
                return instance;
            }
        }


        public void Update(double elapsedTime)
        {
            previousState = currentState;
            currentState = Keyboard.GetState();
        }


        public bool getPressedState(KeyboadInputEnum input)
        {
            switch (input)
            {
                case KeyboadInputEnum.UP:
                    return currentState.IsKeyDown(Keys.W) || currentState.IsKeyDown(Keys.Up);
                case KeyboadInputEnum.DOWN:
                    return currentState.IsKeyDown(Keys.S) || currentState.IsKeyDown(Keys.Down);
                case KeyboadInputEnum.LEFT:
                    return currentState.IsKeyDown(Keys.A) || currentState.IsKeyDown(Keys.Left);
                case KeyboadInputEnum.RIGHT:
                    return currentState.IsKeyDown(Keys.D) || currentState.IsKeyDown(Keys.Right);
                case KeyboadInputEnum.SHIFT:
                    return currentState.IsKeyDown(Keys.LeftShift);
                case KeyboadInputEnum.ENTER:
                    return currentState.IsKeyDown(Keys.Enter);
                case KeyboadInputEnum.ESCAPE:
                    return currentState.IsKeyDown(Keys.Escape);
                case KeyboadInputEnum.ACTION:
                    return currentState.IsKeyDown(Keys.E);
                case KeyboadInputEnum.CONTROL:
                    return currentState.IsKeyDown(Keys.LeftControl);
                default:
                    throw new Exception("key not registered");
            }
        }

        public bool getPrevisouState(KeyboadInputEnum input)
        {
            switch (input)
            {
                case KeyboadInputEnum.UP:
                    return previousState.IsKeyDown(Keys.W) || previousState.IsKeyDown(Keys.Up);
                case KeyboadInputEnum.DOWN:
                    return previousState.IsKeyDown(Keys.S) || previousState.IsKeyDown(Keys.Down);
                case KeyboadInputEnum.LEFT:
                    return previousState.IsKeyDown(Keys.A) || previousState.IsKeyDown(Keys.Left);
                case KeyboadInputEnum.RIGHT:
                    return previousState.IsKeyDown(Keys.D) || previousState.IsKeyDown(Keys.Right);
                case KeyboadInputEnum.SHIFT:
                    return previousState.IsKeyDown(Keys.LeftShift);
                case KeyboadInputEnum.ENTER:
                    return previousState.IsKeyDown(Keys.Enter);
                case KeyboadInputEnum.ESCAPE:
                    return previousState.IsKeyDown(Keys.Escape);
                case KeyboadInputEnum.ACTION:
                    return previousState.IsKeyDown(Keys.E);
                case KeyboadInputEnum.CONTROL:
                    return previousState.IsKeyDown(Keys.LeftControl);
                default:
                    throw new Exception("key not registered");
            }
        }

    }
}
