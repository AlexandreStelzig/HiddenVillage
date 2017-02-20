using HiddenVillage.Manager;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace HiddenVillage.ContentHelper
{
    public static class ImageHelper
    {


        public static List<Texture2D> getSubImages(GameManager gameManager, string path, int rowStart, int rowEnd, int columnStart, int columnEnd, int pixelWidth, int pixelHeight)
        {

            List<Texture2D> subImages = new List<Texture2D>();

            for(int rowCount = rowStart; rowCount < rowEnd; rowCount++)
            {
                for(int columnCount = columnStart; columnCount < columnEnd; columnCount++)
                {

                    Texture2D originalTexture = gameManager.getContent().Load<Texture2D>(path);
                    Rectangle sourceRectangle = new Rectangle(pixelWidth * columnCount, pixelHeight * rowCount, pixelWidth, pixelHeight);

                    Texture2D cropTexture = new Texture2D(gameManager.getGraphicsDevice(), sourceRectangle.Width, sourceRectangle.Height);
                    Color[] data = new Color[sourceRectangle.Width * sourceRectangle.Height];
                    originalTexture.GetData(0, sourceRectangle, data, 0, data.Length);
                    cropTexture.SetData(data);

                    subImages.Add(cropTexture);

                }


            }

            

            return subImages;

        }



    }
}
