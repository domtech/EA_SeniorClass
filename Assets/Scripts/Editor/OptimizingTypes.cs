using UnityEditor;
using UnityEngine;

namespace OptimizingTypes
{

    public enum Platform
    {
        Default,
        Web,
        Standalone,
        iPhone,
        Android,
        FlashPlayer,
        All
    }

    enum Actions
    {
        SetTextureFormat,//设置纹理的压缩格式
        CheckNonPowOfTwo,//检测纹理是否是2的幂指数， 把不是的打印出来
        SetNonPowOfTwo,//如果图片宽和高是2的幂指数,关闭texture -> advanced -> Non Power of 2
        CheckAndSetTexAlpha,//如果texture 没有alpha通道，或者通道均为0 or 1， 那么关闭alpha is transparency.
        SetTexReadWriteMode,
    }

    class TextureImportParams
    {
        public Platform platform;
        public Actions action;
        public TextureImporterFormat TexImportFormat;
        public int maxSize;
        public bool TexReadWrite = false;

        public TextureImportParams(Actions oneAction, Platform somePlatform = Platform.Default)
        {
            platform = somePlatform;
            action = oneAction;
        }
    }


}
