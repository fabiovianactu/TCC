using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

using VoaGalinha.Grafico;
using VoaGalinha.Fisica;

namespace VoaGalinha
{
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        
        // Sensor
        Movimento movimento;

        public static EstadoJogo EstadoCorrente { get; set; }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 480;
            graphics.PreferredBackBufferHeight = 800;
            Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333);

            // Extend battery life under lock.
            InactiveSleepTime = TimeSpan.FromSeconds(1);
        }

        protected override void Initialize()
        {
            // Inicializa Movimento
            movimento = new Movimento();
            movimento.InicializaMovimento();
            
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            EstadoCorrente = EstadoJogo.Inicio;

            // Carrega Cenario
            new Cenario();
            Cenario.CarregaCenario(this.Content);

            // Carrega Personagem
            new Pontuacao();
            Pontuacao.CarregaPontuacao(this.Content);

            // Carrega plataformas
            new Plataformas();
            Plataformas.CarregaPlataforma(this.Content);

            // Carrega Personagem
            new Personagem();
            Personagem.CarregaPersonagem(this.Content);
        }

        protected override void UnloadContent()
        {
            this.Content.Unload();
        }

        protected override void Update(GameTime gameTime)
        {
            GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (EstadoCorrente == EstadoJogo.Ativo)
            {
                Plataformas.AtualizaPlataformas(gameTime);
                Personagem.AtualizaPersonagem(gameTime);
                Pontuacao.AtualizaPontuacao();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            Cenario.DesenhaCenario(graphics, spriteBatch);

            if (EstadoCorrente == EstadoJogo.Ativo)
            {
                for (int i = 0; i < Plataformas.qtPlataformas; i++)
                {
                    Plataformas.DesenhaPlataforma(graphics, spriteBatch, i);
                }

                Pontuacao.DesenhaPontuacao(graphics, spriteBatch);
                Personagem.DesenhaPersonagem(graphics, spriteBatch);
            }
            
            base.Draw(gameTime);
        }
    }
}
