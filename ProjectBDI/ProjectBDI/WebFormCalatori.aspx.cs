﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProjectBDI
{
    public partial class WebFormCalatori : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WebFormCurseTransportComun.aspx");
        }

        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/WebFormAutobuze.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            OleDbConnection connection = new OleDbConnection();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Projects\asp\ProjectBDI\dbProject.accdb";
            String nume = TextBox1.Text;
            String prenume = TextBox2.Text;
            String cnp = TextBox3.Text;
            String adresa = TextBox4.Text;
            String mail = TextBox5.Text;
            String telefon = TextBox6.Text;

            String query = "SELECT MAX(id_calator) FROM Calatori";
            OleDbCommand command = new OleDbCommand(query, connection);
            command.Connection = connection;
            int id = 0;
         
            
            if (!int.TryParse(TextBox3.Text, out id) || !int.TryParse(TextBox6.Text, out id) || TextBox1.Text=="" || TextBox2.Text == "" || TextBox4.Text == "" || TextBox5.Text == "")



            {
                TextBox1.Text = "Camp gol!";
                TextBox2.Text = "Camp gol!";
                TextBox4.Text = "Camp gol!";
                TextBox5.Text = "Camp gol!";


                TextBox3.Text = "CNP invalid!";     
                TextBox6.Text = "Numar de telefon invalid!";                                                                 
            }
            else
            {
            try
            {
                connection.Open();
                id = (Int32)command.ExecuteScalar() + 1;
                connection.Close();

                OleDbCommand cmd = new OleDbCommand("INSERT into Calatori (id_calator, nume, prenume, cnp, adresa, mail, telefon) " +
              "Values(@id,@nume, @prenume, @cnp, @adresa, @mail, @telefon)");
                cmd.Connection = connection;

                connection.Open();

                if (connection.State == System.Data.ConnectionState.Open)
                {
                    cmd.Parameters.Add("@id", OleDbType.Integer).Value = id++;
                    cmd.Parameters.Add("@nume", OleDbType.VarChar).Value = nume;
                    cmd.Parameters.Add("@prenume", OleDbType.VarChar).Value = prenume;
                    cmd.Parameters.Add("@cnp", OleDbType.VarChar).Value = cnp;
                    cmd.Parameters.Add("@adresa", OleDbType.VarChar).Value = adresa;
                    cmd.Parameters.Add("@mail", OleDbType.VarChar).Value = mail;
                    cmd.Parameters.Add("@telefon", OleDbType.VarChar).Value = telefon;


                    try
                    {
                        cmd.ExecuteNonQuery();
                        connection.Close();

                    }
                    catch (OleDbException ex)
                    {
                        TextBox7.Text += "EROARE - " + ex.Message;
                        connection.Close();
                    }
                }
                else
                {
                    TextBox7.Text += "Eroare la conexiunea cu BD ";
                }
            }
            catch (Exception exc)
            {
                TextBox7.Text += exc.ToString();
            }

            GridView1.DataBind();
            }
        }
    }
}