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
using System.Numerics;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using RagnarockWebsocket;
using RagnarockWebsocket.Enums;
using System.Media;
using WatsonWebsocket;
using RagnarockWebsocketCore.Data;
using RagnarockWebsocketCore.Enums;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

/*
 TODO:
    1. Implement WebSocket connection from and to the game
    2. Make a SQLite connection
    3. Close connection on exit
    4. Implement backup feature that copies the previous data and saves it as a dated file
    5. Save data from WS connection to the SQLite DB
    6. TBD

 Flow:
    1. Establish connection to the game via WebSocket connection (127.0.0.1:8033)
    --- Start of loop ---
    2. Retrieve data as JSON / Request data as JSON
    3. Deserialize data received
    4. Parse data
    5. Record data when song ends
    --- End of loop ---
    6. Close connection
 */


namespace RagnaLogger
{
    public partial class MainScreen : Form
    {
        public MainScreen()
        {
            InitializeComponent();
            this.Load += MainScreen_Load;
            InitializeWS();
        }

        // Let's hope I understood it correctly
        // This currently uses a "blind" singleton approach
        public static RagnaWS ServerSocket { get; private set; }

        public static void InitializeWS()
        {
            if (ServerSocket == null)
            {
                // Note: The connection should be made using the address in config
                // If empty, it should use the default address
                ServerSocket = new RagnaWS("http://localhost:8033", ConnectionMode.Server);
            }
        }

        private void MainScreen_Load(object sender, EventArgs e)
        {
            // A separate thread from the stock Load event for experiment
            // Likely to be moved into the stock Load event after it is over

            // Note: The connection should be made using the address in config
            // If empty, it should use the default address
            // RagnaWS ServerSocket = new RagnaWS("http://localhost:8033", ConnectionMode.Server);

            ServerSocket.Connected += GameConnected;
            ServerSocket.Disconnected += GameDisconnected;
            ServerSocket.Message += OnMessageReceived;
            ServerSocket.BeatHit += OnBeatHit;
            ServerSocket.BeatMiss += OnBeatMiss;
            ServerSocket.ComboTriggered += OnComboTriggered;
            ServerSocket.StartSong += OnStartSong;
            ServerSocket.SongInfos += OnReceiveSongInfo;
            ServerSocket.Score += OnReceiveScore;
        }

        public class AppDbContext : DbContext
        {
            public DbSet<LogEntry> LogEntries { get; set; }
            protected override void OnConfiguring(DbContextOptionsBuilder options)
            {
                options.UseSqlite("Data Source=events.db");
            }
        }

        /// <summary>
        /// Please bind this function to a button on the UI.
        /// </summary>
        private void ResetConnection() 
        { 
            ServerSocket.RestartConnection();
        }
        private void GameConnected()
        {
            // Things to do when it is connected
            Console.WriteLine("Connected to game");
        }

        private void GameDisconnected() 
        { 
            // Things to do when it is disconnected
            // Requires resetting AND writing to the DB at this point
            Console.WriteLine("Game disconnected");
        }

        private void OnMessageReceived(string eventName, JToken data) 
        {
            // On message, do...
            // This is the catch-all handler for the connection
            // You can probably just do switch-case with this thing and not bother with other objects and events
            // Please note: This function is timing critical. 
            // Offload functions and actions that require heavy load (i.e. disk write) to another function after the period this event is expected to end.

            Console.WriteLine("Got message: {0} - {1}", eventName, data);
        }

        private void OnStartSong(StartSongData data)
        {
            // On song start, do...
            Console.WriteLine("Race begins");
            Console.WriteLine("Info: {0} by {1}", data.songTitle, data.songArtist);
        }
        private void OnComboTriggered(ComboTriggeredData data)
        {
            // When the player triggers the combo, do...
            // Perhaps something like storing the event as an array of timestamps could come in handy
            Console.WriteLine("Combo triggered");
        }
        private void OnComboLost(ComboLostData data)
        {
            // When the player loses the combo, do...
            // Perhaps something like storing the event as an array of timestamps could come in handy

            Console.WriteLine("Combo lost");
        }
        private void OnBeatHit(BeatHitData data)
        {
            // When the player hits the note, do...
            // Perhaps something like storing the event as an array of timestamps could come in handy
            // Please note: This function is timing critical. 
            // Offload functions and actions that require heavy load (i.e. disk write) to another function after the period this event is expected to end.

            Console.WriteLine("Note hit");
        }

