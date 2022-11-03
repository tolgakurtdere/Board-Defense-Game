using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using TK.Manager;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BoardDefense.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        [ShowInInspector, ReadOnly] private int _aliveEnemyCount;
        [ShowInInspector, ReadOnly] private List<EnemyBase> _enemies;

        private void OnEnable()
        {
            LevelManager.OnLevelStarted += OnLevelStarted;
            LevelManager.OnLevelStopped += OnLevelStopped;
            EnemyBase.OnEnemyDied += OnEnemyDied;
        }

        private void OnDisable()
        {
            LevelManager.OnLevelStarted -= OnLevelStarted;
            LevelManager.OnLevelStopped -= OnLevelStopped;
            EnemyBase.OnEnemyDied -= OnEnemyDied;
        }

        private void Awake()
        {
            _enemies = GetComponentsInChildren<EnemyBase>().ToList();

            _aliveEnemyCount = _enemies.Count;
            _enemies.ForEach(enemy => enemy.Deactivate());
        }

        private void OnLevelStarted()
        {
            StartCoroutine(StartSpawn());
        }

        private void OnLevelStopped(bool isSuccess)
        {
            StopAllCoroutines();
        }

        private void OnEnemyDied()
        {
            _aliveEnemyCount--;
            if (_aliveEnemyCount <= 0)
            {
                LevelManager.StopLevel(true);
            }
        }

        private IEnumerator StartSpawn()
        {
            while (true)
            {
                if (_enemies.Count <= 0) break;

                yield return new WaitForSeconds(Random.Range(2, 5));

                Spawn();
            }
        }

        [Button]
        private void Spawn()
        {
            var enemy = _enemies[Random.Range(0, _enemies.Count)];
            enemy.Activate();
            _enemies.Remove(enemy);
        }
    }
}