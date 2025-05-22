using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Razor.Tokenizer;

namespace CRUD_Assignment_ZesTec.BAL
{
	public class CustomerBAL
	{
		string connectionString = "Server=DESKTOP-NU466DS;Database=CRUD_ZesTec;Integrated Security=True";
	
		public void SaveAddCustomer(NameValueCollection form)
		{
			using (SqlConnection con = new SqlConnection(connectionString))
			{
				string query = @"INSERT INTO CustomerMaster (customer_name, mobile_no,gender, email_id, address, pincode) VALUES ('" + form["CustomerName"] + "', '"+ form["Mobileno"] + "','"+ form["Gender"] + "', '"+ form["EmailID"] + "', '"+ form["Address"] + "', '"+ form["Pincode"] + "')";

				SqlCommand cmd = new SqlCommand(query, con);
				con.Open();
				cmd.ExecuteNonQuery();
				con.Close();
			}
		}

		
		public DataSet GetEditCustomerData(int id)
		{

			DataSet ds = new DataSet();

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					string query = "SELECT * FROM CustomerMaster where id="+id+"";
					using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
					{
						adapter.Fill(ds);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error fetching customer data: " + ex.Message);
			}
			return ds;
		}

		
		public void SaveEditCustomer(NameValueCollection form)
		{
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string query = @"UPDATE CustomerMaster SET customer_name = '" + form["CustomerName"] + "', mobile_no = '" + form["Mobileno"] + "',gender = '" + form["Gender"] + "', email_id = '" + form["EmailID"] + "', address = '" + form["Address"] + "', pincode = '" + form["Pincode"] + "' WHERE id = "+ form["UpdateRecordId"] + "";

					SqlCommand cmd = new SqlCommand(query, con);
					con.Open();
					cmd.ExecuteNonQuery();
					con.Close();
				}

			}
			catch (Exception ex)
			{
				
			}
		}

		
		public DataSet CheckDuplicateRecord(string ColName, string Value)
		{
			DataSet ds = new DataSet();

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					string query = "SELECT COUNT("+ ColName + ") as RESULT FROM CustomerMaster where "+ ColName + " = '"+ Value + "'";
					using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
					{
						adapter.Fill(ds);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error fetching customer data: " + ex.Message);
			}

			return ds;
		}

	
		public int DeleteCustomerRecord(string ID)
		{
			try
			{
				using (SqlConnection con = new SqlConnection(connectionString))
				{
					string query = @"delete from CustomerMaster WHERE id = " + ID + "";

					SqlCommand cmd = new SqlCommand(query, con);
					con.Open();
					cmd.ExecuteNonQuery();
					con.Close();
				}

			}
			catch (Exception ex)
			{
				
			}
			return 0;
		}

		public DataSet GetCustomerData()
		{
			DataSet ds = new DataSet();

			try
			{
				using (SqlConnection conn = new SqlConnection(connectionString))
				{
					string query = "SELECT * FROM CustomerMaster";
					using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
					{
						adapter.Fill(ds);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Error fetching customer data: " + ex.Message);
			}

			return ds;
		}
	}
}