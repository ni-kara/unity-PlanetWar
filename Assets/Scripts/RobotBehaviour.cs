using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RobotBehaviour : Robot
{
    [Tooltip("Assign bullet prefab")]
    public GameObject bullet;
    [Tooltip("Assign Healthbar")]
    public Slider healthBar;
    // This variable keep the index number from list with the robots points
    public int indexNum { get; set; }

    private bool isGround;
    private bool existEnemy;

    private Vector3 bulletDirection;
    private Vector3 targetPosition;

    private int bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        InitialHealthBar();
        SpecsRobot();
        SetBulletSpeed();
        StartCoroutine(CreateBullet());        
    }

    // Update is called once per frame
    void Update()
    {
        CheckForEnemy();
        DestroyRobot();
    }
   

    private IEnumerator CreateBullet() 
    {
        for (;;)
        {
            yield return new WaitUntil(() => isGround);
            yield return new WaitUntil(() => existEnemy);
            yield return new WaitForSeconds(SpeedAttackTime());
            GameObject obj = Instantiate(bullet, transform.position + (bulletDirection / 4) + (Vector3.up/6), Quaternion.identity, transform);
            obj.GetComponent<ConstantForce>().force = (targetPosition - transform.position).normalized * bulletSpeed;
            Destroy(obj, 3f);
        }
    }

    private void SetBulletSpeed() => bulletSpeed = 10;
    private void CheckForEnemy() 
    {
        if (gameObject.tag.Equals(TagRobot.Green))
        {   //Enemy is enabled when he is exist and he is found over the planet
            if (GameObject.FindGameObjectWithTag(TagRobot.Red) && GameObject.FindGameObjectWithTag(TagRobot.Red).GetComponent<RobotBehaviour>().IsGround())
            { 
                existEnemy = true;
                targetPosition = GameObject.FindGameObjectWithTag(TagRobot.Red).GetComponent<Transform>().position;
            }
            else
                existEnemy = false;
        }

        if (gameObject.tag.Equals(TagRobot.Red))
        {   //Enemy is enabled when he is exist and he is found over the planet
            if (GameObject.FindGameObjectWithTag(TagRobot.Green) && GameObject.FindGameObjectWithTag(TagRobot.Green).GetComponent<RobotBehaviour>().IsGround())
            { 
                existEnemy = true;
                targetPosition = GameObject.FindGameObjectWithTag(TagRobot.Green).GetComponent<Transform>().position;           
            }
            else
                existEnemy = false;
        }        
    }
    // Specs of Robots
    private void SpecsRobot() {
        if (gameObject.tag.Equals(TagRobot.Green))
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
            bulletDirection = Vector3.right;
        }
        if (gameObject.tag.Equals(TagRobot.Red))
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
            bulletDirection = Vector3.left;
        }
    }
    private void InitialHealthBar() 
    {
        healthBar.maxValue = MaxLife();
        healthBar.value = healthBar.maxValue;
    }
    private void CurrentHealthBar() => healthBar.value = CurrentLife();
    public bool IsGround() => this.isGround;
    private void DestroyRobot()
    {
        if (CurrentLife() <= 0)
        {
            GameObject.Find("Canvas").GetComponent<DeadRobot>().IsDead(indexNum, gameObject.tag);
            Destroy(this.gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Planet"))
            isGround = true;       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Bullet"))
        {
            Damage();
            Destroy(other.gameObject);

            CurrentHealthBar();
        }
    }    
}
