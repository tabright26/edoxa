import React, { Fragment, Suspense } from "react";
import Loading from "components/Shared/Override/Loading";

const Informations = React.lazy(() => import("./Informations"));
const Email = React.lazy(() => import("./Email"));
const AddressBook = React.lazy(() => import("./AddressBook"));

const ProfileDetails = () => (
  <Fragment>
    <h5 className="text-uppercase">PROFILE DETAILS</h5>
    <Suspense fallback={<Loading />}>
      <Informations className="my-4" />
    </Suspense>
    <Suspense fallback={<Loading />}>
      <Email className="my-4" />
    </Suspense>
    <Suspense fallback={<Loading />}>
      <AddressBook className="my-4" />
    </Suspense>
  </Fragment>
);

export default ProfileDetails;
