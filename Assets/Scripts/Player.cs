using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotateSpeed = 100f;

    [SerializeField] [Range(0,2)] private float coolDown = 2f;
    [SerializeField] private Transform nextPortal;

    private float timeIn = 0f;
    private Vector3 posInicial;
    private bool IsNormal = true;

    private float randX;
    private float randY;
    private float randZ;

    private PlayerData playerData;

    private void Start(){
        posInicial = transform.position;
        playerData = GetComponent<PlayerData>();
    }

    #region Move Player
    private void Update()
    {
        CheckMovement();
        CheckRotation();
    }

    private void CheckMovement()
    {
        var xMove = transform.right * (Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime);
        var zMove = transform.forward * (Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime);
        var move = xMove + zMove;

        transform.position += move;
    }

    private void CheckRotation()
    {
        var rotation = Input.GetAxisRaw("Mouse X") * rotateSpeed * Time.deltaTime * 500;

        transform.Rotate(0f, rotation, 0f);
    }
    #endregion

    #region Practica de OnCollision
    // private void OnCollisionExit(Collision other) {
    //     Debug.Log("OnCollisionExit");
    //     Debug.Log("OnCollisionExit ->" + other.gameObject.name);
    //     Debug.Log("OnCollisionExit ->" + other.gameObject.tag);

    //     if(other.gameObject.CompareTag("Portal")){
    //         Vector3 local = transform.localScale;
	// 		transform.localScale = local*2;
    //     }

    // }

    // private void OnCollisionEnter(Collision other) {
    //     Debug.Log("OnCollisionEnter");
    //     Debug.Log("OnCollisionEnter ->" + other.gameObject.name);
    //     Debug.Log("OnCollisionEnter ->" + other.gameObject.tag);

    // }



    // private void OnCollisionStay(Collision other) {
    //     Debug.Log("OnCollisionStay");
    //     Debug.Log("OnCollisionStay ->" + other.gameObject.name);
    //     Debug.Log("OnCollisionStay ->" + other.gameObject.tag);        
    // }
    #endregion

    private void Respawn(){
        transform.position = posInicial;
        
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("OnCollisionEnter ->" + other.gameObject.name);
        timeIn = 0f;

        if(other.transform.gameObject.tag == "coin"){
            Destroy(other.transform.gameObject);
            //Incrementar # Coin 
            //GetComponent<PlayerData>().NumberOfCoin ++; // FORMA01 - Genera multiples instancias.
            playerData.CountUp(1);
        }
    }
    private void OnTriggerExit(Collider other) {
        Debug.Log("OnCollisionExit ->" + other.gameObject.name);
        if(other.transform.gameObject.tag == "Portal"){
            if(IsNormal){
                Debug.Log("Crece ->" + other.gameObject.name);
                Respawn();
                transform.localScale = transform.localScale /4 ;
                IsNormal =false;
            }
            else{
                Debug.Log("DeCrece ->" + other.gameObject.name);
                Respawn();
                transform.localScale = transform.localScale*4;
                IsNormal =true;
            }
        }
    }
    private void OnTriggerStay(Collider other) {
        Debug.Log("OnCollisionStay ->" + other.gameObject.name);
        if( other.transform.gameObject.tag=="GoldenWall"){
            timeIn += Time.deltaTime;
            if (timeIn > coolDown){
                randX = Random.Range(-24,24);
                randY = Random.Range(-50,50);
                randZ = Random.Range(-24,24);
                other.transform.position = new Vector3(randX,1,randZ);
                other.transform.rotation = Quaternion.Euler(0,randY,0);
                timeIn = 0;
            }
        }
    }

}
