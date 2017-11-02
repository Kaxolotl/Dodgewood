using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    public GameObject player;
    public float ballSpeed;
    Rigidbody myrigid;

    public Vector3 firePos;
    public Vector3 RVector;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        myrigid = GetComponent<Rigidbody>();
        ballSpeed = 10000;
    }

    void OnEnable()
    {
        firePos = gameObject.transform.position;

        myrigid.AddForce((gameObject.transform.position - player.transform.position).normalized * ballSpeed * Time.smoothDeltaTime);
    }

    private void FixedUpdate()
    {
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.5f, gameObject.transform.position.z);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag != "WALL")
            return;

        Debug.Log("원점 : " + firePos);

        //반대벡터 = 원점(발사한곳의 벡터) - 교차지점(충돌지점의벡터)를 계산하면 나오게 됩니다.
        Vector3 relativePos = firePos - collision.transform.position;
        
        //반사각을 구하기 위해서는 입사각의 법선벡터가 필요합니다.
        //입사각 = 교차지점(충돌) - 시작지점(출발)
        //반사각 = Vector3.Reflect(입사각, 법선벡터) 로 구할수 있습니다.

        Vector3 inVector = collision.transform.position - relativePos; //(입사벡터)        
        Vector3 inversVector = -inVector; // (입사벡터의 음수)(입사각의 대칭각(반대각)벡터)  
        Vector3 normalVector = collision.contacts[0].normal; //(입사각의법선벡터) 
        Vector3 relectVector = Vector3.Reflect(inVector, normalVector); //(반사벡터) 
        Debug.Log("반대벡터 : " + relativePos + "\n" + "입사벡터의 음수 : " + inversVector +"\n" + "입사각의 법선벡터 : " + normalVector + "\n" + "반사벡터 : " + relectVector);

        firePos = gameObject.transform.position;
        RVector = relectVector;
        myrigid.AddForce(RVector.normalized * ballSpeed * Time.smoothDeltaTime);
        Debug.Log("새 원점 : " + firePos);
    }

}
