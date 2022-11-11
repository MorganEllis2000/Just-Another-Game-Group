using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.Jobs;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Threading.Tasks;

namespace WaveFunctionCollaps
{
    
    public class WFCCore
    {
        public OutputGrid outputGrid;

        public PatternManager patternManager;

        public int maxIterations = 0;

        public WFCCore(int outputWidth, int outputHeight, PatternManager patternManager, int maxIterations)
        {
            this.outputGrid = new OutputGrid(outputWidth, outputHeight, patternManager.GetNumberOfPatterns());
            this.patternManager = patternManager;
            this.maxIterations = maxIterations;
        }

        public int[][] CreateOutputGrid()
        {
            //CreateOutputGridAsync();
            //return outputGrid.GetSolvedOutputGrid();
            int iteration = 0;


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

        public async void CreateOutputGridAsync() {

            int iteration = 0;
            var result = await Task.Run(() => {

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
                    Debug.Log("Couldn't solve in " + this.maxIterations + " iterations");
                }
                return outputGrid.GetSolvedOutputGrid();
            });
        }
    }    
}

