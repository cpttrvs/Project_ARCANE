using UnityEngine;
using System.Collections;

public class Requiem : Channel {

    protected bool isClone = false;

    private float remainingTime;

    public override void channelingState()
    {
        transform.position = caster.transform.position;

        caster.GetComponent<Player>().setSpeedModifcator(1 - Mathf.Clamp(channelTime, 0, maxChannelingTime));
    }

    public override void castingState()
    {
        if (isClone)
        {
            //survive until end of mark
            Debug.Log(gameObject.ToString());
            waitEndLife(remainingTime);
        } else
        {
            GameObject[] players = new GameObject[2] {
            GameObject.Find("Game").transform.Find("GameInstance").transform.Find("Player1").gameObject,
            GameObject.Find("Game").transform.Find("GameInstance").transform.Find("Player2").gameObject };

            if (channelTime <= 1.5) //only caster
            {
                transform.position = caster.transform.position;
                gameObject.GetComponent<Animator>().Play("cast");
                caster.GetComponent<Player>().addMark(mark);
            }
            else // both
            {
                for (int i = 0; i < players.Length; i++)
                {
                    GameObject clone = Instantiate(gameObject,
                    new Vector3(players[i].transform.position.x, players[i].transform.position.y, players[i].transform.position.z),
                    players[i].transform.rotation) as GameObject;

                    clone.GetComponent<Spell>().setCaster(players[i]);
                    clone.GetComponent<Channel>().setChannelState(false);
                    clone.GetComponent<Requiem>().setRemainingTime(recastCooldown);
                    clone.GetComponent<Requiem>().setCloneState(true);

                    clone.GetComponent<Animator>().Play("cast");

                    players[i].GetComponent<Player>().addMark(mark);
                }
            }
        }
    }

    public override void movement()
    {
        transform.position = caster.transform.position;
    }

    private IEnumerator waitEndLife(float s)
    {
        Debug.Log("cacaca");
        yield return new WaitForSeconds(s);
        Destroy(gameObject);
    }


    public void setCloneState(bool state)
    { isClone = state; }
    public void setRemainingTime(float time)
    { remainingTime = time; }
}
