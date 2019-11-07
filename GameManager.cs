using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject bear;
    public GameObject wolf;
    public GameObject sheep;
    public GameObject rabbit;
    public GameObject plant;
    public GameObject bush;

    private static Dictionary<string, Queue<GameObject>> theDead;
    static Vector3 plusOne = new Vector3(1.0f, 0.0f, 1.0f);
    
    private float lastTime;
    // Start is called before the first frame update
    void Start()
    {
        theDead = new Dictionary<string, Queue<GameObject>>();        

        // bears
        for (int i = 0; i < 25; i++)
        {
            AddToDeadQueue(Instantiate(bear, new Vector3(Random.Range(0.0f, 90.0f), 0, Random.Range(0.0f, 90.0f)), Quaternion.identity));
        }
        
        // wolves
        for (int i = 0; i < 50; i++)
        {
            AddToDeadQueue(Instantiate(wolf, new Vector3(Random.Range(0.0f, 90.0f), 0, Random.Range(0.0f, 90.0f)), Quaternion.identity));
        }

        // rabbits
        for(int i = 0; i < 200; i++)
        {
            AddToDeadQueue(Instantiate(rabbit, new Vector3(Random.Range(0.0f, 90.0f), 0, Random.Range(0.0f, 90.0f)), Quaternion.identity));
            AddToDeadQueue(Instantiate(sheep, new Vector3(Random.Range(0.0f, 90.0f), 0, Random.Range(0.0f, 90.0f)), Quaternion.identity));
        }

        // trees
        for(int i = 0; i < 20; i++)
        {
            Instantiate(plant, new Vector3(Random.Range(2.0f, 100.0f), 0, Random.Range(1.0f, 100.0f)), Quaternion.identity);
        }

        // plants
        for(int i = 0; i < 250; i++)
        {
            AddToDeadQueue(Instantiate(bush, new Vector3(Random.Range(0.0f, 90.0f), 0, Random.Range(0.0f, 90.0f)), Quaternion.identity));
        }

        foreach(var q in theDead.Values)
        {
            for(int i = 0; i < 10; i++)
            {
                q.Dequeue().SetActive(true);
            }
        }

        lastTime = Time.time;
    }


    private void FixedUpdate()
    {
        //Debug.Log(theDead.Count);
        if((Time.time - lastTime) > 3.0f)
        {
            lastTime = Time.time;
            for(int i = 0; i < 25; i++)
            {
                if(theDead["plant"].Count > 0)
                {
                    var go = theDead["plant"].Dequeue();
                    go.transform.position = new Vector3(Random.Range(0.0f, 90.0f), 0, Random.Range(0.0f, 90.0f));
                    go.transform.rotation = Quaternion.identity;
                    go.SetActive(true);
                }
            }
            //while(theDead["plant"].Count > 0)
            //{
            //    var go = theDead["plant"].Dequeue();
            //    go.transform.position = new Vector3(Random.Range(0.0f, 90.0f), 0, Random.Range(0.0f, 90.0f));
            //    go.transform.rotation = Quaternion.identity;
            //    go.SetActive(true);
            //}

            //for (int i = 0; i < 70; i++)
            //{
            //    Instantiate(bush, new Vector3(Random.Range(0.0f, 90.0f), 0, Random.Range(0.0f, 90.0f)), Quaternion.identity);
            //    //Instantiate(plant, new Vector3(Random.Range(0.0f, 90.0f), 0, Random.Range(0.0f, 90.0f)), Quaternion.identity);
            //}
        }
    }

    public static void AddToDeadQueue(GameObject go)
    {
        go.SetActive(false);
        if(!theDead.ContainsKey(go.tag))
        {
            theDead.Add(go.tag, new Queue<GameObject>());
        }
           
        theDead[go.tag].Enqueue(go);
    }

    public static void Resurrect(string t, Transform tf)
    {       

        if(theDead.ContainsKey(t) && theDead[t].Count > 0)
        {
            var go =  theDead[t].Dequeue();
            Vector3 candidate = tf.position;
            while (ContainsOtherEntity(Physics.OverlapSphere(candidate, 1f)))
            {
                candidate += plusOne;
            }

            go.transform.position = candidate;
            go.transform.rotation = Quaternion.identity;
            go.SetActive(true);
        }
    }

    private static bool ContainsOtherEntity(Collider[] arr)
    {
        foreach (var col in arr)
        {
            if (col.CompareTag("plant") || col.CompareTag("tree") || col.CompareTag("wolf") || col.CompareTag("bear") || col.CompareTag("rabbit") || col.CompareTag("sheep"))
            {
                return true;
            }
        }
        return false;
    }
}
