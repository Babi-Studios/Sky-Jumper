using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private GameObject player;

    private Vector3 initialPos;
    float offsetz;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        initialPos = new Vector3(transform.position.x,transform.position.y,transform.position.z);
        offsetz = player.transform.position.z - transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position=new Vector3(initialPos.x, initialPos.y,player.transform.position.z-offsetz);
    }
}
