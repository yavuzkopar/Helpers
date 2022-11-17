using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yavuz.Helpers;

public class Test : MonoBehaviour
{
    Rigidbody rb;
    void Start()
    {
     
        rb = GetComponent<Rigidbody>();
    }
    [SerializeField] Transform pik, pik2;
    [SerializeField] float speed;
    [SerializeField] bool complate;
   
    void Update()
    {
         
        
        
        if (Input.GetMouseButtonDown(0))
        {
            //Utils.HitInfo();
            //StartCoroutine("GoBaby");
            //transform.GoToPoint(transform.forward);
            //DenemeClass denemeClass = new DenemeClass();
            //denemeClass.Dene();


            //transform.GoToPointB(Utils.HitInfo().point);
            //rb.Jump(5, transform.forward * 10);
            //this.LerpedMove(pik.position, pik2.position, speed, false);

            //Vector3 a = Vector3.Reflect(Utils.HitInfo().point, Utils.HitInfo().normal);
            //Utils.CreateSomeThing();
            // gameObject.ChangeLayerTo("Water");
            //Utils.CreateTextMesh(Vector3.zero,Vector3.one*20, "Heyyo", 100);
            //GameObject go = Helpers.CreateTextMesh("alo", 100);
            //go.transform.localScale = Vector3.one * 20;
            //Vector3 a = new Vector3(2.6f,3,5);
            transform.Mover(pik, pik2,1f,complate);
            
            
        }
       
        //    Debug.Log("front : " +pik.IsInFrontOf(transform));
        //Debug.Log("Top : " +pik.IsOnTopOf(transform));
        //Debug.Log("Right : " +pik.IsInRightOf(transform));
        //transform.LookSmoothly(pik.position, 5);
    }
    

}
