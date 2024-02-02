using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;


public enum DirectionBlock
{
    Down,
    Up,
    Back,
    Forward,
    Left,
    Right
}
public enum StatusBlock
{
    Die,
    Normal,
}

public class BlockMini : MonoBehaviour
{
    public StatusBlock statusBlock = StatusBlock.Normal;


    private Vector3 dir2;
    private Vector3 dir;
    private RaycastHit hit;
    public DirectionBlock Direction;
    public GameObject ModelBlock;
    public bool checkTrigger = false;
    private int intDirection;
    private void OnEnable()
    {
        GetDirectionBlock(UnityEngine.Random.Range(1,6));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            checkTrigger = true;
        }        
    }

    public void GetDirectionBlock(int input)
    {
        intDirection = input;
        switch (input)
        {
            case 1:
                Direction = DirectionBlock.Down;
                dir2 = Vector3.down;
                ModelBlock.transform.localEulerAngles = new Vector3(90, 0, 0);
                break;
            case 2:
                Direction = DirectionBlock.Up;
                dir2 = Vector3.up;
                ModelBlock.transform.localEulerAngles = new Vector3(-90, 0, 0);
                break;
            case 3:
                Direction = DirectionBlock.Back;
                dir2 = Vector3.back;
                ModelBlock.transform.localEulerAngles = new Vector3(180, 0, 0);
                break;
            case 4:
                Direction = DirectionBlock.Forward;
                ModelBlock.transform.localEulerAngles = new Vector3(0, 0, 0);
                dir2 = Vector3.forward;
                break;
            case 5:
                Direction = DirectionBlock.Left;
                dir2 = Vector3.left;
                ModelBlock.transform.localEulerAngles = new Vector3(0, -90, 0);
                break;
            case 6:
                Direction = DirectionBlock.Right;
                dir2 = Vector3.right;
                ModelBlock.transform.localEulerAngles = new Vector3(0, 90, 0);
                break;
            default:
                Direction = DirectionBlock.Right;
                dir2 = Vector3.right;
                ModelBlock.transform.localEulerAngles = new Vector3(0, 90, 0);
                break;
        }
    }

    public void checkRay()
    {
        checkDirection();
        if (Physics.Raycast(transform.position, dir, out hit, Mathf.Infinity, (1 << 6)))
        {
            BlockMini blockcheck = hit.collider.GetComponent<BlockMini>();
            if (blockcheck.statusBlock == StatusBlock.Die)
            {
                RunBlock();
                return;
            }
        }
        else
        {
            RunBlock();
        }
      //  checkRay2();
    }

    public void checkRay2()
    {
        checkDirection();
        RaycastHit[] hits = Physics.RaycastAll(transform.position, dir, Mathf.Infinity, (1 << 6));
     
        foreach (RaycastHit hit in hits)
        {
            BlockMini blockcheck = hit.collider.GetComponent<BlockMini>();
            if(CheckBugOpposite(blockcheck)==true)
            {
                blockcheck.RandomIngore(intDirection);
              //  blockcheck.gameObject.SetActive(false);
            }
          
        }
    }

    public void checkDirection()
    {
        switch (Direction)
        {
            case DirectionBlock.Left:
                dir = -transform.right;
                break;
            case DirectionBlock.Right:
                dir = transform.right;
                break;
            case DirectionBlock.Up:
                dir = transform.up;
                break;
            case DirectionBlock.Down:
                dir = -transform.up;
                break;
            case DirectionBlock.Forward:
                dir = transform.forward;
                break;
            case DirectionBlock.Back:
                dir = -transform.forward;
                break;
            default:
                dir = -transform.forward;
                break;
        }
    }
    public void RunBlock()
    {    

        statusBlock = StatusBlock.Die;
        Vector3 dir3 = transform.TransformDirection(dir2);      
        StartCoroutine(Run(dir3));
    }
    IEnumerator Run(Vector3 direction)
    {
       
        float alpha = 12;
        while (alpha >= 0)
        {
            transform.Translate(direction * 20 * Time.deltaTime, Space.World);
            yield return null;
            alpha -= 0.1f;
        }
        gameObject.SetActive(false);
    }

    public BlockMini CheckFast()
    {
        checkDirection();
        if(statusBlock == StatusBlock.Die)
        {
            return null;
        }

        if (Physics.Raycast(transform.position, dir, out hit, Mathf.Infinity, 1 << 6))
        {

            BlockMini blockcheck = hit.collider.GetComponent<BlockMini>();

            if (blockcheck.statusBlock == StatusBlock.Die)
            {
                return this;
            }
            else
            {
                return null;
            }
        }
        else
        {
            return this;
        }
    }
    public bool CheckBugOpposite(BlockMini blockOpposite)
    {
        bool checkboolbug = false;
        switch (Direction)
        {
            case DirectionBlock.Down:
                if (blockOpposite.Direction == DirectionBlock.Up)
                {
                    checkboolbug = true;
                }
                break;
            case DirectionBlock.Up:
                if (blockOpposite.Direction == DirectionBlock.Down)
                {
                    checkboolbug = true;
                }
                break;

            case DirectionBlock.Forward:
                if (blockOpposite.Direction == DirectionBlock.Back)
                {
                    checkboolbug = true;
                }
                break;
            case DirectionBlock.Back:
                if (blockOpposite.Direction == DirectionBlock.Forward)
                {
                    checkboolbug = true;
                }
                break;
            case DirectionBlock.Left:
                if (blockOpposite.Direction == DirectionBlock.Right)
                {
                    checkboolbug = true;
                }
                break;
            case DirectionBlock.Right:
                if (blockOpposite.Direction == DirectionBlock.Left)
                {
                    checkboolbug = true;
                }
                break;
        }
        return checkboolbug;
    }

    public void RandomIngore(int a)
    {
      
        int randomNumber;

        do
        {
            randomNumber = UnityEngine.Random.Range(1, 6); // Sinh số ngẫu nhiên từ 1 đến 6
        } while (randomNumber == a); // Lặp lại nếu số ngẫu nhiên bằng 2

        GetDirectionBlock(randomNumber);
    }
}
