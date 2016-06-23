/*
    Infrastructure Manager

	Copyright (c) 2016, Brendon Matheson

	http://bren.cc/infman

	This work is copyright.  You may not reproduce or transmit it any any
	form or by any means without permission in writing from the owner of this
	work, Brendon Matheson.  If you infringe our copyright, you render yourself
    liable for prosecution.
*/

namespace cc.bren.infman.spec
{
    using System;

    public class HostSpecFactory : HostSpecEntity, HostSpecInsert
    {
        public static HostSpecEntity Entity(
            Guid hostSpecId,
            string name)
        {
            return new HostSpecFactory(hostSpecId, name);
        }

        private HostSpecFactory(
            Guid hostSpecId,
            string name)
        {
            if (name == null) { throw new ArgumentNullException("name"); }

            this.Name = name;
        }

        public static HostSpecInsert Insert(
            string name)
        {
            return new HostSpecFactory(name);
        }

        public HostSpecFactory(
            string name)
        {
            if (name == null) { throw new ArgumentNullException("name"); }

            this.Name = name;
        }

        public Guid HostSpecId { get; private set; }

        public string Name { get; private set; }
    }
}
