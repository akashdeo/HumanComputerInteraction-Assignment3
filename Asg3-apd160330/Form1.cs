using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
/* Header Comments*/
/* Author:Akash Deo
 * Assignment 3
 * netid-apd160330
 * 
 * So ,in this application,we have 12 fields
 * 1)First_Name
 * 2)Last_Name
 * 3)Middle_Initial(Optional)
 * 4)Address Line 1
 * 5)Address Line 2(Optional)
 * 6)City
 * 7)State(list provided)
 * 8)ZipCode
 * 9)Phone_Number
 * 10)Email_address
 * 11)Proof_of_residence
 * 12)Date_Recieved
 * 
 * Function Start_process(button_click)
 * {
 * On this button click,we basically start entering the records.
 * we start the total timer here.
 * }
 * 
 * Function End_process(button_click)
 * {
 * we record the total time after we click this button. 
 * }
 * 
 * Function Start_record_button(button8_click)
 * {
 * In this function,we basically record the time at which the record is entered.It is used to count the time for entering a record.(all types-average,min,max)
 * }
 * 
 * Function Record_Done_Button(button7_click)
 * {
 * In this function,we record the time at which the record is fully filled up.
 * }
 * 
 * Function Record_timer_button(button9_click)
 * {
 * on this button click,we record the time to take the time difference between completing a record and starting a new one. 
 * }
 * 
 * Function Clear_button(button4_click)
 * {
 * on this click,we as empty the fields of the form.
 * }
 * 
 * Function Add_Record(button1_click)
 * {
 * In this function,we take all the values from the form and then build a record and then check whether the record already exists.
 * If exists,then error message is produced,otherwise it is added.
 * 
 * }
 * 
 * Function Delete_Record(button3_click)
 * {
 * Here we Delete a specific record by selecting a record from the listbox,if the record is available in the listbox,then we delete the record .
 * And print the message
 * }
 * Function Modify_Record(button2_click)
 * {
 * Here we select a record from the listbox,the values of the record are populated into the fields,then we change the values as we want 
 * and then we press the modify button.Message is displayed if the record has been modified or not.
 * }
 * 
 * ListBox
 * {
 * Here we read the contents of the text file and then we populate the listbox with the name and phone number.It is basically used for
 * Selecting a particular record for deletion and modification. 
 * 
 * Here we also load the file in which we store all the filling_information such as record_intervaltime,total time,no of backspaces,the time to enter a record.
 *
 * }
 * Form_load(This constructor loads the text data into the listbox when we execute the program)
 * {
 * here we load the file which has previous records.(can be empty as well).
 * Here we also load the file in which we store all the filling_information such as record_intervaltime,total time,no of backspaces,the time to enter a record.
 * }
 * 
 * I have set maximum characters allowed for each field and also what kind of data is allowed in each field
 * 
 * for example,first and last name cannot have spaces
 * 
 * also email can have all kinds of characters
 *  
 * phone number,pincode can only consist of numbers
 * 
 * State has drop down list of all the 50 states of the US. etc
 * */
