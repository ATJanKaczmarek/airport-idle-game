using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Queue : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private List<GameObject> _persons;
    
    private PersonSpawner _spawner;

    public List<Vector3> vectors;
    public Vector3 _queueFullPos;
    private float _posSize = 0.8f;

    private GameObject _timer;
    public float _waitingDuration = 10f;
    public float _spawnTime = 10f;

    public int _queueLength = 1;
    private int _lengthOwned = 1;

    private double _nextLengthUpgradePrice;
    private double _nextWaitingTimeUpgradePrice;
    private double _nextSpawnrateUpgradePrice;

    private int _waitingTimeUpgradesOwned = 0;
    private int _spawnrateUpgradesOwned = 0;

    private bool _personDone = false;

    private bool HP_running = false;

    private Constants.FlightLevel _flightLevel = Constants.FlightLevel.SIGHTSEEING_FLIGHT;
    private Constants.FlightClass _flightClass;

    #endregion

    #region Unity-Methods
    private void Awake()
    {
        _spawner = transform.GetChild(0).GetComponent<PersonSpawner>();
        _timer = transform.GetChild(1).gameObject;
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

    private void FixedUpdate()
    {
        //ActivateOrDeactivateTimer();
        // double price = System.Math.Round(baseCost * Mathf.Pow(Constants.MULTIPLIER, owned), 2); // !!!!
    }

    private void Update()
    {
        _nextLengthUpgradePrice = System.Math.Round(Constants.QUEUE_LENGTH_UPGRADE_BASE_COST * Mathf.Pow(Constants.MULTIPLIER, _lengthOwned), 2);
        _nextWaitingTimeUpgradePrice = System.Math.Round(Constants.QUEUE_TIME_UPGRADE_BASE_COST * Mathf.Pow(Constants.MULTIPLIER, _waitingTimeUpgradesOwned), 2);
        _nextSpawnrateUpgradePrice = System.Math.Round(Constants.QUEUE_SPAWN_UPGRADE_BASE_COST * Mathf.Pow(Constants.MULTIPLIER, _spawnrateUpgradesOwned), 2);
        if(IsHandlingPerson() == true && HP_running == false)
        {
            StartCoroutine(HandlePerson());
        }
    }
    #endregion

    // Spawn -> wenn Person auf [0] -> HandlePerson() -> -> -> +$

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
            Person p = _spawner.Spawn(vectors[_persons.Count]);
            _persons.Add(p.gameObject);
        }
        else
        {
            Person p = _spawner.Spawn(_queueFullPos);
            //_personCheckedFor = p;
            //_checkPerson = true;
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
        _personDone = true;
        p.MoveTo(vectors[0] + new Vector3(1, 0) * 5f);
        if (p != null)
        {
            RemovePerson(p);
        }
        _timer.SetActive(false);
        GameManager.Instance.GainMoney(_flightLevel, _flightClass);
        HP_running = false;
    }

    #endregion

    #region Upgrades
    public void UpgradeQueueLength()
    {
        if (GameManager.coins >= _nextLengthUpgradePrice)
        {
            GameManager.coins -= (float)_nextLengthUpgradePrice;
            _lengthOwned++;
            _queueLength++;

            Vector3 firstPos = transform.position;
            vectors = new List<Vector3>();
            for (int i = 0; i < _queueLength; i++)
            {
                vectors.Add(firstPos + new Vector3(-1, 0) * _posSize * i);
            }
            _queueFullPos = firstPos + new Vector3(-1, 0) * _posSize * _queueLength;
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
            GameManager.coins -= (float)_nextWaitingTimeUpgradePrice;
            _waitingTimeUpgradesOwned++;
            _waitingDuration--;
        }
    }

    public void UpgradeSpawnrate()
    {
        if (GameManager.coins >= _nextSpawnrateUpgradePrice)
        {
            GameManager.coins -= (float)_nextSpawnrateUpgradePrice;
            _spawnrateUpgradesOwned++;
            _spawnTime--;
        }
    }
    #endregion
}
