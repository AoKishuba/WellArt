namespace WellArt
{
    partial class InputForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputForm));
            ColorLB = new CheckedListBox();
            toolTip1 = new ToolTip(components);
            GenerateProcedureButton = new Button();
            SettingsCountUD = new NumericUpDown();
            SettingsCountLabel = new Label();
            InstructionsTB = new Label();
            ((System.ComponentModel.ISupportInitialize)SettingsCountUD).BeginInit();
            SuspendLayout();
            // 
            // ColorLB
            // 
            ColorLB.CheckOnClick = true;
            ColorLB.FormattingEnabled = true;
            ColorLB.Location = new Point(12, 44);
            ColorLB.Name = "ColorLB";
            ColorLB.Size = new Size(75, 94);
            ColorLB.TabIndex = 0;
            toolTip1.SetToolTip(ColorLB, "Check a color to include it in the design");
            ColorLB.SelectedIndexChanged += ColorLB_SelectedIndexChanged;
            // 
            // GenerateProcedureButton
            // 
            GenerateProcedureButton.Location = new Point(12, 202);
            GenerateProcedureButton.Name = "GenerateProcedureButton";
            GenerateProcedureButton.Size = new Size(75, 50);
            GenerateProcedureButton.TabIndex = 5;
            GenerateProcedureButton.Text = "Generate Procedure";
            toolTip1.SetToolTip(GenerateProcedureButton, "Click to create a pipetting exercise procedure.\r\nThe program will automatically generate volumes and coördinates.\r\n\r\nA popup will display the file location when the process is finished.");
            GenerateProcedureButton.UseVisualStyleBackColor = true;
            GenerateProcedureButton.Click += GenerateProcedureButton_Click;
            // 
            // SettingsCountUD
            // 
            SettingsCountUD.Location = new Point(12, 163);
            SettingsCountUD.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            SettingsCountUD.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            SettingsCountUD.Name = "SettingsCountUD";
            SettingsCountUD.Size = new Size(75, 23);
            SettingsCountUD.TabIndex = 7;
            SettingsCountUD.TextAlign = HorizontalAlignment.Right;
            toolTip1.SetToolTip(SettingsCountUD, resources.GetString("SettingsCountUD.ToolTip"));
            SettingsCountUD.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // SettingsCountLabel
            // 
            SettingsCountLabel.AutoSize = true;
            SettingsCountLabel.Location = new Point(12, 145);
            SettingsCountLabel.Name = "SettingsCountLabel";
            SettingsCountLabel.Size = new Size(59, 15);
            SettingsCountLabel.TabIndex = 3;
            SettingsCountLabel.Text = "# Settings";
            // 
            // InstructionsTB
            // 
            InstructionsTB.AutoSize = true;
            InstructionsTB.Location = new Point(12, 9);
            InstructionsTB.Name = "InstructionsTB";
            InstructionsTB.Size = new Size(377, 30);
            InstructionsTB.TabIndex = 6;
            InstructionsTB.Text = "Instructions: Select desired colors, then click well buttons to set colors.\r\nWhen finished, click \"Generate Procedure\".";
            // 
            // InputForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(432, 262);
            Controls.Add(SettingsCountUD);
            Controls.Add(InstructionsTB);
            Controls.Add(GenerateProcedureButton);
            Controls.Add(SettingsCountLabel);
            Controls.Add(ColorLB);
            Name = "InputForm";
            Text = "WellArt";
            ((System.ComponentModel.ISupportInitialize)SettingsCountUD).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckedListBox ColorLB;
        private ToolTip toolTip1;
        private Label SettingsCountLabel;
        private Button GenerateProcedureButton;
        private Label InstructionsTB;
        private NumericUpDown SettingsCountUD;
    }
}
