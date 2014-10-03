using UnityEngine;
using System.Collections.Generic;
using Asteroids.View;

namespace Asteroids.Highscore
{
    public class HighscoreView : AbstractView
    {
        private Vector2 recordSize;
        private Vector2 recordMargin;

        public HighscoreView()
        {
            size = new Vector2(Screen.width * 0.3f, Screen.height * 0.8f);
            margin = new Vector2(Screen.width * 0.1f, Screen.height * 0.1f);
            recordSize = new Vector2(Screen.width * 0.2f, Screen.height * 0.07f);
            recordMargin = new Vector2(Screen.width * 0.15f,Screen.height * 0.2f);
            style = new GUIStyle();
            //style.normal.textColor = Color.white;
            //style.hover.textColor = Color.yellow;
            //style.stretchHeight = true;
            //style.stretchWidth = true;
            style.wordWrap = true;
        }

        public HighscoreView(Vector2 _highscoreTableSize, Vector2 _highscoreTableMargin, Vector2 _highscoreTableRecordSize, Vector2 _highscoreTableRecordMargin, GUIStyle _highscoreStyle)
        {
            size = _highscoreTableSize;
            margin = _highscoreTableMargin;
            recordSize = _highscoreTableRecordSize;
            recordMargin = _highscoreTableRecordMargin;
            style = _highscoreStyle;
        }

        public override bool Draw(System.Object drawParams){
            List<KeyValuePair<string,int>> highscores= drawParams as List<KeyValuePair<string,int>>;
            float recordOffset = 0;
            GUI.Box(new Rect(margin.x, margin.y,size.x, size.y), "Highscore");
            foreach (KeyValuePair<string, int> record in highscores)
            {
                GUI.Box(new Rect(recordMargin.x, recordMargin.y + recordOffset, recordSize.x, recordSize.y), record.Key + " " + record.Value.ToString());
                recordOffset += recordSize.y;
            }
            return false;
        }
    }
}
