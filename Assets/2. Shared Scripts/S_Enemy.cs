using System.Collections.Generic;
using UnityEngine;

public class S_Enemy : MonoBehaviour
{
    public int hitPoints;
    
    public List<Transform> points;
    public int nextID=0;
    int idChangeValue = 1;
    public float speed = 2;
    public float scaleFactor;
    public float spriteScaling;


    private void Start()
    {
        Init();
    }

    void Init()
    {
        GameObject root = new GameObject(name + "_Root");
        root.transform.position = transform.position;
        transform.SetParent(root.transform);
        GameObject waypoints = new GameObject("Waypoints");
        waypoints.transform.SetParent(root.transform);
        waypoints.transform.position = root.transform.position;
        GameObject p1 = new GameObject("Point1"); p1.transform.SetParent(waypoints.transform);p1.transform.position = new Vector2(root.transform.position.x + scaleFactor,root.transform.position.y);
        GameObject p2 = new GameObject("Point2"); p2.transform.SetParent(waypoints.transform);p2.transform.position = new Vector2(root.transform.position.x - scaleFactor,root.transform.position.y);

        //Init points list then add the points to it
        points = new List<Transform>();
        points.Add(p1.transform);
        points.Add(p2.transform);
    }

    private void Update()
    {
        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        Transform goalPoint = points[nextID];
        if (goalPoint.transform.position.x > transform.position.x)
            transform.localScale = new Vector3(spriteScaling, spriteScaling, spriteScaling);
        else
            transform.localScale = new Vector3(-spriteScaling, spriteScaling, spriteScaling);
        transform.position = Vector2.MoveTowards(transform.position,goalPoint.position,speed*Time.deltaTime);
        if(Vector2.Distance(transform.position, goalPoint.position)<0.2f)
        {
            if (nextID == points.Count - 1)
                idChangeValue = -1;
            if (nextID == 0)
                idChangeValue = 1;
            nextID += idChangeValue;
        }
    }

    public void TakeDamage(int amount)
    {
        hitPoints -= amount;

        if (hitPoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //hurt player
        }
    }
}