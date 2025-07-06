using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("必要なコンポーネント")]
    [SerializeField] private Text TimerText;
    [SerializeField] 
    //変数
    private bool _stopTime=false;
   [SerializeField]public float _timer = 200;//中ボスが出るまで

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }
    private void Timer()
    {
        if (!_stopTime&&TimerText!=null)
        {
            _timer -= Time.deltaTime; 
            TimerText.text = _timer.ToString();
        }

        if (_timer<1)
        {
            Debug.Log("BOSS");
            GameObject.Destroy(TimerText);
            TimerText = null;
        }

    }
}
