using System;
using System.Collections.Generic;

namespace CompareFiles
{
    public class StringsComparer
    {
        public IEnumerable<CompareStringResult> Compare(string[] oldValues, string[] newValues)
        {
            var board = new int[oldValues.Length + 1, newValues.Length + 1];
            for (var i = 0; i < board.GetLength(0); i++) board[i, 0] = i;
            for (var i = 0; i < board.GetLength(1); i++) board[0, i] = i;

            // подготовка таблицы изменений
            for (var i = 1; i < board.GetLength(0); i++)
            {
                for (var j = 1; j < board.GetLength(1); j++)
                {
                    var stringsEquals = string.CompareOrdinal(oldValues[i - 1], newValues[j - 1]) == 0;
                    var add = (stringsEquals ? 0 : 1);
                    board[i, j] = Math.Min(board[i - 1, j - 1] + add * 2, Math.Min(board[i - 1, j] + 1, board[i, j - 1] + 1));
                }
            }

            var stack = new Stack<CompareStringResult>();

            var ii = board.GetLength(0) - 1;
            var jj = board.GetLength(1) - 1;

            // прогулка из правого нижнего угла таблицы в верхний левый по пути с минимальной суммой
            while (ii > 0 && jj > 0)
            {
                var max = board[ii, jj];
                var min = Math.Min(board[ii - 1, jj - 1], Math.Min(board[ii - 1, jj], board[ii, jj - 1]));
                if (min == board[ii - 1, jj - 1])
                {
                    if (min != max)
                    {
                        stack.Push(new CompareStringResult(oldValues[ii - 1], newValues[jj - 1], CompareStringResult.ActionType.Changed));
                    }

                    else
                        stack.Push(new CompareStringResult(newValues[jj - 1], newValues[jj - 1], CompareStringResult.ActionType.NotChanged));

                    ii--;
                    jj--;
                }
                else if (min == board[ii - 1, jj])
                {
                    if (min != max)
                        stack.Push(new CompareStringResult(oldValues[ii - 1], null, CompareStringResult.ActionType.Deleted));
                    else
                        stack.Push(new CompareStringResult(oldValues[ii - 1], newValues[jj], CompareStringResult.ActionType.NotChanged));
                    ii--;
                }
                else if (min == board[ii, jj - 1])
                {
                    if (min != max)
                        stack.Push(new CompareStringResult(null, newValues[jj - 1], CompareStringResult.ActionType.Added));
                    else
                        stack.Push(new CompareStringResult(oldValues[ii], newValues[jj - 1], CompareStringResult.ActionType.NotChanged));
                    jj--;
                }
            }

            while (ii > 0)
            {
                var max = board[ii, jj];
                var min = board[ii - 1, jj];

                if (min != max)
                    stack.Push(new CompareStringResult(oldValues[ii - 1], null, CompareStringResult.ActionType.Deleted));
                else
                    stack.Push(new CompareStringResult(oldValues[ii - 1], newValues[jj], CompareStringResult.ActionType.NotChanged));
                ii--;

            }

            while (jj > 0)
            {
                var max = board[ii, jj];
                var min = board[ii, jj - 1];

                if (min != max)
                    stack.Push(new CompareStringResult(null, newValues[jj - 1], CompareStringResult.ActionType.Added));
                else
                    stack.Push(new CompareStringResult(oldValues[ii], newValues[jj - 1], CompareStringResult.ActionType.NotChanged));
                jj--;

            }

            return stack;
        }
    }
}