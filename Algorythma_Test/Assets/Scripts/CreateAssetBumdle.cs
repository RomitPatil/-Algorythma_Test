
#if UNITY_EDITOR
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEditor;

public class CreateAssetBumdles : MonoBehaviour {

    [MenuItem("Assets/Build Asset Bundle")]

    static void BuildAllAssetBundles() {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles" , BuildAssetBundleOptions.None ,BuildTarget.Android);
    }
}
#endif