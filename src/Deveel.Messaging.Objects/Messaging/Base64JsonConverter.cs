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

using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Deveel.Messaging {
	class Base64JsonConverter : JsonConverter<string> {
		public override string? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
			string? s;
			if (reader.TokenType != JsonTokenType.String || (s = reader.GetString()) == null)
				return null;

			var buffer = new byte[((s.Length * 3) + 3) / 4 -
				(s.Length > 0 && s[s.Length - 1] == '=' ?
				s.Length > 1 && s[s.Length - 2] == '=' ?
				2 : 1 : 0)];

			if (!Convert.TryFromBase64String(s, buffer, out var bytes))
				return null;

			return Encoding.UTF8.GetString(buffer);
		}

		public override void Write(Utf8JsonWriter writer, string value, JsonSerializerOptions options) {
			var bytes = Encoding.UTF8.GetBytes(value);
			var base64 = Convert.ToBase64String(bytes);

			writer.WriteStringValue(base64);
		}
	}
}
