using UnityEngine;
using System.Collections.Generic;
using System.Text;
using Asteroids.Model;

namespace Asteroids.Highscore
{
    public class HighscoreModel : AbstractModel
    {

        private static string highscore = null;
        public HighscoreModel()
        {
            if (highscore == null)
            {
                highscore = PlayerPrefs.GetString("highscore");
                if (highscore == "") resetHighscore(10);
            }
            DrawParams = ParseHighscore();
        }
        public void UpdateHighscore(KeyValuePair<string, int> newScore)
        {
            AddNewScore(newScore);
            PlayerPrefs.SetString("highscore", highscore);
        }

        public void resetHighscore(int length)
        {
            StringBuilder newHighscoreBuilder = new StringBuilder();
            List<KeyValuePair<string, int>> newHighscore = new List<KeyValuePair<string, int>>();
            for (int i = 0; i < length; i++)
            {
                newHighscore.Add(new KeyValuePair<string,int>("Random", 0));
                newHighscoreBuilder.Append("Random+0" + ((i != length-1) ? '*' : '\0'));
            }
            highscore = newHighscoreBuilder.ToString();
            PlayerPrefs.SetString("highscore", highscore);
        }

        private void AddNewScore(KeyValuePair<string, int> newScore)
        {
            StringBuilder updatedHighscoreBuilder = new StringBuilder();
            string[] parsedHighscore = highscore.Split('*');
            int iterator = highscore.Length;
            foreach (string record in parsedHighscore)
            {
                if (iterator > 0)
                {
                    CompareRecords(updatedHighscoreBuilder, record, newScore, iterator);
                }
            }
            highscore = updatedHighscoreBuilder.ToString();
        }

        private void CompareRecords(StringBuilder updatedHighscoreBuilder, string record, KeyValuePair<string, int> newScore, int iterator)
        {
            string[] recordCells = record.Split('+');
            if (newScore.Value > int.Parse(recordCells[1]))
            {
                updatedHighscoreBuilder.Append(newScore.Key + '+' + newScore.Value);
                iterator--;
                if (iterator > 0)
                    updatedHighscoreBuilder.Append('*');
                else
                    return;
            }
            updatedHighscoreBuilder.Append(recordCells[0] + '+' + recordCells[1] + ((iterator > 1) ? '*' : '\0'));
            iterator--;
        }

        private List<KeyValuePair<string, int>> ParseHighscore()
        {
            if (highscore == "")
                return null;
            List<KeyValuePair<string, int>> returnDictionary = new List<KeyValuePair<string, int>>();
            foreach (string record in highscore.Split('*'))
            {
                string[] recordCells = record.Split('+');
                returnDictionary.Add(new KeyValuePair<string,int>(recordCells[0], int.Parse(recordCells[1])));
            }
            return returnDictionary;
        }
    }
}