using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class AlarmTrigger : MonoBehaviour
{
    [SerializeField] private Alarm _alarm;

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Criminal>())
        {
            _alarm.Switch();
        }
    }
}
