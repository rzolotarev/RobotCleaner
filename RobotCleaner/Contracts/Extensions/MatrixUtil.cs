using Contracts.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Extensions
{
    public static class MatrixUtil
    {
        public static MatrixConvertionResult ToPlaceStatuses(this string[,] sourceMatrix)
        {
            var rowCount = sourceMatrix.GetLength(0);
            var columnCount = sourceMatrix.GetLength(1);
            var destMatrix = new PlaceStatus[rowCount, columnCount];
            var result = new MatrixConvertionResult();

            for (int i = 0; i < rowCount; i++)
                for (int j = 0; j < columnCount; j++)
                {
                    var parsingResult = Enum.TryParse(sourceMatrix[i, j], out destMatrix[i, j]);
                    if (!parsingResult)
                    {
                        if (sourceMatrix[i, j] == "null")
                            destMatrix[i, j] = PlaceStatus.Null;
                        else
                            return new MatrixConvertionResult() { Matrix = destMatrix };
                    }
                        
                }

            return new MatrixConvertionResult() { Matrix = destMatrix, IsSucceed = true };
        }
    }
}
