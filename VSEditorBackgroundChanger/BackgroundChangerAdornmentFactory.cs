namespace NateGreenwood.VSEditorBackgroundChanger
{
    using System;
    using System.ComponentModel.Composition;
    using System.Diagnostics;
    using Microsoft.VisualStudio.Text.Editor;
    using Microsoft.VisualStudio.Utilities;

    [Export(typeof (IWpfTextViewCreationListener))]
    [ContentType("text")]
    [TextViewRole("DOCUMENT")]
    internal sealed class BackgroundChangerAdornmentFactory : IWpfTextViewCreationListener
    {
        [Export(typeof (AdornmentLayerDefinition))] [Name("BackgroundTextViewWorker")] [Order(Before = PredefinedAdornmentLayers.TextMarker)] public AdornmentLayerDefinition EditorAdornmentLayer;

        public BackgroundChangerAdornmentFactory()
        {
        }

        #region IWpfTextViewCreationListener Members

        public void TextViewCreated(IWpfTextView textView)
        {
            if (textView == null)
                throw new ArgumentException("textView was null");

            // Simply instantiate the object. Nothing more to do.
            new BackgroundTextViewWorker(textView);
        }

        #endregion
    }
}