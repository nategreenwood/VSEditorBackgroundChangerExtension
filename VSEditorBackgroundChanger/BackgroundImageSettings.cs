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

        public string EnvironmentImagePathAndName { get; internal set; }
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
                        if (ImagePath.Contains("[VSIXInstallDirectory]"))
                        {
                            string[] parts = ImagePath.Split('\\');
                            if (parts.Any() && !string.IsNullOrEmpty(parts[1]))
                            {
                                string baseDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                                if (baseDirectory != null) ImagePath = Path.Combine(baseDirectory, parts[1], ImageName);
                            }
                        }
                        if (ImagePath != null)
                        {
                            Bitmap = new BitmapImage(new Uri(ImagePath));
                        }
                        else
                        {
                            throw new Exception(
                                "A valid image directory could not be parsed from the configuration file.");
                        }

                        Opacity = Convert.ToDouble(options.Single(d => d.Contains("opacity")).Split('=')[1]) / 100;
                        Location = options.Single(d => d.Contains("location")).Split('=')[1];
                        ImageFill = options.Single(d => d.Contains("fill")).Split('=')[1];

                        Image.Source = Bitmap;
                        Image.Opacity = Opacity;

                        Image.Width = Bitmap.PixelWidth;
                        Image.Height = Bitmap.PixelHeight;

                        Image.StretchDirection = StretchDirection.Both;

                        // Environment related settings
                        var envImageOption = options.Single(d => d.Contains("env_image"));
                        if(!string.IsNullOrEmpty(envImageOption))
                        {
                            EnvironmentImagePathAndName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),envImageOption.Split('=')[1]);
                        }
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