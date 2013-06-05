using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VoaGalinha
{
    public enum EstadoJogo
    {
        Ativo,
        Inicio,
        Fim,
        Pausado,
        Menu
    }

    public enum EstadoMenu
    {
        Intro,
        Main,
        Settings,
        Playing
    }
}
