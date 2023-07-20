using System.Text.Json;
using System.Text.Json.Serialization;
using TrackerLibrary.Models;
using static TrackerLibrary.Models.StreetAction;

namespace TrackerLibrary.CRUD
{
    /// <summary>
    /// Class that overwrites Read/Write Methods, necessary for JSON De/Serialization;
    /// </summary>
    public class FullActionListConverter : JsonConverter<List<FullAction>>
    {

        public override List<FullAction> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.StartArray)
            {
                var actionList = new List<FullAction>();

                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndArray)
                        break;

                    if (reader.TokenType == JsonTokenType.StartObject)
                    {
                        var action = new FullAction();

                        while (reader.Read())
                        {
                            if (reader.TokenType == JsonTokenType.EndObject)
                                break;

                            if (reader.TokenType == JsonTokenType.PropertyName)
                            {
                                var propertyName = reader.GetString();

                                reader.Read();

                                switch (propertyName)
                                {
                                    case "Player":
                                        action.Player = reader.GetString();
                                        break;
                                    case "Act":
                                        action.Act = reader.GetString();
                                        break;
                                    case "Size":
                                        action.Size = reader.GetSingle();
                                        break;
                                    case "AI":
                                        action.AI = reader.GetSByte();
                                        break;
                                        // Handle other properties if necessary
                                }
                            }
                        }

                        actionList.Add(action);
                    }
                }

                return actionList;
            }

            throw new JsonException("Invalid JSON format for List<Action> property.");
        }

        public override void Write(Utf8JsonWriter writer, List<FullAction> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            foreach (var action in value)
            {
                writer.WriteStartObject();

                writer.WriteString("Player", action.Player);
                writer.WriteString("Act", action.Act);
                writer.WriteNumber("Size", action.Size);
                writer.WriteNumber("AI", action.AI);

                // Write other properties if necessary

                writer.WriteEndObject();
            }

            writer.WriteEndArray();
        }

        
    }

    /// <summary>
    /// Class that overwrites Read/Write Methods, necessary for JSON De/Serialization;
    /// </summary>
    public class PlayerActionListConverter : JsonConverter<PlayerActionList>
    {
        public override PlayerActionList Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.StartArray)
            {
                var actionList = new PlayerActionList();

                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndArray)
                        break;

                    if (reader.TokenType == JsonTokenType.StartObject)
                    {
                        var action = new PlayerAction();

                        while (reader.Read())
                        {
                            if (reader.TokenType == JsonTokenType.EndObject)
                                break;

                            if (reader.TokenType == JsonTokenType.PropertyName)
                            {
                                var propertyName = reader.GetString();

                                reader.Read();

                                switch (propertyName)
                                {
                                    case "Act":
                                        action.Act = reader.GetString();
                                        break;
                                    case "Size":
                                        action.Size = reader.GetSingle();
                                        break;
                                    case "AI":
                                        action.AI = reader.GetSByte();
                                        break;
                                        // Handle other properties if necessary
                                }
                            }
                        }

                        actionList.Add(action);
                    }
                }

                return actionList;
            }

            throw new JsonException("Invalid JSON format for List<Action> property.");
        }

        public override void Write(Utf8JsonWriter writer, PlayerActionList value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();

            foreach (var action in value)
            {
                writer.WriteStartObject();

                writer.WriteString("Act", action.Act);
                writer.WriteNumber("Size", action.Size);
                writer.WriteNumber("AI", action.AI);

                // Write other properties if necessary

                writer.WriteEndObject();
            }

            writer.WriteEndArray();
        }
    }

    /// <summary>
    /// Class that overwrites Read/Write Methods, necessary for JSON De/Serialization;
    /// </summary>
    public class SettingsConverter : JsonConverter<Settings>
    {
        public override Settings Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Settings settings = new Settings(); 

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                    break;

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();

                    reader.Read();

                    switch (propertyName)
                    {
                        case "Server":
                            settings.Server = reader.GetString();
                            break;
                        case "Port":
                            settings.Port = reader.GetString();
                            break;
                        case "User":
                            settings.User = reader.GetString();
                            break;
                        case "Password":
                            settings.Password = reader.GetString();
                            break;
                        case "MinTorney":
                            settings.MinTourney = reader.GetString();
                            break;
                        case "RegFile":
                            settings.RegFile = reader.GetString();
                            break;
                        case "HhSplitSize":
                            settings.HhSplitSize = reader.GetString();
                            break;
                        case "CurrentDbRead":
                            settings.CurrentDbRead = reader.GetString();
                            break;
                        case "SqlDatabase":
                            settings.SqlDatabase = reader.GetString();
                            break;
                        case "NosqlDatabase":
                            settings.NosqlDatabase = reader.GetString();
                            break;
                        case "DbTypeWrite":
                            settings.DbTypeWrite = reader.GetString();
                            break;
                        case "DbTypeRead":
                            settings.DbTypeRead = reader.GetString();
                            break;
                        case "ActivePlayer":
                            settings.ActivePlayer = reader.GetString();
                            break;
                        case "TourneyType":
                            settings.TourneyType = reader.GetString();
                            break;

                    }
                }
            }

            return settings;
                       
        }

        public override void Write(Utf8JsonWriter writer, Settings value, JsonSerializerOptions options)
        {
            //writer.WriteStartArray();


            writer.WriteStartObject();

            writer.WriteString("Server", value.Server);
            writer.WriteString("Port", value.Port);
            writer.WriteString("User", value.User);
            writer.WriteString("Password", value.Password);

            writer.WriteString("SqlDatabase", value.SqlDatabase);
            writer.WriteString("NosqlDatabase", value.NosqlDatabase);
            writer.WriteString("DbTypeWrite", value.DbTypeWrite);
            writer.WriteString("DbTypeRead", value.DbTypeRead);

            writer.WriteString("MinTorney", value.MinTourney);
            writer.WriteString("RegFile", value.RegFile);
            writer.WriteString("HhSplitSize", value.HhSplitSize);
            writer.WriteString("CurrentDbRead", value.CurrentDbRead);
            writer.WriteString("ActivePlayer", value.ActivePlayer);
            writer.WriteString("TourneyType", value.TourneyType);

            // Write other properties if necessary

            writer.WriteEndObject();
            //writer.WriteEndArray();

        }
    }

}
