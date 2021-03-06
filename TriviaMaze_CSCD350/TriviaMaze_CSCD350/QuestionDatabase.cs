﻿/* Twenty Hats
 * Nathan Graham, Kyle Johnson, Daniel Moore, Eric Laib
 * CSCD 350
 * 
 * Class - QuestionDatabase
 */

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
         * Questions: QIndex SMALLINT, QType SMALLINT, QText VARCHAR(255), QAnswer SMALLINT,
         * QOption1 VARCHAR(64), QOption2 VARCHAR(64), QOption3 VARCHAR(64), QOption4 VARCHAR(64),
         * QAuxiliary SMALLINT, QAuxFile VARCHAR(255)
         */

        SQLiteConnection dbConnection;
        ArrayList dbIgnore = new ArrayList();
        string dbPath;
        int dbEntries;
        Random rand = new Random();
        bool validConnection;

        private static QuestionDatabase dbInstance = null;

        public static QuestionDatabase GetInstance() {
            if (dbInstance == null)
                dbInstance = new QuestionDatabase("../../gameqs.db");
            return dbInstance;
        }

        //=====================================================================
        //Create the database object, open it, and count entries
        private QuestionDatabase(string dbPath) {
            this.dbPath = dbPath;
            validConnection = OpenDatabase();
            dbEntries = CountEntries();
        }

        //=====================================================================
        //Create a new database connection, open it, and test the connection (returns bool type for success flag)
        private bool OpenDatabase() {
            dbConnection = new SQLiteConnection("Data Source=" + dbPath + ";Version=3;");
            dbConnection.Open();

            try { // Try a simple SQL command to check for open success
                string sql = "SELECT QIndex FROM Questions WHERE QIndex = 1";
                SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                SQLiteDataReader reader = command.ExecuteReader();
            } catch (SQLiteException) {
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

            if (!validConnection)
                return null;

            if (!HasQuestions()) //If no more new questions, repeat one
                return GetQuestion(rand.Next(dbEntries) + 1);

            int index = -1;
            do {
                index = rand.Next(dbEntries) + 1;
            } while (!ValidIndex(index));

            dbIgnore.Add(index);

            return GetQuestion(index);
        }

        //=====================================================================
        //Grabs a question object from the database and returns it.
        public Question GetQuestion(int index) {

            if (!validConnection || (index < 1 || index > dbEntries))
                return null;

            string sql = "SELECT * FROM Questions WHERE QIndex = " + index;
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();

            Question tempQuestion = QuestionFromType(Convert.ToInt32("" + reader["QType"]));

            tempQuestion.SetText("" + reader["QText"]);
            tempQuestion.SetAnswer(Convert.ToInt32(reader["QAnswer"]));
            tempQuestion.SetChoice(1, "" + reader["QOption1"]);
            tempQuestion.SetChoice(2, "" + reader["QOption2"]);
            tempQuestion.SetChoice(3, "" + reader["QOption3"]);
            tempQuestion.SetChoice(4, "" + reader["QOption4"]);
            tempQuestion.SetAuxiliary(Convert.ToInt32("" + reader["QAuxiliary"]));
            tempQuestion.SetAuxFile("" + reader["QAuxFile"]);

            return tempQuestion;
        }

        //=====================================================================
         
        public void AddQuestionToDatabase(Question q) {

            if (!validConnection)
                return;

            int index = dbEntries + 1;
            String sql = "INSERT INTO Questions (QIndex,QType,QText,QAnswer,QOption1,QOption2,QOption3,QOption4,QAuxiliary,QAuxFile) " +
                         "VALUES (" + index + ", " + q.GetQType() + ", \"" + EscapeString(q.GetText()) + "\", " + q.GetAnswer() +
                         ", \"" + EscapeString(q.GetChoice(1)) + "\", \"" + EscapeString(q.GetChoice(2)) +
                         "\", \"" + EscapeString(q.GetChoice(3)) + "\", \"" + EscapeString(q.GetChoice(4)) +
                         "\", " + q.GetAuxiliary() + ", \"" + q.GetAuxFile() + "\")";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();
            dbEntries++;
        }

        //=====================================================================
         
        public void DeleteQuestionFromDatabase(int index) {

            if (!validConnection)
                return;

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
        //Sets existing question of ID index to contain values of Question object mod
        public void ModifyQuestionInDatabase(int index, Question mod) {

            if (!validConnection)
                return;

            if (index < 1 || index > dbEntries)
                return;

            String sql = "UPDATE Questions SET ";
            sql += "QType = " + mod.GetQType() + ", ";
            sql += "QText = \"" + EscapeString(mod.GetText()) + "\", ";
            sql += "QAnswer = " + mod.GetAnswer() + ", ";
            sql += "QOption1 = \"" + EscapeString(mod.GetChoice(1)) + "\", ";
            sql += "QOption2 = \"" + EscapeString(mod.GetChoice(2)) + "\", ";
            sql += "QOption3 = \"" + EscapeString(mod.GetChoice(3)) + "\", ";
            sql += "QOption4 = \"" + EscapeString(mod.GetChoice(4)) + "\", ";
            sql += "QAuxiliary = " + mod.GetAuxiliary() + ", ";
            sql += "QAuxFile = \"" + EscapeString(mod.GetAuxFile()) + "\" ";
            sql += "WHERE QIndex = " + index;
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();
        }

        //=====================================================================
         
        private bool HasQuestions() {
            if (dbIgnore.Count == dbEntries) {
                //Console.WriteLine("No more questions!");
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
         
        private int CountEntries() {

            if (!validConnection)
                return 0;

            int entryCount = 0;
            string sql = "SELECT * FROM Questions";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                entryCount++;
            return entryCount;
        }

        //=====================================================================
         
        public int GetEntries() {
            return dbEntries;
        }

        //=====================================================================
         
        private void UnignoreAll() {
            dbIgnore.Clear();
        }

        //=====================================================================
         
        private Question QuestionFromType(int type) {
            if (type == 1)
                return new QuestionShort();
            if (type == 2)
                return new QuestionTF();
            return new QuestionMulti();
        }

        //=====================================================================
        //Prevent SQL inqection by scrubbing strings
        private String EscapeString(String input) {
            input = input.Replace("\"","''");
            return input;
        }

        //=====================================================================
    }
}
