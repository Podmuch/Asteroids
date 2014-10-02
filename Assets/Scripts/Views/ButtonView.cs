using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using Asteroids.View;

namespace Asteroids.Button {
    public class ButtonView : AbstractView
    {
        public ButtonView(Vector2 _margin, Vector2 _size, Texture2D _normalButton, Texture2D _hoverButton)
        {
            size = _size;
            margin = _margin;
            style = new GUIStyle();
            style.normal.background = _normalButton;
            style.hover.background = _hoverButton;
        }
        public ButtonView(Vector2 _margin, Vector2 _size, GUIStyle _buttonStyle)
        {
            size = _size;
            margin = _margin;
            style= _buttonStyle;
        }

        public override void Draw(System.Object drawParams)
        {
            Action onClick = drawParams as Action;
            if (GUI.Button(new Rect(margin.x, margin.y, size.x, size.y), "", style))
                onClick();
        }
    }
}
