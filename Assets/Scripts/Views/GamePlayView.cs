using UnityEngine;
using Asteroids.Interface;
using Asteroids.View;
using Asteroids.Controller;
using System;

namespace Asteroids.GamePlay
{
    public class GamePlayView : AbstractView
    {
        private int lastScore;
        private string nick;
        public GamePlayView(Vector2 _margin, Vector2 _size)
        {
            size = _size;
            margin = _margin;
            style = new GUIStyle();
            style.normal.textColor = Color.white;
            style.wordWrap = true;
            style.fontSize = 20;
            style.alignment = TextAnchor.UpperCenter;
            nick = "Enter your Nick";
        }
        public GamePlayView(Vector2 _margin, Vector2 _size, GUIStyle _Style)
        {
            size = _size;
            margin = _margin;
            style= _Style;
        }

        public override bool Draw(System.Object drawParams)
        {
            IPlayer player = drawParams as IPlayer;
            if (player != null) { 
                GUI.Box(new Rect(margin.x, margin.y, size.x, size.y), "PLAYER", style);
                GUI.Box(new Rect(margin.x, margin.y + size.y * 0.5f, size.x * 0.5f, size.y * 0.5f), "LIVES: " + player.Lives, style);
                GUI.Box(new Rect(margin.x + size.x * 0.5f, margin.y + size.y * 0.5f, size.x * 0.5f, size.y * 0.5f), "SCORE: " + player.Score, style);
                lastScore=player.Score;
            }
            else
            {
                Action<string> onClick = drawParams as Action<string>;
                GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
                boxStyle.fontSize = 30;
                boxStyle.alignment = TextAnchor.UpperCenter;
                GUI.Box(new Rect(Screen.width * 0.3f, Screen.height * 0.3f, Screen.width * 0.4f, Screen.height * 0.4f), "GAME OVER", boxStyle);
                GUI.Box(new Rect(Screen.width * 0.3f, Screen.height * 0.4f, Screen.width * 0.4f, Screen.height * 0.1f), "YOUR SCORE: "+lastScore, style);
                boxStyle.alignment = TextAnchor.LowerCenter;
                nick=GUI.TextField(new Rect(Screen.width * 0.3f, Screen.height * 0.5f, Screen.width * 0.4f, Screen.height * 0.1f), nick, boxStyle);
                if (nick.Length > 20) 
                    nick=nick.Substring(0, 20);
                if (GUI.Button(new Rect(Screen.width * 0.3f, Screen.height * 0.6f, Screen.width * 0.4f, Screen.height * 0.1f), "Continue", boxStyle))
                    onClick(nick);
            }
            return false;  
        }
    }
}
