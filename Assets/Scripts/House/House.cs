using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class House : MonoBehaviour
{
    [SerializeField] private List<GameObject> _cells = new List<GameObject>();
    private Queue<Brick> _cellsBricks = new ();

    private int _builtBricks = 0;

    public int FullCountBricks => _cells.Count;
    public int BuiltBricks => _builtBricks;
    public bool IsCanBuild => _cellsBricks.Count > 0;

    private void Start()
    {
        var bricks = transform.GetComponentsInChildren<Brick>();

        for(int i = 0; i < bricks.Length; i++)
        {
            _cellsBricks.Enqueue(bricks[i]);
        }
        

        //foreach (Transform child in transform)
        //{
        //    if (child.GetComponent<Brick>())
        //    {
        //        _cells.Add(child.gameObject);
        //        child.gameObject.SetActive(false);
        //    }
        //}
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

    public void ActivateCell(int number)
    {
        GameObject brick = _cells[number - 1];

        if (brick != null)
        {
            brick.SetActive(true);
        }
    }

    public void ChangeQuantityBricks()
    {
        if (_builtBricks < FullCountBricks)
        {
            _builtBricks++;
        }
    }

    public GameObject GetCell(int number)
    {
        if (_builtBricks < FullCountBricks)
        {
            GameObject brick = _cells[number];
            return brick;
        }
        return null;
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
