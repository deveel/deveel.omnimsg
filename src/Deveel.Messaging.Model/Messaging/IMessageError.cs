using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deveel.Messaging {
	/// <summary>
	/// Describes an error that is associated to a message.
	/// </summary>
	public interface IMessageError {
		/// <summary>
		/// Gets the error code that is assigned
		/// by the messaging system.
		/// </summary>
		string Code { get; }

		/// <summary>
		/// Gets a descriptive message of the error.
		/// </summary>
		string? Message { get; }

		/// <summary>
		/// Gets a flag indicating if the error occurred
		/// in the local messaging system or in the remote
		/// provider of the messaging services.
		/// </summary>
		bool IsRemote { get; }
	}
}
