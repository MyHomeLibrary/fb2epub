﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Fb2epubSettings {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
    public sealed partial class Fb2Epub : global::System.Configuration.ApplicationSettingsBase {
        
        private static Fb2Epub defaultInstance = ((Fb2Epub)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Fb2Epub())));
        
        public static Fb2Epub Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool Transliterate {
            get {
                return ((bool)(this["Transliterate"]));
            }
            set {
                this["Transliterate"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool TransliterateFileName {
            get {
                return ((bool)(this["TransliterateFileName"]));
            }
            set {
                this["TransliterateFileName"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool FB2Info {
            get {
                return ((bool)(this["FB2Info"]));
            }
            set {
                this["FB2Info"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("2")]
        public int FixMode {
            get {
                return ((int)(this["FixMode"]));
            }
            set {
                this["FixMode"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool AddSequences {
            get {
                return ((bool)(this["AddSequences"]));
            }
            set {
                this["AddSequences"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool TransliterateTOC {
            get {
                return ((bool)(this["TransliterateTOC"]));
            }
            set {
                this["TransliterateTOC"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool FlatStructure {
            get {
                return ((bool)(this["FlatStructure"]));
            }
            set {
                this["FlatStructure"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool ConvertAlphaPNG {
            get {
                return ((bool)(this["ConvertAlphaPNG"]));
            }
            set {
                this["ConvertAlphaPNG"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool EmbedStyles {
            get {
                return ((bool)(this["EmbedStyles"]));
            }
            set {
                this["EmbedStyles"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<Fonts xmlns:xsi=\"http://www.w3.org/2001" +
            "/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <Font gene" +
            "ric_family=\"\" style=\"normal\" font-variant=\"\" font-weight=\"400\">\r\n    <Destinatio" +
            "ns type=\"Embedded\">\r\n      <Path>%ResourceFolder%\\Fonts/LiberationSerif-Regular." +
            "ttf</Path>\r\n    </Destinations>\r\n    <decorate>true</decorate>\r\n    <CSSTargets>" +
            "body</CSSTargets>\r\n    <CSSTargets>.epub</CSSTargets>\r\n  </Font>\r\n  <Font generi" +
            "c_family=\"\" style=\"italic\" font-variant=\"\" font-weight=\"400\">\r\n    <Destinations" +
            " type=\"Embedded\">\r\n      <Path>%ResourceFolder%\\Fonts/LiberationSerif-Italic.ttf" +
            "</Path>\r\n    </Destinations>\r\n    <decorate>true</decorate>\r\n    <CSSTargets>bod" +
            "y</CSSTargets>\r\n    <CSSTargets>.epub</CSSTargets>\r\n  </Font>\r\n  <Font generic_f" +
            "amily=\"\" style=\"normal\" font-variant=\"\" font-weight=\"700\">\r\n    <Destinations ty" +
            "pe=\"Embedded\">\r\n      <Path>%ResourceFolder%\\Fonts/LiberationSerif-Bold.ttf</Pat" +
            "h>\r\n    </Destinations>\r\n    <decorate>true</decorate>\r\n    <CSSTargets>body</CS" +
            "STargets>\r\n    <CSSTargets>.epub</CSSTargets>\r\n  </Font>\r\n  <Font generic_family" +
            "=\"\" style=\"italic\" font-variant=\"\" font-weight=\"700\">\r\n    <Destinations type=\"E" +
            "mbedded\">\r\n      <Path>%ResourceFolder%\\Fonts/LiberationSerif-BoldItalic.ttf</Pa" +
            "th>\r\n    </Destinations>\r\n    <decorate>true</decorate>\r\n    <CSSTargets>body</C" +
            "SSTargets>\r\n    <CSSTargets>.epub</CSSTargets>\r\n  </Font>\r\n  <Font generic_famil" +
            "y=\"\" style=\"italic\" font-variant=\"\" font-weight=\"\">\r\n    <Destinations type=\"Emb" +
            "edded\">\r\n      <Path>%ResourceFolder%\\Fonts/LiberationSerif-Italic.ttf</Path>\r\n " +
            "   </Destinations>\r\n    <decorate>true</decorate>\r\n    <CSSTargets>code</CSSTarg" +
            "ets>\r\n  </Font>\r\n</Fonts>")]
        public global::FontsSettings.FontSettings Fonts {
            get {
                return ((global::FontsSettings.FontSettings)(this["Fonts"]));
            }
            set {
                this["Fonts"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("%bt% %sa.l%-%sn%")]
        public string SequenceFormat {
            get {
                return ((string)(this["SequenceFormat"]));
            }
            set {
                this["SequenceFormat"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("%bt% (%sf.l%)")]
        public string NoSequenceFormat {
            get {
                return ((string)(this["NoSequenceFormat"]));
            }
            set {
                this["NoSequenceFormat"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("%bt%")]
        public string NoSeriesFormat {
            get {
                return ((string)(this["NoSeriesFormat"]));
            }
            set {
                this["NoSeriesFormat"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("%f.c%%m.c%%l.c%%n.c:b%")]
        public string AuthorFormat {
            get {
                return ((string)(this["AuthorFormat"]));
            }
            set {
                this["AuthorFormat"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("%l.c%f.c")]
        public string FileAsFormat {
            get {
                return ((string)(this["FileAsFormat"]));
            }
            set {
                this["FileAsFormat"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("True")]
        public bool Capitalize {
            get {
                return ((bool)(this["Capitalize"]));
            }
            set {
                this["Capitalize"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool SkipAboutPage {
            get {
                return ((bool)(this["SkipAboutPage"]));
            }
            set {
                this["SkipAboutPage"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("False")]
        public bool UseAdobeTemplate {
            get {
                return ((bool)(this["UseAdobeTemplate"]));
            }
            set {
                this["UseAdobeTemplate"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("")]
        public string AdobeTemplatePath {
            get {
                return ((string)(this["AdobeTemplatePath"]));
            }
            set {
                this["AdobeTemplatePath"] = value;
            }
        }
    }
}
