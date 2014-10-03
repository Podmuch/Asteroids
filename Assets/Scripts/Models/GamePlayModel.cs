using UnityEngine;
using Asteroids.Interface;
using Asteroids.Model;

namespace Asteroids.GamePlay
{
    public class GamePlayModel : AbstractModel
    {
        public int numberOfAsteroidsToSpawn;
        public int asteroidsWaveCounter;
        public bool isAsteroidsReadyToSpawn;
        public int numberOfEnemyShipsToSpawn;
        public int enemyShipsWaveCounter;
        public bool isEnemyShipsReadyToSpawn;
        public GamePlayModel(System.Object player)
        {
            DrawParams=player;
            numberOfAsteroidsToSpawn = 3;
            asteroidsWaveCounter = 0;
            isAsteroidsReadyToSpawn = true;
            numberOfEnemyShipsToSpawn = 1;
            enemyShipsWaveCounter = 0;
            isEnemyShipsReadyToSpawn = false;
        }

        public Vector3 GetRandomPosition()
        {
            return new Vector3(Random.Range(-11, 11), (Random.Range(0, 1) == 1) ? Random.Range(3, 5) : Random.Range(-5, -3), 0);
        }
    
        public void SetRandomRotation(ref Transform _newObject)
        {
 	        _newObject.eulerAngles = new Vector3(0, 0, Random.Range(0, 360));
            _newObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
