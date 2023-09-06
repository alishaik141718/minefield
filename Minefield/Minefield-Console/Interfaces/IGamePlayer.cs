
using System;
namespace MinefieldGameConsole.Interfaces
{
	public interface IGamePlayer
	{

        void GoUp();
        void GoDown();
        void GoLeft();
        void GoRight();

        bool Alive();
        bool HasReachedTarget();

        void ReduceLives(int numOfLives);
        int GetTotalMoves();

        void Reset();



    }
}

