using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    const int FIELDAMMOMAX = 10;

	public	Vector3	fieldPos;
	public	Vector3	fieldSize;
	public	GameObject	ammo;
	public	List<GameObject>	ammos;

	void Awake()
	{
        CreateAmmo();
        InvokeRepeating("SpawnAmmo", 0, 2f);
	}

    void CreateAmmo()
    {
        for (int i = 0; i < FIELDAMMOMAX; i++)
        {
            GameObject clone = Instantiate(ammo);
            clone.transform.parent = gameObject.transform;
            ammos.Add(clone);
            clone.SetActive(false);
        }
    }


    void SpawnAmmo()
	{
        for (int i = 0; i < FIELDAMMOMAX; i++)
        {
            if (!ammos[i].activeInHierarchy)
            {
                float x = Random.Range(-fieldSize.x * 0.5f + fieldPos.x + 2.0f, fieldSize.x * 0.5f + fieldPos.x - 2.0f);
                float z = Random.Range(-fieldSize.z * 0.5f + fieldPos.z + 2.0f, fieldSize.z * 0.5f + fieldPos.z - 2.0f);

                ammos[i].transform.position = new Vector3(x, 0.5f, z);
                ammos[i].SetActive(true);
                return;
            }
        }
	}
}

