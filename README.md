# RSolver
A Rubik's Cube Solver in Unity

Built from the ground up, this project aims to solve a standard 3x3 Rubiks Cube using the [SingMaster Solution](http://www.linkedresources.com/teach/rubik/solution.php)

<p align="center"><a href="https://www.youtube.com/watch?v=dJ6OHkSNoWU" target="_blank"><img title="" src="https://github.com/stuartsoft/RSolver/raw/master/sample.gif"/></a></p>

This project also employs the use of intermediate DFS searching during stages 2, 3, and 4 to select the shortest overall order in which to perform a given step.
Additionally, it make use of iterative deepending DFS at the beginning of the solution to check for simple solutions to lightly scrambled cubes, and also checks for solutions to pre-determined patterns such as the Checkerboard pattern and "6 Dot" pattern.

**Hotkeys**

**F1** = Run checkerboard pattern

**F2** = Run 6 Dot pattern

**R** = Scramble the cube with 50 random rotations

**S** = Solve using the Singmaster's method

**D** = Solve using Singmaster's method using intermediate DFS, solution trimming, checks for pattern solutions, and iterative deepening DFS for simple scrambles up to 3 random turns.

**H** = Halts on screen animation
