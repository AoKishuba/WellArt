using System.Diagnostics;

namespace WellArt
{
    public partial class InputForm : Form
    {
        private List<Color> colorList = [Color.White];
        private readonly List<RoundButton> wellButtonList = [];

        public InputForm()
        {
            InitializeComponent();
            int buttonDiameter = this.Width / 22;
            int startY = (int)Math.Ceiling(InstructionsTB.Height * 2m);
            int startX = (int)Math.Ceiling(ColorLB.Width * 1.5);
            InitializeGrid(buttonDiameter, buttonDiameter, startX, startY, 8, 12, 5, 5);

            // Initialize color-picking Checked ListBox
            ColorLB.Items.Add(Color.Red);
            ColorLB.Items.Add(Color.Green);
            ColorLB.Items.Add(Color.Blue);
            ColorLB.Items.Add(Color.Yellow);
            ColorLB.Items.Add(Color.Orange);
            ColorLB.Items.Add(Color.Purple);
        }

        /// <summary>
        /// Create grid of butttons for wells
        /// </summary>
        /// <param name="buttonWidth">Button width in pixels</param>
        /// <param name="buttonHeight">Button height in pixels</param>
        /// <param name="startX">Starting x coördinate</param>
        /// <param name="startY">Starting y coördinate</param>
        /// <param name="rowCount">Number of horizontal rows</param>
        /// <param name="columnCount">Number of vertical columns</param>
        /// <param name="xSpacing">Space between buttons horizontally, edge-edge, in pixels</param>
        /// <param name="ySpacing">Space between buttons vertically, edge-edge, in pixels</param>
        private void InitializeGrid(
            int buttonWidth,
            int buttonHeight,
            int startX,
            int startY,
            int rowCount,
            int columnCount,
            int xSpacing,
            int ySpacing)
        {
            // Create row and column labels
            for (int column = 0; column < columnCount; column++)
            {
                int xCoord = startX + (xSpacing + buttonWidth) * column + 3;
                Label columnLabel = new()
                {
                    Anchor = AnchorStyles.Top,
                    AutoSize = true,
                    Location = new Point(xCoord, startY - buttonHeight),
                    Text = (column + 1).ToString(),
                    TextAlign = ContentAlignment.MiddleCenter
                };
                Controls.Add(columnLabel);
            }

            for (int row = 0; row < rowCount; row++)
            {
                int yCoord = startY + (ySpacing + buttonHeight) * row + 3;
                char labelText = (char)('A' + row);
                Label columnLabel = new()
                {
                    Anchor = AnchorStyles.Left,
                    AutoSize = true,
                    Location = new Point(startX - buttonWidth, yCoord),
                    Text = labelText.ToString(),
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Controls.Add(columnLabel);
            }

            // Create buttons
            // Create dictionary for storing coördinates and color index
            for (int row = 0; row < rowCount; row++)
            {
                for (int column = 0; column < columnCount; column++)
                {
                    int xCoord = startX + (buttonWidth + xSpacing) * column;
                    int yCoord = startY + (buttonHeight + ySpacing) * row;

                    // Set button info
                    Well well = new(column, row);
                    RoundButton button = new()
                    {
                        Anchor = AnchorStyles.Top,
                        Width = buttonWidth,
                        Height = buttonHeight,
                        Location = new Point(xCoord, yCoord),
                        Tag = well,
                        BackColor = well.Color
                    };

                    // Add click handler function (defined below)
                    button.Click += Well_Button_Click;
                    Controls.Add(button);
                    wellButtonList.Add(button);
                }
            }
        }

        /// <summary>
        /// Changes color of well button when clicked
        /// </summary>
        private void Well_Button_Click(object? sender, EventArgs e)
        {
            RoundButton clickedButton = (RoundButton)sender;

            Well well = (Well)clickedButton.Tag;

            // Change to next color on list, or wrap around if at end of list
            int currentIndex = colorList.IndexOf(well.Color);
            int nextIndex = currentIndex >= colorList.Count - 1 ? 0 : currentIndex + 1;

            Color newColor = colorList[nextIndex];
            well.Color = newColor;
            clickedButton.BackColor = newColor;

            // Update button
            clickedButton.Tag = well;
        }

        /// <summary>
        /// Reset button colors to reflect updated color list
        /// </summary>
        private void ColorLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Update color list
            colorList = [Color.White];
            foreach (Color checkedColor in ColorLB.CheckedItems)
            {
                colorList.Add(checkedColor);
            }

            // Update button tag info
            foreach (RoundButton button in wellButtonList)
            {
                Well well = (Well)button.Tag;

                if (!colorList.Contains(button.BackColor))
                {
                    // Reset to white if current color is no longer on list
                    button.BackColor = Color.White;
                    well.Color = Color.White;
                }
                // Update tag
                button.Tag = well;
            }

            // Need at least as many pipettor settings as colors
            SettingsCountUD.Minimum = Math.Max(1, ColorLB.CheckedItems.Count);
        }


        private void GenerateProcedureButton_Click(object sender, EventArgs e)
        {
            colorList.Remove(Color.White);
            ProcedureGenerator.GenerateProcedure(colorList, wellButtonList, (int)SettingsCountUD.Value);
        }

        /// <summary>
        /// Creates popup message displaying filename and directory of procedure file
        /// </summary>
        /// <param name="fileName">Auto-generated filename of text file containing procedure</param>
        public static void ShowFilenameAndDirectory(string fileName)
        {
            // Initialize MessageBox variables
            string directoryName = Directory.GetCurrentDirectory();
            string message = $"File {fileName} has been saved at {directoryName}";
            string caption = "Procedure Created";
            MessageBoxButtons buttons = MessageBoxButtons.OK;

            // Dispay MessageBox
            MessageBox.Show(message, caption, buttons);
        }
    }
}
