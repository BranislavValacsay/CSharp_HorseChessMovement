using System;
using System.Collections.Generic;

namespace ChessPuzzles
{
    /// <summary>
    /// Solves the minimum number of moves for a knight to reach a target position on a chessboard.
    /// </summary>
    public class KnightMovesSolver
    {
        /// <summary>
        /// Represents a cell on the chessboard with its position and distance from the starting point.
        /// </summary>
        private class Cell
        {
            public int X { get; }
            public int Y { get; }
            public int Distance { get; }

            public Cell(int x, int y, int distance)
            {
                X = x;
                Y = y;
                Distance = distance;
            }
        }

        // Possible moves for a knight (8 directions)
        private static readonly int[] DX = { -2, -1, 1, 2, -2, -1, 1, 2 };
        private static readonly int[] DY = { -1, -2, -2, -1, 1, 2, 2, 1 };

        /// <summary>
        /// Checks if the given position is within the chessboard boundaries.
        /// </summary>
        /// <param name="x">X-coordinate of the position.</param>
        /// <param name="y">Y-coordinate of the position.</param>
        /// <param name="boardSize">Size of the chessboard.</param>
        /// <returns>True if the position is inside the board, otherwise false.</returns>
        private static bool IsInside(int x, int y, int boardSize)
        {
            return x >= 1 && x <= boardSize && y >= 1 && y <= boardSize;
        }

        /// <summary>
        /// Calculates the minimum number of steps for a knight to reach the target position.
        /// </summary>
        /// <param name="knightPos">Starting position of the knight.</param>
        /// <param name="targetPos">Target position to reach.</param>
        /// <param name="boardSize">Size of the chessboard.</param>
        /// <returns>Minimum number of steps to reach the target, or int.MaxValue if unreachable.</returns>
        public static int MinStepsToReachTarget(int[] knightPos, int[] targetPos, int boardSize)
        {
            var queue = new Queue<Cell>();
            var visited = new bool[boardSize + 1, boardSize + 1];

            // Start from the knight's initial position
            queue.Enqueue(new Cell(knightPos[0], knightPos[1], 0));
            visited[knightPos[0], knightPos[1]] = true;

            while (queue.Count > 0)
            {
                var currentCell = queue.Dequeue();

                // If we've reached the target, return the distance
                if (currentCell.X == targetPos[0] && currentCell.Y == targetPos[1])
                {
                    return currentCell.Distance;
                }

                // Explore all possible moves from the current position
                for (int i = 0; i < 8; i++)
                {
                    int newX = currentCell.X + DX[i];
                    int newY = currentCell.Y + DY[i];

                    if (IsInside(newX, newY, boardSize) && !visited[newX, newY])
                    {
                        visited[newX, newY] = true;
                        queue.Enqueue(new Cell(newX, newY, currentCell.Distance + 1));
                    }
                }
            }

            // If we can't reach the target
            return int.MaxValue;
        }

        public static void Main(string[] args)
        {
            int boardSize = 30;
            int[] knightPos = { 1, 1 };
            int[] targetPos = { 1, 3 };

            int result = MinStepsToReachTarget(knightPos, targetPos, boardSize);
            Console.WriteLine($"Minimum steps: {result}");
        }
    }
}