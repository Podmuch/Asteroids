//GamePlay Controller
//  controls new enemies spawn
//  handles end of the game
using UnityEngine;
using Asteroids.MovableObject.Player;
using Asteroids.MovableObject.Enemy.Asteroid;
using Asteroids.MovableObject.Enemy.EnemyShip;
using Asteroids.Interface;
using Asteroids.Highscore;
using System.Collections.Generic;

namespace Asteroids.GamePlay
{
    //inherits from base abstract class for all Controllers (drawing)
    public class GamePlayController : AbstractController
    {
        //controling friendlyfire
        public static bool PlayerFriendlyFire { get; set; }
        public static bool EnemyFriendlyFire { get; set; }
        //containing a number of Asteroids on the scene (prevents the creation of new asteroids)
        public static int NumberOfAsteroidsInGame { get; set; }
        //containing a number of EnemyShips on the scene (prevents the creation of new enemyships)
        public static int NumberOfEnemyShipsInGame { get; set; }

        //Asteroid and EnemyShip prefabs
        public Transform asteroidPointer;
        public Transform enemyShipPointer;
        //Player prefab
        public Transform playerPointer;
        //background image
        public Transform backgroundImage;
        
        //stopping spawns 
        private bool isEndGame;
        //Score which is send to highscore
        private int lastScore;
        //Timers
        private float EnemyShipsSpawnTime = 15.0f, AsteroidsSpawnTime = 5.0f;
        private void Awake()
        {
            PlayerFriendlyFire = false;
            EnemyFriendlyFire = false ;
            isEndGame = false;
            NumberOfAsteroidsInGame = 0;

            NumberOfEnemyShipsInGame = 0;
        }

        private void Start()
        {
            //create player
            Instantiate(playerPointer, Vector3.zero, transform.rotation);
            //create GamePlay model
            model = new GamePlayModel(FindObjectOfType<PlayerController>().model);
            //adapts gameplay to screen resolution
            view = new GamePlayView(backgroundImage);
            //scales map corners
            GamePlayModel.leftTopCorner.x *= backgroundImage.localScale.x;
            GamePlayModel.rightBottomCorner.x *= backgroundImage.localScale.x;
        }

        private void Update()
        {
            if (!isEndGame)
            {
                if (EnemyShipsSpawnTime < 0.0f)
                {
                    (model as GamePlayModel).isEnemyShipsReadyToSpawn = true;
                    EnemyShipsSpawnTime = 15.0f;
                }
                else
                    EnemyShipsSpawnTime -= Time.deltaTime;
                if (AsteroidsSpawnTime < 0.0f)
                {
                    (model as GamePlayModel).isAsteroidsReadyToSpawn = true;
                    AsteroidsSpawnTime = 15.0f;
                }
                else
                    AsteroidsSpawnTime -= Time.deltaTime;
                AsteroidsSpawn();
                EnemyShipsSpawn();
                EndGame();
            }
        }

        private void EndGame()
        {
            if ((model.DrawParams as IPlayer).Lives == 0)
            {
                isEndGame = true;
                lastScore = (model.DrawParams as IPlayer).Score;
                //stop displaying points and lives and start displaying final window
                model.DrawParams = new System.Action<string>(SaveScore);
            }
        }

        private void SaveScore(string _nick)
        {
            HighscoreModel highscore = new HighscoreModel();
            highscore.UpdateHighscore(new KeyValuePair<string, int>(_nick, lastScore));
            Application.LoadLevel(0);
        }

        private void EnemyShipsSpawn()
        {
            GamePlayModel gpModel = model as GamePlayModel;
            //if enemyships can be spawn and number of exist ships is less than max number
            if (gpModel.isEnemyShipsReadyToSpawn && NumberOfEnemyShipsInGame < 3)
            {
                //next waves are bigger than previous
                gpModel.enemyShipsWaveCounter++;
                for (int i = 0; i < gpModel.numberOfEnemyShipsToSpawn; i++)
                {
                    Transform newEnemyShip = (Transform)Instantiate(enemyShipPointer, gpModel.GetRandomPosition(), transform.rotation);
                    gpModel.SetRandomRotation(ref newEnemyShip);
                    //random lives (size) of new enemyships
                    newEnemyShip.gameObject.GetComponent<EnemyShipController>().AddModel(Random.Range(1, 3));
                    NumberOfEnemyShipsInGame++;
                }
                //change number of EnemyShips to Spawn in next wave
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
            //if asteroids can be spawn and number of exist asteroids is less than max number
            if (gpModel.isAsteroidsReadyToSpawn&&NumberOfAsteroidsInGame<10)
            {
                //next waves are bigger than previous
                gpModel.asteroidsWaveCounter++;
                for (int i = 0; i < gpModel.numberOfAsteroidsToSpawn; i++)
                {
                    Transform newAsteroid = (Transform)Instantiate(asteroidPointer, gpModel.GetRandomPosition(), transform.rotation);
                    gpModel.SetRandomRotation(ref newAsteroid);
                    //random lives (size) of new asteroids
                    newAsteroid.gameObject.GetComponent<AsteroidController>().AddModel(Random.Range(1, 3));
                    NumberOfAsteroidsInGame++;
                }
                gpModel.isAsteroidsReadyToSpawn = false;
                //change number of Asteroids to Spawn in next wave
                if (gpModel.numberOfAsteroidsToSpawn < 10&&gpModel.asteroidsWaveCounter>10)
                {
                    gpModel.numberOfAsteroidsToSpawn++;
                    gpModel.asteroidsWaveCounter = 0;
                }
            }
        }
    }
}
