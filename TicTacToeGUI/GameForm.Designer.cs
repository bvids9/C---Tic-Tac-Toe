namespace TicTacToeGUI
{
    partial class GameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_new_game = new System.Windows.Forms.Button();
            this.btn_saveGame = new System.Windows.Forms.Button();
            this.btn_loadGame = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnHuman = new System.Windows.Forms.RadioButton();
            this.btn_hardMode = new System.Windows.Forms.RadioButton();
            this.btn_easyMode = new System.Windows.Forms.RadioButton();
            this.btn_undo = new System.Windows.Forms.Button();
            this.btn_redo = new System.Windows.Forms.Button();
            this.btn_Resume = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.stateCaption = new System.Windows.Forms.TextBox();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(62, 46);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(450, 450);
            this.panel1.TabIndex = 0;
            // 
            // btn_new_game
            // 
            this.btn_new_game.Location = new System.Drawing.Point(534, 44);
            this.btn_new_game.Name = "btn_new_game";
            this.btn_new_game.Size = new System.Drawing.Size(215, 42);
            this.btn_new_game.TabIndex = 1;
            this.btn_new_game.Text = "New Game";
            this.btn_new_game.UseVisualStyleBackColor = true;
            this.btn_new_game.Click += new System.EventHandler(this.btn_newGame_Click);
            // 
            // btn_saveGame
            // 
            this.btn_saveGame.Location = new System.Drawing.Point(534, 92);
            this.btn_saveGame.Name = "btn_saveGame";
            this.btn_saveGame.Size = new System.Drawing.Size(215, 42);
            this.btn_saveGame.TabIndex = 2;
            this.btn_saveGame.Text = "Save Game";
            this.btn_saveGame.UseVisualStyleBackColor = true;
            this.btn_saveGame.Click += new System.EventHandler(this.btn_saveGame_Click);
            // 
            // btn_loadGame
            // 
            this.btn_loadGame.Location = new System.Drawing.Point(534, 140);
            this.btn_loadGame.Name = "btn_loadGame";
            this.btn_loadGame.Size = new System.Drawing.Size(215, 42);
            this.btn_loadGame.TabIndex = 3;
            this.btn_loadGame.Text = "Load Game";
            this.btn_loadGame.UseVisualStyleBackColor = true;
            this.btn_loadGame.Click += new System.EventHandler(this.btn_loadGame_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(521, 231);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(241, 32);
            this.label1.TabIndex = 5;
            this.label1.Text = "Computer Difficulty";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnHuman);
            this.panel2.Controls.Add(this.btn_hardMode);
            this.panel2.Controls.Add(this.btn_easyMode);
            this.panel2.Location = new System.Drawing.Point(518, 286);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(265, 50);
            this.panel2.TabIndex = 6;
            // 
            // btnHuman
            // 
            this.btnHuman.AutoSize = true;
            this.btnHuman.Location = new System.Drawing.Point(3, 16);
            this.btnHuman.Name = "btnHuman";
            this.btnHuman.Size = new System.Drawing.Size(65, 19);
            this.btnHuman.TabIndex = 2;
            this.btnHuman.Text = "Human";
            this.btnHuman.UseVisualStyleBackColor = true;
            this.btnHuman.CheckedChanged += new System.EventHandler(this.btnHuman_CheckedChanged);
            // 
            // btn_hardMode
            // 
            this.btn_hardMode.AutoSize = true;
            this.btn_hardMode.Location = new System.Drawing.Point(165, 16);
            this.btn_hardMode.Name = "btn_hardMode";
            this.btn_hardMode.Size = new System.Drawing.Size(85, 19);
            this.btn_hardMode.TabIndex = 1;
            this.btn_hardMode.Text = "Hard Mode";
            this.btn_hardMode.UseVisualStyleBackColor = true;
            this.btn_hardMode.CheckedChanged += new System.EventHandler(this.btn_hardMode_CheckedChanged);
            // 
            // btn_easyMode
            // 
            this.btn_easyMode.AutoSize = true;
            this.btn_easyMode.Checked = true;
            this.btn_easyMode.Location = new System.Drawing.Point(74, 16);
            this.btn_easyMode.Name = "btn_easyMode";
            this.btn_easyMode.Size = new System.Drawing.Size(82, 19);
            this.btn_easyMode.TabIndex = 0;
            this.btn_easyMode.TabStop = true;
            this.btn_easyMode.Text = "Easy Mode";
            this.btn_easyMode.UseVisualStyleBackColor = true;
            this.btn_easyMode.CheckedChanged += new System.EventHandler(this.btn_easyMode_CheckedChanged);
            // 
            // btn_undo
            // 
            this.btn_undo.Location = new System.Drawing.Point(534, 358);
            this.btn_undo.Name = "btn_undo";
            this.btn_undo.Size = new System.Drawing.Size(215, 42);
            this.btn_undo.TabIndex = 7;
            this.btn_undo.Text = "Undo Last Move";
            this.btn_undo.UseVisualStyleBackColor = true;
            this.btn_undo.Click += new System.EventHandler(this.btn_undo_Click);
            // 
            // btn_redo
            // 
            this.btn_redo.Location = new System.Drawing.Point(534, 406);
            this.btn_redo.Name = "btn_redo";
            this.btn_redo.Size = new System.Drawing.Size(215, 42);
            this.btn_redo.TabIndex = 8;
            this.btn_redo.Text = "Redo Last Move";
            this.btn_redo.UseVisualStyleBackColor = true;
            this.btn_redo.Click += new System.EventHandler(this.btn_redo_Click);
            // 
            // btn_Resume
            // 
            this.btn_Resume.Location = new System.Drawing.Point(534, 454);
            this.btn_Resume.Name = "btn_Resume";
            this.btn_Resume.Size = new System.Drawing.Size(215, 42);
            this.btn_Resume.TabIndex = 9;
            this.btn_Resume.Text = "Resume Game";
            this.btn_Resume.UseVisualStyleBackColor = true;
            this.btn_Resume.Click += new System.EventHandler(this.btn_Resume_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(49, -4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(236, 47);
            this.label2.TabIndex = 10;
            this.label2.Text = "Game Status:";
            // 
            // stateCaption
            // 
            this.stateCaption.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point);
            this.stateCaption.Location = new System.Drawing.Point(275, 4);
            this.stateCaption.Name = "stateCaption";
            this.stateCaption.Size = new System.Drawing.Size(237, 39);
            this.stateCaption.TabIndex = 11;
            this.stateCaption.Text = "Running";
            // 
            // GameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 522);
            this.Controls.Add(this.stateCaption);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btn_Resume);
            this.Controls.Add(this.btn_redo);
            this.Controls.Add(this.btn_undo);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_loadGame);
            this.Controls.Add(this.btn_saveGame);
            this.Controls.Add(this.btn_new_game);
            this.Controls.Add(this.panel1);
            this.Name = "GameForm";
            this.Text = "Tic Tac Toe - IFQ 563 - Object Oriented Design - Brent Vidler";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private Button btn_new_game;
        private Button btn_saveGame;
        private Button btn_loadGame;
        private Label label1;
        private Panel panel2;
        private RadioButton btn_hardMode;
        private RadioButton btn_easyMode;
        private Button btn_undo;
        private Button btn_redo;
        private Button btn_Resume;
        private Label label2;
        private TextBox stateCaption;
        private RadioButton btnHuman;
    }
}