using UnityEngine;

public class SpawnerHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _animalPrifab;

    [SerializeField]
    private float _minSpawnTime; 

    [SerializeField]
    private float _maxSpawnTime;

    private float _timeUntilSpawn;

    public GameObject animalCur;
    public bool animalLock;

    void Awake()
    {
        SetTimeUntilSpawn();



    }

    void Update()
    {

        animalCur = GameObject.Find("Animal(Clone)");
        Debug.Log(animalCur);
        _timeUntilSpawn -= Time.deltaTime;

        if(animalCur == null){
            if(_timeUntilSpawn <= 0){
                animalLock = true;
                Instantiate(_animalPrifab, transform.position, Quaternion.identity);
                SetTimeUntilSpawn();
            }else{
                animalLock = false;
            }
        }

    }


    private void SetTimeUntilSpawn(){

        _timeUntilSpawn = Random.Range(_minSpawnTime, _maxSpawnTime);
    }




}


