﻿
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
    public class PlayerActionListConverter : JsonConverter<List<PlayerAction>>
    {
        public override List<PlayerAction> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.StartArray)
            {
                var actionList = new List<PlayerAction>();

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

        public override void Write(Utf8JsonWriter writer, List<PlayerAction> value, JsonSerializerOptions options)
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
}

