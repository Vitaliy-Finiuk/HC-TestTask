using CodeBase.Services.Input;
using UnityEngine;

namespace CodeBase.Infrastucture
{
    public class Game
    {
        public static IInputService InputService;

        public Game()
        {
            InputService = new StandaloneInputService();
        }
    }
}
