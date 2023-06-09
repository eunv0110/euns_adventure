using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public AudioClip audioSkill0;
    public AudioClip audioSkill1;
    public AudioClip audioSkill2;

    AudioSource audioSource;

    public GameObject playerBullet1;
    public GameObject playerBullet2;
    public GameObject Fire;

    private PlayerMove playerMove;
    private Vector3 bulletPosition;
    public int skill;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        playerMove = this.GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (skill == 0)
            {
                GameObject temp = Instantiate(playerBullet1, transform.position, transform.rotation);
                Destroy(temp, 1f);
                PlaySound("SKILL0");
            }
            else if (skill == 1)
            {
                GameObject temp = Instantiate(playerBullet2, transform.position, transform.rotation);
                Destroy(temp, 1f);
                PlaySound("SKILL1");
            }
            else if (skill == 2)
            {
                float y = (float)(transform.position.y + 0.5);
                if (playerMove.direction == -1)
                    bulletPosition = new(transform.position.x - 3, y, transform.position.z);
                else
                    bulletPosition = new(transform.position.x + 3, y, transform.position.z);

                GameObject temp = Instantiate(Fire, bulletPosition, transform.rotation);

                //temp.transform.position = bulletPosition; 움직이는거 따라 fire도 이동

                Destroy(temp, 1f);
                PlaySound("SKILL2");
            }
        }
    }

    void PlaySound(string action)
    {
        switch (action)
        {
            case "SKILL0":
                audioSource.clip = audioSkill0;
                break;
            case "SKILL1":
                audioSource.clip = audioSkill1;
                break;
            case "SKILL2":
                audioSource.clip = audioSkill2;
                break;
        }
        audioSource.Play();
    }
}
