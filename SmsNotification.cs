using System;
using System.Data.SQLite;
using System.Net.Http;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace TeacherPortal
{
    internal class SmsNotification
    {
        private const string ApiUrl = "https://sms.iprogtech.com/api/v1/sms_messages";
        private const string ApiToken = "9ba4c19eb66b1388b1547437ab990c3182f8fcf9";
        public void SendStudentNotifications()
        {
            var dbConnection = new DBConnection();

            using (var connection = dbConnection.GetConnection)
            {
                connection.Open();

                string query = @"
                    SELECT 
                        s.lname || ', ' || s.fname || ' ' || s.mname AS StudentName,
                        u.fname || ' ' || u.lname AS TeacherName,
                        COALESCE(att.total_present, 0) AS PresentCount,
                        COALESCE(att.total_absent, 0) AS AbsentCount,
                        COALESCE(
                            (SELECT ROUND(AVG(g.totalGrade), 2) FROM tblGrade g WHERE g.lrn = s.lrn),
                            0
                        ) AS TotalGrades,
                        s.contact AS StudentContact
                    FROM tblstudent s
                    JOIN tblenrollment e ON s.lrn = e.lrn
                    JOIN tblsection sec ON e.sectionid = sec.id
                    JOIN tbluser u ON sec.adviserID = u.username
                    LEFT JOIN vwStudentAttendance att ON s.lrn = att.lrn 
                         AND att.month = strftime('%Y-%m', DATE('now'))
                    WHERE e.status = 'Enrolled'
                    GROUP BY s.lrn;
                ";

                using (var command = new SQLiteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string studentName = reader["StudentName"].ToString();
                        string teacherName = reader["TeacherName"].ToString();
                        int presentCount = Convert.ToInt32(reader["PresentCount"]);
                        int absentCount = Convert.ToInt32(reader["AbsentCount"]);
                        double totalGrades = Convert.ToDouble(reader["TotalGrades"]);
                        string studentContact = reader["StudentContact"].ToString();

                        string message = $"Hello {studentName},\n" +
                                         $"Teacher: {teacherName}\n" +
                                         $"Attendance - Present: {presentCount}, Absent: {absentCount}\n" +
                                         $"Average Grade: {totalGrades}";

                        SendSms(studentContact, message);
                    }
                }
            }
        }

        private void SendSms(string phoneNumber, string message)
        {
            var options = new RestClientOptions(ApiUrl)
            {
                ThrowOnAnyError = true,
                Timeout = TimeSpan.FromMilliseconds(10000)
            };

            var client = new RestClient(options);
            var request = new RestRequest
            {
                Method = Method.Post
            };

            request.AddHeader("Authorization", $"Bearer {ApiToken}");
            request.AddJsonBody(new
            {
                phone_number = phoneNumber,
                message = message
            });

            try
            {
                var response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    Console.WriteLine($"SMS sent successfully to {phoneNumber}");
                }
                else
                {
                    Console.WriteLine($"Failed to send SMS to {phoneNumber}. Status: {response.StatusCode}, Error: {response.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred while sending SMS to {phoneNumber}. Exception: {ex.Message}");
            }
        }
    }
}
