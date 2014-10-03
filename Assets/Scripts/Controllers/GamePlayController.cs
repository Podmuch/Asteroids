using UnityEngine;
using System.Timers;
using Asteroids.MovableObject.Player;
using Asteroids.MovableObject.Enemy.Asteroid;
using Asteroids.MovableObject.Enemy.EnemyShip;
using Asteroids.Controller;
using Asteroids.Interface;
using Asteroids.Highscore;
using System.Collections.Generic;

namespace Asteroids.GamePlay
{
    public class GamePlayController : AbstractController
    {
        public static bool PlayerFriendlyFire { get; set; }
        public static bool EnemyFriendlyFire { get; set; }
        public static int NumberOfAsteroidsInGame { get; set; }
        public static int NumberOfEnemyShipsInGame { get; set; }

        public Transform asteroidPointer;
        public Transform enemyShipPointer;

        private Timer asteroidsSpawnTimer;
        private Timer enemyShipSpawnTimer;
        
        private bool isEndGame;
        private int lastScore;
        private void Awake()
        {
            PlayerFriendlyFire = false;
            EnemyFriendlyFire = false ;
            isEndGame = false;
            asteroidsSpawnTimer = new Timer(5000);
            asteroidsSpawnTimer.Elapsed+= AsteroidsSpawnTimer_Tick;
            NumberOfAsteroidsInGame = 0;
            asteroidsSpawnTimer.Start();

            enemyShipSpawnTimer = new Timer(15000);
            enemyShipSpawnTimer.Elapsed += EnemyShipSpawnTimer_Tick;
            NumberOfEnemyShipsInGame = 0;
            enemyShipSpawnTimer.Start();
        }

        private void Start()
        {
            model = new GamePlayModel(FindObjectOfType<PlayerController>().model);
            view = new GamePlayView(Vector2.zero, new Vector2(Screen.width * 0.25f, Screen.height * 0.1f));
        }

        private void Update()
        {
            if (!isEndGame)
            {
                AsteroidsSpawn();
                EnemyShipsSpawn();
                EndGame();
            }
        }

        private void EndGame()
        {
            if ((model.DrawParams as IPlayer).Lives == 0)
            {
                enemyShipSpawnTimer.Stop();
                asteroidsSpawnTimer.Stop();
                isEndGame = true;
                lastScore = (model.DrawParams as IPlayer).Score;
                model.DrawParams = new System.Action<string>(SaveScore);
            }
        }

        private void SaveScore(string _nick)
        {
            HighscoreModel highscore = new HighscoreModel();
            highscore.UpdateHighscore(new KeyValuePair<string, int>(_nick, lastScore));
            Application.LoadLevel(0);
        }
        private void EnemyShipSpawnTimer_Tick(object sender, ElapsedEventArgs e)
        {
            (model as GamePlayModel).isEnemyShipsReadyToSpawn = true;
        }

        private void AsteroidsSpawnTimer_Tick(object sender, ElapsedEventArgs e)
        {
            (model as GamePlayModel).isAsteroidsReadyToSpawn = true;
        }

        private void EnemyShipsSpawn()
        {
            GamePlayModel gpModel = model as GamePlayModel;
            if (gpModel.isEnemyShipsReadyToSpawn && NumberOfEnemyShipsInGame < 3)
            {
                gpModel.enemyShipsWaveCounter++;
                for (int i = 0; i < gpModel.numberOfEnemyShipsToSpawn; i++)
                {
                    Transform newEnemyShip = (Transform)Instantiate(enemyShipPointer, gpModel.GetRandomPosition(), transform.rotation);
                    gpModel.SetRandomRotation(ref newEnemyShip);
                    newEnemyShip.gameObject.GetComponent<EnemyShipController>().AddModel(Random.Range(1, 3));
                    NumberOfEnemyShipsInGame++;
                }
                if (gpModel.numberOfEnemyShipsToSpawn < 3 && gpModel.enemyShipsWaveCounter > 10)
                {
                    gpModel.numberOfEnemyShipsToSpawn++;
                    gpModel.enemyShipsWaveCounter = 0;
                }
                gpModel.isEnemyShipsReadyToSpawn = false;
            }
        }

        private void AsteroidsSpawn()
        {
            GamePlayModel gpModel = model as GamePlayModel;
            if (gpModel.isAsteroidsReadyToSpawn&&NumberOfAsteroidsInGame<10)
            {
                gpModel.asteroidsWaveCounter++;
                for (int i = 0; i < gpModel.numberOfAsteroidsToSpawn; i++)
                {
                    Transform newAsteroid = (Transform)Instantiate(asteroidPointer, gpModel.GetRandomPosition(), transform.rotation);
                    gpModel.SetRandomRotation(ref newAsteroid);
                    newAsteroid.gameObject.GetComponent<AsteroidController>().AddModel(Random.Range(1, 3));
                    NumberOfAsteroidsInGame++;
                }
                gpModel.isAsteroidsReadyToSpawn = false;
                if (gpModel.numberOfAsteroidsToSpawn < 10&&gpModel.asteroidsWaveCounter>10)
                {
                    gpModel.numberOfAsteroidsToSpawn++;
                    gpModel.asteroidsWaveCounter = 0;
                }
            }
        }
    }
}
