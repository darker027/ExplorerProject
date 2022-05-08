using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class answerManager : MonoBehaviour
{
    static public int[] waterArr = new int[8] { 1, 1, 1, 1, 1, 1, 1, 1 };

    static private List<int>[] _waterList;
    void Awake()
    {
        _waterList = new List<int>[8];
        for(int i = 0; i < 8; i++)
        {
            _waterList[i] = new List<int>();
        }
    }


    static public void addWaterList(int waterID1, int waterID2, int result)
    {
   
        //for (int i = 0; i < _waterList[waterID1].Count; i++)
        //{
        //    Debug.Log("Index " + _waterList[waterID1][i]);
        //    Debug.Log("Result " + result);
        //    waterArr[_waterList[waterID1][i]] = result;
        //}
        //for (int i = 0; i < _waterList[waterID2].Count; i++)
        //{
        //    Debug.Log("Index2 " + _waterList[waterID2][i]);
        //    Debug.Log("Result2 " + result);
        //    waterArr[_waterList[waterID2][i]] = result;
        //}
        for(int i = 0; i < _waterList.Length; i++)
        {
            //for(int j = 0; j < _waterList[i].Count; j++)
            //{

            //}
            bool containId1 = _waterList[i].Contains(waterID1);
            bool containId2 = _waterList[i].Contains(waterID2);
            if (containId1|| containId2)
            {
                waterArr[i] = result;

                //recursion check
                if (containId1 == false) 
                {
                    _waterList[i].Add(waterID1);
                }
                if (containId2 == false)
                {
                    _waterList[i].Add(waterID2);
                }

                continue;
            }
        }
        Debug.Log("id1 " + waterID1);
        Debug.Log("id2 " + waterID2);
        for (int i = 0; i < waterArr.Length; i++)
        {


            Debug.Log("index" + i + " " + waterArr[i]);

        }
        _waterList[waterID1].Add(waterID2);
        _waterList[waterID2].Add(waterID1);

    }
}
