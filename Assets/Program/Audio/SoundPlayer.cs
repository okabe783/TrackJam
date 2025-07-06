using UnityEngine;

// AudioManagerはゲーム中に1つあればいいのでシングルトン化する
public class SoundPlayer : SingletonMonoBehaviour<SoundPlayer>
{
    // シングルトン化してしまうとどこからでも参照を取得することができてしまうので
    // 外部に公開する必要のないものはしっかりとprivateにしておく
    
    [Header("Audio Component")]
    [SerializeField] private AudioSource[] _seSource;
    // BGMは複数再生することはあまりないので複数のObjectはつくらない
    [SerializeField] private AudioSource _bgmSource;

    // BGMを再生する
    public void PlayBgm(AudioClip clip)
    {
        _bgmSource.clip = clip; 
        _bgmSource.Play();
    }

    // BGMを止める
    public void StopBgm()
    {
        _bgmSource.Stop();
    }

    // SEを再生する
    public void PlaySe(AudioClip clip)
    {
        foreach (AudioSource source in _seSource)
        {
            if (source.isPlaying == true) 
                continue;
            
            source.clip = clip;
            source.Play();
            return;
        }

        Debug.LogWarning("No se played");
    }
}
