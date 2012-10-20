namespace NateGreenwood.VSEditorBackgroundChanger
{using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
    using System.Reflection;
    using System.Windows.Media;
    using Microsoft.VisualStudio.Shell;

    internal class EnvBackgroundManager
    {
        public static void ApplyEnvironmentBackground(BackgroundImageSettings settings)
        {
            string imageFileName = settings.EnvironmentImagePathAndName;
            ResourceDictionary resources = Application.Current.Resources;

            SetEnvironmentImageBrush(resources, imageFileName, false);
        }

        private static void SetEnvironmentImageBrush(ResourceDictionary resources, string imageFileName, bool tileImage)
        {
            BitmapSource bitmapImage = new BitmapImage(new Uri(imageFileName));
            SetEnvironmentImageBrush(resources, bitmapImage, tileImage);
        }

        private static void SetEnvironmentImageBrush(ResourceDictionary resources, BitmapSource bitmapImage, bool tileImage)
        {
            TileMode tileMode;
            ImageBrush imageBrush = new ImageBrush(bitmapImage);
            ImageBrush imageBrush1 = imageBrush;

            tileMode = tileImage ? TileMode.Tile : TileMode.None;

            imageBrush.TileMode = tileMode;
            imageBrush.Viewport = new Rect(0, 0, bitmapImage.Width, bitmapImage.Height);
            imageBrush.ViewboxUnits = BrushMappingMode.Absolute;
            if(imageBrush.CanFreeze)
                imageBrush.Freeze();
            
            //todo: Create a persistentance mechanism
            //SaveDefaultEnvironmentBrush(resources);
            
            resources[VsBrushes.EnvironmentBackgroundTextureKey] = imageBrush;
        }
    }
}
