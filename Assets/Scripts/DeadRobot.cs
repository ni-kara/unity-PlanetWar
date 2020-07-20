using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeadRobot : MonoBehaviour
{
    private int GreenKillsCounter;
    private int RedKillsCounter;

    private void RefreshGreenKillsCounter() =>
        gameObject.transform.Find("GreenKillsCounter").GetComponent<Text>().text = "Kills: " + GreenKillsCounter;
    private void RefreshRedKillsCounter() =>
        gameObject.transform.Find("RedKillsCounter").GetComponent<Text>().text = "Kills: " + RedKillsCounter;
    private void CounterKills(ref int counter) => counter++;
    public void IsDead(int index, string tag)
    {
        GameObject.Find("Canvas").GetComponent<CreateRobot>().RevivalRobot(index, tag);
        
        if (tag.Equals(TagRobot.Green))
        {
            CounterKills(ref RedKillsCounter);
            RefreshRedKillsCounter();
        }
        if (tag.Equals(TagRobot.Red))
        {
            CounterKills(ref GreenKillsCounter);
            RefreshGreenKillsCounter();
        }
    }
}
