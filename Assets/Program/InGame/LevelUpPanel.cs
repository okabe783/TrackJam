using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelupPanel : MonoBehaviour
{
    public static LevelupPanel instance;
    Text itemText;
    public GameManager GMscript;
    string ItemName;

    [SerializeField] GameObject LevelUPUI;
    Image itemimage;

    [SerializeField] Sprite imageFunnel;
    [SerializeField] Sprite imageTsuba;
    public int argmentrnd;
    public Image Imagename;
    //public GamemanegerScript GMscript;
    [SerializeField] GameObject drone;
    [SerializeField] StatusData statusdata;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LevelPanelprocess()
    {
        //argmentrnd = Random.Range(0, 2);
        //itemimage = this.GetComponentInChildren<Image>();
        //if (argmentrnd == 0)
        //{
        //    Debug.Log(argmentrnd);
        //    itemimage.sprite = imageFunnel;
        //    GMscript.gameObject.GetComponent<GamemanegerScript>().ItemNum(argmentrnd);
        //}
        //if (argmentrnd == 1)
        //{
        //    Debug.Log(argmentrnd);
        //    itemimage.sprite = imageTsuba;
        //    GMscript.gameObject.GetComponent<GamemanegerScript>().ItemNum(argmentrnd);
        //}

    }

    public void Onclick()
    {
        Imagename = this.gameObject.GetComponent<Image>();
        Debug.Log(Imagename.sprite.name);
        if (Imagename.sprite.name == "ItemPanel0")
        {
            //var Drone = Instantiate(drone, transform.position, transform.rotation);//ドローンの生成
        }

        if (Imagename.sprite.name == "ItemPanel1")
        {
            Debug.Log("パンチを選択");
            //statusdata.ATK++;
        }

        if (Imagename.sprite.name == "ItemPanel2")
        {
            Debug.Log("聖水を選択");
        }

        Time.timeScale = 1;
        LevelUPUI.GetComponent<Canvas>().enabled = false;

    }


}
