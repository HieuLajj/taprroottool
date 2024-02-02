using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using System.Xml;
using System;

public class Controller : MonoBehaviour
{
    public GameObject blockDoidien;
    private static Controller instance;
    public static Controller Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Controller>();
            }
            return instance;
        }
        private set
        {
            instance = value;
        }
    }

    public Transform pretransform;

    public List<BlockMini> blockMiniList;
    public List<BlockMini> blockMiniListBansao;

    public Dictionary<string, BlockMini> blockMiniDictionary = new Dictionary<string, BlockMini>();


    public List<BlockMini> blockMiniListTemp_FilterObject;



    public List<BlockMini> listcansapxeplai;

    [Button()]    
    
    public void FilterObject()
    {
        for(int i = 0; i < blockMiniList.Count; i++)
        {
            if (blockMiniList[i].checkTrigger == false)
            {
                blockMiniList[i].gameObject.SetActive(false);
                blockMiniListTemp_FilterObject.Add(blockMiniList[i]);
            }
            else
            {

                int xx = (int)(blockMiniList[i].transform.position.x * 10000);
                int yy = (int)(blockMiniList[i].transform.position.y * 10000);
                int zz = (int)(blockMiniList[i].transform.position.z * 10000);
                string key = xx + "|" + yy + "|" + zz;
                blockMiniDictionary.Add(key, blockMiniList[i]);
            }
        }


        for(int i=0; i<blockMiniListTemp_FilterObject.Count; i++)
        {
            blockMiniList.Remove(blockMiniListTemp_FilterObject[i]);
        }
    }

    // chuc nang doi xung

    //đối xứng trục y qua ox (la thay doi z);
    [Button()]
    public void PrintArrayOy()
    {
        Debug.Log("dang tien hanh");
        foreach (KeyValuePair<string, BlockMini> pair in blockMiniDictionary)
        {
           
            string keyValue = pair.Key;
            //Debug.Log(keyValue);
            string[] numbersAsStrings = keyValue.Split('|');
         
            float[] numbersAsFloats = new float[numbersAsStrings.Length];
       
            for (int i = 0; i < numbersAsStrings.Length; i++)
            {
                numbersAsFloats[i] = float.Parse(numbersAsStrings[i]);
              
            }
            string keyValueOpposite = numbersAsFloats[0] + "|" + numbersAsFloats[1] + "|" + -1 * numbersAsFloats[2];
            if (blockMiniDictionary.ContainsKey(keyValueOpposite))
            {
            }
            else
            {
                BlockMini blockMini = pair.Value;
                blockMini.gameObject.SetActive(false);
                blockMiniList.Remove(pair.Value);
            }
        }
    }


    //đối xứng trục y qua oz (la thay doi x);
    [Button()]
    public void PrintArray2Oy()
    {
        Debug.Log("dang tien hanh 2");

        foreach (KeyValuePair<string, BlockMini> pair in blockMiniDictionary)
        {
            string keyValue = pair.Key;
            string[] numbersAsStrings = keyValue.Split('|');
            float[] numbersAsFloats = new float[numbersAsStrings.Length];

            for (int i = 0; i < numbersAsStrings.Length; i++)
            {
                numbersAsFloats[i] = float.Parse(numbersAsStrings[i]);

            }
            string keyValueOpposite = -1 * numbersAsFloats[0] + "|" + numbersAsFloats[1] + "|" + numbersAsFloats[2];
            if (blockMiniDictionary.ContainsKey(keyValueOpposite))
            {
            }
            else
            {
                BlockMini blockMini = pair.Value;
                blockMini.gameObject.SetActive(false);
                blockMiniList.Remove(pair.Value);
            }
        }
    }

    [Button()]

    public void TienhanhCheckNhanh()
    {
        blockMiniListBansao = blockMiniList;
        CheckNhanh();
    }
    public List<BlockMini> ListenerBlock222 = new List<BlockMini>();

    public void CheckNhanh()
    {
        int soluongtruocdo = blockMiniList.Count;
        int soluongsaudo = soluongtruocdo;
        foreach (var item in blockMiniList)
        {
            BlockMini blockmini = item.CheckFast();
            if (blockmini != null)
            {
                blockmini.RunBlock();
                soluongsaudo--;
                if (listcansapxeplai.Contains(blockmini))
                {
                    listcansapxeplai.Remove(blockmini);
                }
                ListenerBlock222.Add(blockmini);
            }
            else
            {
                listcansapxeplai.Add(item);
            }
        }

        foreach (var item in ListenerBlock222)
        {
            blockMiniList.Remove(item);
        }
        ListenerBlock222.Clear();

        if (soluongsaudo == soluongtruocdo)
        {
            Debug.Log("KHong co duong thoat o level, can thuc hien lai vong lap");
            for(int g=0; g < blockMiniList.Count; g++)
            {
               blockMiniList[g].GetDirectionBlock(UnityEngine.Random.Range(1, 6));
            }
            CheckNhanh();
            return;
        }
        if (blockMiniList.Count == 0)
        {
            Debug.Log("hoan tat" +"===movethucte+++==============================");      
            return;
        }
        CheckNhanh();
    }


    [Button]
    public void TienhanhCheckNhanhCacKhoiDoiDau()
    {
        foreach (var item in blockMiniList)
        {
            item.checkRay2();
        }

    }


}
