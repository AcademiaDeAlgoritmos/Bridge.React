﻿using System;
using System.Collections.Generic;

namespace Bridge.React
{
	public interface IDispatcher
	{
		/// <summary>
		/// Dispatches an action that will be sent to all callbacks registered with this dispatcher.
		/// </summary>
		/// <param name="action">The action to dispatch; may not be null.</param>
		void Dispatch(IDispatcherAction action);

		/// <summary>
		/// Registers a callback to receive actions dispatched through this dispatcher. For historical reasons, this is called Receive instead of Register - this interface originally only had a Register method
		/// that accepted a callback for DispatcherMessage instance, which were actions paired with a source of either View or Server and which were dispatched via methods HandleViewAction or HandleServerAction.
		/// These methods and the DispatcherMessage class are now considered obsolete, as is the Register method that receives DispatcherMessage instance. However, in order to avoid breaking existing code, the
		/// method that registers to receive unwrapped IDispatcherAction instances must be called a name other than Register (otherwise previously-compiling code could be afflicted by call-is-ambiguous errors).
		/// </summary>
		/// <param name="callback">The callback; may not be null.</param>
		/// <remarks>
		/// Actions will be sent to each receiver in the same order as which the receivers called Register.
		/// </remarks>
		DispatchToken Receive(Action<IDispatcherAction> callback);

		/// <summary>
		/// Unregisters the callback associated with the given token.
		/// </summary>
		/// <param name="token">The dispatch token to unregister; may not be null.</param>
		/// <remarks>
		/// This method cannot be called while a dispatch is in progress.
		/// </remarks>
		void Unregister(DispatchToken token);

		[Obsolete("Support for Actions attributed to different sources (i.e. View vs. Server actions) will be removed from the IDispatcher interface. Use the Dispatch method instead of HandleViewAction or HandleServerAction.")]
		void HandleServerAction(IDispatcherAction action);

		[Obsolete("Support for Actions attributed to different sources (i.e. View vs. Server actions) will be removed from the IDispatcher interface. Use the Dispatch method instead of HandleViewAction or HandleServerAction.")]
		void HandleViewAction(IDispatcherAction action);

		[Obsolete("Support for Actions attributed to different sources (i.e. View vs. Server actions) will be removed from the IDispatcher interface. Use the Receive(Action<IDispatcherAction>) method instead of Register(Action<DispatcherMessage>).")]
		DispatchToken Register(Action<DispatcherMessage> callback);

		/// <summary>
		/// Waits for the callbacks associated with the given tokens to be called first during a dispatch operation.
		/// </summary>
		/// <param name="tokens">The tokens to wait on; may not be null.</param>
		/// <remarks>
		/// This method can only be called while a dispatch is in progress.
		/// </remarks>
		void WaitFor(IEnumerable<DispatchToken> tokens);

		/// <summary>
		/// Waits for the callbacks associated with the given tokens to be called first during a dispatch operation.
		/// </summary>
		/// <param name="tokens">The tokens to wait on; may not be null.</param>
		/// <remarks>
		/// This method can only be called while a dispatch is in progress.
		/// </remarks>
		void WaitFor(params DispatchToken[] tokens);
	}
}