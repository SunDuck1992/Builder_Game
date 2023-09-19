using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class House : MonoBehaviour
{
    private Queue<Brick> _cellsBricks = new ();

    public bool IsCanBuild => _cellsBricks.Count > 0;
    public int MaxCount { get; private set; }
    


    private void Awake()
    {
        var bricks = transform.GetComponentsInChildren<Brick>();

        for(int i = 0; i < bricks.Length; i++)
        {
            _cellsBricks.Enqueue(bricks[i]);
        }

        MaxCount = bricks.Length;
    }

    public void BuildElement(Transform target, float speed)
    {
        if(target == null)
        {
            return;
        }

        var brick = _cellsBricks.Dequeue();
        brick.PutBrick(target, speed);
    }

    [ContextMenu("Hide")]
    public void HideBricks()
    {
        var bricks = transform.GetComponentsInChildren<Brick>();

        for (int i = 0; i < bricks.Length; i++)
        {
            bricks[i].GetComponent<MeshRenderer>().enabled = false;
        }
    }

    [ContextMenu("Show")]
    public void ShowBricks()
    {
        var bricks = transform.GetComponentsInChildren<Brick>();

        for (int i = 0; i < bricks.Length; i++)
        {
            bricks[i].GetComponent<MeshRenderer>().enabled = true;
        }
    }
}
