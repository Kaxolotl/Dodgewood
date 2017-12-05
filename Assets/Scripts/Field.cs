using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public GameManager _gameManager;
    public GameManager GameManager
    {
        get
        {
            if (_gameManager == null)
                _gameManager = FindObjectOfType<GameManager>();

            return _gameManager;
        }
    }

    const int FIELDAMMOMAX = 10;

    enum Character {
        matilda     = 0,
        captain     = 1,
        navi        = 2,
        terminator  = 3 }

    public	Vector3	fieldPos;
	public	Vector3	fieldSize;
	public	GameObject	ammo;
	public	List<GameObject>	ammos;

    public GameObject matilda;
    public GameObject captain;
    public GameObject navi;
    public GameObject terminator;

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

    void SpawnPlayer()
    {
        float x = Random.Range(-fieldSize.x * 0.5f + fieldPos.x + 2.0f, fieldSize.x * 0.5f + fieldPos.x - 2.0f);
        float z = Random.Range(-fieldSize.z * 0.5f + fieldPos.z + 2.0f, fieldSize.z * 0.5f + fieldPos.z - 2.0f);

        GameObject player_1 = Instantiate(SelectCharacter(GameManager.Instance.player_1));
        player_1.transform.parent = gameObject.transform;
        player_1.transform.position = new Vector3(x, 0.5f, z);
    }

    GameObject SelectCharacter(int charNum)
    {
        if (charNum == 0)
        {
            Debug.Log("M");
            return matilda;
        }
        else if (charNum == 1)
        {
            Debug.Log("C");
            return captain;
        }
        else if (charNum == 2)
        {
            Debug.Log("N");
            return navi;
        }
        else
        {
            Debug.Log("T");
            return terminator;
        }
    }
}

