using UnityEngine;

public static class RuntimeInitializer
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void RuntimeInitializeOnLoadMethod()
    {
        // ResourcesFolderにあるRootObjectオブジェクトを取得する
        GameObject root = Resources.Load<GameObject>("RootObject");

        if (root == null)
        {
            Debug.LogWarning($"{root.name} is not found in Resources.");
            return;
        }
        if(GameObject.Find(root.name))
            return;

        // ここでInstance
        GameObject rootInstance = GameObject.Instantiate(root);
        // InstanceをしないとPrefabを直接書き換えてしまうので注意
        GameObject.DontDestroyOnLoad(rootInstance);
    }
}
