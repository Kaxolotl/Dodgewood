using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
	public	Vector3	fieldPos;
	public	Vector3	fieldSize;
	public	GameObject	ammo;
	public	List<GameObject>	ammos;

	void Awake()
	{
		InvokeRepeating("CreateAmmo", 0.0f, 2.0f);
	}
	void CreateAmmo()
	{
		if ( ammos.Count >= 10 )
		{
			return;
        }
        float x = Random.Range(-fieldSize.x * 0.5f + fieldPos.x + 2.0f, fieldSize.x * 0.5f + fieldPos.x - 2.0f);
        float z = Random.Range(-fieldSize.z * 0.5f + fieldPos.z + 2.0f, fieldSize.z * 0.5f + fieldPos.z - 2.0f);

        GameObject clone = Instantiate(ammo);
		clone.transform.position = new Vector3(x, 0.5f, z);
		clone.name = "Ammo";
		ammos.Add(clone);
	}
}

