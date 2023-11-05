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

using System.Text.Json.Serialization;

namespace Deveel.Messaging {
	/// <summary>
	/// An implementation of <see cref="IMessage"/> that is used to
	/// represent a message that can be sent or received.
	/// </summary>
	public class Message : IMessage {
		/// <summary>
		/// Constructs an empty message instance.
		/// </summary>
		public Message() {
		}

		/// <summary>
		/// Constructs a message instance from the given <paramref name="message"/>.
		/// </summary>
		/// <param name="message">
		/// The message that is used as source of the new instance.
		/// </param>
		public Message(IMessage message) {
			Id = message.Id;
			TenantId = message.TenantId;
			Direction = message.Direction;
			Sender = new Terminal(message.Sender);
			Receiver = new Terminal(message.Receiver);
			Channel = new MessageChannel(message.Channel);
			Content = MessageContent.Create(message.Content);
			Options = message.Options?.ToDictionary(x => x.Key, x => x.Value);
			Context = message.Context?.ToDictionary(x => x.Key, x => x.Value);
			Properties = message.Properties?.ToDictionary(x => x.Key, x => x.Value);
			TimeStamp = message.TimeStamp;
		}

		/// <inheritdoc/>
		public string Id { get; set; }

		/// <inheritdoc/>
		public string? TenantId { get; set; }

		/// <inheritdoc/>
		public MessageDirection Direction { get; set; }

		/// <inheritdoc/>
		public Terminal Sender { get; set; }

		/// <inheritdoc/>
		public Terminal Receiver { get; set; }

		ITerminal IMessage.Sender => Sender;

		ITerminal IMessage.Receiver => Receiver;

		/// <inheritdoc/>
		public MessageChannel Channel { get; set; }

		IMessageChannel IMessage.Channel => Channel;

		/// <inheritdoc/>
		[JsonConverter(typeof(MessageContentJsonConverter))]
		public MessageContent Content { get; set; }

		IMessageContent IMessage.Content => Content;

		/// <inheritdoc/>
		public IDictionary<string, object>? Options { get; set; }
		
		/// <inheritdoc/>
		public IDictionary<string, object>? Context { get; set; }

		/// <inheritdoc/>
		public IDictionary<string, object>? Properties { get; set; }

		/// <inheritdoc/>
		public DateTimeOffset TimeStamp { get; set; }
	}
}
