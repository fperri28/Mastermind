using System;

namespace mastermind
{
    class Mastermind
    {
        static void Main(string[] args)
        {
            Game game = new Game();

       	    game.PrintInstructions();
    	    game.Run();
        }
    }
}
