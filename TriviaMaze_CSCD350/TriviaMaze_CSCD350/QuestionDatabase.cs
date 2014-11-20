using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace TriviaMaze_CSCD350 {
    class QuestionDatabase {

        /* [DATABASE SETUP]
         * Questions: QIndex SMALLINT, QType SMALLINT, QAuxiliary SMALLINT, QAuxFile VARCHAR(255), QText VARCHAR(255),
         * QAnswer SMALLINT, QOption1 VARCHAR(64), QOption2 VARCHAR(64), QOption3 VARCHAR(64), QOption4 VARCHAR(64)
         */

        SQLiteConnection dbConnection;
        ArrayList dbIgnore = new ArrayList();
        string dbPath;
        int dbEntries;
        Random rand = new Random();

        //=====================================================================
        // Create the database object, open it, and count entries
        public QuestionDatabase(string dbPath) {
            this.dbPath = dbPath;
            if (OpenDatabase())
                CountEntries();
        }

        //=====================================================================
        // Create a new database connection, open it, and test the connection (returns bool type for success flag)
        public bool OpenDatabase() {
            dbConnection = new SQLiteConnection("Data Source=" + dbPath + ";Version=3;");
            dbConnection.Open();

            try { // Try a simple SQL command to check for open success
                string sql = "SELECT QIndex FROM Questions WHERE QIndex = 1";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
            }
            catch (SQLiteException) {
                return false;
            }
            return true;
        }

        //=====================================================================

        public void CloseDatabase() {
            dbConnection.Close();
        }

        //=====================================================================

        public Question RandomQuestion() {

            int index = -1;
            do {
                index = rand.Next(dbEntries) + 1;
            } while (!ValidIndex(index));

            //dbIgnore.Add(index);

            return GetQuestion(index);
        }

        //=====================================================================
        // Grabs a question object from the database and returns it.
        public Question GetQuestion(int index) {

            if (!HasQuestions() || !ValidIndex(index))
                return null;

            string sql = "SELECT * FROM Questions WHERE QIndex = " + index;
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();

            Question tempQuestion = new Question();

            tempQuestion.SetQType(Convert.ToInt32("" + reader["QType"]));
            tempQuestion.SetAuxiliary(Convert.ToInt32("" + reader["QAuxiliary"]));
            tempQuestion.SetAuxFile("" + reader["QType"]);
            tempQuestion.SetText("" + reader["QText"]);
            tempQuestion.SetAnswer(Convert.ToInt32(reader["QAnswer"]));
            tempQuestion.SetChoices(new String[4] { "" + reader["QOption1"], "" + reader["QOption2"], "" + reader["QOption3"], "" + reader["QOption4"] });

            return tempQuestion;
        }

        //=====================================================================

        public void AddQuestionToDatabase(Question q) {
            int index = dbEntries + 1;
            String sql = "INSERT INTO Questions (QIndex,QType,QAuxiliary,QAuxFile,QText,QAnswer,QOption1,QOption2,QOption3,QOption4) " +
                         "VALUES (" + index + ", " + q.GetQType() + ", " + q.GetAuxiliary() + ", \"" + q.GetAuxFile() + "\", \"" +
                         q.GetText() + "\", " + q.GetAnswer() + ", \"" + q.GetChoices()[0] + "\", \"" + q.GetChoices()[1] + "\", \"" +
                         q.GetChoices()[2] + "\", \"" + q.GetChoices()[3] + "\")";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();
            dbEntries++;
        }

        //=====================================================================

        public void DeleteQuestionFromDatabase(int index) {
            if (index < 1 || index > dbEntries)
                return;
            String sql = "DELETE FROM Questions WHERE QIndex = " + index;
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();
            if (index != dbEntries) {
                sql = "UPDATE Questions SET QIndex = " + index + " WHERE QIndex = " + dbEntries;
                command = new SQLiteCommand(sql, dbConnection);
                command.ExecuteNonQuery();
            }
            dbEntries--;
        }

        //=====================================================================

        private bool HasQuestions() {
            if (dbIgnore.Count == dbEntries) {
                Console.WriteLine("No more questions!");
                return false;
            }
            return true;
        }

        //=====================================================================

        private bool ValidIndex(int index) {
            if (dbIgnore.IndexOf(index) == -1)
                return true;
            return false;
        }

        //=====================================================================

        private void CountEntries() {
            int entryCount = 0;
            string sql = "SELECT * FROM Questions";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                entryCount++;
            dbEntries = entryCount;
        }

        //=====================================================================

        private void UnignoreAll() {
            dbIgnore.Clear();
        }

        //=====================================================================
    }
}
