using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object_pool : MonoBehaviour
{
    //bullet prefab + pool's size
    public GameObject prefab;
    public int pool_size = 14;
    //list to store GO bulletdata
    private List<GameObject> pool;
    public BulletData bulletdata;
    public Transform parentObject;

    // does the instruction right after the game awake.
    private void Awake()
    {
        //using loop create and store GO in list 
        pool = new List<GameObject>();
        for(int i = 0; i < pool_size; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            Bullets bullet = obj.GetComponent<Bullets>();
            if (bullet != null)
            {
                bullet.SetPool(this);
            }
            else
            {
                Debug.LogError("Bullet prefab is missing Bullets script!", obj);
            }

            obj.transform.SetParent(parentObject);
            pool.Add(obj);
        }
        
    }
    // used to set up bullet behavior(applies data)
    private void SetupBullet(GameObject bullet)
    {
        var b = bullet.GetComponent<Bullets>();
        if (b != null)
        {
            b.ApplayData(bulletdata);//data from BulletData to ApplayData func in Bullets script
        }
    }
    // get the pooled obj using for each create new bullet if none in list
    public GameObject GetPooledObject()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                SetupBullet(obj);
                obj.SetActive(true);
                return obj;
            }
        }
        GameObject objnew = Instantiate(prefab);
        SetupBullet(objnew);
        objnew.SetActive(true);
        objnew.transform.SetParent(parentObject);
        pool.Add(objnew);
        return objnew;
        
    }
    // deactivate the game object after return.
    public void ReturnToPool(GameObject obj)
    {
        obj.SetActive(false);
    }
}
