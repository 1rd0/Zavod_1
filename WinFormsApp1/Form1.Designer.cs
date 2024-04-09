namespace WinFormsApp1
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
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            CannyEdgeDetection = new Button();
            button2 = new Button();
            button3 = new Button();
            Main = new Button();
            textBox5 = new TextBox();
            textBox7 = new TextBox();
            textBox6 = new TextBox();
            textBox8 = new TextBox();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(25, 24);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(948, 371);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.Location = new Point(25, 404);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(948, 371);
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.TabIndex = 2;
            pictureBox2.TabStop = false;
            // 
            // CannyEdgeDetection
            // 
            CannyEdgeDetection.Location = new Point(1010, 24);
            CannyEdgeDetection.Name = "CannyEdgeDetection";
            CannyEdgeDetection.Size = new Size(89, 56);
            CannyEdgeDetection.TabIndex = 7;
            CannyEdgeDetection.Text = "Canny Edge Detection";
            CannyEdgeDetection.UseVisualStyleBackColor = true;
            CannyEdgeDetection.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(1010, 86);
            button2.Name = "button2";
            button2.Size = new Size(89, 49);
            button2.TabIndex = 7;
            button2.Text = "Hough Transform";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(1185, 24);
            button3.Name = "button3";
            button3.Size = new Size(89, 56);
            button3.TabIndex = 8;
            button3.Text = "Hough Transform MODE";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // Main
            // 
            Main.BackColor = SystemColors.ActiveCaption;
            Main.Location = new Point(1010, 141);
            Main.Name = "Main";
            Main.Size = new Size(89, 64);
            Main.TabIndex = 12;
            Main.Text = "Main";
            Main.UseVisualStyleBackColor = false;
            Main.Click += button6_Click;
            // 
            // textBox5
            // 
            textBox5.BackColor = SystemColors.InactiveCaption;
            textBox5.Location = new Point(89, 781);
            textBox5.Multiline = true;
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(884, 48);
            textBox5.TabIndex = 13;
            // 
            // textBox7
            // 
            textBox7.BackColor = Color.PaleGreen;
            textBox7.Location = new Point(521, 871);
            textBox7.Multiline = true;
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(452, 83);
            textBox7.TabIndex = 15;
            // 
            // textBox6
            // 
            textBox6.BackColor = Color.NavajoWhite;
            textBox6.Location = new Point(89, 871);
            textBox6.Multiline = true;
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(426, 83);
            textBox6.TabIndex = 14;
            // 
            // textBox8
            // 
            textBox8.BackColor = SystemColors.InactiveCaption;
            textBox8.Location = new Point(89, 835);
            textBox8.Multiline = true;
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(884, 30);
            textBox8.TabIndex = 16;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(45, 784);
            label1.Name = "label1";
            label1.Size = new Size(33, 15);
            label1.TabIndex = 17;
            label1.Text = "Угол";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(23, 835);
            label2.Name = "label2";
            label2.Size = new Size(60, 15);
            label2.TabIndex = 18;
            label2.Text = "Интервал";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1337, 975);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBox8);
            Controls.Add(textBox7);
            Controls.Add(textBox6);
            Controls.Add(textBox5);
            Controls.Add(Main);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(CannyEdgeDetection);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Button CannyEdgeDetection;
        private Button button2;
        private Button button3;
        private Button Main;
        private TextBox textBox5;
        private TextBox textBox7;
        private TextBox textBox6;
        private TextBox textBox8;
        private Label label1;
        private Label label2;
    }
}