        private void OnBeatMiss(BeatMissData data)
        {
            // When the player misses the note, do...
            // Perhaps something like storing the event as an array of timestamps could come in handy
            // Please note: This function is timing critical. 
            // Offload functions and actions that require heavy load (i.e. disk write) to another function after the period this event is expected to end.

            Console.WriteLine("Note missed");
        }

        private void OnSongEnd(EndSongData data)
        {
            // On song end, do...
            // Does not count quits. Disconnect event happens on quit too, while this doesn't.
            Console.WriteLine("Race finished");
        }

        private void OnReceiveScore(ScoreData data)
        {
            // When you receive the score, do...
            Console.WriteLine("Score tallied");
            Console.WriteLine("Scores: Distance:{0}, Hit:{1}, Missed:{2}, Perfects:{3}%, Blue Combos:{4}, Yellow Combos:{5}, Hit {6}% of notes, Average offset: {7}", data.distance, data.stats.hit, data.stats.missed, data.stats.percentageOfPerfects, data.stats.comboBlue, data.stats.comboYellow, data.stats.hitPercentage, data.stats.hitDeltaAverage);
        }

        private void OnReceiveSongInfo(SongInfosData data)
        {
            // When you receive the song info, do...
            // This is a reply from the game when you request the info.
            Console.WriteLine("Got current song info");
            Console.WriteLine("Info: {0} by {1}", data.songTitle, data.songArtist);
            // You probably need to install file hook to the game's log to get info such as paths (OSTs starting with "../") and map IDs which are important for not polluting the records
        }

        // End of event receivers

        // Start of event emitters

        /// <summary>
        /// This function should emit Ahou event.
        /// A macro function for that event because it's a bit too long to type.
        /// Otherwise it has no other function.
        /// </summary>
        private void EmitEventAhou(int ID) {
            // Make the rowers go AHOU
            Console.WriteLine("AHOU requested with ID {0}", ID);
            // ServerSocket.AHOU(rowersID);
            // Macro layer for this function. Only used to make things shorter.
            
            //(12) (13)
            // V     V
            // 3     7 <- Fourth (11)
            // 2     6 <- Third (10)
            // 1     5 <- Second (9)
            // 0     4 <- First (8)
            //  [YOU]
            // 14 or other values trigger all rowers
            switch (ID)
            {
                case 0:
                    ServerSocket.AHOU(Rowers.FirstRowRight);
                    break;
                case 1:
                    ServerSocket.AHOU(Rowers.SecondRowRight);
                    break;
                case 2:
                    ServerSocket.AHOU(Rowers.ThirdRowRight);
                    break;
                case 3:
                    ServerSocket.AHOU(Rowers.FourthRowRight);
                    break;
                case 4:
                    ServerSocket.AHOU(Rowers.FirstRowLeft);
                    break;
                case 5:
                    ServerSocket.AHOU(Rowers.SecondRowLeft);
                    break;
                case 6:
                    ServerSocket.AHOU(Rowers.ThirdRowLeft);
                    break;
                case 7:
                    ServerSocket.AHOU(Rowers.FourthRowLeft);
                    break;
                case 8:
                    ServerSocket.AHOU(RowersUtil.FirstRow);
                    break;
                case 9:
                    ServerSocket.AHOU(RowersUtil.SecondRow);
                    break;
                case 10:
                    ServerSocket.AHOU(RowersUtil.ThirdRow);
                    break;
                case 11:
                    ServerSocket.AHOU(RowersUtil.FourthRow);
                    break;
                case 12:
                    ServerSocket.AHOU(RowersUtil.LeftSide);
                    break;
                case 13:
                    ServerSocket.AHOU(RowersUtil.RightSide);
                    break;
                default:
                    ServerSocket.AHOU(RowersUtil.All);
                    break;
            }

        }

        /// <summary>
        /// This function should emit song info event. It takes in no parameter.
        /// </summary>
        private void EmitEventInfo()
        {
            // Tell game to give us currently playing song info
            // Probably not going to be very useful
            Console.WriteLine("Requested song info");
            ServerSocket.CurrentSong();
        }

