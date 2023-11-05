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

namespace Deveel.Messaging.Channels {
    class ReadOnlyChannelSchema : IChannelSchema {
        private readonly IChannelSchema schema;

        public ReadOnlyChannelSchema(IChannelSchema schema) {
            this.schema = schema;
        }

        public string Type => schema.Type;

        public string Provider => schema.Provider;

        public ChannelDirection Directions => schema.Directions;

        public IEnumerable<string> AllowedSenderTypes => schema.AllowedSenderTypes;

        public IEnumerable<string> RequiredSenderTypes => schema.RequiredSenderTypes;

        public IEnumerable<string> AllowedReceiverTypes => schema.AllowedReceiverTypes;

        public IEnumerable<string> RequiredReceiverTypes => schema.RequiredReceiverTypes;

        public IEnumerable<string> AllowedContentTypes => schema.AllowedContentTypes;

        public IEnumerable<string> CredentialTypes => schema.CredentialTypes;

        public IEnumerable<string> Options => schema.Options;
    }
}
