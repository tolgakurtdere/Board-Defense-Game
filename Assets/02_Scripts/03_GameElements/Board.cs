using System;
using System.Collections.Generic;
using BoardDefense.DefenseItem;
using BoardDefense.Enemy;
using Sirenix.OdinInspector;
using TK.Manager;
using TK.Utility;
using Random = UnityEngine.Random;

namespace BoardDefense
{
    public class Board : MonoSingleton<Board>
    {
        [ShowInInspector, ReadOnly, TableMatrix(Transpose = true)]
        public Block[,] board = new Block[8, 4];

        protected override void Awake()
        {
            base.Awake();

            var blocks = GetComponentsInChildren<Block>();
            var index = 0;
            var rowCount = board.GetLength(0);
            var columnCount = board.GetLength(1);

            for (var i = 0; i < rowCount; i++)
            {
                for (var j = 0; j < columnCount; j++)
                {
                    var block = blocks[index];
                    block.Initialize(i, j, i >= rowCount / 2);
                    board[i, j] = block;
                    index++;
                }
            }
        }

        public void GoNextBlock(IMover enemy)
        {
            var currentBlock = FindItemBlock(enemy);

            if (currentBlock)
            {
                if (currentBlock.RowIndex >= board.GetUpperBound(0))
                {
                    print("LEVEL FAILED!");
                    LevelManager.StopLevel(false);
                    return;
                }

                currentBlock.RemoveItem(enemy);
                var newBlock = board[currentBlock.RowIndex + 1, currentBlock.ColumnIndex];
                newBlock.AddItem(enemy);
            }
            else
            {
                board[0, Random.Range(0, board.GetLength(1))].AddItem(enemy);
            }
        }

        public List<Block> FindTargetBlocks(IDefender defender)
        {
            var targets = new List<Block>();
            var currentBlock = FindItemBlock(defender);

            if (currentBlock)
            {
                switch (defender.Direction)
                {
                    case Direction.Forward:
                        for (var i = 0; i <= defender.Range; i++)
                        {
                            if (currentBlock.RowIndex - i >= 0)
                            {
                                targets.Add(board[currentBlock.RowIndex - i, currentBlock.ColumnIndex]);
                            }
                        }

                        break;
                    case Direction.All:
                        for (var i = 0; i <= defender.Range; i++)
                        {
                            if (currentBlock.RowIndex - i >= 0) //forward
                            {
                                targets.Add(board[currentBlock.RowIndex - i, currentBlock.ColumnIndex]);
                            }

                            if (currentBlock.RowIndex + i < board.GetLength(0)) //backward
                            {
                                targets.Add(board[currentBlock.RowIndex + i, currentBlock.ColumnIndex]);
                            }

                            if (currentBlock.ColumnIndex + i < board.GetLength(1)) //right
                            {
                                targets.Add(board[currentBlock.RowIndex, currentBlock.ColumnIndex + i]);
                            }

                            if (currentBlock.ColumnIndex - i >= 0) //left
                            {
                                targets.Add(board[currentBlock.RowIndex, currentBlock.ColumnIndex - i]);
                            }
                        }

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return targets;
        }

        public void RemoveItem(IItem item)
        {
            var currentBlock = FindItemBlock(item);

            if (currentBlock)
            {
                currentBlock.RemoveItem(item);
            }
        }

        private Block FindItemBlock(IItem item)
        {
            for (var i = 0; i < board.GetLength(0); i++)
            {
                for (var j = 0; j < board.GetLength(1); j++)
                {
                    var block = board[i, j];
                    if (block.IsItemHere(item))
                    {
                        return block;
                    }
                }
            }

            return null;
        }
    }
}