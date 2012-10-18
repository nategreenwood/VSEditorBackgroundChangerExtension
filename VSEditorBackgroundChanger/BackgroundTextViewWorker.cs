namespace NateGreenwood.VSEditorBackgroundChanger
{
    using System;
    using System.IO;
    using System.Reflection;
    using System.Windows.Controls;
    using System.Windows.Media;
    using Microsoft.VisualStudio.Text.Editor;

    internal class BackgroundTextViewWorker
    {
        private const string CONFIGURATION_FILE_NAME = @"config.dat";
        private readonly IAdornmentLayer _adornmentLayer;
        private readonly IWpfTextView _backgroundView;
        private readonly string _configurationDataPath;
        private readonly string _configurationDirectory;
        private readonly BackgroundImageSettings _settings;

        public BackgroundTextViewWorker(IWpfTextView textView)
        {
            if (textView == null)
                throw new ArgumentException("textView is null");
            _backgroundView = textView;

            EventHandler onSizeChangedEventHandler = (object sender, EventArgs args) => OnSizeChanged();
            try
            {
                _configurationDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if (_configurationDirectory == null || string.IsNullOrEmpty(_configurationDirectory))
                    throw new ArgumentException("_configurationDirectory is null or empty");

                _configurationDataPath = Path.Combine(_configurationDirectory, CONFIGURATION_FILE_NAME);

                if (_configurationDataPath != null)
                {
                    _settings = new BackgroundImageSettings(new Uri(_configurationDataPath));
                }
                _adornmentLayer = textView.GetAdornmentLayer("BackgroundTextViewWorker");

                IWpfTextView newTextView = _backgroundView;
                newTextView.ViewportWidthChanged += onSizeChangedEventHandler;
                newTextView.ViewportHeightChanged += onSizeChangedEventHandler;
            }
            catch (Exception e)
            {
                // todo: better logging
            }
        }

        public void OnSizeChanged()
        {
            try
            {
                _adornmentLayer.RemoveAllAdornments();

                // Adjust the image fill
                switch (_settings.ImageFill)
                {
                    case "None":
                        {
                            _settings.Image.Stretch = Stretch.None;
                            break;
                        }
                    case "Uniform":
                        {
                            _settings.Image.Stretch = Stretch.Uniform;
                            break;
                        }

                    case "Fill":
                        {
                            _settings.Image.Stretch = Stretch.Fill;
                            break;
                        }
                    case "UniformToFill":
                        {
                            _settings.Image.Stretch = Stretch.UniformToFill;
                            break;
                        }
                    case "Normal":
                    default:
                        {
                            _settings.Image.Stretch = Stretch.None;
                            break;
                        }
                }

                // Position the image
                switch (_settings.Location)
                {
                    case "BottomRight":
                        {
                            Canvas.SetLeft(_settings.Image, _backgroundView.ViewportRight - _settings.Image.Width);
                            Canvas.SetTop(_settings.Image, _backgroundView.ViewportBottom - _settings.Image.Height);
                            break;
                        }
                    case "BottomLeft":
                        {
                            Canvas.SetLeft(_settings.Image, _backgroundView.ViewportLeft);
                            Canvas.SetTop(_settings.Image, _backgroundView.ViewportBottom - _settings.Image.Height);
                            break;
                        }
                    case "TopRight":
                        {
                            Canvas.SetLeft(_settings.Image, _backgroundView.ViewportRight - _settings.Image.Width);
                            Canvas.SetTop(_settings.Image, _backgroundView.ViewportRight);
                            break;
                        }
                    case "TopLeft":
                        {
                            Canvas.SetLeft(_settings.Image, 0);
                            Canvas.SetTop(_settings.Image, 0);
                            break;
                        }
                    default:
                        {
                            // Default to center
                            Canvas.SetLeft(_settings.Image,
                                           (_backgroundView.ViewportWidth/2) - (_settings.Image.Width/2.0));
                            Canvas.SetTop(_settings.Image,
                                          (_backgroundView.ViewportHeight/2) - (_settings.Image.Height/2.0));
                            break;
                        }
                }

                _adornmentLayer.AddAdornment(AdornmentPositioningBehavior.ViewportRelative, null, null, _settings.Image,
                                             null);
            }
            catch (Exception e)
            {
                //todo: Add logging maybe? Does it matter?
            }
        }
    }
}