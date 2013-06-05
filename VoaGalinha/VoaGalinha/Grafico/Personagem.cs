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
using Microsoft.Devices.Sensors;

using VoaGalinha;

namespace VoaGalinha.Grafico
{
    public class Personagem
    {
        public static Texture2D imagem { get; set; }
        public static Vector2 posicao { get; set; }
        public static Vector2 posicaoAtual { get; set; }
        public static Rectangle posicaoImagem { get; set; }
        public static Color cor { get; set; }
        public static int passo { get; set; }
        public static int pulo { get; set; }
        public enum situacaoMovimento {Subindo, Caindo, Parado }
        public static situacaoMovimento situacaoAtual { get; set; }

        public Personagem()
        {
            cor = Color.White;
            posicaoImagem = new Rectangle();
            posicao = new Vector2();
            posicaoAtual = new Vector2();
            passo = 0;
            pulo = 0;
            situacaoAtual = situacaoMovimento.Parado;
        }
        public static void CarregaPersonagem(ContentManager content)
        {
            imagem = content.Load<Texture2D>(".\\Imagens\\spritesOvo");
            posicao = new Vector2(0, Cenario.chaoCenario);
            posicaoAtual = new Vector2(0, Cenario.chaoCenario);
            posicaoImagem = new Rectangle(0, 0, 60, 60);
        }
        public static void AtualizaPersonagem(GameTime gameTime)
        {
            TouchCollection touches = TouchPanel.GetState();

            if (touches.Count > 0)
            {
                posicaoImagem = new Rectangle(pulo, 0, 60, 60);                
            }
            else
            {
                posicaoImagem = new Rectangle(passo, 0, 60, 60);
                pulo = passo;
            }            
        }
        public static void AtualizaMovimentoPersonagem(SensorReadingEventArgs<MotionReading> e)
        {
            TouchCollection touches = TouchPanel.GetState();

            if (Game1.EstadoCorrente == EstadoJogo.Ativo)
            {
                if (((int)posicaoAtual.X  > 0) && (((int)posicaoAtual.X  < 420)))
                {
                    if (e.SensorReading.Gravity.X * 22.2f >= 0)
                    {
                        if ((int)posicaoAtual.X % 3 == 0)
                        {
                            passo = passo + 60;

                            if (passo >= 660) { passo = 0; }
                        }
                    }
                    else if (e.SensorReading.Gravity.X * 22.2f < 0)
                    {
                        if ((int)posicaoAtual.X % 3 == 0)
                        {
                            passo = passo - 60;

                            if (passo <= 0) { passo = 660; }
                        }
                    }
                }
                else
                {
                    passo = 0;
                }

                if ((posicaoAtual.X + e.SensorReading.Gravity.X) > 480 - 60)
                {
                    posicaoAtual = new Vector2(480 - 60, 0);
                }
                else if ((posicaoAtual.X + e.SensorReading.Gravity.X) < 0.0f)
                {
                    posicaoAtual = new Vector2(0, 0);
                }
                else
                {
                    posicaoAtual += new Vector2(e.SensorReading.Gravity.X * 22.2f, 0);
                }
                posicao = posicaoAtual;

                if (situacaoAtual == situacaoMovimento.Caindo)
                {
                    for (int i = 0; i < Plataformas.qtPlataformas; i++)
                    {
                        if (EmCimaDaPlataforma(i))
                        {
                            Plataformas.plataformasLimite = i - 3;

                            for (int i2 = 0; i2 < Plataformas.qtPlataformas; i2++)
                            {
                                Plataformas.alturaAtual[i2] = Plataformas.retangulo[i2].Y;
                                Plataformas.alturaMax[i2] = Plataformas.retangulo[i2].Y + 200;
                            }
                        }
                        else
                        {
                            if (CaiuDaPlataforma(i))
                            {
                                if (i <= Plataformas.plataformasLimite)
                                {
                                    Game1.EstadoCorrente = EstadoJogo.Fim;
                                }

                                for (int i2 = 0; i2 < Plataformas.qtPlataformas; i2++)
                                {
                                    Plataformas.velocidadeMovimento[i2] = new Vector2(0, Plataformas.retangulo[i2].Y);
                                    Plataformas.velocidadeMovimento[i2] -= Vector2.Multiply(new Vector2(0, 1), 10);
                                    Plataformas.retangulo[i2] = new Rectangle(Plataformas.retangulo[i2].X, (int)Plataformas.velocidadeMovimento[i2].Y, Plataformas.imagem[i2].Width, Plataformas.imagem[i2].Height);
                                }
                            }
                        }
                    }
                }
                if (Plataformas.alturaAtual[0] < Cenario.chaoCenario)
                {
                    Plataformas.alturaAtual[0] = 0;
                }
            }
            else if (Game1.EstadoCorrente == EstadoJogo.Inicio)
            {
                if (touches.Count > 0)
                {
                    if (touches[0].State == TouchLocationState.Moved)
                    {
                        Game1.EstadoCorrente = EstadoJogo.Ativo;
                    }
                }
            }
        }
        public static void DesenhaPersonagem(GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(imagem, new Vector2(posicao.X, Cenario.chaoCenario - 64), posicaoImagem, cor, 0.0f, new Vector2(0, 0), 1, SpriteEffects.None, 0);
            spriteBatch.End();
        }
        public static bool EmCimaDaPlataforma(int index)
        {
            bool emCima = false;

            if ((Plataformas.retangulo[index].Y) == Cenario.chaoCenario)
            {
                if (((posicao.X + posicaoImagem.Width) >= Plataformas.retangulo[index].X) && (posicao.X < (Plataformas.retangulo[index].X + Plataformas.retangulo[index].Width)))
                {
                    emCima = true;

                    if (Plataformas.plataformasPontuada[index] == false)
                    {
                        Plataformas.plataformasPontuada[index] = true;
                        Pontuacao.pontuacaoAtual = Pontuacao.pontuacaoAtual + 100;
                    }
                }
            }
            return emCima;    
        }
        public static bool CaiuDaPlataforma(int index)
        {
            bool caiu = false;
 
            if ((Plataformas.retangulo[index].Y) == Cenario.chaoCenario)
            {
                if (((posicao.X + posicaoImagem.Width) < Plataformas.retangulo[index].X) || ((posicao.X) > (Plataformas.retangulo[index].X + Plataformas.retangulo[index].Width)))
                {
                    caiu = true;
                }                
            }
            if (index == 0)
            {
                caiu = false;            
            }
            return caiu;
        }
    }
}
