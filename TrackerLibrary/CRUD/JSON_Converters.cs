
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TrackerLibrary.Models;
using static TrackerLibrary.Models.StreetAction;

namespace TrackerLibrary.CRUD
{
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
        //public override List<PlayerAction> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        //{
        //    if (reader.TokenType == JsonTokenType.StartArray)
        //    {
        //        var actionList = new List<PlayerAction>();

        //        while (reader.Read())
        //        {
        //            if (reader.TokenType == JsonTokenType.EndArray)
        //                break;

        //            if (reader.TokenType == JsonTokenType.StartObject)
        //            {
        //                var action = new PlayerAction();

        //                while (reader.Read())
        //                {
        //                    if (reader.TokenType == JsonTokenType.EndObject)
        //                        break;

        //                    if (reader.TokenType == JsonTokenType.PropertyName)
        //                    {
        //                        var propertyName = reader.GetString();

        //                        reader.Read();

        //                        switch (propertyName)
        //                        {
        //                            case "Act":
        //                                action.Act = reader.GetString();
        //                                break;
        //                            case "Size":
        //                                action.Size = reader.GetSingle();
        //                                break;
        //                            case "AI":
        //                                action.AI = reader.GetSByte();
        //                                break;
        //                                // Handle other properties if necessary
        //                        }
        //                    }
        //                }

        //                actionList.Add(action);
        //            }
        //        }

        //        return actionList;
        //    }

        //    throw new JsonException("Invalid JSON format for List<Action> property.");
        //}

        //public override void Write(Utf8JsonWriter writer, List<PlayerAction> value, JsonSerializerOptions options)
        //{
        //    writer.WriteStartArray();

        //    foreach (var action in value)
        //    {
        //        writer.WriteStartObject();

        //        writer.WriteString("Act", action.Act);
        //        writer.WriteNumber("Size", action.Size);
        //        writer.WriteNumber("AI", action.AI);

        //        // Write other properties if necessary

        //        writer.WriteEndObject();
        //    }

        //    writer.WriteEndArray();
        //}
    }

    public class SettingsConverter : JsonConverter<SettingsModel.Settings>
    {
        public override SettingsModel.Settings Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            SettingsModel.Settings settings = new SettingsModel.Settings(); 

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

                    }
                }
            }

            return settings;
                       
        }

        public override void Write(Utf8JsonWriter writer, SettingsModel.Settings value, JsonSerializerOptions options)
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

            // Write other properties if necessary

            writer.WriteEndObject();
            //writer.WriteEndArray();

        }
    }

}
