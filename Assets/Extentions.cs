using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extentions 
{
    #region Navigation
    public static bool IsInRightOf(this Transform trans, Transform target)
    {
        return Vector3.Dot(target.right, (trans.position - target.position).normalized) > 0;
    }
    public static bool IsOnTopOf(this Transform trans, Transform target)
    {
        return Vector3.Dot(target.up, (trans.position - target.position).normalized) > 0;
    }
    public static bool IsInFrontOf(this Transform trans, Transform target)
    {
        return Vector3.Dot(target.forward, (trans.position - target.position).normalized) > 0;
    }
    #endregion
    public static void Jump(this Rigidbody rb, float y, Vector3 yon)
    {
        float gravity = Physics.gravity.y;
        Vector3 displacementXZ = new Vector3(yon.x, 0, yon.z);
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * y);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * y / gravity) + Mathf.Sqrt(2 * -y / gravity));
        rb.velocity = velocityXZ + velocityY;
    }

    public static Transform FindClosestTransform(this Transform transform, IEnumerable<Transform> transforms)
    {
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform item in transforms)
        {
            Vector3 directionToTarget = item.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = item;
            }
        }

        return bestTarget;
    }
    public static GameObject FindClosestTransform(this Transform transform, IEnumerable<GameObject> transforms)
    {
        GameObject bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (GameObject item in transforms)
        {
            Vector3 directionToTarget = item.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if (dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = item;
            }
        }

        return bestTarget;
    }
    public static void ChangeLayerTo(this GameObject go,string layerName)
    {
        int LayerIgnoreRaycast = LayerMask.NameToLayer(layerName);
        go.layer = LayerIgnoreRaycast;
    }
    public static void LookSmoothly(this Transform transform, Vector3 point,float speed)
    {
        Vector3 lookPos = point - transform.position;
        Quaternion look = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, look, Time.deltaTime * speed);
    }
}
