using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private	Vector3		moveDir;
	private	float		speed;
    Collider bulletCollider;

	public void Init(Vector3 _pos, Vector3 _moveDir)
	{
		transform.position	= _pos;
		moveDir				= _moveDir;
		speed				= 30.0f;

	}

    private void Start()
    {
        bulletCollider = GetComponent<Collider>();
        bulletCollider.enabled = false;
        Invoke("EnableTouch", 0.2f);
    }

    void Update()
	{
		transform.position += moveDir * speed * Time.deltaTime;
	}

	void OnTriggerEnter(Collider col)
	{
		if ( col.name.Equals("Wall") )
		{
			moveDir = Vector3.Reflect(moveDir, col.GetComponent<Wall>().normal);
        }
    }

    void EnableTouch()
    {
        bulletCollider.enabled = true;
    }
}

