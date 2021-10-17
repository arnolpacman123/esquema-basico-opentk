using System;

namespace EsquemaBasico
{
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game(800, 600, "Esquema Basico");
            game.Run(60.0);
        }
    }
}