using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GuiScript : MonoBehaviour
{
    public TextMeshProUGUI goldText,livesText,scoreText;
    //public TextMeshProUGUI livesText;
    public int gold,lives,score;
    // Start is called before the first frame update
    void Start()
    {
        gold = 100;
        lives = 20;
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        goldText.text = "Gold: " + gold.ToString();//"D4"
        livesText.text = "Lives: " + lives.ToString();
        scoreText.text = "Score: " + score.ToString();
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                //StartCoroutine(ScaleMe(hit.transform));
                if (hit.transform.tag == "Tower")//make this a switch statement
                {
                    hit.collider.gameObject.GetComponent<TowerScript>().towerClicked();
                }else if (hit.transform.parent.tag == "Enemy")
                {
                    hit.collider.transform.parent.GetComponent<Enemy>().takeDamage(1);
                }else if (hit.transform.parent.tag == "SmartEnemy")
                {
                    hit.collider.transform.parent.GetComponent<SmartEnemy>().takeDamage(1);
                }
                Debug.Log("You selected the " + hit.transform.name); // ensure you picked right object
            }
        }
    }
    IEnumerator ScaleMe(Transform objTr)
    {
        //objTr.localScale *= 1.2f;
        yield return new WaitForSeconds(0.5f);
        //objTr.localScale /= 1.2f;
    }
    public void addScore(int value)
    {
        score += value;
    }
    public void addGold(int value)
    {
        gold += value;
    }
    public bool spendGold(int value)
    {
        if (value <= gold)
        {
            gold -= value;
            return true;
        }
        Debug.Log("Not enough gold");
        return false;
    }
    public void loseLives(int livesLost)
    {
        lives -= livesLost;
        if (lives <= 0)
        {
            Debug.Log("Defeat! public void loseLives");
        }
    }
}
