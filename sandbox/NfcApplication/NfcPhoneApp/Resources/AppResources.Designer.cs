﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン: 4.0.30319.17626
//
//     このファイルを変更すると、正しくない動作の原因になる場合があります。
//     コードが再生成されます。
// </auto-generated>
//------------------------------------------------------------------------------

namespace NfcPhoneApp.Resources
{
    using System;


    /// <summary>
    ///   ローカライズされた文字列などを検索するための、厳密に型指定されたリソース クラスです。
    /// </summary>
    // このクラスは、StronglyTypedResourceBuilder によって自動生成されました
    // または Visual Studio のようなツールを使用して自動生成されました。
    // メンバーを追加または削除するには、.ResX ファイルを編集し、ResGen を
    // /str オプションを指定して再実行するか、VS プロジェクトをビルドし直します。
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute( "System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0" )]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class AppResources
    {

        private static global::System.Resources.ResourceManager resourceMan;

        private static global::System.Globalization.CultureInfo resourceCulture;

        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute( "Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode" )]
        internal AppResources()
        {
        }

        /// <summary>
        ///   このクラスで使用されるキャッシュされた ResourceManager インスタンスを返します。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute( global::System.ComponentModel.EditorBrowsableState.Advanced )]
        public static global::System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if ( object.ReferenceEquals( resourceMan, null ) ) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager( "NfcPhoneApp.Resources.AppResources", typeof( AppResources ).Assembly );
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }

        /// <summary>
        ///   現在のスレッドの CurrentUICulture プロパティをオーバーライドします
        ///   クラスを使用して、すべてのリソース ルックアップに対してオーバーライドします。
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute( global::System.ComponentModel.EditorBrowsableState.Advanced )]
        public static global::System.Globalization.CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        /// <summary>
        ///   LeftToRight に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string ResourceFlowDirection
        {
            get
            {
                return ResourceManager.GetString( "ResourceFlowDirection", resourceCulture );
            }
        }

        /// <summary>
        ///   us-EN に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string ResourceLanguage
        {
            get
            {
                return ResourceManager.GetString( "ResourceLanguage", resourceCulture );
            }
        }

        /// <summary>
        ///   マイ アプリケーションに類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string ApplicationTitle
        {
            get
            {
                return ResourceManager.GetString( "ApplicationTitle", resourceCulture );
            }
        }

        /// <summary>
        ///   ボタンに類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string AppBarButtonText
        {
            get
            {
                return ResourceManager.GetString( "AppBarButtonText", resourceCulture );
            }
        }

        /// <summary>
        ///   メニュー項目に類似しているローカライズされた文字列を検索します。
        /// </summary>
        public static string AppBarMenuItemText
        {
            get
            {
                return ResourceManager.GetString( "AppBarMenuItemText", resourceCulture );
            }
        }
    }
}
