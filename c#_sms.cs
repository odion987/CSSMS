using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data .SqlClient ;
using System.IO;


namespace CsStudentDetails
{
    public partial class add_student_details : Form
    {
        public add_student_details()
        {
           
            InitializeComponent();
        }
        
        OpenFileDialog open1 = new OpenFileDialog();
        private void Button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog open1 = new OpenFileDialog();

           openFileDialog1 .FileName   = "Select picture";
            openFileDialog1 .ShowHelp   = false;
            openFileDialog1.Filter = "JPEG files (*.jpg)|*.jpg|All files(*.*)|*.*";
            openFileDialog1.FileName = String.Empty;
         DialogResult useresponse = openFileDialog1 .ShowDialog ();
         string url =openFileDialog1.FileName .ToString ();
        string path;
        try 
            {
            if  (url == "") 
            {
                MessageBox .Show ("Please select a picture. Only Jpeg pictures are accepted.");
            }
            else if    (PassportPictureBox.Image == null ) 
            {
                
                path =openFileDialog1.FileName .ToString ();
                url = String.Format(path);
                PassportPictureBox.Image = System.Drawing.Image.FromFile(path);
            }
            else if  (PassportPictureBox.Image != null ) 
            {
                PassportPictureBox.Image.Dispose();
                PassportPictureBox.Image =  null ;
                path = openFileDialog1 .FileName .ToString ();
                url = String.Format(path);
                PassportPictureBox.Image = System.Drawing.Image.FromFile(path);
                
            }
        }
        
        catch   (Exception )
            {
           MessageBox .Show ("Invalid Image Selected. Only Jpeg Image are allowed.");
            }
            

        }

        






         private void SurnameTextBox1_TextChanged(object sender, EventArgs e)
         {

         }






        




       
      











