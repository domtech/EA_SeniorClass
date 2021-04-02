using UnityEngine;
using UnityEditor;
using OptimizingTypes;

public class OptimizingTexture : ScriptableObject
{
    #region Paras
    static string LogTitle = "Optimization:";
    static int currentMaxTextureSize;
    static TextureImporterFormat currentTIFormat;
    #endregion

    #region Set Texture Read/Write
    //SetTexReadWriteMode
    [MenuItem("Optimization/Texture Import Settings/SetTexReadWriteMode/EnableReadWrite")]
    static void ChangeTextureProperty_etTexReadWriteMode_EnableReadWrite()
    {

        TextureImportParams tiParams = new TextureImportParams(Actions.SetTexReadWriteMode);

        tiParams.TexReadWrite = true;

        SelectedChangeAnyPlatformSettings(tiParams);
    }

    [MenuItem("Optimization/Texture Import Settings/SetTexReadWriteMode/DisableReadWrite")]
    static void ChangeTextureProperty_etTexReadWriteMode_DisableReadWrite()
    {

        TextureImportParams tiParams = new TextureImportParams(Actions.SetTexReadWriteMode);

        tiParams.TexReadWrite = false;

        SelectedChangeAnyPlatformSettings(tiParams);
    }

    #endregion

    #region Texture Format
    [MenuItem("Optimization/Texture Import Settings/Change Texture Format/Android/ETC_RGB4")]
    static void ChangeTextureFormat_Android_ETC_RGB4()
    {
        ChangeTextureFormat(TextureImporterFormat.ETC_RGB4, Platform.Android);
    }


    [MenuItem("Optimization/Texture Import Settings/Change Texture Format/Android/ETC_RGB4Crunched")]
    static void ChangeTextureFormat_Android_ETC_RGB4Crunched()
    {
        ChangeTextureFormat(TextureImporterFormat.ETC_RGB4Crunched, Platform.Android);
    }

    [MenuItem("Optimization/Texture Import Settings/Change Texture Format/Android/ETC2_RGBA8")]
    static void ChangeTextureFormat_Android_ETC2_RGBA8()
    {
        ChangeTextureFormat(TextureImporterFormat.ETC2_RGBA8, Platform.Android);
    }

    [MenuItem("Optimization/Texture Import Settings/Change Texture Format/Android/ETC2_RGBA8Crunched")]
    static void ChangeTextureFormat_Android_ETC2_RGBA8Crunched()
    {
        ChangeTextureFormat(TextureImporterFormat.ETC2_RGBA8Crunched, Platform.Android);  
    }

    [MenuItem("Optimization/Texture Import Settings/Change Texture Format/Android/RGB(A)_COMPRESSED_ASTC_4x4")]
    static void ChangeTextureFormat_Android_ASTC4X4()
    {
        ChangeTextureFormat(TextureImporterFormat.ASTC_4x4, Platform.Android);
    }

    [MenuItem("Optimization/Texture Import Settings/Change Texture Format/Android/RGB(A)_COMPRESSED_ASTC_5x5")]
    static void ChangeTextureFormat_Android_ASTC5X5()
    {
        ChangeTextureFormat(TextureImporterFormat.ASTC_5x5, Platform.Android);
    }


    [MenuItem("Optimization/Texture Import Settings/Change Texture Format/Android/RGB(A)_COMPRESSED_ASTC_6x6")]
    static void ChangeTextureFormat_Android_ASTC6X6()
    {
        ChangeTextureFormat(TextureImporterFormat.ASTC_6x6, Platform.Android);
    }

    [MenuItem("Optimization/Texture Import Settings/Change Texture Format/Android/RGB(A)_HDR_COMPRESSED_ASTC_4x4")]
    static void ChangeTextureFormat_Android_HDR_ASTC4X4()
    {
        ChangeTextureFormat(TextureImporterFormat.ASTC_HDR_4x4, Platform.Android);
    }

    [MenuItem("Optimization/Texture Import Settings/Change Texture Format/Android/RGB(A)_HDR_COMPRESSED_ASTC_5x5")]
    static void ChangeTextureFormat_Android_HDR_ASTC5X5()
    {
        ChangeTextureFormat(TextureImporterFormat.ASTC_HDR_5x5, Platform.Android);
    }


    [MenuItem("Optimization/Texture Import Settings/Change Texture Format/Android/RGB(A)_HDR_COMPRESSED_ASTC_6x6")]
    static void ChangeTextureFormat_Android_HDR_ASTC6X6()
    {
        ChangeTextureFormat(TextureImporterFormat.ASTC_HDR_6x6, Platform.Android);
    }

    static void ChangeTextureFormat(TextureImporterFormat newFormat, Platform somePlatform = Platform.Default)
    {
        Debug.Log(System.String.Format("{0} Set TextureImporterFormat '{2}' @ {1} platform", LogTitle, somePlatform, newFormat));
        TextureImportParams tiParams = new TextureImportParams(Actions.SetTextureFormat, somePlatform);
        tiParams.TexImportFormat = newFormat;

        SelectedChangeAnyPlatformSettings(tiParams);
    }

