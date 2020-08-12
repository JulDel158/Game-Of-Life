using GOL.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GOL
{
    public partial class Form1 : Form
    {
        // The universe array
        Cell[,] universe; // = new Cell[AxisX, AxisY];
        // Scratch pad array
        Cell[,] pad; // = new Cell[AxisX, AxisY];
        // The Timer class
        Timer timer = new Timer();

        public Form1()
        {
            InitializeComponent();
            //Reading settings
            graphicsPanel1.BackColor = Properties.Settings.Default.BackgroundColor;

            //calling my method for initializing/resizing universe
            SizeUniverse(Properties.Settings.Default.UniXAxis, Properties.Settings.Default.UniYAxis);
            // Setup the timer
            timer.Interval = Properties.Settings.Default.TimerMilliseconds; // milliseconds
            timer.Tick += Timer_Tick;
        }
        
        //Resize method
        private void SizeUniverse (int a, int b)
        {
            //initializing universe/pad with given data
            universe = new Cell[a, b];
            pad = new Cell[a, b];
            //Recalculating total cells
            Properties.Settings.Default.CellTotalValue = a * b;
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    //populating the universe/pad
                    universe[x, y] = new Cell();
                    pad[x, y] = new Cell();
                }
            }
        }
        //Method for counting active neighbors in a finite space
        private int CountNeighborsF (int x, int y)
        {
            //Changing string for HUD
            Properties.Settings.Default.UniverseString = Properties.Resources.FiniteS;
            int count = 0;
            //size of x dimmension
            int xL = universe.GetLength(0);
            //size of y dimmension
            int yL = universe.GetLength(1);
            // y range of cells next to our main cell
            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                //x range of cells next to out main cell
                for ( int xOffset = -1; xOffset <= 1; xOffset++)
                {
                    //calculating the coordinates for the cells around main cell
                    int xC = x + xOffset;
                    int yC = y + yOffset;

                    //preventing cell from counting itself
                    //finite count, considering anything outside as dead
                    if (xOffset == 0 && yOffset == 0 || (xC < 0 || yC < 0 || xC >= xL || yC >= yL))
                        continue;
                    //adding to count if the cell being checked is active
                    if (universe[xC, yC].active == true)
                        count++;
                }
            }
            return count;
        }
        //Method for counting active neighbors in a toroidal space
        private int CountNeighborsT (int x, int y)
        {
            //Changing my string for HUD
            Properties.Settings.Default.UniverseString = Properties.Resources.ToroidalS;
            int count = 0;
            int xL = universe.GetLength(0);
            int yL = universe.GetLength(1);
            //y range of cells next to main cell
            for (int yOffset = -1; yOffset <= 1; yOffset++)
            {
                //x range of cells next to main cell
                for (int xOffset = -1; xOffset <= 1; xOffset++)
                {
                    //coordinates for cells around main cell
                    int xC = x + xOffset;
                    int yC = y + yOffset;
                    //excluding our main cell from the count
                    if (xOffset == 0 && yOffset == 0)
                        continue;
                    //checking to see if x is less than 0 or more than our univerce, can't be both. Adjusting to count the opposite side
                    if (xC < 0)
                        xC = xL - 1;
                    else if (xC >= xL)
                        xC = 0;
                    //checking to see if y is less than 0 or more than our univerce, can't be both
                    if (yC < 0)
                        yC = yL - 1;
                    else if (yC >= yL)
                        yC = 0;
                    //adding to count if neighbor is active
                    if (universe[xC, yC].active == true)
                        count++;
                }
            }
            return count;
        }
        //Method for randomizing the generation
        private void GenerateMap(int Seed)
        {
            //passing seed into random
            Random rng = new Random(Seed);
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    //Activating cells based on outcome
                    if (rng.Next(0, 2) == 0)
                    {
                        universe[x, y].active = true;
                    }
                    else
                        universe[x, y].active = false;
                }
            }
        }
        //Random map with time based seed
        private void RandomMap()
        {
            //storing seed from time
            Properties.Settings.Default.SeedValue = (int)DateTime.Now.Ticks;
            //passing seed into random
            Random rng = new Random(Properties.Settings.Default.SeedValue);
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    //Activating cells based on outcome
                    if (rng.Next(0, 2) == 0)
                    {
                        universe[x, y].active = true;
                    }
                    else
                        universe[x, y].active = false;
                }
            }
        }
        // Calculate the next generation of cells
        private void NextGeneration()
        {
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    //if cell is on but outside of boundaries it will die next cycle
                    if (universe[x, y].active == true && (universe[x, y].nCount < 2 || universe[x, y].nCount > 3))
                        pad[x, y].active = false;
                    //if cell is on and withim boundaries it will remain alive next cycle
                    else if (universe[x, y].active == true && (universe[x, y].nCount == 2 || universe[x, y].nCount == 3))
                        pad[x, y].active = true;
                    //if cell is off but has a count of 3 it will be turned on next cycle
                    else if (universe[x, y].active == false && universe[x, y].nCount == 3)
                        pad[x, y].active = true;
                    //copying from the pad into the universe
                    universe[x, y].active = pad[x, y].active;
                }
            }
            // Increment generation count
            Properties.Settings.Default.GenerationValue++;

            // Update status strip generations
            toolStripStatusLabelGenerations.Text = "Generations = " + Properties.Settings.Default.GenerationValue.ToString();
            //toolStripStatusLabelCellCount.Text = "Active Cells = " + cCount.ToString();
        }
        // The event called by the timer every Interval milliseconds.
        private void Timer_Tick(object sender, EventArgs e)
        {
            timer.Interval = Properties.Settings.Default.TimerMilliseconds;
            NextGeneration();
            //repainting
            graphicsPanel1.Invalidate();
        }
        //Paint event
        private void graphicsPanel1_Paint(object sender, PaintEventArgs e)
        {
            //resetting count
            Properties.Settings.Default.ActiveCellCount = 0;
            // Calculate the width and height of each cell in pixels
            // CELL WIDTH = WINDOW WIDTH / NUMBER OF CELLS IN X
            float cellWidth = (float)graphicsPanel1.ClientSize.Width / (float)universe.GetLength(0);
            // CELL HEIGHT = WINDOW HEIGHT / NUMBER OF CELLS IN Y
            float cellHeight = (float)graphicsPanel1.ClientSize.Height / (float)universe.GetLength(1);

            // A Pen for drawing the grid lines (color, width)
            Pen gridPen = new Pen(Properties.Settings.Default.GridColor, 1);
            //Brush for HUD
            Brush HUDBrush = new SolidBrush(Properties.Settings.Default.HUDColor);
            // A Brush for filling living cells interiors (color)
            Brush cellBrush = new SolidBrush(Properties.Settings.Default.ActiveCellColor);
            //color for numbers
            Color numColor = Color.Red;
            //brush for numbers
            Brush numBrush = new SolidBrush(numColor);
            //font for numbers/ cell count
            Font numFont = new Font(new FontFamily("Arial"), 12f, FontStyle.Bold, GraphicsUnit.Point);
            //format for number
            StringFormat numFormat = new StringFormat();
            //Alignment of format
            numFormat.Alignment = StringAlignment.Center;
            numFormat.LineAlignment = StringAlignment.Center;
            //font for HUD
            Font HUDFont = new Font(new FontFamily("Arial"), 15f, FontStyle.Bold, GraphicsUnit.Point);
            //format for HUD
            StringFormat HUDFormat = new StringFormat();
            //Alignment of hud format
            HUDFormat.Alignment = StringAlignment.Near;
            HUDFormat.LineAlignment = StringAlignment.Near;


            // Iterate through the universe in the y, top to bottom
            for (int y = 0; y < universe.GetLength(1); y++)
            {
                // Iterate through the universe in the x, left to right
                for (int x = 0; x < universe.GetLength(0); x++)
                {
                    // A rectangle to represent each cell in pixels
                    RectangleF cellRect = RectangleF.Empty;
                    cellRect.X = x * cellWidth;
                    cellRect.Y = y * cellHeight;
                    cellRect.Width = cellWidth;
                    cellRect.Height = cellHeight;

                    // Fill the cell with a brush if alive and counting active cell
                    if (universe[x, y].active)
                    {
                        //adding to total active count
                        Properties.Settings.Default.ActiveCellCount++;
                        //filling rectangle / cell
                        e.Graphics.FillRectangle(cellBrush, cellRect);
                    }

                    //updating count of cells
                    if (Properties.Settings.Default.FiniteBorder == false)
                        universe[x, y].nCount = CountNeighborsT(x, y);
                    else
                        universe[x, y].nCount = CountNeighborsF(x, y);
                    //Drawing number of active neighbors if said number is more than 0 per cell
                    if (Properties.Settings.Default.DrawCellCount && universe[x, y].nCount > 0)
                        e.Graphics.DrawString(universe[x, y].nCount.ToString(), numFont, numBrush, cellRect, numFormat);
                    //Checking if we need to draw grid
                    if (Properties.Settings.Default.DrawGrid)
                    {
                        //Drawing vertical lines
                        e.Graphics.DrawLine(gridPen, (float)(x * cellWidth), 0, (float)(x * cellWidth), (float)graphicsPanel1.ClientSize.Height);
                        //drawing horizontal lines
                        e.Graphics.DrawLine(gridPen, 0, (float)(y * cellHeight), (float)graphicsPanel1.ClientSize.Width, (float)(y * cellHeight));
                    }
                }
            }
            //Drawing HUD
            if (Properties.Settings.Default.DrawHUD)
            {
                //string to hold hud info
                Properties.Settings.Default.HUDInfo = Resources.GenS + Properties.Settings.Default.GenerationValue.ToString() + "\n"
                    + Resources.CountS + Properties.Settings.Default.ActiveCellCount.ToString() + "\n"
                    + Resources.BoundS + Properties.Settings.Default.UniverseString + "\n"
                    + Resources.GridS + Properties.Settings.Default.UniXAxis.ToString()
                    + Resources.Grid2S + Properties.Settings.Default.UniYAxis.ToString() + "}";
            
                //drawing heads up display
                e.Graphics.DrawString(Properties.Settings.Default.HUDInfo, HUDFont, HUDBrush, new PointF(0, 0), HUDFormat);
            }
            //Updating Generations, needed for settings
            toolStripStatusLabelGenerations.Text = "Generations = " + Properties.Settings.Default.GenerationValue.ToString();
            //Updating Seed, needed for settings
            toolStripSeedLabel.Text = "Seed: " + Properties.Settings.Default.SeedValue.ToString();
            //Displaying updated cell count
            toolStripStatusLabelCellCount.Text = "Active Cells = " + Properties.Settings.Default.ActiveCellCount.ToString() + "/" + Properties.Settings.Default.CellTotalValue.ToString();
            // Cleaning up pens and brushes
            gridPen.Dispose();
            cellBrush.Dispose();
            numBrush.Dispose();
            HUDBrush.Dispose();
        }
        //toogle for cells
        private void graphicsPanel1_MouseClick(object sender, MouseEventArgs e)
        {
            // If the left mouse button was clicked
            if (e.Button == MouseButtons.Left)
            {
                // Calculate the width and height of each cell in pixels
                float cellWidth = (float)graphicsPanel1.ClientSize.Width / (float)universe.GetLength(0);
                float cellHeight = (float)graphicsPanel1.ClientSize.Height / (float)universe.GetLength(1);

                // Calculate the cell that was clicked in
                // CELL X = MOUSE X / CELL WIDTH
                int x = (int)(e.X / cellWidth);
                // CELL Y = MOUSE Y / CELL HEIGHT
                int y = (int)(e.Y / cellHeight);

                // Toggle the cell's state
                universe[x, y].active = !universe[x, y].active;

                // Tell Windows you need to repaint
                graphicsPanel1.Invalidate();
            }
        }
        //new button 
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //stopping the timer
            timer.Stop();
            //assigning default value to axis
            Properties.Settings.Default.UniXAxis = 30;
            Properties.Settings.Default.UniYAxis = 30;
            //resizing universe to default
            SizeUniverse(Properties.Settings.Default.UniXAxis, Properties.Settings.Default.UniYAxis);
            
            //assigning default value to milliseconds
            Properties.Settings.Default.TimerMilliseconds = 100;
            //resetting seed
            Properties.Settings.Default.SeedValue = 0;
            //restarting generation count
            Properties.Settings.Default.GenerationValue = 0;
            //Resetting active cell count done by paint

            //repainting after turning off all cells
            graphicsPanel1.Invalidate();
        }
        //closing the program
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //starting the timer event
        private void Start_Click(object sender, EventArgs e)
        {
            //starting the timer
            timer.Start();
        }
        //Stopping the timer event
        private void Pause_Click(object sender, EventArgs e)
        {
            //stopping the timer
            timer.Stop();
        }
        //next generation event
        private void Next_Click(object sender, EventArgs e)
        {
            NextGeneration();
            graphicsPanel1.Invalidate();
        }
        //Toggles finite universe (default)
        private void finiteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.FiniteBorder = true;
           // finite = true;
            graphicsPanel1.Invalidate();
        }
        //Toggles toroidal universe
        private void toroidalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.FiniteBorder = false;
           // finite = false;
            graphicsPanel1.Invalidate();
        }
        //Generates random map
        private void randomizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //calling my default random method
            RandomMap();
            //updating seed display
            toolStripSeedLabel.Text = "Seed: " + Properties.Settings.Default.SeedValue.ToString();
            //repaiting after the generation is complete
            graphicsPanel1.Invalidate();
        }
        //Generate map from input seed
        private void seedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //creating instance of my modal dialog box
            SeedMD seedbox = new SeedMD();
            //passing the current seed value to numeric up down box  to display the current seed
            seedbox.Seed = Properties.Settings.Default.SeedValue;
            //shows dialog box and checks if ok was pressed
            if (DialogResult.OK == seedbox.ShowDialog())
            {
                //assing input value into seed
                Properties.Settings.Default.SeedValue = (int)seedbox.Seed;
                //generate map using seed
                GenerateMap(Properties.Settings.Default.SeedValue);
            }
            //updating seed display
            toolStripSeedLabel.Text = "Seed: " + Properties.Settings.Default.SeedValue.ToString();
            //repaiting after the generation is complete
            graphicsPanel1.Invalidate();
        }
        //Generate map from currrent seed
        private void currentSeedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GenerateMap(Properties.Settings.Default.SeedValue);
            //updating seed display
            toolStripSeedLabel.Text = "Seed: " + Properties.Settings.Default.SeedValue.ToString();
            //repaiting after the generation is complete
            graphicsPanel1.Invalidate();
        }
        //Resizing universe based on user input
        private void resizeUniverseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //creating instance of our grid modal box
            GridMD grid = new GridMD();
            //setting the value inside the numupdown to current value
            grid.xSize = Properties.Settings.Default.UniXAxis;
            grid.ySize = Properties.Settings.Default.UniYAxis;
            //showing box/ handling ok
            if (DialogResult.OK == grid.ShowDialog())
            {
                //setting out axis variables = to user input
                Properties.Settings.Default.UniXAxis = (int)grid.xSize;
                Properties.Settings.Default.UniYAxis = (int)grid.ySize;
                //resizing universe
                SizeUniverse(Properties.Settings.Default.UniXAxis, Properties.Settings.Default.UniYAxis);
            }
            //asking for a repainting
            graphicsPanel1.Invalidate();
        }
        //Modal box for changing the milliseconds of our timer
        private void timerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //creating instance of our timerMD modal box and passing parameter to my overloaded constructor
            TimerMD timerBox = new TimerMD(Properties.Settings.Default.TimerMilliseconds);
            //shows box and executes code if ok is pressed
            if (DialogResult.OK == timerBox.ShowDialog())
            {
                //changes the milliseconds to user input
                Properties.Settings.Default.TimerMilliseconds = (int)timerBox.Milliseconds;
            }
        }
        //Toggles Grid view
        private void gridToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.DrawGrid = !Properties.Settings.Default.DrawGrid;
            //changes grid boolean to it's opposite value
            //dGrid = !dGrid;
            //asking windows to repaint panel
            graphicsPanel1.Invalidate();
        }
        //Toggles neigbor count view
        private void neighborCountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //setting count to it's opposite value
            Properties.Settings.Default.DrawCellCount = !Properties.Settings.Default.DrawCellCount;
            //asking window to repaint panel
            graphicsPanel1.Invalidate();
        }
        //Toggles the HUD
        private void hUDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Changing bool for hud display
            Properties.Settings.Default.DrawHUD = !Properties.Settings.Default.DrawHUD;
            //asking window to repaint
            graphicsPanel1.Invalidate();
        }
        //Changes the background color
        private void backgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //creating instance of the color dialog box
            ColorDialog BackGrColor = new ColorDialog();
            //passing the current color to display it to the user
            BackGrColor.Color = graphicsPanel1.BackColor;
            //displaying the color box and checking if they pressed ok. 
            if (DialogResult.OK == BackGrColor.ShowDialog())
            {
                //Assigning the value the user picked to the panel
                graphicsPanel1.BackColor = BackGrColor.Color;
            }
            //asking for repaint 
            graphicsPanel1.Invalidate();
        }
        //Changes the grid color
        private void gridToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Creating instance of color box
            ColorDialog GridColor = new ColorDialog();
            //passing the current color to color box
            GridColor.Color = Properties.Settings.Default.GridColor;
            //displays color box and checks ok button action
            if (DialogResult.OK == GridColor.ShowDialog())
            {
                //changing the grid color to user input
                Properties.Settings.Default.GridColor = GridColor.Color;
            }
            //asking for repaint
            graphicsPanel1.Invalidate();
        }
        //changes color of cells
        private void cellsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //creating color box
            ColorDialog CellsColor = new ColorDialog();
            //assigning the current color of the cells to display on box
            CellsColor.Color = Properties.Settings.Default.ActiveCellColor;
            //displaying color box and handling ok
            if (DialogResult.OK == CellsColor.ShowDialog())
            {
                //assigning user input to cell color
                Properties.Settings.Default.ActiveCellColor = CellsColor.Color;
            }
            //Asking window to repaint
            graphicsPanel1.Invalidate();
        }
        //Changes color of HUD
        private void hUDToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //creating color box
            ColorDialog HUDColorB = new ColorDialog();
            //passing our current color to box
            HUDColorB.Color = Properties.Settings.Default.HUDColor;
            //displaying color box and handling ok button
            if (DialogResult.OK == HUDColorB.ShowDialog())
            {
                //assigning input to our hud color
                Properties.Settings.Default.HUDColor = HUDColorB.Color;
            }
            //asking window to repaint
            graphicsPanel1.Invalidate();
        }
        //executes when closing program
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //assigns current background color to setting
            Properties.Settings.Default.BackgroundColor = graphicsPanel1.BackColor;
            //saves settings
            Properties.Settings.Default.Save();
        }
        //Reloading current settings
        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //reloading settings
            Properties.Settings.Default.Reload();
            //assings reloaded color to background color
            graphicsPanel1.BackColor = Properties.Settings.Default.BackgroundColor;
            //resizing universe to reloaded value
            SizeUniverse(Properties.Settings.Default.UniXAxis, Properties.Settings.Default.UniYAxis);
            //asking window to redraw
            graphicsPanel1.Invalidate();
        }
        //Resetting to default settings
        private void resetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //resets settings
            Properties.Settings.Default.Reset();
            //assigns default color to our background
            graphicsPanel1.BackColor = Properties.Settings.Default.BackgroundColor;
            //resizing universe to default value
            SizeUniverse(Properties.Settings.Default.UniXAxis, Properties.Settings.Default.UniYAxis);
            //asking window to redraw
            graphicsPanel1.Invalidate();
        }
    }
}
