import React, { Fragment, Suspense } from "react";
import Loading from "components/Shared/Override/Loading";

const PhoneNumber = React.lazy(() => import("./PhoneNumber"));

const ProfileSecurity = () => (
  <Fragment>
    <h5>PROFILE SECURITY</h5>
    <Suspense fallback={<Loading />}>
      <PhoneNumber className="card-accent-primary my-4" />
    </Suspense>
  </Fragment>
);

export default ProfileSecurity;
