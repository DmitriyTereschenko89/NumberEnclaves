using System;
using System.Runtime.ExceptionServices;

namespace NumberEnclaves
{
    internal class Program
    {
        public class NumberEnclaves
        {
            private readonly int[][] directions = new int[][] {
                new int[] { 0, - 1 }, new int[] { -1, 0 }, new int[] { 0, 1 }, new int[] { 1, 0 }
            };

            private List<int[]> GetNeighbors(int[][] grid, int r, int c, int rows, int cols)
            {
                List<int[]> neighbors = new List<int[]>();
                foreach (int[] direction in directions)
                {
                    int newR = r + direction[0];
                    int newC = c + direction[1];
                    if (newR >= 0 && newR < rows && newC >= 0 && newC < cols)
                    {
                        neighbors.Add(new int[] { newR, newC });
                    }
                }
                return neighbors;
            }

            private void BFS(int[][] grid, bool[,] visited, int r, int c, int rows, int cols)
            {
                Queue<int[]> queue = new();
                grid[r][c] = 2;
                queue.Enqueue(new int[] { r, c });
                while (queue.Count > 0)
                {
                    int[] node = queue.Dequeue();
                    List<int[]> neighbors = GetNeighbors(grid, node[0], node[1], rows, cols);
                    foreach (int[] neighbor in neighbors)
                    {
                        int newR = neighbor[0];
                        int newC = neighbor[1];
                        if (visited[newR, newC])
                        {
                            continue;
                        }
                        visited[newR, newC] = true;
                        if (grid[newR][newC] == 0)
                        {
                            continue;
                        }
                        grid[newR][newC] = 2;
                        queue.Enqueue(new int[] { newR, newC });
                    }
                }
            }

            public int NumEnclaves(int[][] grid)
            {
                int rows = grid.Length;
                int cols = grid[0].Length;
                bool[,] visited = new bool[rows, cols];
                int numEnclaves = 0;
                for (int r = 0; r < rows; ++r)
                {
                    for (int c = 0; c < cols; ++c)
                    {
                        if (r == 0 || r == rows - 1 || (r > 0 && r < rows && c == 0) || (r > 0 && r < rows && c == cols - 1))
                        {
                            if (grid[r][c] == 1 && !visited[r, c])
                            {
                                BFS(grid, visited, r, c, rows, cols);
                            }
                        }
                    }
                }
                for (int r = 0; r < rows; ++r)
                {
                    for (int c = 0; c < cols; ++c)
                    {
                        if (grid[r][c] == 1)
                        {
                            ++numEnclaves;
                        }
                    }
                }
                return numEnclaves;
            }
        }
        static void Main(string[] args)
        {
            NumberEnclaves numberEnclaves = new();
            Console.WriteLine(numberEnclaves.NumEnclaves(new int[][] 
            {
                new int[] { 0, 0, 0, 0 },
                new int[] { 1, 0, 1, 0 },
                new int[] { 0, 1, 1, 0 },
                new int[] { 0, 0, 0, 0 }
            }));
            Console.WriteLine(numberEnclaves.NumEnclaves(new int[][]
            {
                new int[] { 0, 1, 1, 0 },
                new int[] { 0, 0, 1, 0 },
                new int[] { 0, 0, 1, 0 },
                new int[] { 0, 0, 0, 0 }
            }));
            Console.WriteLine(numberEnclaves.NumEnclaves(new int[][]
            {
                new int[] { 0, 0, 0, 1, 1, 1, 0, 1, 0, 0 },
                new int[] { 1, 1, 0, 0, 0, 1, 0, 1, 1, 1 },
                new int[] { 0, 0, 0, 1, 1, 1, 0, 1, 0, 0 },
                new int[] { 0, 1, 1, 0, 0, 0, 1, 0, 1, 0 },
                new int[] { 0, 1, 1, 1, 1, 1, 0, 0, 1, 0 },
                new int[] { 0, 0, 1, 0, 1, 1, 1, 1, 0, 1 },
                new int[] { 0, 1, 1, 0, 0, 0, 1, 1, 1, 1 },
                new int[] { 0, 0, 1, 0, 0, 1, 0, 1, 0, 1 },
                new int[] { 1, 0, 1, 0, 1, 1, 0, 0, 0, 0 },
                new int[] { 0, 0, 0, 0, 1, 1, 0, 0, 0, 1 }
            }));
        }
    }
}