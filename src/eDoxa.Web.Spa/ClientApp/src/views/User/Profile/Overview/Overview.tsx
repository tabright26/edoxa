import React, { Fragment, Suspense } from "react";
import Loading from "components/Shared/Override/Loading";

const Doxatag = React.lazy(() => import("./Doxatag"));

const ProfileOverview = () => (
  <Fragment>
    <h5 className="text-uppercase">PROFILE OVERVIEW</h5>
    <Suspense fallback={<Loading />}>
      <Doxatag className="my-4" />
    </Suspense>
  </Fragment>
);

export default ProfileOverview;
