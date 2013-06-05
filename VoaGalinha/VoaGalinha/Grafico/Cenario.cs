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
    public class Cenario
    {
        private const int _chaoCenario = 636;
        private const int _largura = 480;
        private const int _altura = 800;

        public static int chaoCenario { get; set; }
        public static int largura { get; set; }
        public static int altura { get; set; }
        public static Texture2D fundoCenario { get; set; }
        public static Texture2D fundoInicio { get; set; }
        public static Texture2D fimJogo { get; set; }
   
        public Cenario()
        {
            chaoCenario = _chaoCenario;
            largura = _largura;
            altura = _altura;
        }
        public static void CarregaCenario(ContentManager content)
        {            
            // Imagens
            fundoCenario = content.Load<Texture2D>(".\\Imagens\\fundoCenario");
            fundoInicio = content.Load<Texture2D>(".\\Imagens\\fundoInicio");
            fimJogo = content.Load<Texture2D>(".\\Imagens\\fim");
        }
        public static void DesenhaCenario(GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            switch (Game1.EstadoCorrente)
            {
                case EstadoJogo.Inicio:
                    {
                        spriteBatch.Draw(fundoInicio, new Rectangle(0, 0, fundoInicio.Width, fundoInicio.Height), Color.White);
                    }
                    break;

                case EstadoJogo.Ativo:
                    {
                        spriteBatch.Draw(fundoCenario, new Rectangle(0, 0, fundoCenario.Width, fundoCenario.Height), Color.White);
                    }
                    break;

                case EstadoJogo.Menu:
                    {
                        //spriteBatch.Draw(cabecalho, new Rectangle(0, 0, cabecalho.Width, cabecalho.Height), Color.Transparent);
                    }
                    break;

                 case EstadoJogo.Pausado:
                    {
                       // spriteBatch.Draw(cabecalho, new Rectangle(0, 0, cabecalho.Width, cabecalho.Height), Color.Black);
                    }
                    break;

                 case EstadoJogo.Fim:
                    {
                        spriteBatch.Draw(fimJogo, new Rectangle(0, 0, fimJogo.Width, fimJogo.Height), Color.Red);
                    }
                    break;

                 default:
                    break;
            }
                    spriteBatch.End();
        }
    }
}
