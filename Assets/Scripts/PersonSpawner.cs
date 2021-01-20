using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonSpawner : MonoBehaviour
{
    public GameObject person01;
    public GameObject person02;
    public GameObject person03;
    public GameObject person04;

    public Person Spawn(Vector3 moveToPos, Queue queue)
    {
        int rnd = Random.Range(0, 4);
        GameObject currInstance;
        switch (rnd)
        {
            case 0:
                currInstance = Instantiate(person01, transform.position, transform.rotation);
                break;
            case 1:
                currInstance = Instantiate(person02, transform.position, transform.rotation);
                break;
            case 2:
                currInstance = Instantiate(person03, transform.position, transform.rotation);
                break;
            case 3:
                currInstance = Instantiate(person04, transform.position, transform.rotation);
                break;
            default:
                return null;
        }
        Person person = currInstance.GetComponent<Person>();
        person.SetQueue(queue);
        person.MoveTo(moveToPos);
        return person;
    }
}