        /// <summary>
        /// This function should pop up the message on the screen.
        /// </summary>
        /// <param name="dialogID">A random traceable number.</param>
        /// <param name="title">The title of the message.</param>
        /// <param name="location">Where in the virtual area the message should appear (X, Y, Z)</param>
        /// <param name="message">The message body itself.</param>
        /// <param name="duration">How long it should last. n > 1</param>
        private void EmiteEventPopup(string dialogID, string title, System.Numerics.Vector3 location, string message, double duration) 
        {
            // Emit popup event 
            Console.WriteLine("Requested popup");
            ServerSocket.DisplayDialogPopup(dialogID, title, location, message, duration);
        }

        // End of event emitters

        /*
         * WAIT! Before you say anything, this is my first ever semi-serious
         * C# project I made, so please, think of this as junior year CS project.
         * It will be easier that way, trust me on this one.
         * Love xoxo,
         * Blackbeard
         */

        Settings settings = new Settings(); // Note: You need to write stuff to this object later.
        RagnaStats stats = new RagnaStats(); // Structure of this object may change later

        private string CheckPath(string path) {
            // Check if the path ends with ".sqlite"
            Regex regexStr = new Regex(@"[0-9A-Za-z]*\.sqlite");
            if (path == null) { 
                // No need to check, throw an error 
                // (for now it will cause an exception but it should handle it more gracefully)
                throw new ArgumentNullException("path");
            } 
            else
            {
                if (regexStr.IsMatch(path))
                {
                    // Path ends with ".sqlite", return it
                    return path;
                }
                else {
                    // Ditto, needs a better error handler here
                    throw new ArgumentException("path");
                }
            }
        }

        private bool CheckOneDrive(string path) 
        {
            Regex regex = new Regex(@"OneDrive");
            if (path == null) 
            { 
                // Path is null so we don't process it further
                // Needs a better error handler here
                throw new ArgumentNullException("path");
            } 
            else
            {
                if (!regex.IsMatch(path))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        private string ParseJSONSettings(string jsonString) {
            // Do stuff with Newtonsoft.Json here
            return null;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Something to do when this form loads
        }

        private void exportCSVBtn_Click(object sender, EventArgs e)
        {
            // TODO: Make a CSV dump of the DB
            // Form:
            /*
             * Song Name,Type,Date,Score
             * SomeSong1,OST,YYYY-MM-DD HH:MM:SS
             * SomeSong2,OST,YYYY-MM-DD HH:MM:SS
             * SomeSong3,OST,YYYY-MM-DD HH:MM:SS
             * SomeSong4,Custom,YYYY-MM-DD HH:MM:SS
             * SomeSong5,Custom,YYYY-MM-DD HH:MM:SS
             * SomeSong6,Custom,YYYY-MM-DD HH:MM:SS
             */

            // Example:
            /*
             * Song Name,Type,Date,Score
             * SomeSong1,OST,2020-01-02 12:00:00
             * SomeSong2,OST,2021-02-03 01:01:11
             * SomeSong3,OST,2022-03-04 02:02:22
             * SomeSong4,Custom,2023-04-05 03:03:33
             * SomeSong5,Custom,2024-05-06 04:04:44
             * SomeSong6,Custom,2025-06-07 05:05:55
             */
        }

        private void exportTXTBtn_Click(object sender, EventArgs e)
        {
            // TODO: Make a Text dump of the DB
            // Form:
            /*
             * RagnaLogger Report as of YYYY-MM-DD HH:MM:SS
             * 
             * [Song Name] (OST/Custom):
             *     1. Score.Decimal (YYYY-MM-DD HH:MM:SS)
             *     2. Score.Decimal (YYYY-MM-DD HH:MM:SS)
             *     ...
             *     99. Score.Decimal (YYYY-MM-DD HH:MM:SS)
             * ...
             * [Song Name] (OST/Custom):
             *     1. Score.Decimal (YYYY-MM-DD HH:MM:SS)
             *     2. Score.Decimal (YYYY-MM-DD HH:MM:SS)
             *     ...
             *     99. Score.Decimal (YYYY-MM-DD HH:MM:SS)
             * 
             * -- End of List --
             */
            // Example:
            /*
             * RagnaLogger Report as of 2025-02-19 21:40:45
             * 
             * [Welcome to Asgard] (OST):
             *     1. 1234.56 (2020-01-02 03:04:05)
             *     2. 2345.67 (2021-02-03 04:05:06)
             *     ...
             *     99. 8901.23 (2025-02-18 23:59:59)
             * ...
             * [Sk8ter Boi] (Custom):
             *     1. 1234.56 (2020-01-02 03:04:05)
             *     2. 2345.67 (2021-02-03 04:05:06)
             *     ...
             *     99. 8901.23 (2025-02-18 23:59:59)
             *     
             * -- End of List --
             */
        }

        private void makeReportBtn_Click(object sender, EventArgs e)
        {
            // TODO: Open window for reportViewer.cs
        }

        private void backupNowBtn_Click(object sender, EventArgs e)
        {
            // TODO: Make a copy of the DB file as filename_YYYYMMDD_HHMMSS.sqlite when invoked
        }

        private void playSound(int type)
        {
            // Switch-case would be useful here
            switch (type)
            {
                // Send something other than 0 or 1 to play nothing
                case 0:
                    // Connect
                    SoundPlayer audio1 = new SoundPlayer(RagnaLogger.Properties.Resources.connectedSnd);
                    audio1.PlaySync();
                    break;
                case 1:
                    // Disconnect
                    SoundPlayer audio2 = new SoundPlayer(RagnaLogger.Properties.Resources.disconnectSnd);
                    audio2.PlaySync();
                    break;
                default:
                    break;
            }
            }

        private void resetBtn_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("I am the creator, I am the destroyer, I create and I destroy. I make the world I live in, I destroy this world I created. I shall bring my own downfall with my hands, not by other being, and fade away without trace when it is all done. You will remember me not, for I am flawed.\n- The Sun Drinker", "Message");
        }
    }
    // WARNING: This code is absolutely fucking stupid.
    // Are your eyes okay? I know mine aren't...

