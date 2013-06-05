using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace VoaGalinha.Grafico
{
    public class Pontuacao
    {
        public static double pontuacaoAtual { get; set; }
        public static Color cor { get; set; }
        public static SpriteFont UIFont { get; set; }

        public Pontuacao()
        {
            pontuacaoAtual = 0;
            cor = Color.White;
        }
        public static void AtualizaPontuacao()
        {
            switch ((int)pontuacaoAtual / 100)
            {
                case 20:
                    {
                        cor = Color.Yellow;
                    }
                    break;

                case 40:
                    {
                        cor = Color.YellowGreen;
                    }
                    break;

                case 60:
                    {
                        cor = Color.Orange;
                    }
                    break;

                case 80:
                    {
                        cor = Color.OrangeRed;
                    }
                    break;

                case 100:
                    {
                        cor = Color.Red;
                    }
                    break;

                default:
                    break;
            }
        }
        public static void CarregaPontuacao(ContentManager content)
        {
            // Fontes
            UIFont = content.Load<SpriteFont>(".\\Imagens\\UI");
        }
        public static void DesenhaPontuacao(GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(UIFont, string.Format("Pontuação: {0:0000000}", Pontuacao.pontuacaoAtual), new Vector2(20, 20), cor);
            spriteBatch.End();
        }
    }
}
