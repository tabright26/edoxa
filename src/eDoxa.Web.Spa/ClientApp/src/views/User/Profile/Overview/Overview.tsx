import React, { Fragment, Suspense } from "react";
import Loading from "components/Shared/Override/Loading";

const DoxaTag = React.lazy(() => import("./DoxaTag"));

const ProfileOverview = () => (
  <Fragment>
    <h5>PROFILE OVERVIEW</h5>
    <Suspense fallback={<Loading />}>
      <DoxaTag className="card-accent-primary my-4" />
    </Suspense>
  </Fragment>
);

export default ProfileOverview;
