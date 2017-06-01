using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace DataBase
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var myBase = new SQLiteConnection("Data Source = MyBase.sqlite; Version = 3");
            myBase.Open();

            //Select
            var select = new SQLiteCommand("SELECT score, name FROM highscores WHERE score > 9", myBase);
            var reader = select.ExecuteReader();
                     while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                    Console.Write(reader[i] + "\t");
                Console.Write("\n");
                        //Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);

            }

            myBase.Close();
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            

        }

        private void create_Click(object sender, EventArgs e)
        {
            SQLiteConnection.CreateFile("MyBase.sqlite");
            var myBase = new SQLiteConnection("Data Source = MyBase.sqlite; Version = 3");
            myBase.Open();

            //Create table
            var createTab = new SQLiteCommand("CREATE TABLE Pracownik (nr_pracownik integer PRIMARY KEY," +
                                                "imie varchar(20)," +
                                                "nazwisko varchar(20)," +
                                                "pesel char(11)," +
                                                "adres varchar(30))", myBase);
            createTab.ExecuteNonQuery();

            createTab = new SQLiteCommand("CREATE TABLE Filia (nr_filia integer PRIMARYKEY," +
                                            "adres varchar(30))", myBase);
            createTab.ExecuteNonQuery();

            createTab = new SQLiteCommand("CREATE TABLE Zatrudnienie(nr_zatrudnienie integer PRIMARY KEY," +
                                            "data_rozpoczecia date," +
                                            "data_zakonczenia date," +
                                            "nr_pracownik integer REFERENCES Pracownik (nr_pracownik)," +
                                            "nr_filia integer REFERENCES Filia (nr_filia))", myBase);
            createTab.ExecuteNonQuery();

            createTab = new SQLiteCommand("CREATE TABLE Ksiazka(nr_ksiazka integer PRIMARY KEY," +
                                            "tytul varchar(30)," +
                                            "data_wydania date," +
                                            "nr_wydania integer," +
                                            "gatunek varchar(20))", myBase);
            createTab.ExecuteNonQuery();

            createTab = new SQLiteCommand("CREATE TABLE Egzemplarz(nr_egzemplarz integer PRIMARY KEY," +
                                            "data_uzyskania date," +
                                            "cena_uzyskania float(2)," +
                                            "nr_filia integer REFERENCES Filia(nr_filia)," +
                                            "nr_ksiazka integer REFERENCES Ksiazka(nr_ksiazka))", myBase);
            createTab.ExecuteNonQuery();

            createTab = new SQLiteCommand("CREATE TABLE Czytelnik(nr_czytelnik integer PRIMARY KEY," +
                                            "imie varchar(20)," +
                                            "nazwisko varchar(20)," +
                                            "pesel char(11)," +
                                            "adres varchar(30))", myBase);
            createTab.ExecuteNonQuery();

            createTab = new SQLiteCommand("CREATE TABLE Wypozyczenie( nr_wypozyczenie integer PRIMARY KEY," +
                                            "data_zwrotu date," +
                                            "nr_czytelnik integer REFERENCES Czytelnik(nr_czytelnik)," +
                                            "nr_pracownik integer REFERENCES Pracownik(nr_pracownik)," +
                                            "nr_filia integer REFERENCES Filia(nr_filia)," +
                                            "nr_egzemplarz integer REFERENCES Egzemplarz(nr_egzemplarz))", myBase);
            createTab.ExecuteNonQuery();

            myBase.Close();

            var but = sender as Button;
            but.Enabled = false;
        }

        private void fill_Click(object sender, EventArgs e)
        {
            var myBase = new SQLiteConnection("Data Source = MyBase.sqlite; Version = 3");
            myBase.Open();

            //PRACOWNIK
            var insert = new SQLiteCommand("insert into Pracownik(nr_pracownik, imie, nazwisko, pesel, adres)" +
                                            "values(1,'Anna', 'Annowicz', 'aaaaaaaaaaa', 'ACity')", myBase);
            insert.ExecuteNonQuery();

            insert = new SQLiteCommand("insert into Pracownik(nr_pracownik, imie, nazwisko, pesel, adres)" +
                                       "values(2, 'Beata', 'Beatowicz', 'bbbbbbbbbbb', 'BCity')", myBase);
            insert.ExecuteNonQuery();

            insert = new SQLiteCommand("insert into Pracownik(nr_pracownik, imie, nazwisko, pesel, adres)" +
                                       "values(3, 'Celina', 'Celinowicz', 'ccccccccccc', 'CCity')", myBase);
            insert.ExecuteNonQuery();

            //CZYTELNIK
            insert = new SQLiteCommand("insert into Czytelnik(nr_czytelnik, imie, nazwisko, pesel, adres)" +
                                            "values(1, 'Andrzej', 'Andrzejczyk', 'aaaaaaaaaaa', 'ATown')", myBase);
            insert.ExecuteNonQuery();

            insert = new SQLiteCommand("insert into Czytelnik(nr_czytelnik, imie, nazwisko, pesel, adres)" +
                                            "values(2, 'Bartek', 'Bartczyk', 'bbbbbbbbbbb', 'BTown')", myBase);
            insert.ExecuteNonQuery();

            insert = new SQLiteCommand("insert into Czytelnik(nr_czytelnik, imie, nazwisko, pesel, adres)" +
                                            "values(3, 'Czarek', 'Czarczyk', 'ccccccccccc', 'CTown')", myBase);
            insert.ExecuteNonQuery();

            //FILIA
            insert = new SQLiteCommand("insert into Filia(nr_filia, adres)" +
                                            "values(1, 'ACity')", myBase);
            insert.ExecuteNonQuery();

            insert = new SQLiteCommand("insert into Filia(nr_filia, adres)" +
                                            "values(2, 'BCity')", myBase);
            insert.ExecuteNonQuery();

            //ZATRUDNIENIE
            insert = new SQLiteCommand("insert into Zatrudnienie(nr_zatrudnienie, data_rozpoczecia, data_zakonczenia, nr_pracownik, nr_filia)" +
                                            "values(1, '2011-12-10','2011-12-10', 1, 1)", myBase);
            insert.ExecuteNonQuery();

            insert = new SQLiteCommand("insert into Zatrudnienie(nr_zatrudnienie, data_rozpoczecia, data_zakonczenia, nr_pracownik, nr_filia)" +
                                            "values(2, '2002-01-01','2002-12-01', 2, 2)", myBase);
            insert.ExecuteNonQuery();

            insert = new SQLiteCommand("insert into Zatrudnienie(nr_zatrudnienie, data_rozpoczecia, data_zakonczenia, nr_pracownik, nr_filia)" +
                                            "values(3, '2003-01-01','2003-12-01', 3, 1)", myBase);
            insert.ExecuteNonQuery();

            //KSIAZKA
            insert = new SQLiteCommand("insert into Ksiazka(nr_ksiazka, tytul, data_wydania, nr_wydania, gatunek)" +
                                            "values(1, 'LoTR', '2011-12-10', 3, 'fantasy')", myBase);
            insert.ExecuteNonQuery();

            insert = new SQLiteCommand("insert into Ksiazka(nr_ksiazka, tytul, data_wydania, nr_wydania, gatunek)" +
                                            "values(2, 'Krzyzacy','2012-12-10', 5, 'historyczna')", myBase);
            insert.ExecuteNonQuery();

            insert = new SQLiteCommand("insert into Ksiazka(nr_ksiazka, tytul, data_wydania, nr_wydania, gatunek)" +
                                            "values(3, 'Quo Vad','2013-12-10', 1, 'historyczna')", myBase);
            insert.ExecuteNonQuery();

            //EGZEMPLARZ
            insert = new SQLiteCommand("insert into Egzemplarz(nr_egzemplarz, data_uzyskania, cena_uzyskania, nr_filia, nr_ksiazka)" +
                                            "values(1, '2004-12-10', 40.00, 2, 1)", myBase);
            insert.ExecuteNonQuery();

            insert = new SQLiteCommand("insert into Egzemplarz(nr_egzemplarz, data_uzyskania, cena_uzyskania, nr_filia, nr_ksiazka)" +
                                            "values(2, '2005-12-10', 19.99, 1, 2)", myBase);
            insert.ExecuteNonQuery();

            insert = new SQLiteCommand("insert into Egzemplarz(nr_egzemplarz, data_uzyskania, cena_uzyskania, nr_filia, nr_ksiazka)" +
                                            "values(3, '2006-12-10', 26.00, 1, 3)", myBase);
            insert.ExecuteNonQuery();

            //WYPOZYCZENIE

            insert = new SQLiteCommand("insert into Wypozyczenie(nr_wypozyczenie, data_zwrotu, nr_czytelnik, nr_pracownik, nr_filia, nr_egzemplarz)" +
                                            "values(1, '2016-10-10', 1, 2, 2, 2)", myBase);
            insert.ExecuteNonQuery();

            insert = new SQLiteCommand("insert into Wypozyczenie(nr_wypozyczenie, data_zwrotu, nr_czytelnik, nr_pracownik, nr_filia, nr_egzemplarz)" +
                                            "values(2, '2016-9-9', 2, 1, 1, 3)", myBase);
            insert.ExecuteNonQuery();

            insert = new SQLiteCommand("insert into Wypozyczenie(nr_wypozyczenie, data_zwrotu, nr_czytelnik, nr_pracownik, nr_filia, nr_egzemplarz)" +
                                            "values(3, '2016-8-8', 3, 3, 1, 1)", myBase);
            insert.ExecuteNonQuery();
            
            myBase.Close();


            var but = sender as Button;
            but.Enabled = false;
        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
                return;

            output.Text = String.Empty;

            var myBase = new SQLiteConnection("Data Source = MyBase.sqlite; Version = 3");
            myBase.Open();

            String input = richTextBox1.Text;

            var select = new SQLiteCommand(input, myBase);

            try
            {
                var reader = select.ExecuteReader();

                while (reader.Read())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                       output.Text += reader[i] + "\t";

                    output.Text += "\n";
                }

                    //Console.WriteLine("Name: " + reader["name"] + "\tScore: " + reader["score"]);
            }
            catch (Exception)
            {

                output.Text = "ERROR";
            }
           

            myBase.Close();
        }
    }
}