    public class Settings
    {
        public string DbPath { get; set; }
        public string WebSocketPath { get; set; }
    }
    public class RagnaStats { 
        public double Accuracy { get; set; }
        public double Deviation { get; set; }
        public double MissedNotes { get; set; }
        public double TotalNotesHit { get; set; }
        public double CombosTriggered { get; set; }
        public string SongTitle { get; set; }
        public string SongAuthor { get; set; }
        public double Distance  { get; set; }
        public bool RaceStatus { get; set; }
    }
    public class LogEntry {
        [Key]
        public int Id { get; set; } // Sequential number, perhaps (this may be left unused)
        public string EventType { get; set; } // The "event" field
        public string Data { get; set; } // Some string, either raw string or JSON data
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
    public class SongEntry {
        [Key]
        public string MapID { get; set; } // The hash of the map, refer to some code that hashes the maps for custom maps
        public string MapAuthor { get; set; } // Who the author of the map was, fetched from the file being loaded
        public string SongName { get; set; } // The name of the song that was played
        public string SongAuthor { get; set; } // The author of this song played
        public int DifficultyPlayed { get; set; } // The difficulty played in this session
        public long BeatsHit { get; set; } // Total notes hit, renamed for consistency
        public long TotalBeats {  get; set; } // Total notes in the map, renamed for consistency
        public int BlueCombos { get; set; } // Blue combos
        public int YellowCombos { get; set; } // Yellow combos; This and the blue combos are added together to get total combos
        public long DistanceTraveled { get; set; } // Distance (score)
        public long PerfectHits { get; set; }
        public long HitDelta { get; set; }
        public string MapType { get; set; } // "OST" or "Custom"; If the path started with "../../../" it is OST, else it is Custom
        public string MapInfoData { get; set; } // JSON data about this map
        /*
         * { "mapInfo": 
         *  { 
         *      "songName": "Dreamscape",
         *      "songAuthor": "009 Sound System",
         *      "mapDifficulties": [1, 3, 7],
         *      "mapHash": "0123456789ABCDEF",
         *      "mapType": "Custom",
         *      "mapAuthor": "Blackbeard",
         *      "mapLength": 9876543,
         *      "mapTotalNotes": 1234,
         *      "mapEditor": "MMA2",
         *      "mapDate": "2023-12-25",
         *      "mapBPM": 125,
         *      "mapEnvironment": "Asgard",
         *      "mapExplicit": false,
         *      "mapNoteJumpSpeeds": [12, 15, 20],
         *      "ranked": false
         *   }
         *  }
         */
        public string RaceType { get; set; } // Perhaps "Solo", "Online" or "LocalMulti" if we can detect that (if not possible, this will be removed)
        public DateTime Timestamp { get; set; } // Timestamp of the record
    }
}