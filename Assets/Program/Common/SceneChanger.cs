using UnityEngine.SceneManagement;

// シーンを切り換えるためのクラス
public class SceneChanger : SingletonMonoBehaviour<SceneChanger>
{
    // シーンの読み込み
    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
