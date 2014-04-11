using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Timers;
using System.Diagnostics;




namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        static int img_select=0;
        Boolean switch1;
        System.Windows.Forms.Timer t1 = new System.Windows.Forms.Timer();
        int player1 = 0;
        int player2 = 0;
        int row = 10,col=10;
        int number_of_player = 0;
        int turn = 1;
        int pixel_multiplier = 65;
        Image[] imagearray= new Image[6];
        
        System.Media.SoundPlayer backmusic = new System.Media.SoundPlayer(Properties.Resources.Background_music_0);
        System.Media.SoundPlayer buttonclick = new System.Media.SoundPlayer(Properties.Resources.button_click);
        System.Media.SoundPlayer diceroll = new System.Media.SoundPlayer(Properties.Resources.Dice_movement_and_rolling);
        System.Media.SoundPlayer areyousure = new System.Media.SoundPlayer(Properties.Resources.Are_you_sure);  
        System.Media.SoundPlayer elseclick = new System.Media.SoundPlayer(Properties.Resources.chord);
        System.Media.SoundPlayer textboxtypesound = new System.Media.SoundPlayer(Properties.Resources.ding);
        System.Media.SoundPlayer ladderup = new System.Media.SoundPlayer(Properties.Resources.tada);
        public struct cell
        {
            public int x_cord;
            public int y_cord;

            public cell(int x, int y)
            {
                x_cord = x;
                y_cord = y;
            }
        }

        cell[,] loc_arr=new cell[10,10];
        
        
        
        
        
        public Form1()
        {
            
            InitializeComponent();
            reset();
            pictureBox3.BorderStyle = BorderStyle.FixedSingle;
            pictureBox4.BorderStyle = BorderStyle.FixedSingle;
            pictureBox7.BorderStyle = BorderStyle.FixedSingle;
            pictureBox8.BorderStyle = BorderStyle.FixedSingle;
            pictureBox8.Location = new Point(690, 321);
            pictureBox7.Location = new Point(691, 151);
            backmusic.PlayLooping();
            
            
            
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)  
        {
            //===============Image loading area=======================//

            buttonclick.Play();
            pictureBox8.Location = new Point(690, 321);
            pictureBox7.Location = new Point(691, 151);
            if (textBox1.Enabled == true || textBox2.Enabled == true) { textBox1.Enabled = false; textBox2.Enabled = false; };
            imagearray[0] = new Bitmap(Properties.Resources._1);
            imagearray[1] = new Bitmap(Properties.Resources._2);
            imagearray[2] = new Bitmap(Properties.Resources._3);
            imagearray[3] = new Bitmap(Properties.Resources._4);
            imagearray[4] = new Bitmap(Properties.Resources._5);
            imagearray[5] = new Bitmap(Properties.Resources._6);
  
            //---------------------Location array of Player 1------------//
            for (int i = 0, flag = 0, level = 0, x1 = 5, y1 = 585; i < row; i++)
                for (int j = 0; j < col;j++ ) {
                    loc_arr[i, j] = new cell(x1,y1);
                    if (flag == 0 && level == 0)
                    {
                        x1 += pixel_multiplier;
                        if (x1 == 590) { flag = 1; level = 1; }
                    }
                    else if(flag==1 && level==1){
                        y1 -= pixel_multiplier;
                        flag = 0;
                    }
                    else if(flag==0 && level==1){
                        x1 -= pixel_multiplier;
                        if (x1 == 5) {
                            flag = 1;
                            level = 0;
                        }
                    }

                    else if(flag==1 && level==0){
                        y1 -= pixel_multiplier;
                        flag = 0;
                        level = 0;
                    }

                    
                }


            
         //   MessageBox.Show(loc_arr[8,5].x_cord +","+loc_arr[8,5].y_cord);
            
                //  button1.Enabled = false;
                radioButton1.Enabled = true;
            radioButton2.Enabled = true;

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)   //---------radiobutton 1-------------//
        {
           //selectclick.Play();
            switch1 = true;
            textBox1.Enabled = true;
            textBox2.Enabled = false;
            button4.Enabled = true;

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)   //---------radiobutton 2-------------//
        {
            // selectclick.Play();
            switch1 = false;
            if (textBox1.Enabled == false || textBox2.Enabled == false) { textBox1.Enabled = true; textBox2.Enabled = true; }
            textBox1.Enabled = true;
            textBox2.Enabled = true;
            button4.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            buttonclick.Play();
            label1.Visible = true;
            button2.Visible = true;
            button3.Visible = true;
            checkBox1.Visible = true;
            pictureBox2.Visible = true;
            pictureBox2.Image = null;

            if (switch1 == true && textBox1.Text == string.Empty) { MessageBox.Show("Please enter player name"); }
            else if (switch1 == false && (textBox1.Text == string.Empty || textBox2.Text == string.Empty)) { MessageBox.Show("Pleaase enter players names"); }
            else
            {
                Random rnd = new Random();
                turn = rnd.Next(1,3);
                if (switch1 == true)
                {
                    pictureBox4.Visible = true; 
                    pictureBox7.Visible = true; 
                    label3.Text = textBox1.Text; 
                    label2.Text = textBox1.Text;
                    number_of_player = 1;
                    turn = 1;
                }
                else { 
                    pictureBox3.Visible = true; 
                    pictureBox4.Visible = true;
                    pictureBox7.Visible = true;
                    pictureBox8.Visible = true;
                    if (turn == 1) { label2.Text = textBox1.Text; turn = 1; }
                    else { label2.Text = textBox2.Text; turn = 2; }
                    label3.Text = textBox1.Text; 
                    label4.Text = textBox2.Text;
                    number_of_player = 2;
                }
                button4.Enabled = false;
                button1.Enabled = false;
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                button3.Enabled = true;
                button2.Enabled = true;
                radioButton1.Enabled = false;
                radioButton2.Enabled = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            areyousure.Play();
            if(DialogResult.Yes==MessageBox.Show("Are you sure, you want to quit ?","",MessageBoxButtons.YesNo)){
               // quitmusic.Play();
                button1.Enabled = true;
                reset(); 
            
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = null;
            diceroll.Play();
            int counter = 0;
            button2.Enabled = false;
            Boolean dice_switch=false;
            label9.Text = "";
            
            Stopwatch stw = new Stopwatch();
           // t1.Interval = 500;
            stw.Start();
          //  t1.Start();
           
         
           
            
            while(stw.ElapsedMilliseconds<=2100){
                //MessageBox.Show("helk");
              //  pictureBox2.Image=imagearray[counter];
               
            }
            stw.Stop();
          //  t1.Stop();
            
            dice_switch = true;
            if(dice_switch==true){
                button2.Enabled = true;
            int a = 0;
            Random rnd1 = new Random();
            a = rnd1.Next(1, 7);
                switch(a){
                    case 1:
                        pictureBox2.Image=imagearray[0];
                        break;
                    case 2:
                        pictureBox2.Image = imagearray[1];
                        break;
                    case 3:
                        pictureBox2.Image = imagearray[2];
                        break;
                    case 4:
                        pictureBox2.Image = imagearray[3];
                        break;
                    case 5:
                        pictureBox2.Image = imagearray[4];
                        break;
                    case 6:
                        pictureBox2.Image = imagearray[5];
                        break;
                
                }
            if (turn == 1) {
                
                
                  
                
                player1 += a;
                if (player1 == 8) { player1 = 31; label9.Text = "Great work! You climbed up the ladder"; ladderup.Play(); }
                else if (player1 == 15) { player1 = 97; label9.Text = "Great work! You climbed up the ladder"; ladderup.Play(); }
                else if (player1 == 42) { player1 = 81; label9.Text = "Great work! You climbed up the ladder"; ladderup.Play(); }
                else if (player1 == 66) { player1 = 87; label9.Text = "Great work! You climbed up the ladder"; ladderup.Play(); }
                else if (player1 == 24) { player1 = 1; label9.Text = "Oops! You are eaten by snake"; elseclick.Play(); }
                else if (player1 == 55) { player1 = 13; label9.Text = "Oops! You are eaten by snake"; elseclick.Play(); }
                else if (player1 == 71) { player1 = 29; label9.Text = "Oops! You are eaten by snake"; elseclick.Play(); }
                else if (player1 == 88) { player1 = 67; label9.Text = "Oops! You are eaten by snake"; elseclick.Play(); }
                else if (player1 == 99) { player1 = 6; label9.Text = "Oops! You are eaten by snake"; elseclick.Play(); }
                if(player1>100)player1-=a;
              

                pictureBox7.Location = new Point(loc_arr[(player1-1) / row, (player1-1) % col].x_cord, loc_arr[(player1-1) / row, (player1-1) % col].y_cord);
              //  MessageBox.Show("player 1");
                if (player1 == 100) {

                    try
                    {
                        backmusic.Play();
                    }
                    catch (Exception ert) { }
                    MessageBox.Show("Congrats "+textBox1.Text+" !!! You won !!!");
                    
                    
                        button1.Enabled = true;
                    
                    reset();

                }
                
                if (number_of_player == 2)
                {
                    turn = 2;
                    label2.Text = textBox2.Text;
                }
              //  MessageBox.Show("player1 is"+player1);
                
            }
            else {
                
                player2 += a;
                if (player2 == 8) { player2 = 31; label9.Text = "Great work! You climbed up the ladder"; ladderup.Play(); }
                else if (player2 == 15) { player2 = 97; label9.Text = "Great work! You climbed up the ladder"; ladderup.Play(); }
                else if (player2 == 42) { player2 = 81; label9.Text = "Great work! You climbed up the ladder"; ladderup.Play(); }
                else if (player2 == 66) { player2 = 87; label9.Text = "Great work! You climbed up the ladder"; ladderup.Play(); }
                else if (player2 == 24) { player2 = 1; label9.Text = "Oops! You are eaten by snake"; elseclick.Play(); }
                else if (player2 == 55) { player2 = 13; label9.Text = "Oops! You are eaten by snake"; elseclick.Play(); }
                else if (player2 == 71) { player2 = 29; label9.Text = "Oops! You are eaten by snake"; elseclick.Play(); }
                else if (player2 == 88) { player2 = 67; label9.Text = "Oops! You are eaten by snake"; elseclick.Play(); }
                else if (player2 == 99) { player2 = 6; label9.Text = "Oops! You are eaten by snake"; elseclick.Play(); }
                if (player2 > 100) player2 -= a;
                

             //   pictureBox3.setLocation.Y += 386;
              


                pictureBox8.Location = new Point(loc_arr[(player2-1) / row, (player2-1) % col].x_cord, loc_arr[(player2-1) / row, (player2-1) % col].y_cord);
                
                if (player2 == 100)
                {

                    try
                    {
                        backmusic.Play();
                    }
                    catch (Exception ert) { }
                    MessageBox.Show("Congrats " + textBox2.Text + "!!! You won !!!");
                    
                    button1.Enabled = true;
                    reset();
                }
                if (number_of_player == 2)
                {
                    turn = 1;
                    label2.Text = textBox1.Text;
                    
                }
                
            }
        }

        }

        private void reset() {
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            label2.Text = "";
            label9.Text = "";
            label3.Text = "";
            label4.Text = "";
            pictureBox1.Visible = false;
            pictureBox2.Visible = false;
            pictureBox3.Visible = false;
            pictureBox4.Visible = false;
            pictureBox5.Visible = false;
            pictureBox6.Visible = true;
            pictureBox7.Visible = false;
            pictureBox8.Visible = false;
            radioButton1.Enabled = false;
            radioButton2.Enabled = false;
            button2.Enabled = false;
            button4.Enabled = false;
            button3.Enabled = false;
            player1 = 0;
            player2 = 0;
            pictureBox8.Location = new Point(11,579);
            pictureBox7.Location= new Point(5,585);
            pictureBox7.Visible = false;
            pictureBox8.Visible = false;
            label9.Text = "";
            checkBox1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            label1.Visible = false;
            backmusic.Stop();

        }

       //============obsolete function==============//
        private void paintdice(object sender, EventArgs e) {
           pictureBox2.Image=imagearray[img_select];
       //    MessageBox.Show("jgjwd");
           if (img_select == 6) img_select = 0;
           else
               img_select++;
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true) {
                backmusic.Stop();
            }
            else
                backmusic.PlayLooping();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            textboxtypesound.Play();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            textboxtypesound.Play();
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            backmusic.PlayLooping();
            new form2().Show();
        }

        

       
    }


    public partial class form2 : Form {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }


        public form2() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(form2));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Image = global::WindowsFormsApplication1.Properties.Resources.q;
            this.pictureBox1.Location = new System.Drawing.Point(3, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(250, 187);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Font = new System.Drawing.Font("Segoe UI Symbol", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label1.Location = new System.Drawing.Point(267, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 23);
            this.label1.TabIndex = 1;
            this.label1.Text = "Snake Ladder Game v1.0";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.linkLabel1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(5, 193);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(476, 165);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "About";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 59);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(394, 78);
            this.label3.TabIndex = 2;
            this.label3.Text = "Also some usefull programs that I have developed are also uploaded at the above " +
                "mentioned \r\nwebsite. You can download them for free ofcourse. Some of them are:" +
                "\r\n1) WinMend Folder Hidden Password Retriever" +
                "\r\n2) Free SMS sender from PC to any mobile" +
                "\r\n3) Desktop File Searching Software\r\n   and many more to be uploaded, so subscribe to my website given above for being" +
                "\r\n   updated with the latest uploads.\r\n   Thanks !!";
               
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkVisited = true;
            this.linkLabel1.Location = new System.Drawing.Point(8, 43);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(173, 13);
            this.linkLabel1.TabIndex = 1;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "http://abysscomputing.blogspot.in/\r\n";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 16);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(419, 26);
            this.label2.TabIndex = 0;
            this.label2.Text = "This is a version of the popular Snake and Ladder Game that I have tried to creat" +
                "e and \r\npublished at my website \r\n";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Contact: ";
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(49, 119);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(115, 13);
            this.linkLabel2.TabIndex = 4;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "yalidas123@gmail.com";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.linkLabel3);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.linkLabel2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(256, 35);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(225, 157);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Author";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.BlueViolet;
            this.label7.Location = new System.Drawing.Point(3, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 16);
            this.label7.TabIndex = 8;
            this.label7.Text = "Utkal Sinha";
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Location = new System.Drawing.Point(48, 137);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(173, 13);
            this.linkLabel3.TabIndex = 7;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "http://abysscomputing.blogspot.in/";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 137);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "website:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(215, 52);
            this.label5.TabIndex = 5;
            this.label5.Text = "\r\nB.Tech, Computer Science and Engineering\r\nNational Institute of Technology, Sil" +
                "char\r\nINDIA ";
            // 
            // Form2
            // 
        
            
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.BurlyWood;
            this.ClientSize = new System.Drawing.Size(486, 363);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = new Form1().Icon;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form2";
            this.Text = "About";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.LinkVisited = true;
            System.Diagnostics.Process.Start("http://abysscomputing.blogspot.in/");

        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel3.LinkVisited = true;
            System.Diagnostics.Process.Start("http://abysscomputing.blogspot.in/");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel2.LinkVisited = true;
            System.Diagnostics.Process.Start("http://yalidas123@gmail.com");
        }
    }


}
