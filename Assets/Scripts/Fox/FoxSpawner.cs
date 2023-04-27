using UnityEngine;
using System.Collections;

public class FoxSpawner : MonoBehaviour
{
  
  private float _startX = 4.5f;
  private float _startY = -0.875f;
  private Transform _transform;
  
  [SerializeField] private GameObject redFox;
  [SerializeField] private GameObject whiteFox;

  void Start()
  {
    _transform = GetComponent<Transform>();
    _transform.localPosition = new Vector3(_startX, _startY, _transform.position.z);

    StartCoroutine(SpawnFoxes());
  }

  private IEnumerator SpawnFoxes()
  {
    while (true) // TODO - Needs to listen to a Game Status Even, only true if game is playing/not paused
    {
      InstantiateRandomFox();
      yield return new WaitForSeconds(2);
    }
  }

  

  void InstantiateRandomFox()
  {
    GameObject foxClone;
    string s = "I am a ";

    if (Random.value < 0.5f)
    {
      // Create RunningFox
      foxClone = Instantiate<GameObject>(redFox, _transform.position, _transform.rotation);
      foxClone.name = "Red Fox (Clone)";
      s += "Red Fox";
    }
    else  
    {
      foxClone = Instantiate<GameObject>(whiteFox, _transform.position, _transform.rotation);
      foxClone.name = "White Fox (Clone)";
      s += "White Fox";
    }
    Debug.Log(s);
  }
}