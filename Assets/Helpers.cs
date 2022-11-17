using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Yavuz.Helpers.Courutines;
namespace Yavuz.Helpers
{
    public static class Helpers
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
        public static bool IsOverUi()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return true;
            else return false;
        }
        // use in Update Method
        public static void GoToPoint(this Transform transform, Vector3 pointB, float speed)
        {
            if (transform.position != pointB)
                transform.position += (transform.position + (pointB - transform.position)).normalized * speed;
        }
        public static void CreateTextMesh(Vector3 position, string text, Vector3 size, int sharpness = 100)
        {
            GameObject obj = new GameObject("Text Mesh");
            GameObject go = new GameObject();
            obj.transform.localScale = size;
            go.transform.SetParent(obj.transform);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one * (1f / sharpness);
            obj.transform.position = position;
            TextMesh tm = go.AddComponent<TextMesh>();
            tm.text = text;
            tm.fontSize = sharpness;
            tm.anchor = TextAnchor.MiddleCenter;
            tm.alignment = TextAlignment.Center;
            obj.transform.forward = Camera.main.transform.forward;
        }
        public static GameObject CreateTextMesh(string text, int sharpness = 100)
        {
            GameObject obj = new GameObject("Text Mesh");
            GameObject go = new GameObject();
            go.transform.SetParent(obj.transform);
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one * (1f / sharpness);
            TextMesh tm = go.AddComponent<TextMesh>();
            tm.text = text;
            tm.fontSize = sharpness;
            tm.anchor = TextAnchor.MiddleCenter;
            tm.alignment = TextAlignment.Center;
            return obj;
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
        public static void DestroyAllChildren(this Transform t)
        {
            foreach (Transform item in t)
            {
                Object.Destroy(item.gameObject);
            }
        }
        // Use instead of WaitForSeconds for optimization
        public static readonly Dictionary<float, WaitForSeconds> WaitDictionary = new Dictionary<float, WaitForSeconds>();
        public static WaitForSeconds GetWait(float time)
        {
            if (WaitDictionary.TryGetValue(time, out WaitForSeconds wait)) return wait;
            WaitDictionary[time] = new WaitForSeconds(time);
            return WaitDictionary[time];
        }
        public static void Mover(this Transform tr, Transform mid, Transform last, float speed,bool complate = false)
        {
            if (tr.gameObject.TryGetComponent(out Enumer enumer1))
            {

                Object.Destroy(enumer1);
            }
            Enumer enumer = tr.gameObject.AddComponent<Enumer>();

            enumer.StartCoroutine(enumer.LerpedMove(mid, last, speed,complate));
        }
        //public static void Mover(this Transform tr, Transform mid, Transform last, float speed, bool complate)
        //{
        //    if (tr.gameObject.TryGetComponent(out Enumer enumer1))
        //    {

        //        Object.Destroy(enumer1);
        //    }
        //    Enumer enumer = tr.gameObject.AddComponent<Enumer>();

        //    enumer.StartCoroutine(enumer.LerpedMove(mid, last, speed,true));
        //}
    }

}
namespace Yavuz.Helpers.Courutines
{
    class Enumer : MonoBehaviour
    {
        public IEnumerator LerpedMove(Transform mid, Transform last, float speed, bool complate)
        {
            float t = 0f;
            Debug.Log(gameObject.name);
            Vector3 firstPosition = transform.position;
            Vector3 normalPoint = (firstPosition + last.position) / 2;
            Vector3 midPlus = mid.position - normalPoint;
            if (complate)
                midPlus += mid.position + midPlus;

            while (true)
            {
                if (t >= 1)
                    Destroy(this);
                Vector3 a = Vector3.Lerp(firstPosition, mid.position, t);
                Vector3 b = Vector3.Lerp(midPlus, last.position, t);
                Vector3 c = Vector3.Lerp(a, b, t);
                t += Time.deltaTime * (1 / speed);
                Debug.Log(c);
                transform.position = c;
                yield return Helpers.GetWait(Time.deltaTime);
            }
        }
    }
}