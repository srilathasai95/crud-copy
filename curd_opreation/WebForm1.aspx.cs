using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace dbconnect
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        string connectionString = "Data Source=VISHNUSANKAR\\SQLEXPRESS;Initial Catalog=STUDENTS;Integrated Security=True";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStudents();
            }
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "INSERT INTO STUDENT_INFO (First_Name, Middle_Name, Last_Name, Course, Gender, BloodGroup, Phone, Email, Age, Address) VALUES (@First_Name, @Middle_Name, @Last_Name, @Course, @Gender, @BloodGroup, @Phone, @Email, @Age, @Address)";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@First_Name", Fname.Text);
                        cmd.Parameters.AddWithValue("@Middle_Name", Mname.Text);
                        cmd.Parameters.AddWithValue("@Last_Name", Lname.Text);
                        cmd.Parameters.AddWithValue("@Course", GetSelectedCourses());
                        cmd.Parameters.AddWithValue("@Gender", Gender.SelectedValue);
                        cmd.Parameters.AddWithValue("@BloodGroup", Bloodgroup.SelectedValue);
                        cmd.Parameters.AddWithValue("@Phone", Phone.Text);
                        cmd.Parameters.AddWithValue("@Email", Email.Text);
                        cmd.Parameters.AddWithValue("@Age", Age.Text);
                        cmd.Parameters.AddWithValue("@Address", Address.Text);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadStudents();
            }
            catch (Exception ex)
            {
                // Handle exception, log error, and display user-friendly message
                Response.Write($"An error occurred: {ex.Message}");
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE STUDENT_INFO SET First_Name=@First_Name, Middle_Name=@Middle_Name, Last_Name=@Last_Name, Course=@Course, Gender=@Gender, BloodGroup=@BloodGroup, Phone=@Phone, Email=@Email, Age=@Age, Address=@Address WHERE Student_ID=@Student_ID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Student_ID", GridViewStudents.SelectedDataKey.Value);
                        cmd.Parameters.AddWithValue("@First_Name", Fname.Text);
                        cmd.Parameters.AddWithValue("@Middle_Name", Mname.Text);
                        cmd.Parameters.AddWithValue("@Last_Name", Lname.Text);
                        cmd.Parameters.AddWithValue("@Course", GetSelectedCourses());
                        cmd.Parameters.AddWithValue("@Gender", Gender.SelectedValue);
                        cmd.Parameters.AddWithValue("@BloodGroup", Bloodgroup.SelectedValue);
                        cmd.Parameters.AddWithValue("@Phone", Phone.Text);
                        cmd.Parameters.AddWithValue("@Email", Email.Text);
                        cmd.Parameters.AddWithValue("@Age", Age.Text);
                        cmd.Parameters.AddWithValue("@Address", Address.Text);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadStudents();
            }
            catch (Exception ex)
            {
                // Handle exception, log error, and display user-friendly message
                Response.Write($"An error occurred: {ex.Message}");
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM STUDENT_INFO WHERE Student_ID=@Student_ID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Student_ID", GridViewStudents.SelectedDataKey.Value);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
                LoadStudents();
            }
            catch (Exception ex)
            {
                // Handle exception, log error, and display user-friendly message
                Response.Write($"An error occurred: {ex.Message}");
            }
        }

        protected void GridViewStudents_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewStudents.EditIndex = e.NewEditIndex;
            LoadStudents();
        }

        protected void GridViewStudents_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewStudents.EditIndex = -1;
            LoadStudents();
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            LoadStudents();
        }

        private void LoadStudents()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM STUDENT_INFO";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        DataTable dtResult = new DataTable();
                        dtResult.Load(reader);
                        GridViewStudents.DataSource = dtResult;
                        GridViewStudents.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception, log error, and display user-friendly message
                Response.Write($"An error occurred: {ex.Message}");
            }
        }

        protected void GridViewStudents_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = GridViewStudents.SelectedRow;
                Fname.Text = row.Cells[1].Text;
                Mname.Text = row.Cells[2].Text;
                Lname.Text = row.Cells[3].Text;
                // Need to parse course selection from cell text to checkbox list
                Gender.SelectedValue = row.Cells[5].Text;
                Bloodgroup.SelectedValue = row.Cells[6].Text;
                Phone.Text = row.Cells[7].Text;
                Email.Text = row.Cells[8].Text;
                Age.Text = row.Cells[9].Text;
                Address.Text = row.Cells[10].Text;
            }
            catch (Exception ex)
            {
                // Handle exception, log error, and display user-friendly message
                Response.Write($"An error occurred: {ex.Message}");
            }
        }

        private string GetSelectedCourses()
        {
            string courses = string.Empty;
            foreach (ListItem item in Course.Items)
            {
                if (item.Selected)
                {
                    courses += item.Text + ",";
                }
            }
            return courses.TrimEnd(',');
        }

        private string GetSelectedCourses(CheckBoxList checkBoxList)
        {
            if (checkBoxList != null)
            {
                string selectedCourses = string.Empty;
                foreach (ListItem item in checkBoxList.Items)
                {
                    if (item.Selected)
                    {
                        selectedCourses += item.Text + ",";
                    }
                }
                return selectedCourses.TrimEnd(',');
            }
            else
            {
                // Handle the case where checkBoxList is null (e.g., return an empty string)
                return string.Empty;
            }
        }

        protected void GridViewStudents_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridViewStudents.Rows[e.RowIndex];

            try
            {
                // Retrieve the updated values from the GridView row
                string studentID = GridViewStudents.DataKeys[e.RowIndex].Value.ToString();
                string firstName = ((TextBox)row.Cells[1].Controls[0]).Text;
                string middleName = ((TextBox)row.Cells[2].Controls[0]).Text;
                string lastName = ((TextBox)row.Cells[3].Controls[0]).Text;

                // Retrieve the updated Course value from the CheckBoxList
                CheckBoxList courseCheckBoxList = row.FindControl("Course") as CheckBoxList;
                string course = courseCheckBoxList != null ? GetSelectedCourses(courseCheckBoxList) : string.Empty;

                // Retrieve the updated Gender value from the RadioButtonList
                RadioButtonList genderRadioButtonList = row.FindControl("Gender") as RadioButtonList;
                string gender = genderRadioButtonList != null ? genderRadioButtonList.SelectedValue : string.Empty;

                // Retrieve the updated Blood Group value from the DropDownList
                DropDownList bloodGroupDropDown = row.FindControl("Bloodgroup") as DropDownList;
                string bloodGroup = bloodGroupDropDown != null ? bloodGroupDropDown.SelectedValue : string.Empty;

                // Retrieve the remaining values
                string phone = ((TextBox)row.Cells[7].Controls[0]).Text;
                string email = ((TextBox)row.Cells[8].Controls[0]).Text;
                string age = ((TextBox)row.Cells[9].Controls[0]).Text;
                string address = ((TextBox)row.Cells[10].Controls[0]).Text;

                // Update the data in the database
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "UPDATE STUDENT_INFO SET First_Name=@First_Name, Middle_Name=@Middle_Name, Last_Name=@Last_Name, Course=@Course, Gender=@Gender, BloodGroup=@BloodGroup, Phone=@Phone, Email=@Email, Age=@Age, Address=@Address WHERE Student_ID=@Student_ID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Student_ID", studentID);
                        cmd.Parameters.AddWithValue("@First_Name", firstName);
                        cmd.Parameters.AddWithValue("@Middle_Name", middleName);
                        cmd.Parameters.AddWithValue("@Last_Name", lastName);
                        cmd.Parameters.AddWithValue("@Course", course);
                        cmd.Parameters.AddWithValue("@Gender", gender);
                        cmd.Parameters.AddWithValue("@BloodGroup", bloodGroup);
                        cmd.Parameters.AddWithValue("@Phone", phone);
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Age", age);
                        cmd.Parameters.AddWithValue("@Address", address);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                // Exit edit mode after updating
                GridViewStudents.EditIndex = -1;
                LoadStudents();
            }
            catch (Exception ex)
            {
                // Handle exception and display user-friendly message
                Response.Write($"An error occurred while updating the record: {ex.Message}");
            }
        }


        protected void GridViewStudents_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                // Retrieve the Student_ID of the row being deleted
                string studentID = GridViewStudents.DataKeys[e.RowIndex].Value.ToString();

                // Delete the record from the database
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM STUDENT_INFO WHERE Student_ID=@Student_ID";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@Student_ID", studentID);
                        con.Open();
                        cmd.ExecuteNonQuery();
                    }
                }

                // Reload the GridView
                LoadStudents();
            }
            catch (Exception ex)
            {
                // Handle exception, log error, and display user-friendly message
                Response.Write($"An error occurred: {ex.Message}");
            }
        }


        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            Fname.Text = "";
            Mname.Text = "";
            Lname.Text = "";
            Gender.ClearSelection();
            Bloodgroup.ClearSelection();
            Phone.Text = "";
            Email.Text = "";
            Age.Text = "";
            Address.Text = "";
            foreach (ListItem item in Course.Items)
            {
                item.Selected = false;
            }
        }
    }
}