    static Object[] GetSelectedTextures()
    {
        return Selection.GetFiltered(typeof(Texture2D), SelectionMode.DeepAssets);
    }
    #endregion

    #region Check Non-PowOfTwo
    [MenuItem("Optimization/Texture Import Settings/Check Texutre Non-PowerOfTwo")]
    static void ChangeTextureProperty_CheckNonPowerOfTwo()
    {
        
        TextureImportParams tiParams = new TextureImportParams(Actions.CheckNonPowOfTwo);

        SelectedChangeAnyPlatformSettings(tiParams);
    }

    #endregion

    #region Set Non-PowerOfTwo
    [MenuItem("Optimization/Texture Import Settings/SetNPOT/NONE")]
    static void ChangeTextureProperty_SetNPOT_NONE()
    {

        TextureImportParams tiParams = new TextureImportParams(Actions.SetNonPowOfTwo);

        SelectedChangeAnyPlatformSettings(tiParams);
    }
    #endregion

    #region Check Big Texture
    [MenuItem("Optimization/Texture Import Settings/CheckTextureSize")]
    static void ChangeTextureProperty_CheckTextureSize()
    {
        TextureImportParams tiParams = new TextureImportParams(Actions.CheckBigTextureSize);

        SelectedChangeAnyPlatformSettings(tiParams);
    }
    static void CheckTextureSize(string path)
    {
        Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(path);

        if(tex.width >= 512 || tex.height >= 512)
        {
            Debug.LogFormat("width:({0}), height:({1}), path:{2}", tex.width, tex.height, path);
        }
    }

    #endregion

    static void SelectedChangeAnyPlatformSettings(TextureImportParams TexImportParam)
    {

        int processingTexturesNumber;

        Object[] originalSelection = Selection.objects;

        Object[] textures = GetSelectedTextures();

        Selection.objects = new Object[0];

        processingTexturesNumber = textures.Length;

        AssetDatabase.StartAssetEditing();

        foreach (var texture in textures)
        {
            if (null == texture)
                return;

            string path = AssetDatabase.GetAssetPath(texture);

            TextureImporter texImporter = AssetImporter.GetAtPath(path) as TextureImporter;

            texImporter.GetPlatformTextureSettings(TexImportParam.platform.ToString(), out currentMaxTextureSize, out currentTIFormat);

            switch (TexImportParam.action)
            {
                case Actions.SetTextureFormat:
                    {
                        switch (TexImportParam.platform)
                        {
                            case Platform.Default:
                                {
                                    break;
                                }
                            case Platform.Android:
                            case Platform.iPhone:
                                {
                                    //TextureImporterPlatformSettings settings = new TextureImporterPlatformSettings();
                                    //settings.format = TexImportParam.TexImportFormat;//纹理格式
                                    //settings.name = TexImportParam.platform.ToString();//平台名称
                                    //settings.maxTextureSize = currentMaxTextureSize;//设置大小
                                    //texImporter.SetPlatformTextureSettings(settings);
                                    texImporter.SetPlatformTextureSettings(TexImportParam.platform.ToString(), currentMaxTextureSize, TexImportParam.TexImportFormat);
                                    break;
                                }
                        }
                        break;
                    }
                case Actions.CheckNonPowOfTwo:
                    {

                        //获取纹理大小
                        Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(path);

                        //判断是否是2的幂指数
                        if (!IsPowerOfTwo((ulong)tex.width) || !IsPowerOfTwo((ulong)tex.height))
                        {
                            Debug.Log("path: " + path);
                        }
                        break;
                    }
                case Actions.SetNonPowOfTwo:
                    {

                        Texture2D tex = AssetDatabase.LoadAssetAtPath<Texture2D>(path);

                        if (IsPowerOfTwo((ulong)tex.width) && IsPowerOfTwo((ulong)tex.height))
                        {
                            texImporter.npotScale = TextureImporterNPOTScale.None;
                        }

                            
                        break;
                    }
           
                case Actions.SetTexReadWriteMode:
                    {

                        texImporter.isReadable = TexImportParam.TexReadWrite;
                        break;

                    }

                case Actions.CheckBigTextureSize:
                    {
                        CheckTextureSize(path);
                        break;
                    }
            }

            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
        }
        AssetDatabase.StopAssetEditing();

        Selection.objects = originalSelection; //Restore selection

        if(TexImportParam.action == Actions.SetTextureFormat)
            Debug.Log("Textures Success, processed: " + processingTexturesNumber);

        AssetDatabase.SaveAssets();
    }

    #region Common Function

    static bool IsPowerOfTwo(ulong x)
    {
        return (x != 0) && ((x & (x - 1)) == 0);
    }

    #endregion
}

