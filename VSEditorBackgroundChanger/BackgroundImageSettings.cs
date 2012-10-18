namespace NateGreenwood.VSEditorBackgroundChanger
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Windows.Controls;
    using System.Windows.Media.Imaging;

    internal class BackgroundImageSettings
    {
        private readonly Uri _configurationFile;

        protected BackgroundImageSettings()
        {
            Image = new Image();
            Bitmap = new BitmapImage();
        }

        public BackgroundImageSettings(Uri configurationFile)
            : this()
        {
            _configurationFile = configurationFile;

            ParseConfigurationFile();
        }

        public Image Image { get; private set; }
        public BitmapImage Bitmap { get; private set; }
        public string ImagePath { get; private set; }
        public string ImageName { get; private set; }
        public double Opacity { get; private set; }
        public string Location { get; private set; }
        public string ImageFill { get; private set; }

        private void ParseConfigurationFile()
        {
            if (_configurationFile == null || string.IsNullOrEmpty(_configurationFile.AbsolutePath))
                throw new ArgumentException("Configuration file was null or empty");

            // Load any configuration settings
            using (var reader = new StreamReader(_configurationFile.LocalPath, Encoding.ASCII))
            {
                try
                {
                    string[] options = reader.ReadToEnd().Split(';');

                    if (options.Any())
                    {
                        ImagePath = options.Single(d => d.Contains("img_directory")).Split('=')[1];
                        ImageName = options.Single(d => d.Contains("img_name")).Split('=')[1];
                        if (ImagePath.Equals("[VSIXInstallDirectory]"))
                        {
                            ImagePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                        }
                        if (ImagePath != null)
                        {
                            Bitmap = new BitmapImage(new Uri(Path.Combine(ImagePath, "Images", ImageName)));
                        }
                        else
                        {
                            throw new Exception(
                                "A valid image directory could not be parsed from the configuration file.");
                        }
                        Opacity = double.Parse(options.Single(d => d.Contains("opacity")).Split('=')[1]);
                        Location = options.Single(d => d.Contains("location")).Split('=')[1];
                        ImageFill = options.Single(d => d.Contains("fill")).Split('=')[1];

                        Image.Source = Bitmap;
                        Image.Opacity = Opacity;

                        Image.Width = Bitmap.PixelWidth;
                        Image.Height = Bitmap.PixelHeight;

                        Image.StretchDirection = StretchDirection.Both;
                    }
                    else
                    {
                        throw new Exception("No settings could be parsed from the configuration file.");
                    }
                }
                catch (Exception e)
                {
                    // Create setting defaults
                }
            }
        }
    }
}