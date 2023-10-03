using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Materials
{
    None = 0,
    Brick,
    Board,
    Roof
}

public class BuildMaterial : MonoBehaviour
{
    [SerializeField] private Materials _materials;

    public Materials Materials => _materials;

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void PutBrick(Transform target, float speed)
    {
        StartCoroutine(PutToPosition(target, speed));
    }

    private IEnumerator PutToPosition(Transform target, float speed)
    {
        bool flag = true;
        float multiplie = 2f;
        Vector3 scale = target.localScale;

        while (flag)
        {
            target.position = Vector3.Lerp(target.position, transform.parent.position, speed * Time.deltaTime);
            target.localScale = Vector3.Lerp(target.localScale, transform.parent.localScale, speed * Time.deltaTime);
            target.rotation = Quaternion.Lerp(target.rotation, transform.parent.rotation, speed * multiplie * Time.deltaTime);

            float distance = Vector3.Distance(target.position, transform.parent.position);
            float rotation = Quaternion.Angle(target.rotation, transform.parent.rotation);

            if(distance <= 0.1f & rotation < 1f)
            {
                target.position = transform.parent.position;
                target.localScale = transform.parent.localScale;
                target.rotation = transform.parent.rotation;

                flag = false;
            }

            yield return null;
        }

        PoolService.Instance.GetPool(target.gameObject).DeSpawn(target.gameObject);
        target.localScale = scale;
        GetComponent<MeshRenderer>().enabled = true;
    }
}
