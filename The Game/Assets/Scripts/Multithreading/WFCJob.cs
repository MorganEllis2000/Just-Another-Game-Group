using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;
using WaveFunctionCollaps;

public struct WFCJob : IJobParallelFor
{
    public OutputGrid outputGrid;

    public PatternManager patternManager;

    public int maxIterations;

    public void Execute(int index) {
        CreateOutputGrid(index);
    }

    public int[][] CreateOutputGrid(int index) {
        int iteration = index;
        while (iteration < this.maxIterations) {
            CoreSolver solver = new CoreSolver(this.outputGrid, this.patternManager);
            int innerIteration = 10000;
            while (!solver.CheckForConflics() && !solver.CheckIfSolved()) {
                Vector2Int position = solver.GetLowestEntropyCell();
                solver.CollapseCell(position);
                solver.Propagate();
                innerIteration--;
                if (innerIteration <= 0) {
                    //outputGrid.PrintResultsToConsol();
                    Debug.Log("Propagation taking too long");
                    return new int[0][];
                }
            }
            if (solver.CheckForConflics()) {

                Debug.Log("\nCOnflict occured. Iteration: " + iteration);
                iteration++;
                outputGrid.ResetAllPossibilities();
                solver = new CoreSolver(this.outputGrid, this.patternManager);
            } else {

                Debug.Log("Solved on " + iteration + " iteration");

                outputGrid.PrintResultsToConsol();
                break;
            }
        }
        if (iteration >= this.maxIterations) {
            Debug.Log("COuldn't solve in " + this.maxIterations + " iterations");
        }
        return outputGrid.GetSolvedOutputGrid();
    }

}



