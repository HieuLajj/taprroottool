using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class ControllerInput : MonoBehaviour
{
    public float speedrotate = 3.5f;
    private Vector3 screenPosition;
    private Vector3 lastMousePos;
    private Vector3 lastMousePos2;
    private float timer = 0;
    private Vector3 lookAt;
    void Start()
    {
        
    }
    void Update()
    {
        userInteraction();
    }

    public void userInteraction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePos = Input.mousePosition;
            lastMousePos2 = lastMousePos;
            timer = 0;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - lastMousePos;
            float delta3 = Vector2.Distance(Input.mousePosition, lastMousePos);
            lastMousePos = Input.mousePosition;
           // timer += Time.deltaTime;
          //  if (delta3 < 15f && timer <= 0.3f) return;
            //if (Controller.Instance.LevelIDInt >= 4)
            {
                lookAt.x = Mathf.Lerp(lookAt.x, -delta.x / speedrotate, 15 * Time.deltaTime);
                lookAt.y = Mathf.Lerp(lookAt.y, delta.y / speedrotate, 15 * Time.deltaTime);


                Controller.Instance.pretransform.transform.Rotate(Vector3.up, lookAt.x, Space.World);
                Controller.Instance.pretransform.transform.Rotate(Vector3.right, lookAt.y, Space.World);
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            screenPosition = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);
            if (Physics.Raycast(ray, out RaycastHit hitData, Mathf.Infinity, 1 << 6))
            {
                BlockMini block = hitData.collider.GetComponent<BlockMini>();
                block.checkRay();
            }
        }


        //if (Input.GetMouseButtonDown(0))
        //{
        //    screenPosition = Input.mousePosition;
        //    Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        //    if (Physics.Raycast(ray, out RaycastHit hitData, Mathf.Infinity, 1<<6))
        //    {
        //        BlockMini block = hitData.collider.GetComponent<BlockMini>();
        //        block.checkRay();
        //    }
        //}
    }
}
