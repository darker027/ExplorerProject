using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class upgrade : MonoBehaviour
{
    private static upgrade _instance;
    private bool isTorch;
    private bool isIce;
    public static upgrade Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(this.gameObject);
        isTorch = false;
        isIce = false;
    }
    public void setTorch(bool torch) {

        isTorch = torch;
    }
    public void setIce(bool ice)
    {
        isIce = ice;
    }
    public bool getTorch()
    {
        return isTorch;
    }
    public bool getIce()
    {
        return isIce;
    }
}
