using Unity.VisualScripting;
using UnityEngine;

public class WeaponColliderEnabled : MonoBehaviour
{
    [SerializeField] private AudioSource _Source;
    private void Start()
    {
        if (Instruct.ItemList.Contains(this.gameObject.name))
        {
            this.gameObject.SetActive(false);
        }

        _Source=this.AddComponent<AudioSource>();
        _Source.clip = Resources.Load<AudioClip>("audio/Ïà»¥»÷ÖÐ");
    }

    private void OnTriggerEnter(Collider other)
    {


        
            if (other.gameObject.tag == "wupin")
            {
                _Source.Play();
            }
        }

    }
