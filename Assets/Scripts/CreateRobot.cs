using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateRobot : MonoBehaviour
{
    [Tooltip("Assign robot prefab")]
    public GameObject robot;
    [Tooltip("The points of green robots")]    
    public List<Transform> GreenPoint;
    [Tooltip("The points of red robots")]
    public List<Transform> RedPoint;
   
    private List<PointRobot> GreenRobot;
    private List<PointRobot> RedRobot;

    private GameObject GreenButton;
    private GameObject RedButton;

    // Start is called before the first frame update
    private void Awake() =>
        FindButtons();
    void Start()=>    
        InitialRobotPoint();
    
    // Update is called once per frame
    void Update()
    {
        CheckGreenRobot();
        CheckRedRobot();
    }
    private void FindButtons() {
        GreenButton = GameObject.Find("Green Spawn");
        RedButton = GameObject.Find("Red Spawn");
    }
    // Initializing the points of robots
    private void InitialRobotPoint() 
    {
        GreenRobot = new List<PointRobot>();
        RedRobot = new List<PointRobot>();

        foreach (Transform item in GreenPoint)
            GreenRobot.Add(new PointRobot { point = item, isExist = false });

        foreach (Transform item in RedPoint)
            RedRobot.Add(new PointRobot { point = item, isExist = false });
    }
    // Check if there is new position for green robot or all position of them are occupied
    private void CheckGreenRobot() 
    {
        int countSpawn=0;

        foreach (PointRobot item in GreenRobot)
        {
            if (item.isExist)
                countSpawn++;
        }
        if (countSpawn == GreenRobot.Count)
        {
            GreenButton.GetComponent<Button>().enabled = false;
            GreenButton.GetComponent<Image>().color = Color.gray;
        }
        if (countSpawn < GreenRobot.Count)
        {
           GreenButton.GetComponent<Button>().enabled = true;
           GreenButton.GetComponent<Image>().color = Color.white;
        }
    }
    //Check if there is new position for red robot or all position of them are occupied
    private void CheckRedRobot()
    {
        int countSpawn = 0;

        foreach (PointRobot item in RedRobot)
        {
            if (item.isExist)
                countSpawn++;
        }
        if (countSpawn == RedRobot.Count)
        {
            RedButton.GetComponent<Button>().enabled = false;
            RedButton.GetComponent<Image>().color = Color.gray;
        }
        if (countSpawn < RedRobot.Count)
        {
            RedButton.GetComponent<Button>().enabled = true;
            RedButton.GetComponent<Image>().color = Color.white;
        }
    }
    // Click Green Button
    public void CreateGreenRobot() 
    {
        int rand;
        rand= RandomPosition(ref GreenRobot);
        GameObject obj = Instantiate(robot,  GreenRobot[rand].point.position, Quaternion.identity);
        obj.name = TagRobot.Green;
        obj.tag = TagRobot.Green;
        obj.GetComponent<RobotBehaviour>().indexNum = rand;
    }
    // Click Red Button
    public void CreateRedRobot() 
    {
        int rand;
        rand = RandomPosition(ref RedRobot);
        GameObject obj = Instantiate(robot, RedRobot[rand].point.position, Quaternion.identity);
        obj.name = TagRobot.Red;
        obj.tag = TagRobot.Red;
        obj.GetComponent<RobotBehaviour>().indexNum = rand;
    }
    // Selection random position of robot points
    public int RandomPosition(ref List<PointRobot> points) 
    {        
        int rand;
        do
        {
            rand = Random.Range(0, points.Count);                    
        } while (points[rand].isExist);

        points[rand].isExist = true;        
        return rand;      
    }
    // When a robot is dead then position of him is released
    public void RevivalRobot(int index, string tag) 
    {
        if (tag.Equals(TagRobot.Green))        
            GreenRobot[index].isExist = false;

        if (tag.Equals(TagRobot.Red))
            RedRobot[index].isExist = false;
    }
}
