using System;
using System.Collections;
using System.Windows;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using System.Diagnostics;
namespace NateGreenwood.VSEditorBackgroundChanger
{
    [Export(typeof(IWpfTextViewCreationListener))]
    [ContentType("text")]
    [TextViewRole("DOCUMENT")]
    internal sealed class BackgroundChangerAdornmentFactory : IWpfTextViewCreationListener
    {
        [Export(typeof(AdornmentLayerDefinition))]
        [Name("BackgroundTextViewWorker")]
        //[Order(After="Caret")]
        [Order(Before = PredefinedAdornmentLayers.TextMarker)]
        public AdornmentLayerDefinition EditorAdornmentLayer;

        public BackgroundChangerAdornmentFactory()
        {
#if DEBUG
            if(Debugger.IsAttached)
                Debugger.Break();
#endif
        }

        public void TextViewCreated(IWpfTextView textView)
        {
            if(textView == null)
                throw new ArgumentException("textView was null");

            // Simply instantiate the object. Nothing more to do.
            new BackgroundTextViewWorker(textView);
        }
    }
}
