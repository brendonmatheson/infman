/*
    Infrastructure Manager
	http://bren.cc/infman

	Copyright (c) 2016, Brendon Matheson

    This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public
    License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any
    later version.

    This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
    warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more
    details.

    You should have received a copy of the GNU General Public License along with this program; if not, write to the
    Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/

namespace cc.bren.infman.framework.eventing.impl
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A simple event router
    /// </summary>
    public class EventRouterImpl : EventRouter
    {
        private IDictionary<Type, IList<object>> _handlers;

        public EventRouterImpl()
        {
            _handlers = new Dictionary<Type, IList<object>>();
        }

        public void Fire<TEvent>(TEvent evt) where TEvent : Event
        {
            if (evt == null) { throw new ArgumentNullException("evt"); }

            foreach(Action<TEvent> handler in this.FindHandlers<TEvent>())
            {
                handler(evt);
            }
        }

        public void Register<TEvent>(Action<TEvent> handler) where TEvent : Event
        {
            if (handler == null) { throw new ArgumentNullException("handler"); }

            this.FindHandlers<TEvent>().Add(handler);
        }

        public void Unregister<TEvent>(Action<TEvent> handler) where TEvent : Event
        {
            if (handler == null) { throw new ArgumentNullException("handler"); }

            this.FindHandlers<TEvent>().Remove(handler);
        }

        private IList<object> FindHandlers<TEvent>()
        {
            IList<object> result = null;

            if (!_handlers.TryGetValue(typeof(TEvent), out result))
            {
                result = new List<object>();
                _handlers.Add(typeof(TEvent), result);
            }

            return result;
        }
    }
}
