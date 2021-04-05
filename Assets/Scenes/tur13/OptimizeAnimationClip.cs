using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OptimizeAnimationClip
{

    public static int FloatNumber = 2;

    [MenuItem("Optimization/优化动画文件/压缩精度")]
    public static void OptimizeAnim()
    {
        var tObjArr = Selection.gameObjects;
        foreach (var obj in tObjArr)
        {
            RemoveAnimationCurve(obj);
        }
    }

    public static List<AnimationClip> GetAllAnimClips(GameObject obj)
    {
        return new List<AnimationClip>(AnimationUtility.GetAnimationClips(obj));
    }

    public static void RemoveAnimationCurve(GameObject _obj)
    {
        List<AnimationClip> tAnimationClipList = new List<AnimationClip>(AnimationUtility.GetAnimationClips(_obj));
        if (tAnimationClipList.Count == 0)
        {
            AnimationClip[] tObjectList = UnityEngine.Object.FindObjectsOfType(typeof(AnimationClip)) as AnimationClip[];
            tAnimationClipList.AddRange(tObjectList);
        }

        foreach (AnimationClip animClip in tAnimationClipList)
        {
            foreach (EditorCurveBinding curveBinding in AnimationUtility.GetCurveBindings(animClip))
            {
                string tName = curveBinding.propertyName.ToLower();
                if (tName.Contains("scale"))
                {
                    AnimationUtility.SetEditorCurve(animClip, curveBinding, null);
                }
            }
            CompressAnimationClip(animClip);
        }

        AssetDatabase.SaveAssets();
        Debug.Log("Finish");
    }

    //压缩精度
    public static void CompressAnimationClip(AnimationClip _clip)
    {
        AnimationClipCurveData[] tCurveArr = AnimationUtility.GetAllCurves(_clip);
        Keyframe tKey;
        Keyframe[] tKeyFrameArr;
        for (int i = 0; i < tCurveArr.Length; ++i)
        {
            AnimationClipCurveData tCurveData = tCurveArr[i];
            if (tCurveData.curve == null || tCurveData.curve.keys == null)
            {
                continue;
            }
            tKeyFrameArr = tCurveData.curve.keys;
            for (int j = 0; j < tKeyFrameArr.Length; j++)
            {
                tKey = tKeyFrameArr[j];
                tKey.value = float.Parse(tKey.value.ToString("f") + FloatNumber.ToString());    //#.###
                tKey.inWeight = float.Parse(tKey.value.ToString("f") + FloatNumber.ToString());
                tKey.outWeight = float.Parse(tKey.value.ToString("f") + FloatNumber.ToString());
                tKey.inTangent = float.Parse(tKey.inTangent.ToString("f") + FloatNumber.ToString());
                tKey.outTangent = float.Parse(tKey.outTangent.ToString("f") + FloatNumber.ToString());
                tKeyFrameArr[j] = tKey;
            }
            tCurveData.curve.keys = tKeyFrameArr;
            _clip.SetCurve(tCurveData.path, tCurveData.type, tCurveData.propertyName, tCurveData.curve);
        }
    }
}
