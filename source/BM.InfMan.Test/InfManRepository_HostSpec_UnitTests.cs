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
    public interface InfManRepository_HostSpec_UnitTests
    {

        //
        // HostSpecSingle
        //

        void HostSpecSingle_ValidRequest_Succeeds();

        void HostSpecSingle_NullFilter_Throws();

        //
        // HostSpecInsert
        //

        void HostSpecInsert_ValidRequest_Succeeds();

    }
}
