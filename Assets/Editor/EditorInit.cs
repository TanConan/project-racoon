using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

/*
From: https://stackoverflow.com/questions/35586103/unity3d-load-a-specific-scene-on-play-mode
*/
[InitializeOnLoad]
public class EditorInit
{
  static EditorInit()
  {
    var pathOfFirstScene = EditorBuildSettings.scenes[0].path;
    var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(pathOfFirstScene);
    EditorSceneManager.playModeStartScene = sceneAsset;
    Debug.Log(pathOfFirstScene + " was set as default play mode scene");
  }
}
