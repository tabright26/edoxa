import React, { Fragment, Suspense } from "react";
import Loading from "components/Shared/Override/Loading";

const PersonalInfo = React.lazy(() => import("./PersonalInfo"));
const Email = React.lazy(() => import("./Email"));
const AddressBook = React.lazy(() => import("./AddressBook"));

const ProfileDetails = () => (
  <Fragment>
    <h5>PROFILE DETAILS</h5>
    <Suspense fallback={<Loading />}>
      <PersonalInfo className="card-accent-primary my-4" />
    </Suspense>
    <Suspense fallback={<Loading />}>
      <Email className="card-accent-primary my-4" />
    </Suspense>
    <Suspense fallback={<Loading />}>
      <AddressBook className="card-accent-primary my-4" />
    </Suspense>
  </Fragment>
);

export default ProfileDetails;
