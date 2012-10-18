// Guids.cs
// MUST match guids.h
using System;

namespace NateGreenwood.VSEditorBackgroundChanger
{
    static class GuidList
    {
        public const string guidVSEditorBackgroundChangerPkgString = "ef854b25-f087-4923-8ae6-dc2cf20415ea";
        public const string guidVSEditorBackgroundChangerCmdSetString = "0fbc5fe5-734e-4f6b-8f19-07d83c2c80b1";

        public static readonly Guid guidVSEditorBackgroundChangerCmdSet = new Guid(guidVSEditorBackgroundChangerCmdSetString);
    };
}