namespace Asg2apd160330
{
    public partial class Form1 : Form
    {
        public Form1()
        { 
            InitializeComponent();
        }
        static class Global
        {
             static int count_Number_Of_backspaces=0;
            static string path3 = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\CS6326Asg3.txt");
            static DateTime dt;
            static DateTime dt1;
            static int[] interval_time = new int[10];
            static int[] record_time = new int[10];
            static int counter=0;
            static int count = 0;
            static DateTime dt2;
            static DateTime dt3;
            public static void interval_time_Clicks(TimeSpan ts)
            {
                interval_time[counter] = ts.Seconds;
                counter++;
                if(counter==10)
                {
                    double avg_value = interval_time.Average();
                    if (avg_value < 0)
                        avg_value = -1 * avg_value;


                    double max_value = interval_time.Max();
                    if (max_value < 0)
                        max_value = -1 * max_value;
                    double min_value = interval_time.Min();
                    if (min_value < 0)
                        min_value = -1 * min_value;
                    using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(path3,true))
                    {
                        file.WriteLine("no of records:10");
                        file.WriteLine("maximum inter-record time:"+max_value);
                        file.WriteLine("minimum inter-record time:"+ min_value);
                        file.WriteLine("average inter-reord time:" + avg_value);
                    }
                }
                
            }
            public static void record_time_clicks(TimeSpan ts1)
            {
                record_time[count] = ts1.Seconds;
                count++;
                if(count==10)
                {
                    double avg_value = record_time.Average();
                    double max_value = record_time.Max();
                    double min_value = record_time.Min();
                    if (avg_value < 0)
                        avg_value = -1 * avg_value;
                    if (max_value < 0)
                        max_value = -1 * max_value;
                    if (min_value < 0)
                        min_value = -1 * min_value;
                    using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(path3,true))
                    {
                        file.WriteLine("maximum entry time:"+ max_value );
                        file.WriteLine("minimum entry time:" + min_value);
                        file.WriteLine("average entry time:" + avg_value);
                    }
                }
            }
            public static void GlobalVar()
            {
                count_Number_Of_backspaces++;
            }
            public static int Glo
            {
                get { return count_Number_Of_backspaces; }
            }
            public static DateTime GlobalTime
            {
                get { return dt; }
                set { dt = DateTime.Now; }
            }
            public static DateTime GlobalT
            {
                get { return dt1; }
            }
            public static DateTime  GlobalTime1(DateTime dt5)
            {
                 dt1=dt5;
                return dt1; 
                
            }
            public static DateTime GlobalTi(DateTime dt6)
            {
                dt2 = dt6;
                
                return dt2;
                
            }
            public static DateTime GT()
            {
                dt3 = dt2;
                return dt3;
            }
        }

        private void button1_Click(object sender, EventArgs e)//ADD RECORD
        {
            FileStream fs;
            string path = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\CS6326Asg2.txt");
            if (!File.Exists(path))
            {
                fs = File.Create(path);
                fs.Close();
            }

            int f = 0;
            using (StreamReader sr = new StreamReader(path))
            {
                string line;

                // Read and display lines from the file until 
                // the end of the file is reached. 
                while ((line = sr.ReadLine()) != null)
                {
                    String[] tokens = line.Split('\t');
                    if (textBox1.Text.Equals(tokens[0]) && textBox2.Text.Equals(tokens[1]))
                    {
                        MessageBox.Show("duplicate records");
                        f = 1;
                        break;
                    }

                }

            }




            if (f == 0)
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    if (textBox1.Text != null && textBox2.Text != null &&textBox3.Text==null&& textBox4.Text != null && textBox5.Text==null&& textBox6.Text != null && textBox8.Text != null && textBox9.Text != null && textBox10.Text != null && comboBox1.Text != null && comboBox2.Text != null && dateTimePicker1.Text.ToString() != null&&textBox3.Text==String.Empty&&textBox5.Text==String.Empty)
                    {
                        
                        sw.WriteLine(textBox1.Text.ToString() + "\t" + textBox2.Text.ToString() + "\t" + textBox4.Text.ToString() + "\t" + textBox6.Text.ToString() + "\t" + comboBox1.SelectedItem.ToString() + "\t" + textBox8.Text.ToString() + "\t" + textBox9.Text.ToString() + "\t" + textBox10.Text.ToString() + "\t" + comboBox2.SelectedItem.ToString() + "\t" + dateTimePicker1.Text.ToString()+ "\t" + DateTime.Now);
                    }
                    if(textBox1.Text != null && textBox2.Text != null && textBox3.Text!=null&&textBox4.Text != null && textBox5.Text!=null&& textBox6.Text != null && textBox8.Text != null && textBox9.Text != null && textBox10.Text != null && comboBox1.Text != null && comboBox2.Text != null && dateTimePicker1.Text.ToString() != null)
                    {
                        sw.WriteLine(textBox1.Text.ToString() + "\t" + textBox2.Text.ToString() + "\t" + textBox3.Text.ToString() + "\t" + textBox4.Text.ToString() + "\t" + textBox5.Text.ToString() + "\t" + textBox6.Text.ToString() + "\t" + comboBox1.SelectedItem.ToString() + "\t" + textBox8.Text.ToString() + "\t" + textBox9.Text.ToString() + "\t" + textBox10.Text.ToString() + "\t" + comboBox2.SelectedItem.ToString() + "\t" + dateTimePicker1.Text.ToString() + "\t" + DateTime.Now);
                    }
                }
                MessageBox.Show("Record added Successfully");

            }

        }
        private void button2_Click(object sender, EventArgs e)//MODIFY BUTTON
        {
            int k = 0;
            string selectItem = Text_File_Contents.SelectedItem.ToString();
            string[] splitSelectItem = selectItem.Split('\t');
            string path = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\CS6326Asg2.txt");
            //string filename = @"C:/books/UI Design/CS6326Asg2.txt";
            string[] records = null;
            try
            {
                records = File.ReadAllLines(path);
            }
            catch (Exception er)
            {
                MessageBox.Show("empty file");
                throw er;
            }
           
            int index = -1;
            int  k1 = 0;
            string[] values = new string[records.Length];

            String answer = "";
            for (int i = 0; i < records.Length; i++)
            {
                string[] fields = records[i].Split(new char[] { '\t' });
                if (fields[0] == splitSelectItem[0] && fields[1] == splitSelectItem[1])
                {
                    if (textBox3.Text != null && textBox5.Text != null)
                        answer = answer + textBox1.Text.ToString() + "\t" + textBox2.Text.ToString() + "\t" + textBox3.Text.ToString() + "\t" + textBox4.Text.ToString() + "\t" + textBox5.Text.ToString() + "\t" + textBox6.Text.ToString() + "\t" + comboBox1.Text.ToString() + "\t" + textBox8.Text.ToString() + "\t" + textBox9.Text.ToString() + "\t" + textBox10.Text.ToString() + "\t" + comboBox2.Text.ToString() + "\t" + dateTimePicker1.Text.ToString();
                    else
                        answer = answer + textBox1.Text.ToString() + "\t" + textBox2.Text.ToString() + "\t" + textBox4.Text.ToString() + "\t" + textBox6.Text.ToString() + "\t" + comboBox1.Text.ToString() + "\t" + textBox8.Text.ToString() + "\t" + textBox9.Text.ToString() + "\t" + textBox10.Text.ToString() + "\t" + comboBox2.Text.ToString() + "\t" + dateTimePicker1.Text.ToString();
                    index = i;
                    k = i;

                    break;
                }
                else
                    values[k1++] = records[i];
            }
            for (int i = k1; i < records.Length - 1; i++)
            {
                values[k1++] = records[i + 1];
            }
            values[records.Length-1] = answer;
            string filename1 = Path.GetTempFileName();
            bool isWhiteSpace = false;
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(filename1))
            {
                foreach (string line in values)
                {
                    isWhiteSpace = string.IsNullOrEmpty(line);

                    if (!isWhiteSpace)
                    {
                        file.WriteLine(line);
                    }
                }
            }
            File.Delete(path);
            File.Move(filename1, path);
            MessageBox.Show("Record Modified Successfully");

        }

        private void button3_Click(object sender, EventArgs e)//DELETE BUTTON
        {
            string selectItem = Text_File_Contents.SelectedItem.ToString();
            string[] splitSelectItem = selectItem.Split('\t');
            //string filename = @"C:/books/UI Design/CS6326Asg2.txt";
            string path = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\CS6326Asg2.txt");
            string[] records = null;
            try
            {
                 records = File.ReadAllLines(path);
            }
            catch(Exception er)
            {
                MessageBox.Show("empty file");
                throw er;
            }
           
            int index = -1;
            int k = 0,k1=0;
            string[] values = new string[records.Length];
            for (int i = 0; i < records.Length; i++)
            {
                string[] fields = records[i].Split(new char[] { '\t' });
                if (fields[0] == splitSelectItem[0] && fields[1] == splitSelectItem[1])
                {
                    index = i;
                    k = i;
                    
                    break;
                }
                else
                    values[k1++] = records[i];
            }
           for(int i=k1;i<records.Length-1;i++)
            {
                values[k1++] = records[i + 1];
            }
            
            string filename1 = Path.GetTempFileName();
            bool isWhiteSpace=false;
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(filename1))
            {
                foreach (string line in values)
                {
                    isWhiteSpace = string.IsNullOrEmpty(line);
                   
                    if (!isWhiteSpace)
                    {
                        file.WriteLine(line);
                    }
                }
            }
            File.Delete(path);
            File.Move(filename1,path);
            MessageBox.Show("Record Deleted Successfully");
           
        }

        void textBox1_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
                // MessageBox.Show("Enter only alphabets");
            }
            if (e.KeyChar.Equals((char)8))
                Global.GlobalVar();
            textBox1.MaxLength = 20;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsLetter(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
            if (e.KeyChar.Equals((char)8))
                Global.GlobalVar();
            textBox2.MaxLength = 20;

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
            if (e.KeyChar.Equals((char)8))
                Global.GlobalVar();
            textBox3.MaxLength = 1;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            var regex = new Regex(@"[^a-zA-Z0-9,\s]");
            if (regex.IsMatch(e.KeyChar.ToString()) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            if (e.KeyChar.Equals((char)8))
                Global.GlobalVar();
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            var regex = new Regex(@"[^a-zA-Z0-9,\s]");
            if (regex.IsMatch(e.KeyChar.ToString()) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            if (e.KeyChar.Equals((char)8))
                Global.GlobalVar();
        }


        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
            if (e.KeyChar.Equals((char)8))
                Global.GlobalVar();
            textBox6.MaxLength = 25;
        }



        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            var regex = new Regex(@"[^0-9]");
            if (regex.IsMatch(e.KeyChar.ToString()) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            if (e.KeyChar.Equals((char)8))
                Global.GlobalVar();
            textBox8.MaxLength = 9;
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            var regex = new Regex(@"[^0-9]");
            if (regex.IsMatch(e.KeyChar.ToString()) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            if (e.KeyChar.Equals((char)8))
                Global.GlobalVar();
            textBox9.MaxLength = 21;
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            var regex = new Regex(@"[^\w!#$%&'*+-/=?^_`{|}~@]");
            if (regex.IsMatch(e.KeyChar.ToString()) && !Char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
            if (e.KeyChar.Equals((char)8))
                Global.GlobalVar();
            textBox10.MaxLength = 60;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)//loads the content of both the text files.
        {
            //String filename = "C:/books/UI Design/CS6326Asg2.txt";
            string path = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\CS6326Asg2.txt");
            string path2 = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\CS6326Asg3.txt");
            string path1 = Path.GetFullPath(path);
            string path3 = Path.GetFullPath(path2);
            if (!File.Exists(path1))
            {
                File.Create(path1).Dispose();
            }
            else
            {
                string[] curItem = File.ReadAllLines(path);
                foreach (var item in curItem)
                {
                    if (string.IsNullOrWhiteSpace(item))
                    {
                        continue;
                    }
                    string[] split = item.Split('\t');
                    Text_File_Contents.Items.Add(split[0] + "\t" + split[1] + "\t" + split[2] + "\t" + split[8]);

                }
            }
                if (!File.Exists(path3))
                {
                    File.Create(path3).Dispose();
                }
                else
                {
                    string[] curItem1 = File.ReadAllLines(path3);
                    foreach (var item in curItem1)
                    {
                        String line = item;
                        filling_Information.Items.Add(line);
                    }

                }
            }
        



        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)//this is used when we click on a record runtime
        {
            string selectItem = Text_File_Contents.SelectedItem.ToString();
            string[] splitSelectItem = selectItem.Split('\t');
            //String filename = "C:/books/UI Design/CS6326Asg2.txt";
            string path = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\CS6326Asg2.txt");
            string[] curItem =File.ReadAllLines(path);
            foreach (var item in curItem)
            {
                if (string.IsNullOrWhiteSpace(item))
                {
                    continue;
                }
                string[] splitCurItem = item.Split('\t');
                if (splitCurItem[0] == splitSelectItem[0] && splitCurItem[1] == splitSelectItem[1]) 
                {
                    textBox1.Text = splitCurItem[0];
                    textBox2.Text = splitCurItem[1];
                    if (textBox3.Text != null)
                    {
                        textBox3.Text = splitCurItem[2];
                        textBox4.Text = splitCurItem[3];
                        if (textBox5.Text != null)
                        {
                            textBox5.Text = splitCurItem[4];
                            textBox6.Text = splitCurItem[5];
                            comboBox1.Text = splitCurItem[6];
                            textBox8.Text = splitCurItem[7];
                            textBox9.Text = splitCurItem[8];
                            textBox10.Text = splitCurItem[9];
                            comboBox2.Text = splitCurItem[10];
                            dateTimePicker1.Text = splitCurItem[11];
                        }
                        else
                        {
                            textBox5.Text = null;
                            textBox6.Text = splitCurItem[4];
                            comboBox1.Text = splitCurItem[5];
                            textBox8.Text = splitCurItem[6];
                            textBox9.Text = splitCurItem[7];
                            textBox10.Text = splitCurItem[8];
                            comboBox2.Text = splitCurItem[9];
                            dateTimePicker1.Text = splitCurItem[10];
                        }
                    }
                    else
                    {
                        textBox3.Text = null;
                        textBox4.Text = splitCurItem[2];
                        if (textBox5.Text != null)
                        {
                            textBox5.Text = splitCurItem[3];
                            textBox6.Text = splitCurItem[4];
                            comboBox1.Text = splitCurItem[5];
                            textBox8.Text = splitCurItem[6];
                            textBox9.Text = splitCurItem[7];
                            textBox10.Text = splitCurItem[8];
                            comboBox2.Text = splitCurItem[9];
                            dateTimePicker1.Text = splitCurItem[10];
                        }
                        else
                        {
                            textBox5.Text = null;
                            textBox6.Text = splitCurItem[3];
                            comboBox1.Text = splitCurItem[4];
                            textBox8.Text = splitCurItem[5];
                            textBox9.Text = splitCurItem[6];
                            textBox10.Text = splitCurItem[7];
                            comboBox2.Text = splitCurItem[8];
                            dateTimePicker1.Text = splitCurItem[9];
                        }
                    }
                    
                    
                    
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)//This is clear button code
        {
            textBox1.Text = String.Empty;
            textBox2.Text = String.Empty;
            textBox3.Text = String.Empty;
            textBox4.Text = String.Empty;
            textBox5.Text = String.Empty;
            textBox6.Text = String.Empty;
            comboBox1.Text = String.Empty;
            textBox8.Text = String.Empty;
            textBox9.Text = String.Empty;
            textBox10.Text = String.Empty;
            comboBox2.Text = String.Empty;
            dateTimePicker1.Value = DateTime.Today;
            DateTime dt = DateTime.Now;
            DateTime dt1 = Global.GlobalT;
            TimeSpan ts = dt1.Subtract(dt);
            Global.interval_time_Clicks(ts);
        }

        private void button5_Click(object sender, EventArgs e)//Finish Process button code
        {

            string path = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\CS6326Asg2.txt");
            string path2 = Path.GetFullPath(Directory.GetCurrentDirectory() + @"\CS6326Asg3.txt");
            //string[] curItem = File.ReadAllLines(path);
            //int i = 0;
            String last = File.ReadLines(path).Last();
            String[] tokens = last.Split('\t');
            String str;
            //DateTime dt;
            if (tokens.Length == 13)
                str = tokens[12];
            else
                str = tokens[10];

            DateTime enteredDate = DateTime.Parse(str);
            MessageBox.Show(enteredDate.ToString());
            DateTime startTime = Global.GlobalTime;
            TimeSpan ts = enteredDate.Subtract(startTime);
            int minutes = ts.Minutes;
            int seconds = ts.Seconds;
            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(path2,true))
            {
                file.WriteLine( "total"+"\t"+"time: " +minutes+":"+seconds);
                file.WriteLine("Backspace Count" +Global.Glo);
            }


        }

        private void button6_Click(object sender, EventArgs e)//Start_process button code
        {
            DateTime dt = DateTime.Now;
            MessageBox.Show(dt.ToString());
            
            Global.GlobalTime = dt;

        }

        private void button7_Click(object sender, EventArgs e)//Record_done button code
        {
            DateTime dt1 = DateTime.Now;
            Global.GlobalTime1(dt1);
           /* DateTime dt2 = Global.GT;
            TimeSpan ts1 = dt1.Subtract(dt2);
            Global.record_time_clicks(ts1);*/
        }

        private void button8_Click(object sender, EventArgs e)//Start_record button Code
        {
            DateTime dt2 = DateTime.Now;
            DateTime s2=Global.GlobalTi(dt2);
        }

        private void button9_Click(object sender, EventArgs e)//record_timer button Code
        {
            DateTime dt1 = DateTime.Now;
            DateTime dt2 = Global.GT();
            TimeSpan ts1 = dt1.Subtract(dt2);
            Global.record_time_clicks(ts1);
        }
    }
}
