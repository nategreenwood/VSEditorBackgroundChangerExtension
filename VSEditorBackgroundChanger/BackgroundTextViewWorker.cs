using Microsoft.VisualStudio.Text.Editor;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace NateGreenwood.VSEditorBackgroundChanger
{
    internal class BackgroundTextViewWorker
    {
        private const string CONFIGURATION_FILE_NAME = @"config.dat";
        private readonly BackgroundImageSettings _settings;
        private readonly IAdornmentLayer _adornmentLayer;
        private readonly IWpfTextView _backgroundView;
        private readonly string _configurationDirectory;
        private readonly string _configurationDataPath;

        public BackgroundTextViewWorker(IWpfTextView textView)
        {
            if (textView == null)
                throw new ArgumentException("textView is null");
            _backgroundView = textView;

            EventHandler onSizeChangedEventHandler = (object sender, EventArgs args) => OnSizeChanged();
            try
            {
                _configurationDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                if(_configurationDirectory == null || string.IsNullOrEmpty(_configurationDirectory))
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

                // Put the image into the bottom right corner of the viewport
                Canvas.SetLeft(_settings.Image, _backgroundView.ViewportRight - _settings.Bitmap.PixelWidth);
                Canvas.SetTop(_settings.Image, _backgroundView.ViewportBottom - _settings.Bitmap.PixelHeight);

                _adornmentLayer.AddAdornment(AdornmentPositioningBehavior.ViewportRelative, null, null, _settings.Image, null);

            }
            catch (Exception e)
            {
                // todo: better logging
            }
        }
    }
}
