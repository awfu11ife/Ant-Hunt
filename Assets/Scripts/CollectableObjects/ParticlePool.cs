using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ParticlePool : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particlePrefab;
    [SerializeField] private int _capacity;

    private List<ParticleSystem> _pool = new List<ParticleSystem>();

    private void Awake()
    {
        CompletePool();
    }

    public void InvokeParticle(Vector3 position, Color color)
    {
        ParticleSystem avaliableParticle = _pool.FirstOrDefault(p => p.gameObject.activeSelf == false);

        if (avaliableParticle != null)
        {
            avaliableParticle.gameObject.SetActive(true);
            avaliableParticle.transform.position = position;

            var avaliableParticleMain = avaliableParticle.main;
            avaliableParticleMain.startColor = color;

            avaliableParticle.Play();
        }
    }

    private void CompletePool()
    {
        for (int i = 0; i < _capacity; i++)
        {
            ParticleSystem spawnd = Instantiate(_particlePrefab, transform);
            spawnd.gameObject.SetActive(false);

            _pool.Add(spawnd);
        }
    }
}
