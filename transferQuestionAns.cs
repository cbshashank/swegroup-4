using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace transferQA
{
	// question structure
	class Question {
		public string term { get; set; };
		public string text { get; set; };
		public List<string> options;
		public List url;
		// public string[] options;
		// public string[] url;
	}

	// the goal of this program is to connect the backend table of the
	// plant urls to the middle end's client communication module
	// to populate the front end question-answer value choices
	class Program
	{
		static void Main(string[] args)
		{
		// have the data structures pre-setup
		List<Question> questions = new List<Question>();

			// connect to the OWL database
			SqlConnection conn = new SqlConnection("user id=username;" + "password=password;" + "server=.\\SQLExpress;" + "Trusted_Connection=yes;" + "database=OWL;" + "Connect Timeout=6000000");
            conn.Open();

			// build sql command (do a sort in the question and answers)
			SqlCommand cmd = new SqlCommand("SELECT question, question_text, answer, url from questionans order by question, answer");

			// execute sql command -> return object
			SqlDataReader reader = cmd.ExecuteReader();

			// check if object has rows
				if (reader.HasRows)
				{
					// keep track of the previous and current question
					string prev_question;
					string curr_question;
					// build each question at a time
					Question q;
					// keep track of the number of options and urls
					int count;

					// **** LATER - case switch for USState dropdown menu

					// if so, while reading row after row of SQL table
					while (reader.Read())
					{
						// question column in as curr_question
						curr_question = reader.GetString(reader.GetOrdinal("question"));

						// if question = term === "USState"
						// if (curr_question == "USState")
						// {
							// then populate the Question object like normal.
							// still pass a question object.
							// there will not be actual URLS in the url list
							// but then again, we won't use the url list
							// even it is in the client communication module
							// so, it is okay to store the USState question as Question object
						// }

						// if the curr_question is different from the prev_question
						// when you are seeing a new question
						if (curr_question != prev_question)
						{
							if (prev_question == "") // at start of SQL table
							{
								// create a new question object
								q = new Question();
								// put in question (= term)
								q.term = curr_question;
								// put in question_text (= text)
								q.text = reader.GetString(reader.GetOrdinal("question_text"));
								// put in the answer (=options[0])
								q.options = new List<string>();
								// put in the url (=url[0])
								q.url = new List<string>();
								q.options.Add(reader.GetString(reader.GetOrdinal("answer")));
								q.url.Add(reader.GetString(reader.GetOrdinal("url")));

								// then make prev_question = curr_question
								prev_question = curr_question;
							}
							else // in middle of SQL table
							{
								// add the last question
								questions.Add(q);

								// create a new question object
								q = new Question();
								// put in question (= term)
								q.term = curr_question;
								// put in question_text (= text)
								q.text = reader.GetString(reader.GetOrdinal("question_text"));
								// put in the answer (=options[0])
								q.options = new List<string>();
								// put in the url (=url[0])
								q.url = new List<string>();
								q.options.Add(reader.GetString(reader.GetOrdinal("answer")));
								q.url.Add(reader.GetString(reader.GetOrdinal("url")));

								// then make prev_question = curr_question
								prev_question = curr_question;
							}

						}
						else
						{
							// curr_question == prev_question

							// if not, continue adding to the curr_question's set of
							// options and those options' urls
							q.options.Add(reader.GetString(reader.GetOrdinal("answer")));
							q.url.Add(reader.GetString(reader.GetOrdinal("url")));
						}
					}
					
					// add the last question object to the list of questions
					questions.Add(q);

				}

			// convert list of question objects into a json object
			JObject json = new JObject(questions);

			// test
			Console.WriteLine("What the JSON objects look like in string indented format:");
			Console.WriteLine();
			Console.WriteLine(json.ToString(Formatting.Indented));
			Console.ReadLine();
		}
	}
}

