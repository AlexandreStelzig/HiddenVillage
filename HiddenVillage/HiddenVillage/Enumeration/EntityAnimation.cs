using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HiddenVillage.Enumeration
{
    public class EntityAnimation
    {


        // animation
        private List<Texture2D> animationImages;
        private int currentAnimation;


        // delay between images
        private float animationDelay;
        private float currentDelay;



        public EntityAnimation(List<Texture2D> animationImages, float animationDelay)
        {
            this.animationImages = animationImages;
            this.animationDelay = animationDelay;
            currentAnimation = 0;
            currentDelay = 0;
        }

        public void Update(double elapsedTime)
        {
            currentDelay += (float)elapsedTime;

            if(currentDelay > animationDelay)
            {
                currentDelay = 0;
                currentAnimation += 1;

                if(currentAnimation > animationImages.Count - 1)
                {
                    currentAnimation = 0;
                }
            }
               

        }

        public Texture2D getAnimation()
        {

            return animationImages[currentAnimation];
        }

        public void setCurrentAnimation(int animationNumber)
        {
            currentAnimation = animationNumber;
        }

        public void setDelay(float delay)
        {
            this.animationDelay = delay;
        }

    }
}
