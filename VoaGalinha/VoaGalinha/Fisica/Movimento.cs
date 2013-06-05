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

using VoaGalinha.Grafico;

namespace VoaGalinha.Fisica
{
    public class Movimento
    {
        public Motion movimento { get; set; }

        public Movimento()
        {
            movimento = new Motion();
        }
        public void InicializaMovimento()
        {
            if (Motion.IsSupported)
            {
                if (movimento != null)
                {
                    movimento.CurrentValueChanged += new EventHandler<SensorReadingEventArgs<MotionReading>>(AtualizaMovimento);
                    movimento.Start();
                }
            }
        }
        void AtualizaMovimento(object sender, SensorReadingEventArgs<MotionReading> e)
        {
            Personagem.AtualizaMovimentoPersonagem(e);
        }
    }
}
