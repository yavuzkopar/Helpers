using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UtilTests
{
    public static class Utils
    {
        #region RaycastHit
        /// <summary>
        /// Cameradan mouse posizyonuna bir ray firlatir
        /// </summary>
        /// <returns>Rayin hit infosunu getirir</returns>
        public static RaycastHit HitInfo()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity);

            return hit;
        }
        public static RaycastHit HitInfo(float maxDistance)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(ray, out RaycastHit hit, maxDistance);
            return hit;
        }
        public static RaycastHit HitInfo(float maxDistance, LayerMask layerMask)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(ray, out RaycastHit hit, maxDistance, layerMask);
            return hit;
        }
        public static RaycastHit HitInfo(LayerMask layerMask)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, layerMask);
            return hit;
        }
        #endregion

        // use in Update Method
        public static void GoToPoint(this Transform transform, Vector3 pointB,float speed)
        {
            if (transform.position != pointB)
                transform.position += (transform.position + (pointB - transform.position)).normalized * speed;
        }
      
       
        #region LerpedMove
        public static void LerpedMove(this MonoBehaviour transform, Vector3 pikPoint, Vector3 targetPoint, float speed, bool isUI)
        {
            Lerped lerped = new Lerped(transform, pikPoint * 2, targetPoint, speed);

            if (isUI)
            {
                lerped.pikPoint -= lerped.transform.transform.position;
                lerped.speed *= GameObject.FindObjectOfType<Canvas>().transform.position.magnitude / 10;
            }

            transform.StartCoroutine(lerped.Execute());

        }
        #endregion

        public static void CreateTextMesh(Vector3 position)
        {
            GameObject obj = new GameObject();
            obj.transform.position = position;
            TextMesh tm = obj.AddComponent<TextMesh>();
            tm.text = "Hello World";
            obj.transform.forward = Camera.main.transform.forward;
        }
       
        public static Transform FindClosestTransform(Transform transform, IEnumerable<Transform> transforms)
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
        public static GameObject FindClosestTransform(Transform transform, IEnumerable<GameObject> transforms)
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
    }

    public class Lerped
    {
        public MonoBehaviour transform;
        public Vector3 pikPoint;
        public Vector3 targetPoint;
        float t = 0;
        public float speed;
        float mesafe;
        public Lerped(MonoBehaviour transform, Vector3 pikPoint, Vector3 targetPoint, float speed)
        {
            this.transform = transform;
            this.pikPoint = pikPoint;
            this.targetPoint = targetPoint;
            this.speed = speed;

            mesafe = Vector3.Distance(transform.transform.position, pikPoint) + Vector3.Distance(pikPoint, targetPoint);
        }
        public IEnumerator Execute()
        {
            Vector3 cc = transform.transform.position;
            while (true)
            {

                transform.transform.position = Don(cc);
                if (t >= 1)
                    break;
                t += Time.deltaTime * speed / mesafe;

                yield return new WaitForEndOfFrame();
            }
        }
        Vector3 Don(Vector3 ab)
        {
            Vector3 a = Vector3.Lerp(ab, pikPoint, t);
            Vector3 b = Vector3.Lerp(pikPoint, targetPoint, t);
            return Vector3.Lerp(a, b, t);
        }
        
    }
    



}