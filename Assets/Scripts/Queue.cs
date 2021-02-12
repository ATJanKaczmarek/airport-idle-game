using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Queue : MonoBehaviour
{
    #region Variables
    public int queueId;

    private GameObject _dutyFree1;
    private GameObject _dutyFree2;
    private GameObject _dutyFree1Button;
    private GameObject _dutyFree2Button;

    [SerializeField]
    private List<GameObject> _persons;

    private PersonSpawner _spawner;

    public List<Vector3> vectors;
    public Vector3 _queueFullPos;
    private float _posSize = 0.8f;

    private GameObject _timer;
    public float _waitingDuration = 10f;
    public float _spawnTime = 10f;

    public GameObject[] barrierTapes;
    public GameObject[] dutyFreeShops;

    public int _queueLength = 1;

    private float _nextLengthUpgradePrice;
    private float _nextWaitingTimeUpgradePrice;
    private float _nextSpawnrateUpgradePrice;

    public int lengthOwned = 0;
    public int waitingTimeUpgradesOwned = 0;
    public int spawnrateUpgradesOwned = 0;

    private bool HP_running = false;

    private Constants.FlightLevel _flightLevel = Constants.FlightLevel.SIGHTSEEING_FLIGHT;
    private Constants.FlightClass _flightClass;

    #endregion

    #region Unity-Methods
    private void Awake()
    {
        _spawner = transform.GetChild(0).GetComponent<PersonSpawner>();
        _timer = transform.GetChild(1).gameObject;

        _dutyFree1 = transform.GetChild(2).Find("DutyFree1").gameObject;
        _dutyFree2 = transform.GetChild(2).Find("DutyFree2").gameObject;
        _dutyFree1.SetActive(false);
        _dutyFree2.SetActive(false);

        _dutyFree1Button = transform.GetChild(2).Find("DutyFree1:Buy").gameObject;
        _dutyFree2Button = transform.GetChild(2).Find("DutyFree2:Buy").gameObject;
        _dutyFree1Button.SetActive(false);
        _dutyFree2Button.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(SpawnTimer());
        _timer.SetActive(false);
     
        Vector3 firstPos = transform.position;
        for (int i = 0; i < _queueLength; i++)
        {
            vectors.Add(firstPos + new Vector3(-1, 0) * _posSize * i);
        }
        _queueFullPos = firstPos + new Vector3(-1, 0) * _posSize * _queueLength;
    }

    private void Update()
    {
        _nextLengthUpgradePrice = (float)System.Math.Round(Constants.QUEUE_LENGTH_UPGRADE_BASE_COST * Mathf.Pow(Constants.MULTIPLIER, lengthOwned), 2);
        _nextWaitingTimeUpgradePrice = (float)System.Math.Round(Constants.QUEUE_TIME_UPGRADE_BASE_COST * Mathf.Pow(Constants.MULTIPLIER, waitingTimeUpgradesOwned), 2);
        _nextSpawnrateUpgradePrice = (float)System.Math.Round(Constants.QUEUE_SPAWN_UPGRADE_BASE_COST * Mathf.Pow(Constants.MULTIPLIER, spawnrateUpgradesOwned), 2);
        if(IsHandlingPerson() == true && HP_running == false)
        {
            StartCoroutine(HandlePerson());
        }
    }
    #endregion

    #region Handling persons
    private IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(_spawnTime);
        AddPerson();
        StartCoroutine(SpawnTimer());
    }

    public void AddPerson()
    {
        if (CanAddPerson() == true)
        {
            Person p = _spawner.Spawn(vectors[_persons.Count], this);
            _persons.Add(p.gameObject);
        }
        else
        {
            Person p = _spawner.Spawn(_queueFullPos, this);
            StartCoroutine(SendPersonBack(p));
        }
    }

    private bool CanAddPerson()
    {
        return _persons.Count < vectors.Count;
    }

    private IEnumerator SendPersonBack(Person personToSendBack)
    {
        personToSendBack.MoveTo(_queueFullPos);
        yield return new WaitForSeconds(0.5f);
        personToSendBack.SetHappiness(HappinessState.Mad);
        yield return new WaitForSeconds(0.5f);
        personToSendBack.MoveTo(new Vector3(-6.25f, 7f));
    }

    public void RemovePerson(Person person)
    {
        _persons.Remove(person.gameObject);
        for (int i = 0; i < _persons.Count; i++)
        {
            _persons[i].GetComponent<Person>().MoveTo(vectors[i]);
        }
    }

    private bool IsHandlingPerson()
    {
        return 0 < _persons.Count && !_persons[0].GetComponent<Person>().isMoving;
    }

    private IEnumerator HandlePerson()
    {
        HP_running = true;
        _timer.SetActive(true);
        Person p = _persons[0].GetComponent<Person>();
        yield return new WaitForSeconds(_waitingDuration);
        p.MoveTo(vectors[0] + new Vector3(1, 0) * 18.2f); // Sends person to Bodyscanner
        if (p != null)
        {
            RemovePerson(p);
            p.SetHappiness(HappinessState.Happy);
        }
        _timer.SetActive(false);
        GameManager.Instance.GainMoney(_flightLevel, _flightClass, p.transform.position);
        HP_running = false;
    }

    #endregion

    #region Upgrades
    public void UpgradeQueueLength()
    {
        if (GameManager.coins >= _nextLengthUpgradePrice)
        {
            if (lengthOwned < 10)
            {
                GameManager.coins -= _nextLengthUpgradePrice;
                lengthOwned++;
                _queueLength++;

                Vector3 firstPos = transform.position;
                vectors = new List<Vector3>();
                for (int i = 0; i < _queueLength; i++)
                {
                    vectors.Add(firstPos + new Vector3(-1, 0) * _posSize * i);
                }
                _queueFullPos = firstPos + new Vector3(-1, 0) * _posSize * _queueLength;

                switch (_queueLength)
                {
                    case 1:
                        barrierTapes[0].SetActive(true);
                        break;
                    case 2:
                        barrierTapes[0].SetActive(true);
                        break;
                    case 3:
                        barrierTapes[1].SetActive(true);
                        break;
                    case 4:
                        barrierTapes[1].SetActive(true);
                        _dutyFree1Button.SetActive(true);
                        break;
                    case 5:
                        barrierTapes[2].SetActive(true);
                        break;
                    case 6:
                        barrierTapes[3].SetActive(true);
                        break;
                    case 7:
                        barrierTapes[3].SetActive(true);
                        break;
                    case 8:
                        barrierTapes[4].SetActive(true);
                        _dutyFree2Button.SetActive(true);
                        break;
                    case 9:
                        barrierTapes[5].SetActive(true);
                        break;
                    case 10:
                        barrierTapes[5].SetActive(true);
                        break;
                    case 11:
                        barrierTapes[6].SetActive(true);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Debug.Log("Maximal queue length reached!");
            }
        }
        else
        {
            Debug.Log("Not enough money!");
        }
    }

    public void UpgradeWaitingTime()
    {
        if (GameManager.coins >= _nextWaitingTimeUpgradePrice)
        {
            if (waitingTimeUpgradesOwned < 39)
            {
                GameManager.coins -= _nextWaitingTimeUpgradePrice;
                waitingTimeUpgradesOwned++;
                _waitingDuration -= 0.25f;
            }
        }
    }

    public void UpgradeSpawnrate()
    {
        if (GameManager.coins >= _nextSpawnrateUpgradePrice)
        {
            if (spawnrateUpgradesOwned < 38)
            {
                GameManager.coins -= _nextSpawnrateUpgradePrice;
                spawnrateUpgradesOwned++;
                _spawnTime -= 0.25f;
            }
        }
    }

    public void ActivateDutyFreeShop(int pos)
    {
        if (pos == 1)
        {
            _dutyFree1.SetActive(true);
        }
        else if (pos == 2)
        {
            _dutyFree2.SetActive(true);
        }
    }
    #endregion
}