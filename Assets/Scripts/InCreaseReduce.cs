using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InCreaseReduce : MonoBehaviour
{
    public float zoomSpeed = 0;
    public float zoomSpeed2 = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        zoomSpeed2 = Input.GetAxis("Mouse ScrollWheel");
        if(zoomSpeed!=zoomSpeed2)
        {
            zoomSpeed = zoomSpeed2;
            // if(zoomSpeed==0){

            // }else if(zoomSpeed>0){
            //     Controller.Instance.bdir = 1;
            // }else{
            //      Controller.Instance.bdir = -1;
            // }
          
            for(int i=0; i< Controller.Instance.blockMiniList.Count;i++){
                Controller.Instance.blockMiniList[i].Tangkhoangcach(zoomSpeed);
            }
        }
    }
}