         private void Button2_Click(object sender, EventArgs e)
         {

             try
             {

            System.Text .RegularExpressions .Regex  StudentRegex =new System.Text .RegularExpressions .Regex ("[a-z]");
            

                if  (HTextBox.Text == "")
                 {
                  MessageBox .Show  ("Height Required.");
                HTextBox.Focus();
                 }
                 else if(StudentRegex .IsMatch(HTextBox.Text))
                    {
                MessageBox .Show ("Height must be in numeric values.");
                HTextBox.Focus();
                    }
           else if (WTextBox.Text == "") 
           {
                MessageBox .Show ("Weight Required.");
                WTextBox.Focus();
           }
            else if (StudentRegex .IsMatch (WTextBox.Text))
           {
                MessageBox .Show ("Weight must be in numeric values.");
                WTextBox.Focus();
            }
            else if (PhoneTextBox.Text == "") 
           {
               MessageBox .Show  ("Parent phone number Required.");
                PhoneTextBox.Focus();
            }
            else if (StudentRegex .IsMatch (PhoneTextBox.Text))
           {
               MessageBox .Show  ("Parent phone number must be in numeric values.");
                PhoneTextBox.Focus();
            }
               else if ( Phone2TextBox.Text == "") 
           {
                    MessageBox .Show ("Guardian phone number Required.");
                Phone2TextBox.Focus();
               }
            else if (StudentRegex .IsMatch (Phone2TextBox.Text)) 
           {
                MessageBox .Show ("Guardian phone number must be in numeric values.");
                Phone2TextBox.Focus();
            }
           else 
           {
                
               DateTime doadate = Convert.ToDateTime(DoaDateTimePicker.Text);
               DateTime dobdate = Convert.ToDateTime(DobDateTimePicker.Text);
               decimal  h = Convert.ToDecimal  (HTextBox.Text);
               decimal  w = Convert.ToDecimal  (WTextBox.Text);
               int phone = Convert.ToInt16(PhoneTextBox .Text );
               int phone2 = Convert.ToInt16(Phone2TextBox.Text);


                 FileInfo fi = new FileInfo( openFileDialog1 .FileName  );

                 FileStream fStream = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                

                 byte[] buffer = new byte[fStream.Length];


                 fStream.Read(buffer, 0, (int)fStream.Length);
                 int filength = Convert.ToInt32 (fi.Length);
                

                 SqlConnection cnn = new SqlConnection("Data Source=" + @".\sqlexpress;Initial Catalog=omon;Integrated Security=True");
                 SqlCommand cmd = new SqlCommand("insert_student_details", cnn);

                 cmd.CommandType = System.Data.CommandType.StoredProcedure;

                 SqlParameter prm = new SqlParameter("return_value", System.Data.SqlDbType.Int);
                 prm.Direction = ParameterDirection.ReturnValue;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("return_value", SqlDbType.NVarChar);
                 prm.Direction = ParameterDirection.ReturnValue;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@passport", SqlDbType.Image);
                 prm.Value = buffer;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@filext", SqlDbType.NVarChar, 50);
                 prm.Value = fi.Extension;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@filelength", SqlDbType.Int);
                 prm.Value = fi.Length;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@filename", SqlDbType.NVarChar, 50);
                 prm.Value = fi.Name;
                 cmd.Parameters.Add(prm);


                 prm = new SqlParameter("@surname", SqlDbType.NVarChar, 50);
                 prm.Value = SurnameTextBox1.Text;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@firstname", SqlDbType.NVarChar, 50);
                 prm.Value = FirstnameTextBox.Text;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@othername", SqlDbType.NVarChar, 50);
                 prm.Value = OthernameTextBox.Text;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@dob", SqlDbType.DateTime);
                 prm.Value = DobDateTimePicker.Text;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@soo", SqlDbType.NVarChar, 50);
                 prm.Value = SooComboBox.Text;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@lg", SqlDbType.NVarChar, 50);
                 prm.Value = LgComboBox.Text;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@gender", SqlDbType.NVarChar, 50);
                 prm.Value = GenderComboBox.Text;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@disability", SqlDbType.NVarChar, 50);
                 prm.Value = DisabilityComboBox.Text;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@nationality", SqlDbType.NVarChar, 50);
                 prm.Value = NationalityComboBox.Text;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@h", SqlDbType.Decimal);
                 prm.Value = Convert.ToDecimal(HTextBox.Text);
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@w", SqlDbType.NVarChar, 50);
                 prm.Value = Convert.ToDecimal(WTextBox.Text);
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@co", SqlDbType.NVarChar, 50);
                 prm.Value = C_oTextBox.Text;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@ra", SqlDbType.NVarChar, 50);
                 prm.Value = RaTextBox.Text;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@city", SqlDbType.NVarChar, 50);
                 prm.Value = City1TextBox.Text;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@state", SqlDbType.NVarChar, 50);
                 prm.Value = StateTextBox.Text;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@country", SqlDbType.NVarChar, 50);
                 prm.Value = CountryTextBox.Text;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@bos", SqlDbType.NVarChar, 50);
                 prm.Value = BosTextBox.Text;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@gr", SqlDbType.NVarChar, 50);
                 prm.Value = GrTextBox.Text;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@doa", SqlDbType.DateTime);
                 prm.Value = DoaDateTimePicker.Text;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@ca", SqlDbType.NVarChar, 50);
                 prm.Value = CaTextBox.Text;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@moa", SqlDbType.NVarChar, 50);
                 prm.Value = MoaTextBox.Text;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@psa", SqlDbType.NVarChar, 50);
                 prm.Value = PsaTextBox.Text;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@pre", SqlDbType.NVarChar, 50);
                 prm.Value = PreComboBox.Text;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@names1", SqlDbType.NVarChar, 50);
                 prm.Value = Names1TextBox.Text;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@offadd", SqlDbType.NVarChar, 50);
                 prm.Value = OffaddTextBox.Text;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@city1", SqlDbType.NVarChar, 50);
                 prm.Value = City1TextBox.Text;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@state1", SqlDbType.NVarChar, 50);
                 prm.Value = State1ComboBox.Text;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@phone", SqlDbType.Int);
                 prm.Value = Convert.ToInt32(PhoneTextBox.Text);
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@email", SqlDbType.NVarChar, 50);
                 prm.Value = EmailTextBox.Text;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@names2", SqlDbType.NVarChar, 50);
                 prm.Value = Names2TextBox.Text;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@ra2", SqlDbType.NVarChar, 50);
                 prm.Value = Ra2TextBox.Text;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@city2", SqlDbType.NVarChar, 50);
                 prm.Value = City2TextBox.Text;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@state2", SqlDbType.NVarChar, 50);
                 prm.Value = State2ComboBox.Text;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@phone2", SqlDbType.Int);
                 prm.Value = Convert.ToInt32(Phone2TextBox.Text);
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@email2", SqlDbType.NVarChar, 50);
                 prm.Value = Email2TextBox.Text;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@relationship", SqlDbType.NVarChar, 50);
                 prm.Value = RelationshipTextBox.Text;
                 cmd.Parameters.Add(prm);

                 int intretval;
                 cnn.Open();
                 intretval = Convert.ToInt32(cmd.ExecuteScalar());
                 cnn.Close();

                 string retstring = string.Format("Details Added. Your id is {0} ", intretval);
                 MessageBox.Show(retstring );
                 SidLabel1.Text = SurnameTextBox1.Text + FirstnameTextBox.Text + ",   -" + "Your Id is " + "" + intretval;
                Label1.Text = "Your Id is " + "" + intretval;
           
            }
         }
             catch (Exception)
             {
                MessageBox.Show("Please upload image");
             }
         }

