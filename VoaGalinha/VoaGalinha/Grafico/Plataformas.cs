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

using VoaGalinha.Grafico;

namespace VoaGalinha.Grafico
{
    public class Plataformas
    {
        private const int _qtPlataformas = 101;

        public static Texture2D cabecalho { get; set; }
        public static Texture2D[] imagem { get; set; }
        public static Rectangle[] retangulo { get; set; }
        public static int[] alturaAtual { get; set; }
        public static int[] alturaMax { get; set; }
        public static int qtPlataformas { get; set; }
        public static bool[] plataformasPontuada { get; set; }
        public static int plataformasLimite { get; set; }
        public static Vector2[] velocidadeMovimento { get; set; }
        public static Color[] cor { get; set; }
        public static Random random { get; set; }

        public static Boolean puloAtivo;

        public Plataformas()
        {
            qtPlataformas = _qtPlataformas;
            alturaAtual = new int[qtPlataformas];
            alturaMax = new int[qtPlataformas];
            cor = new Color[qtPlataformas];
            imagem = new Texture2D[qtPlataformas];
            retangulo = new Rectangle[qtPlataformas];
            velocidadeMovimento = new Vector2[qtPlataformas];
            plataformasPontuada = new bool[qtPlataformas];
            plataformasLimite = 0;
        }
        public static void CarregaPlataforma(ContentManager content)
        {
            random = new Random();
            
            // Cabeçalho para ocultar plataformas
            cabecalho = content.Load<Texture2D>(".\\Imagens\\cabecalho");

            // Primeira Plataforma
            imagem[0] = content.Load<Texture2D>(".\\Imagens\\plataformaInicio");
            retangulo[0] = new Rectangle(0, Cenario.chaoCenario - (0 * 100), 480, imagem[0].Height);
            alturaAtual[0] = retangulo[0].Y;
            alturaMax[0] = retangulo[0].Y + 200;
            cor[0] = Color.White;
            plataformasPontuada[0] = true;

            // Ultima Plataforma
            imagem[100] = content.Load<Texture2D>(".\\Imagens\\plataformaFim");
            retangulo[100] = new Rectangle(0, Cenario.chaoCenario - (100 * 100), 480, imagem[100].Height);
            alturaAtual[100] = retangulo[100].Y;
            alturaMax[100] = retangulo[100].Y + 200;
            cor[100] = Color.White;
            plataformasPontuada[100] = true;

            for (int i = 1; i < qtPlataformas - 1; i++)
            {
                imagem[i] = content.Load<Texture2D>(".\\Imagens\\plataforma");
                retangulo[i] = new Rectangle(random.Next(0, 380), Cenario.chaoCenario - (i * 100), imagem[i].Width, imagem[i].Height);
                alturaAtual[i] = retangulo[i].Y;
                alturaMax[i] = retangulo[i].Y + 200;
                cor[i] = Color.White;
                plataformasPontuada[i] = false;
            }
        }
        public static void AtualizaPlataformas(GameTime gameTime)
        {
            TouchCollection touches = TouchPanel.GetState();
            random = new Random();

            if (touches.Count > 0)
            {
                if (touches[0].State == TouchLocationState.Moved)
                {
                    for (int i = 0; i < qtPlataformas; i++)
                    {
                        if ((retangulo[i].Y) >= alturaMax[i])
                        {
                            puloAtivo = false;
                        }
                    }
                }
            }
            else
            {
                puloAtivo = false;
            }
            if (puloAtivo)
            {                
                for (int i = 0; i < qtPlataformas; i++)
                {
                    if ((retangulo[i].Y) <= alturaMax[i])
                    {
                        velocidadeMovimento[i] = new Vector2(0, retangulo[i].Y);
                        velocidadeMovimento[i] += Vector2.Multiply(new Vector2(0, 1), 10);
                        retangulo[i] = new Rectangle(retangulo[i].X, (int)velocidadeMovimento[i].Y, imagem[i].Width, imagem[i].Height);
                        Personagem.situacaoAtual = Personagem.situacaoMovimento.Subindo;
                    }
                }
            }
            else
            {
                for (int i = 0; i < qtPlataformas; i++)
                {
                    if ((retangulo[i].Y) != alturaAtual[i])
                    {
                        velocidadeMovimento[i] = new Vector2(0, retangulo[i].Y);
                        velocidadeMovimento[i] -= Vector2.Multiply(new Vector2(0, 1), 10);
                        retangulo[i] = new Rectangle(retangulo[i].X, (int)velocidadeMovimento[i].Y, imagem[i].Width, imagem[i].Height);
                        Personagem.situacaoAtual = Personagem.situacaoMovimento.Caindo;
                    }
                    else
                    {
                        puloAtivo = true;                       
                    }
                }                
            }
        }
        public static void DesenhaPlataforma(GraphicsDeviceManager graphics, SpriteBatch spriteBatch, int index)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(imagem[index], retangulo[index], cor[index]);
            spriteBatch.Draw(cabecalho, new Rectangle(0, 0, cabecalho.Width, cabecalho.Height), Color.White);
            spriteBatch.End();
        }
    }
}
