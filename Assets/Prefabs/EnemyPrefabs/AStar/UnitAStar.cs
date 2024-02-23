using UnityEngine;
using System.Collections;

public class UnitAStar : MonoBehaviour
{


    public Transform target;
    public float speed ;
    Vector3[] path;
    int targetIndex;

    public void Chase(Transform _target)
    {

            target = _target;
            PathRequestManager.RequestPath(gameObject.transform.position, target.position, OnPathFound);
        
    }

    public void OnPathFound(Vector3[] newPath, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            path = newPath;
            targetIndex = 0;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        if (path.Length!=0)
        {
            Vector3 currentWaypoint = path[0];
            while (true)
            {
                if (transform.position == currentWaypoint)
                {
                    targetIndex++;
                    if (targetIndex >= path.Length)
                    {
                        yield break;
                    }
                    currentWaypoint = path[targetIndex];
                }

                transform.position = Vector3.MoveTowards(transform.position, currentWaypoint, speed * Time.deltaTime);
                yield return null;

            }
        }
    }

}