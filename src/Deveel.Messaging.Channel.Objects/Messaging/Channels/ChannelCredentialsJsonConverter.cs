// Copyright 2023 Deveel AS
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Deveel.Messaging.Channels {
	class ChannelCredentialsJsonConverter : JsonConverter<ChannelCredentials> {
		public override ChannelCredentials? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
			// Read the type discriminator
			if (!reader.Read() || reader.TokenType != JsonTokenType.PropertyName || reader.GetString() != "$type")
				throw new JsonException("Unable to find the '$type' property");

			// Read the type value
			if (!reader.Read() || reader.TokenType != JsonTokenType.String)
				throw new JsonException("Unable to read the type value");

			var typeValue = reader.GetString();

			if (string.IsNullOrWhiteSpace(typeValue))
				throw new JsonException("The type value is not valid");

			// Read the credentials
			if (!reader.Read() || reader.TokenType != JsonTokenType.PropertyName || reader.GetString() != "credentials")
				throw new JsonException("Unable to find the 'credentials' property");

			if (!reader.Read() || reader.TokenType != JsonTokenType.StartObject)
				throw new JsonException("Unable to read the credentials object");

			// Read the credentials value
			ChannelCredentials? credentials = typeValue switch {
				KnownChannelCredentialsTypes.ApiKey => JsonSerializer.Deserialize<ApiKeyChannelCredentials>(ref reader, options),
				KnownChannelCredentialsTypes.BasicAuth => JsonSerializer.Deserialize<BasicAuthChannelCredentials>(ref reader, options),
				KnownChannelCredentialsTypes.Token => JsonSerializer.Deserialize<TokenChannelCredentials>(ref reader, options),
				_ => throw new NotSupportedException($"The credentials of type '{typeValue}' is not supported")
			};

			if (credentials == null)
				throw new JsonException("The credentials object is not valid");

			// Read the end of the object
			if (!reader.Read() || reader.TokenType != JsonTokenType.EndObject)
				throw new JsonException("Unable to read the end of the object");

			return credentials;

		}

		public override void Write(Utf8JsonWriter writer, ChannelCredentials value, JsonSerializerOptions options) {
			var typeValue = value switch {
				ApiKeyChannelCredentials _ => KnownChannelCredentialsTypes.ApiKey, 
				BasicAuthChannelCredentials _ => KnownChannelCredentialsTypes.BasicAuth,
				TokenChannelCredentials _ => KnownChannelCredentialsTypes.Token,
				_ => throw new NotSupportedException($"The credentials of type '{value.GetType().Name}' is not supported")
			};

			writer.WriteStartObject();
			writer.WriteString("$type", typeValue);
			writer.WritePropertyName("credentials");
			JsonSerializer.Serialize(writer, value, value.GetType(), options);
			writer.WriteEndObject();
		}
	}
}
