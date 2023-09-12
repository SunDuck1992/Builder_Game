using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
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

        while (flag)
        {
            target.position = Vector3.Lerp(target.position, transform.position, speed * Time.deltaTime);
            target.localScale = Vector3.Lerp(target.localScale, transform.localScale, speed * Time.deltaTime);
            target.rotation = Quaternion.Lerp(target.rotation, transform.rotation, speed * multiplie * Time.deltaTime);

            float distance = Vector3.Distance(target.position, transform.position);
            float rotation = Quaternion.Angle(target.rotation, transform.rotation);

            if(distance <= 0.1f & rotation < 1f)
            {
                target.position = transform.position;
                target.localScale = transform.localScale;
                target.rotation = transform.rotation;

                flag = false;
            }

            yield return null;
        }
    }
}
