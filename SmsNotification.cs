using System;
using System.Data.SQLite;
using System.Net.Http;
using RestSharp;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace TeacherPortal
{
    internal class SmsNotification
    {
        private const string ApiUrl = "https://sms.iprogtech.com/api/v1/sms_messages";
        private const string ApiToken = "9ba4c19eb66b1388b1547437ab990c3182f8fcf9";


        public async Task SendStudentNotificationsAsync()
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

                        await SendSmsAsync(studentContact, message);
                    }
                }
            }
        }

        private async Task SendSmsAsync(string phoneNumber, string message)
        {
            string internationalPhoneNumber = ConvertToInternationalFormat(phoneNumber);

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

            request.AddJsonBody(new
            {
                api_token = ApiToken,
                phone_number = internationalPhoneNumber,
                message = message,
                sms_provider = 1
            });

            try
            {
                var response = await client.ExecuteAsync(request);

                Console.WriteLine($"Response Status: {response.StatusCode}");
                Console.WriteLine($"Response Content: {response.Content}");

                if (response.IsSuccessful)
                {
                    Console.WriteLine($"SMS sent successfully to {internationalPhoneNumber}");
                }
                else
                {
                    Console.WriteLine($"Failed to send SMS to {internationalPhoneNumber}. Status: {response.StatusCode}, Error: {response.ErrorMessage}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred while sending SMS to {internationalPhoneNumber}. Exception: {ex.Message}");
            }
        }



        private string ConvertToInternationalFormat(string localPhoneNumber)
        {
            if (localPhoneNumber.StartsWith("0"))
            {
                return "+63" + localPhoneNumber.Substring(1);
            }
            else
            {
                return localPhoneNumber;
            }
        }


    }
}