         private void add_student_details_Load(object sender, EventArgs e)
         {

         }
         
}

    }
    



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace CsStudentDetails
{
    public partial class modify_student_details : Form
    {
        public modify_student_details()
        {
            InitializeComponent();
        }





        public static int   select_student_details(int student_id)
            {

         int  retval;
            //notice the difference in the connection string as '@' need to be added when '\' is part of the string
        SqlConnection cnn=new SqlConnection ("Data Source=" + @".\sqlexpress;Initial Catalog=omon;Integrated Security=True");
            SqlCommand cmd=new SqlCommand ("select_student_details", cnn);
            cmd.CommandType  = CommandType.StoredProcedure;

            SqlParameter prm=new SqlParameter() ;
            prm.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters .Add (prm );


                prm = new SqlParameter("@sid", SqlDbType.Int );
                 prm.Value = student_id ;
                 cmd.Parameters.Add(prm);

            cnn.Open();
            SqlDataReader reader= cmd.ExecuteReader ();
            retval = Convert.ToInt32(reader.Read() );

            //Always call Close when done reading.
            reader.Close();
            cnn.Close();
                return retval ;
        }
        
        






        private void Button2_Click(object sender, EventArgs e)
        {

            try
            {

                System.Text.RegularExpressions.Regex StudentRegex = new System.Text.RegularExpressions.Regex("[a-z]");

                if (SidLabel1.Text == "")
                {
                    MessageBox.Show("Please login to update details");
                }
                else if (HTextBox.Text == "")
                {
                    MessageBox.Show("Height Required.");
                    HTextBox.Focus();
                }
                else if (StudentRegex.IsMatch(HTextBox.Text))
                {
                    MessageBox.Show("Height must be in numeric values.");
                    HTextBox.Focus();
                }
                else if (WTextBox.Text == "")
                {
                    MessageBox.Show("Weight Required.");
                    WTextBox.Focus();
                }
                else if (StudentRegex.IsMatch(WTextBox.Text))
                {
                    MessageBox.Show("Weight must be in numeric values.");
                    WTextBox.Focus();
                }
                else if (PhoneTextBox.Text == "")
                {
                    MessageBox.Show("Parent phone number Required.");
                    PhoneTextBox.Focus();
                }
                else if (StudentRegex.IsMatch(PhoneTextBox.Text))
                {
                    MessageBox.Show("Parent phone number must be in numeric values.");
                    PhoneTextBox.Focus();
                }
                else if (Phone2TextBox.Text == "")
                {
                    MessageBox.Show("Guardian phone number Required.");
                    Phone2TextBox.Focus();
                }
                else if (StudentRegex.IsMatch(Phone2TextBox.Text))
                {
                    MessageBox.Show("Guardian phone number must be in numeric values.");
                    Phone2TextBox.Focus();
                }
                else
                {

                    DateTime doadate = Convert.ToDateTime(DoaDateTimePicker.Text);
                    DateTime dobdate = Convert.ToDateTime(DobDateTimePicker.Text);
                    decimal h = Convert.ToDecimal(HTextBox.Text);
                    decimal w = Convert.ToDecimal(WTextBox.Text);
                    int phone = Convert.ToInt16(PhoneTextBox.Text);
                    int phone2 = Convert.ToInt16(Phone2TextBox.Text);


                    SqlConnection cnn = new SqlConnection("Data Source=" + @".\sqlexpress;Initial Catalog=omon;Integrated Security=True");
                    SqlCommand cmd = new SqlCommand("update_student_details", cnn);

                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlParameter prm = new SqlParameter("return_value", System.Data.SqlDbType.Int);
                    prm.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("return_value", SqlDbType.NVarChar);
                    prm.Direction = ParameterDirection.ReturnValue;
                    cmd.Parameters.Add(prm);



                    prm = new SqlParameter("@surname", SqlDbType.NVarChar, 50);
                    prm.Value = SurnameTextBox.Text;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@firstname", SqlDbType.NVarChar, 50);
                    prm.Value = FirstnameTextBox.Text;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@othername", SqlDbType.NVarChar, 50);
                    prm.Value = OthernameTextBox.Text;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@dob", SqlDbType.DateTime);
                    prm.Value = DobDateTimePicker.Text;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@soo", SqlDbType.NVarChar, 50);
                    prm.Value = SooComboBox.Text;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@lg", SqlDbType.NVarChar, 50);
                    prm.Value = LgComboBox.Text;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@gender", SqlDbType.NVarChar, 50);
                    prm.Value = GenderComboBox.Text;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@disability", SqlDbType.NVarChar, 50);
                    prm.Value = DisabilityComboBox.Text;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@nationality", SqlDbType.NVarChar, 50);
                    prm.Value = NationalityComboBox.Text;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@h", SqlDbType.Decimal);
                    prm.Value = Convert.ToDecimal(HTextBox.Text);
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@w", SqlDbType.NVarChar, 50);
                    prm.Value = Convert.ToDecimal(WTextBox.Text);
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@co", SqlDbType.NVarChar, 50);
                    prm.Value = C_oTextBox.Text;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@ra", SqlDbType.NVarChar, 50);
                    prm.Value = RaTextBox.Text;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@city", SqlDbType.NVarChar, 50);
                    prm.Value = City1TextBox.Text;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@state", SqlDbType.NVarChar, 50);
                    prm.Value = StateTextBox.Text;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@country", SqlDbType.NVarChar, 50);
                    prm.Value = CountryTextBox.Text;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@bos", SqlDbType.NVarChar, 50);
                    prm.Value = BosTextBox.Text;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@gr", SqlDbType.NVarChar, 50);
                    prm.Value = GrTextBox.Text;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@doa", SqlDbType.DateTime);
                    prm.Value = DoaDateTimePicker.Text;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@ca", SqlDbType.NVarChar, 50);
                    prm.Value = CaTextBox.Text;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@moa", SqlDbType.NVarChar, 50);
                    prm.Value = MoaTextBox.Text;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@psa", SqlDbType.NVarChar, 50);
                    prm.Value = PsaTextBox.Text;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@pre", SqlDbType.NVarChar, 50);
                    prm.Value = PreComboBox.Text;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@names1", SqlDbType.NVarChar, 50);
                    prm.Value = Names1TextBox.Text;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@offadd", SqlDbType.NVarChar, 50);
                    prm.Value = OffaddTextBox.Text;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@city1", SqlDbType.NVarChar, 50);
                    prm.Value = City1TextBox.Text;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@state1", SqlDbType.NVarChar, 50);
                    prm.Value = State1ComboBox.Text;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@phone", SqlDbType.Int);
                    prm.Value = Convert.ToInt32(PhoneTextBox.Text);
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@email", SqlDbType.NVarChar, 50);
                    prm.Value = EmailTextBox.Text;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@names2", SqlDbType.NVarChar, 50);
                    prm.Value = Names2TextBox.Text;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@ra2", SqlDbType.NVarChar, 50);
                    prm.Value = Ra2TextBox.Text;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@city2", SqlDbType.NVarChar, 50);
                    prm.Value = City2TextBox.Text;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@state2", SqlDbType.NVarChar, 50);
                    prm.Value = State2ComboBox.Text;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@phone2", SqlDbType.Int);
                    prm.Value = Convert.ToInt32(Phone2TextBox.Text);
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@email2", SqlDbType.NVarChar, 50);
                    prm.Value = Email2TextBox.Text;
                    cmd.Parameters.Add(prm);

                    prm = new SqlParameter("@relationship", SqlDbType.NVarChar, 50);
                    prm.Value = RelationshipTextBox.Text;
                    cmd.Parameters.Add(prm);

                    int intretval;
                    cnn.Open();
                    intretval = Convert.ToInt32(cmd.ExecuteScalar());
                    cnn.Close();

                    MessageBox.Show("your details has been updated. " );
                    SidLabel1.Text = SurnameTextBox .Text  + "    " + FirstnameTextBox.Text + ",   -" + "your details has been added ";
                   // Label1.Text = "Your Id is ";

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Please upload image");
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
          
        System.Text .RegularExpressions .Regex   SalesIdRegex=new System .Text .RegularExpressions .Regex("[a-z]") ;
       if  (StudentIdTextBox.Text == "")
       {
           MessageBox .Show ("Student Id Required.");
            StudentIdTextBox.Focus();
       }
       else if  (SalesIdRegex.IsMatch(StudentIdTextBox.Text) )
       {
            MessageBox .Show ("Student Id requires numeric values");
            StudentIdTextBox.Focus();
       }
        else 
       {
           int studentid= Convert.ToInt32 (StudentIdTextBox .Text );
            int retval= select_student_details(studentid );

            if  (retval == 0) 
            {
                MessageBox .Show ("Invalid Student Id");
            }
            else 
            {
                SqlConnection cnn = new SqlConnection("Data Source=." + @"\sqlexpress;Initial Catalog=omon;Integrated Security=True");
                SqlCommand cmd=new SqlCommand ("select_student_details", cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter prm = new SqlParameter("return_value", System.Data.SqlDbType.Int);
                prm.Direction = ParameterDirection.ReturnValue;


                prm = new SqlParameter("@sid", SqlDbType.Int);
                prm.Value =Convert.ToInt32 ( StudentIdTextBox .Text) ;
               cmd.Parameters .Add (prm );


                cnn.Open();

                SqlDataReader reader =cmd.ExecuteReader();

                try 
                {
                    while (reader .Read())
                    {

                        String surname = String.Format("{0}", reader[4]);
                        String firstname= String.Format("{0}", reader[5]);

                        display1.Text = surname  + "  "  +  "  " + firstname  + "  " + " "  +  "You can now edit your details";



                        Byte[] byteData = new Byte[0];
                        //byteBLOBData = (Byte[])(ds.Tables["tblBLOB"].Rows[c - 1]["BLOBData"]);

                        byteData = (Byte[])(reader [0]);
                         MemoryStream stmData = new MemoryStream(byteData); 
	                     PassportPictureBox .Image  = Image.FromStream(stmData);
                         


                        SurnameTextBox.Text = String.Format("{0}", reader[4]);
                        FirstnameTextBox.Text = String.Format("{0}", reader[5]);
                        OthernameTextBox.Text = String.Format("{0}", reader[6]);
                        DobDateTimePicker.Text = String.Format("{0}", reader[7]);
                        SooComboBox.Text = String.Format("{0}", reader[8]);
                        LgComboBox.Text = String.Format("{0}", reader[9]);
                        GenderComboBox.Text = String.Format("{0}", reader[10]);
                        DisabilityComboBox.Text = String.Format("{0}", reader[11]);
                        NationalityComboBox.Text = String.Format("{0}", reader[12]);
                        HTextBox.Text = String.Format("{0}", reader[13]);
                        WTextBox.Text = String.Format("{0}", reader[14]);
                        C_oTextBox.Text = String.Format("{0}", reader[15]);
                        RaTextBox.Text = String.Format("{0}", reader[16]);
                        City1TextBox.Text = String.Format("{0}", reader[17]);
                        State1ComboBox.Text = String.Format("{0}", reader[18]);
                        CountryTextBox.Text = String.Format("{0}", reader[19]);
                        BosTextBox.Text = String.Format("{0}", reader[20]);
                        GrTextBox.Text = String.Format("{0}", reader[21]);
                        DoaDateTimePicker.Text = String.Format("{0}", reader[22]);
                        CaTextBox.Text = String.Format("{0}", reader[23]);
                        MoaTextBox.Text = String.Format("{0}", reader[24]);
                        PsaTextBox.Text = String.Format("{0}", reader[25]);
                        PreComboBox.Text = String.Format("{0}", reader[26]);
                        Names1TextBox.Text = String.Format("{0}", reader[27]);
                        OffaddTextBox.Text = String.Format("{0}", reader[28]);
                        City1TextBox.Text = String.Format("{0}", reader[29]);
                        StateTextBox.Text = String.Format("{0}", reader[30]);
                        PhoneTextBox.Text = String.Format("{0}", reader[31]);
                        EmailTextBox.Text = String.Format("{0}", reader[32]);
                        Names2TextBox.Text = String.Format("{0}", reader[33]);
                        RaTextBox.Text = String.Format("{0}", reader[34]);
                        City2TextBox.Text = String.Format("{0}", reader[35]);
                        State2ComboBox.Text = String.Format("{0}", reader[36]);
                        Phone2TextBox.Text = String.Format("{0}", reader[37]);
                        Email2TextBox.Text = String.Format("{0}", reader[38]);
                        RelationshipTextBox.Text = String.Format("{0}", reader[39]);
                        SidLabel1.Text = String.Format("{0}", reader[40]);
                    }
                }

                finally 
                {
                    //Always call Close when done reading.
                    reader.Close();
                }
                

            

        }

       }
     }   
    }
}




































using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;

namespace CsStudentDetails
{
    public partial class delete_student_details : Form
    {
        public delete_student_details()
        {
            InitializeComponent();
        }





        public static int select_student_details(int student_id)
        {

            int retval;
            //notice the difference in the connection string as '@' need to be added when '\' is part of the string
            SqlConnection cnn = new SqlConnection("Data Source=" + @".\sqlexpress;Initial Catalog=omon;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("select_student_details", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter prm = new SqlParameter();
            prm.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(prm);


            prm = new SqlParameter("@sid", SqlDbType.Int);
            prm.Value = student_id;
            cmd.Parameters.Add(prm);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            retval = Convert.ToInt32(reader.Read());

            //Always call Close when done reading.
            reader.Close();
            cnn.Close();
            return retval;
        }









        private void Button1_Click(object sender, EventArgs e)
        {

            System.Text.RegularExpressions.Regex SalesIdRegex = new System.Text.RegularExpressions.Regex("[a-z]");
            if (StudentIdTextBox.Text == "")
            {
                MessageBox.Show("Student Id Required.");
                StudentIdTextBox.Focus();
            }
            else if (SalesIdRegex.IsMatch(StudentIdTextBox.Text))
            {
                MessageBox.Show("Student Id requires numeric values");
                StudentIdTextBox.Focus();
            }
            else
            {
                int studentid = Convert.ToInt32(StudentIdTextBox.Text);
                int retval = select_student_details(studentid);
                if (retval == 0)
                {
                    MessageBox.Show("Invalid Student Id");
                }
                else
                {
                    SqlConnection cnn = new SqlConnection("Data Source=." + @"\sqlexpress;Initial Catalog=omon;Integrated Security=True");
                    SqlCommand cmd = new SqlCommand("select_student_details", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter prm = new SqlParameter("return_value", System.Data.SqlDbType.Int);
                    prm.Direction = ParameterDirection.ReturnValue;


                    prm = new SqlParameter("@sid", SqlDbType.Int);
                    prm.Value = Convert.ToInt32(StudentIdTextBox.Text);
                    cmd.Parameters.Add(prm);


                    cnn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    try
                    {
                        while (reader.Read())
                        {

                            String surname = String.Format("{0}", reader[4]);
                            String firstname = String.Format("{0}", reader[5]);

                            Label1 .Text  = surname + "  " + "  " + firstname + "  " + " " + "You can now delete your details";

                            Byte[] byteData = new Byte[0];
                            //byteBLOBData = (Byte[])(ds.Tables["tblBLOB"].Rows[c - 1]["BLOBData"]);

                            byteData = (Byte[])(reader[0]);
                            MemoryStream stmData = new MemoryStream(byteData);
                            PassportPictureBox.Image = Image.FromStream(stmData);
                             
                            

                            SurnameLabel2 .Text  = String.Format("{0}", reader[4]);
                            FirstnameLabel2 .Text  = String.Format("{0}", reader[5]);
                            OthernameLabel2 .Text  = String.Format("{0}", reader[6]);
                            DobLabel2 .Text  = String.Format("{0}", reader[7]);
                            SooLabel2 .Text  = String.Format("{0}", reader[8]);
                            LgLabel2 .Text = String.Format("{0}", reader[9]);
                            GenderLabel2 .Text  = String.Format("{0}", reader[10]);
                            DisabilityLabel2 .Text = String.Format("{0}", reader[11]);
                            NationalityLabel2 .Text  = String.Format("{0}", reader[12]);
                            HLabel2 .Text  = String.Format("{0}", reader[13]);
                            WLabel2 .Text  = String.Format("{0}", reader[14]);
                            C_oLabel2 .Text  = String.Format("{0}", reader[15]);
                           RaLabel2 .Text  = String.Format("{0}", reader[16]);
                            CityLabel2  .Text = String.Format("{0}", reader[17]);
                             StateLabel2.Text   = String.Format("{0}", reader[18]);
                            CountryLabel2 .Text  = String.Format("{0}", reader[19]);
                            BosLabel2 .Text  = String.Format("{0}", reader[20]);
                            GrLabel2 .Text = String.Format("{0}", reader[21]);
                            DoaLabel2 .Text  = String.Format("{0}", reader[22]);
                            CaLabel2 .Text = String.Format("{0}", reader[23]);
                            MoaLabel2 .Text  = String.Format("{0}", reader[24]);
                            PsaLabel2 .Text  = String.Format("{0}", reader[25]);
                            PreLabel2 .Text = String.Format("{0}", reader[26]);
                            Names1Label2 .Text  = String.Format("{0}", reader[27]);
                            OffaddLabel2 .Text  = String.Format("{0}", reader[28]);
                            City1Label2 .Text  = String.Format("{0}", reader[29]);
                            State1Label2 .Text  = String.Format("{0}", reader[30]);
                            PhoneLabel2 .Text  = String.Format("{0}", reader[31]);
                            EmailLabel2 .Text  = String.Format("{0}", reader[32]);
                          Names2Label2 .Text  = String.Format("{0}", reader[33]);
                            RaLabel2 .Text = String.Format("{0}", reader[34]);
                           City2Label2 .Text = String.Format("{0}", reader[35]);
                           State2Label2 .Text  = String.Format("{0}", reader[36]);
                            Phone2Label2.Text   = String.Format("{0}", reader[37]);
                            Email2Label2 .Text  = String.Format("{0}", reader[38]);
                            RelationshipLabel2 .Text  = String.Format("{0}", reader[39]);
                            SidLabel2.Text  = String.Format("{0}", reader[40]);
                        }
                    }

                    finally
                    {
                        //Always call Close when done reading.
                        reader.Close();
                    }
                }
            }
        }




        public  static int delete_data(int student_id ) 
        {
            SqlConnection cnn = new SqlConnection("Data Source=" + @".\sqlexpress;Initial Catalog=omon;Integrated Security=True");
        SqlCommand cmd=new SqlCommand ("delete student_details where sid=@sid", cnn);
        cmd.CommandType  = CommandType .Text ;
        //Pls note that the command type is commandtype.text instead of commandtype.storedprocedure

        SqlParameter prm=new SqlParameter();


         prm=new SqlParameter ("@student_id", SqlDbType.NVarChar);
        prm.Direction =ParameterDirection .ReturnValue ;
        cmd.Parameters.Add(prm);

         prm=new SqlParameter ("@sid", SqlDbType.NVarChar);
        prm.Value = student_id ;
        cmd.Parameters.Add(prm);


        cnn.Open();
         int retval=Convert.ToInt32 ( cmd.ExecuteScalar ());
        cnn.Close();
        return retval;
        }






        private void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                int sid = Convert.ToInt32(SidLabel2.Text);
                if (SidLabel2.Text == "")
                {
                    MessageBox.Show("Pls login to delete details.");
                }
                else
                {
                    delete_data(sid);
                    MessageBox.Show("Details deleted");
                }
            }
            catch
            {
                MessageBox.Show("Delete Unsuccessful!!!");
            }

        }
    }
}







using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.IO;

namespace CsStudentDetails
{
    public partial class view_student_details : Form
    {
        public view_student_details()
        {
            InitializeComponent();
        }





        public static int select_student_details(int student_id)
        {

            int retval;
            //notice the difference in the connection string as '@' need to be added when '\' is part of the string
            SqlConnection cnn = new SqlConnection("Data Source=" + @".\sqlexpress;Initial Catalog=omon;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("select_student_details", cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter prm = new SqlParameter();
            prm.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(prm);


            prm = new SqlParameter("@sid", SqlDbType.Int);
            prm.Value = student_id;
            cmd.Parameters.Add(prm);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            retval = Convert.ToInt32(reader.Read());

            //Always call Close when done reading.
            reader.Close();
            cnn.Close();
            return retval;
        }








        private void Button1_Click(object sender, EventArgs e)
        {

            System.Text.RegularExpressions.Regex SalesIdRegex = new System.Text.RegularExpressions.Regex("[a-z]");
            if (StudentIdTextBox.Text == "")
            {
                MessageBox.Show("Student Id Required.");
                StudentIdTextBox.Focus();
            }
            else if (SalesIdRegex.IsMatch(StudentIdTextBox.Text))
            {
                MessageBox.Show("Student Id requires numeric values");
                StudentIdTextBox.Focus();
            }
            else
            {
                int studentid = Convert.ToInt32(StudentIdTextBox.Text);
                int retval = select_student_details(studentid);
                if (retval == 0)
                {
                    MessageBox.Show("Invalid Student Id");
                }
                else
                {
                    SqlConnection cnn = new SqlConnection("Data Source=." + @"\sqlexpress;Initial Catalog=omon;Integrated Security=True");
                    SqlCommand cmd = new SqlCommand("select_student_details", cnn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter prm = new SqlParameter("return_value", System.Data.SqlDbType.Int);
                    prm.Direction = ParameterDirection.ReturnValue;


                    prm = new SqlParameter("@sid", SqlDbType.Int);
                    prm.Value = Convert.ToInt32(StudentIdTextBox.Text);
                    cmd.Parameters.Add(prm);


                    cnn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    try
                    {
                        while (reader.Read())
                        {

                            String surname = String.Format("{0}", reader[4]);
                            String firstname = String.Format("{0}", reader[5]);

                            Label1.Text = surname + "  " + "  " + firstname + "  " + " " + "Please view your details";

                            Byte[] byteData = new Byte[0];
                            //byteBLOBData = (Byte[])(ds.Tables["tblBLOB"].Rows[c - 1]["BLOBData"]);

                            byteData = (Byte[])(reader[0]);
                            MemoryStream stmData = new MemoryStream(byteData);
                            PassportPictureBox.Image = Image.FromStream(stmData);
                             

                            SurnameLabel2.Text = String.Format("{0}", reader[4]);
                            FirstnameLabel2.Text = String.Format("{0}", reader[5]);
                            OthernameLabel2.Text = String.Format("{0}", reader[6]);
                            DobLabel2.Text = String.Format("{0}", reader[7]);
                            SooLabel2.Text = String.Format("{0}", reader[8]);
                            LgLabel2.Text = String.Format("{0}", reader[9]);
                            GenderLabel2.Text = String.Format("{0}", reader[10]);
                            DisabilityLabel2.Text = String.Format("{0}", reader[11]);
                            NationalityLabel2.Text = String.Format("{0}", reader[12]);
                            HLabel2.Text = String.Format("{0}", reader[13]);
                            WLabel2.Text = String.Format("{0}", reader[14]);
                            C_oLabel2.Text = String.Format("{0}", reader[15]);
                            RaLabel2.Text = String.Format("{0}", reader[16]);
                            CityLabel2.Text = String.Format("{0}", reader[17]);
                            StateLabel2.Text  = String.Format("{0}", reader[18]);
                            CountryLabel2.Text = String.Format("{0}", reader[19]);
                            BosLabel2.Text = String.Format("{0}", reader[20]);
                            GrLabel2.Text = String.Format("{0}", reader[21]);
                            DoaLabel2.Text = String.Format("{0}", reader[22]);
                            CaLabel2.Text = String.Format("{0}", reader[23]);
                            MoaLabel2.Text = String.Format("{0}", reader[24]);
                            PsaLabel2.Text = String.Format("{0}", reader[25]);
                            PreLabel2.Text = String.Format("{0}", reader[26]);
                            Names1Label2.Text = String.Format("{0}", reader[27]);
                            OffaddLabel2.Text = String.Format("{0}", reader[28]);
                            City1Label2.Text = String.Format("{0}", reader[29]);
                            State1Label2.Text = String.Format("{0}", reader[30]);
                            PhoneLabel2.Text = String.Format("{0}", reader[31]);
                            EmailLabel2.Text = String.Format("{0}", reader[32]);
                            Names2Label2.Text = String.Format("{0}", reader[33]);
                            RaLabel2.Text = String.Format("{0}", reader[34]);
                            City2Label2.Text = String.Format("{0}", reader[35]);
                            State2Label2.Text = String.Format("{0}", reader[36]);
                            Phone2Label2.Text = String.Format("{0}", reader[37]);
                            Email2Label2.Text = String.Format("{0}", reader[38]);
                            RelationshipLabel2.Text = String.Format("{0}", reader[39]);
                            SidLabel2.Text = String.Format("{0}", reader[40]);
                        }
                    }

                    finally
                    {
                        //Always call Close when done reading.
                        reader.Close();
                    }
                }
            }
        }
    }
}
















using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data ;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace CsStudentDetails
{
    public partial class changepic_student_details : Form
    {
        public changepic_student_details()
        {
            InitializeComponent();
            
        }



        public static int select_student_details(int student_id)
        {

            int retval;
            //notice the difference in the connection string as '@' need to be added when '\' is part of the string
            SqlConnection cnn = new SqlConnection("Data Source=" + @".\sqlexpress;Initial Catalog=omon;Integrated Security=True");
            SqlCommand cmd = new SqlCommand("select_student_details", cnn);
            cmd.CommandType = CommandType.StoredProcedure ;

            SqlParameter prm = new SqlParameter();
            prm.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(prm);


            prm = new SqlParameter("@sid", SqlDbType.Int);
            prm.Value = student_id;
            cmd.Parameters.Add(prm);

            cnn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            retval = Convert.ToInt32(reader.Read());

            //Always call Close when done reading.
            reader.Close();
            cnn.Close();
            return retval;
        }




        private void login_Click(object sender, EventArgs e)
        {
               
        System.Text .RegularExpressions .Regex   SalesIdRegex=new System .Text .RegularExpressions .Regex("[a-z]") ;
       if  (StudentIdTextBox.Text == "")
       {
           MessageBox .Show ("Student Id Required.");
            StudentIdTextBox.Focus();
       }
       else if (SalesIdRegex.IsMatch(StudentIdTextBox.Text))
       {
           MessageBox.Show("Student Id requires numeric values");
           StudentIdTextBox.Focus();
       }
       else
       {
           int studentid = Convert.ToInt32(StudentIdTextBox.Text);
           int retval = select_student_details(studentid);

           if (retval == 0)
           {
               MessageBox.Show("Invalid Student Id");
           }
           else
           {
               SqlConnection cnn = new SqlConnection("Data Source=." + @"\sqlexpress;Initial Catalog=omon;Integrated Security=True");
               SqlCommand cmd = new SqlCommand("select passport,sid,surname,firstname from student_details where sid=@sid", cnn);
               cmd.CommandType = CommandType.Text ;

               SqlParameter prm = new SqlParameter("return_value", System.Data.SqlDbType.Int);
               prm.Direction = ParameterDirection.ReturnValue;


               prm = new SqlParameter("@sid", SqlDbType.Int);
               prm.Value = Convert.ToInt32(StudentIdTextBox.Text);
               cmd.Parameters.Add(prm);


               cnn.Open();

               SqlDataReader reader = cmd.ExecuteReader();

               try
               {
                   while (reader.Read())
                   {

                       String surname = String.Format("{0}", reader[2]);
                       String firstname = String.Format("{0}", reader[3]);
                       string a = surname + "  " + "  " + firstname + "  " + " " + "You can now edit your passport";
                       MessageBox.Show(a);
                       displaylabel.Text = a;


                       StudentIdLabel.Text = String.Format("{0}", reader[1]);

                       Byte[] byteData = new Byte[0];
                       //byteBLOBData = (Byte[])(ds.Tables["tblBLOB"].Rows[c - 1]["BLOBData"]);

                       byteData = (Byte[])(reader[0]);
                       MemoryStream stmData = new MemoryStream(byteData);
                       PassportPictureBox.Image = Image.FromStream(stmData);
                   }
               }
               catch (Exception)
               {
                   MessageBox.Show("Invalid Student Id.");
               }
           }
       }
        }

        private void Button1_Click(object sender, EventArgs e)
        {

            OpenFileDialog open1 = new OpenFileDialog();

            openFileDialog1.FileName = "Select picture";
            openFileDialog1.ShowHelp = false;
            openFileDialog1.Filter = "JPEG files (*.jpg)|*.jpg|All files(*.*)|*.*";
            openFileDialog1.FileName = String.Empty;
            DialogResult useresponse = openFileDialog1.ShowDialog();
            string url = openFileDialog1.FileName.ToString();
            string path;
            try
            {
                if (url == "")
                {
                    MessageBox.Show("Please select a picture. Only Jpeg pictures are accepted.");
                }
                else if (PassportPictureBox.Image == null)
                {

                    path = openFileDialog1.FileName.ToString();
                    url = String.Format(path);
                    PassportPictureBox.Image = System.Drawing.Image.FromFile(path);
                }
                else if (PassportPictureBox.Image != null)
                {
                    PassportPictureBox.Image.Dispose();
                    PassportPictureBox.Image = null;
                    path = openFileDialog1.FileName.ToString();
                    url = String.Format(path);
                    PassportPictureBox.Image = System.Drawing.Image.FromFile(path);

                }
            }

            catch (Exception)
            {
                MessageBox.Show("Invalid Image Selected. Only Jpeg Image are allowed.");
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
             try 
             {
        

          FileInfo fi = new FileInfo( openFileDialog1 .FileName  );

                 FileStream fStream = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                

                 byte[] buffer = new byte[fStream.Length];


                 fStream.Read(buffer, 0, (int)fStream.Length);
                 int filength = Convert.ToInt32 (fi.Length);

            if (fi.Name ==null )
            {
                MessageBox .Show ("You must upload an image for update");
            }
            else if (fi.Name ==null)
                {
                MessageBox .Show ("You must upload an image for update");
                }
            else 
            {
                SqlConnection cnn = new SqlConnection("Data Source=" + @".\sqlexpress;Initial Catalog=omon;Integrated Security=True");
                 SqlCommand cmd = new SqlCommand("update student_details set passport=@passport,filext=@filext,filelength=@filelength," +
                                                 "filename=@filename where sid=@sid", cnn);

                 cmd.CommandType = System.Data.CommandType.Text ;

                 SqlParameter prm = new SqlParameter("return_value", System.Data.SqlDbType.Int);
                 prm.Direction = ParameterDirection.ReturnValue;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("return_value", SqlDbType.NVarChar);
                 prm.Direction = ParameterDirection.ReturnValue;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@passport", SqlDbType.Image);
                 prm.Value = buffer;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@filext", SqlDbType.NVarChar, 50);
                 prm.Value = fi.Extension;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@filelength", SqlDbType.Int);
                 prm.Value = fi.Length;
                 cmd.Parameters.Add(prm);

                 prm = new SqlParameter("@filename", SqlDbType.NVarChar, 50);
                 prm.Value = fi.Name;
                 cmd.Parameters.Add(prm);

                prm = new SqlParameter("@sid", SqlDbType.Int );
                 prm.Value = StudentIdLabel .Text ;
                 cmd.Parameters.Add(prm);


                 cnn.Open();
                 cmd.ExecuteScalar();
                 cnn.Close();

                MessageBox .Show ("Passport Updated.");
                Retvallabel.Text = "Passport Updated.";
            }
             }
        catch (Exception )
            {
            MessageBox.Show ("Error! Data not inserted.");
            }
        

    


        }
    }
}













using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CsStudentDetails
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void addStudentDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //pls note the difference in declaring variables in c#
            // the variable name comes after the instance it is being declared for
            //in vb, when a new instance of an object is declared, 
            //it goes as dim addstudentDetais as new addstudentDetails
            // but in c#, the pattern is different as shown below
            //this is used instead of me
            //the end of almost every line is marked by a semicolon (;)
            add_student_details addstudentDetails = new add_student_details();
            addstudentDetails.MdiParent = this;
            addstudentDetails.Show();
        }

        //pls note the difference in declaring variables in c#
        // the variable name comes after the instance it is being declared for
        //in vb, when a new instance of an object is declared, 
        //it goes as dim modStudentDetais as new mod_student_Details
        // but in c#, the pattern is different as shown below
        //'this' is used instead of 'me'
        //the end of almost every line is marked by a semicolon (;)
        private void modifyStudentDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {

            modify_student_details modStudentDetails = new modify_student_details(); 
            modStudentDetails.MdiParent =this;
            modStudentDetails .Show ();

        }

        //pls note the difference in declaring variables in c#
        // the variable name comes after the instance it is being declared for
        //in vb, when a new instance of an object is declared, 
        //it goes as dim delStudentDetais as new delete_student_Details
        // but in c#, the pattern is different as shown below
        //'this' is used instead of 'me'
        //the end of almost every line is marked by a semicolon (;)
        private void deleteStudentDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            delete_student_details delStudentDetails = new delete_student_details();
            delStudentDetails.MdiParent = this;
            delStudentDetails.Show();
        }

        //pls note the difference in declaring variables in c#
        // the variable name comes after the instance it is being declared for
        //in vb, when a new instance of an object is declared, 
        //it goes as dim viewStudentDetails as new view_student_details
        // but in c#, the pattern is different as shown below
        //'this' is used instead of 'me'
        //the end of almost every line is marked by a semicolon (;)
        private void viewStudentDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            view_student_details viewStudentDetails = new view_student_details();
            viewStudentDetails.MdiParent = this;
            viewStudentDetails.Show();
        }

        //pls note the difference in declaring variables in c#
        // the variable name comes after the instance it is being declared for
        //in vb, when a new instance of an object is declared, 
        //it goes as dim changeStudentPic as new changepic_student_details
        // but in c#, the pattern is different as shown below
        //'this' is used instead of 'me'
        //the end of almost every line is marked by a semicolon (;)
        private void changePictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            changepic_student_details changeStudentPic = new changepic_student_details(); 
            changeStudentPic.MdiParent =this;
            changeStudentPic .Show();
        }
    }
}


