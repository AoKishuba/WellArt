namespace WellArt
{
    public partial class Form1 : Form
    {
        private List<Color> colorList = [Color.White];
        private List<RoundButton> wellButtons = [];

        public Form1()
        {
            InitializeComponent();
            InitializeGrid(20, 20, 125, 60, 8, 12, 5, 5);

            // Initialize color-picking Checked ListBox
            ColorLB.Items.Add(Color.Red);
            ColorLB.Items.Add(Color.Green);
            ColorLB.Items.Add(Color.Blue);
            ColorLB.Items.Add(Color.Yellow);
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
                    Dictionary<string, int> tagDict = [];
                    tagDict.Add("x", column);
                    tagDict.Add("y", row);
                    tagDict.Add("colorIndex", 0);
                    RoundButton button = new()
                    {
                        Width = buttonWidth,
                        Height = buttonHeight,
                        Location = new Point(xCoord, yCoord),
                        Tag = tagDict,
                        BackColor = Color.White
                    };

                    // Add click handler function (defined below)
                    button.Click += Well_Button_Click;
                    Controls.Add(button);
                    wellButtons.Add(button);
                }
            }
        }

        /// <summary>
        /// Changes color of well button when clicked
        /// </summary>
        private void Well_Button_Click(object? sender, EventArgs e)
        {
            RoundButton clickedButton = (RoundButton)sender;

            // Change to next color on list, or wrap around if at end of list
            // Get values from Tag dictionary (which also contains coördinates)
            Dictionary<string, int> tagDict = (Dictionary<string, int>)clickedButton.Tag;

            int currentIndex = tagDict["colorIndex"];
            int nextIndex = currentIndex == colorList.Count - 1 ? 0 : currentIndex + 1;
            tagDict["colorIndex"] = nextIndex;
            clickedButton.BackColor = colorList[nextIndex];
            clickedButton.FlatAppearance.BorderColor = colorList[nextIndex];
        }

        private void ColorLB_SelectedIndexChanged(object sender, EventArgs e)
        {
            colorList = [Color.White];
            foreach (Color checkedColor in ColorLB.CheckedItems)
            {
                colorList.Add(checkedColor);
            }
        }
    }
}
