import React, { Fragment, Suspense } from "react";
import Loading from "components/Shared/Override/Loading";

const Phone = React.lazy(() => import("./Phone"));

const ProfileSecurity = () => (
  <Fragment>
    <h5 className="text-uppercase">PROFILE SECURITY</h5>
    <Suspense fallback={<Loading />}>
      <Phone className="my-4" />
    </Suspense>
  </Fragment>
);

export default ProfileSecurity;
