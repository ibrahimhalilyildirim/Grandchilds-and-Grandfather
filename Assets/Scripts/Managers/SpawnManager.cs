using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BabeSpawnpoint : MonoBehaviour
{
    [Header("Spawnpoints")]
    [SerializeField] private List<Transform> _spawnPointList = new List<Transform>();

    [Header("Babes Prefabs")]
    [SerializeField] private List<GameObject> _babePrefabsList = new List<GameObject>();

    private void Start()
    {
        RandomBabeSpawner();
    }
    private void RandomBabeSpawner()
    {
       List<Transform> _tempSpawnPointList = new List<Transform> (_spawnPointList);

        for (int i = 0; i < _babePrefabsList.Count; i++) // Bebek sayısı kadar çalışacak.
        {
            int _randomIndex = Random.Range(0, _tempSpawnPointList.Count); // Rastgele bir spawn noktası seçiyoruz.(0 ile doğma noktası sayısı arasında rastgele bir sayı.)
            Transform _choosedPoint = _tempSpawnPointList[_randomIndex]; // Rastgele seçilen noktayı geçici doğma noktasında kullanmak için bir değişkene alıyoruz.
            Instantiate(_babePrefabsList[i], _choosedPoint.position, _choosedPoint.rotation); // Bebek oluşturuluyor.
            _tempSpawnPointList.RemoveAt(_randomIndex); // Seçilen nokta listeden kaldırılıyor ki aynı yerde ikinci bebek doğma ihtimali(random) olmasın.
        } 
    }
}
