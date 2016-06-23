/*
    Infrastructure Manager

	Copyright (c) 2016, Brendon Matheson

	http://bren.cc/infman

	This work is copyright.  You may not reproduce or transmit it any any
	form or by any means without permission in writing from the owner of this
	work, Brendon Matheson.  If you infringe our copyright, you render yourself
    liable for prosecution.
*/

namespace cc.bren.infman
{
    using cc.bren.infman.spec;
    using System.Collections.Generic;

    public interface InfManRepository
    {

        //
        // HostSpec
        //

        HostSpecEntity HostSpecSingle(HostSpecFilter filter);

        IList<HostSpecEntity> HostSpecList(HostSpecFilter filter);

        HostSpecEntity HostSpecInsert(HostSpecInsert request);

    }
}
