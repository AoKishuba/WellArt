namespace WellArt
{
    partial class Form1
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
            ColorLB = new CheckedListBox();
            toolTip1 = new ToolTip(components);
            P2StepCountUD = new NumericUpDown();
            P20StepCountUD = new NumericUpDown();
            GenerateProcedureButton = new Button();
            P2StepCountLabel = new Label();
            P20StepCountLabel = new Label();
            InstructionsTB = new Label();
            ((System.ComponentModel.ISupportInitialize)P2StepCountUD).BeginInit();
            ((System.ComponentModel.ISupportInitialize)P20StepCountUD).BeginInit();
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
            // P2StepCountUD
            // 
            P2StepCountUD.Location = new Point(48, 144);
            P2StepCountUD.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            P2StepCountUD.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            P2StepCountUD.Name = "P2StepCountUD";
            P2StepCountUD.Size = new Size(39, 23);
            P2StepCountUD.TabIndex = 1;
            P2StepCountUD.TextAlign = HorizontalAlignment.Right;
            toolTip1.SetToolTip(P2StepCountUD, "How many times should the student need to set the P2 volume?\r\nThe value entered will be split evenly between the colors.");
            P2StepCountUD.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // P20StepCountUD
            // 
            P20StepCountUD.Location = new Point(48, 173);
            P20StepCountUD.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            P20StepCountUD.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            P20StepCountUD.Name = "P20StepCountUD";
            P20StepCountUD.Size = new Size(39, 23);
            P20StepCountUD.TabIndex = 2;
            P20StepCountUD.TextAlign = HorizontalAlignment.Right;
            toolTip1.SetToolTip(P20StepCountUD, "How many times should the student need to set the P20 volume?\r\nThe value entered will be split evenly between the colors.");
            P20StepCountUD.Value = new decimal(new int[] { 10, 0, 0, 0 });
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
            // 
            // P2StepCountLabel
            // 
            P2StepCountLabel.AutoSize = true;
            P2StepCountLabel.Location = new Point(12, 148);
            P2StepCountLabel.Name = "P2StepCountLabel";
            P2StepCountLabel.Size = new Size(20, 15);
            P2StepCountLabel.TabIndex = 3;
            P2StepCountLabel.Text = "P2";
            // 
            // P20StepCountLabel
            // 
            P20StepCountLabel.AutoSize = true;
            P20StepCountLabel.Location = new Point(12, 177);
            P20StepCountLabel.Name = "P20StepCountLabel";
            P20StepCountLabel.Size = new Size(26, 15);
            P20StepCountLabel.TabIndex = 4;
            P20StepCountLabel.Text = "P20";
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
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(InstructionsTB);
            Controls.Add(GenerateProcedureButton);
            Controls.Add(P20StepCountLabel);
            Controls.Add(P2StepCountLabel);
            Controls.Add(P20StepCountUD);
            Controls.Add(P2StepCountUD);
            Controls.Add(ColorLB);
            Name = "Form1";
            Text = "WellArt";
            ((System.ComponentModel.ISupportInitialize)P2StepCountUD).EndInit();
            ((System.ComponentModel.ISupportInitialize)P20StepCountUD).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckedListBox ColorLB;
        private ToolTip toolTip1;
        private NumericUpDown P2StepCountUD;
        private NumericUpDown P20StepCountUD;
        private Label P2StepCountLabel;
        private Label P20StepCountLabel;
        private Button GenerateProcedureButton;
        private Label InstructionsTB;
    }
}
