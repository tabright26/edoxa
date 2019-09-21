import React, { Fragment, Suspense } from "react";
import Loading from "components/Shared/Loading";

import PersonalInfoCard from "./PersonalInfo";
import EmailCard from "./Email";
import PhoneNumberCard from "./PhoneNumber";
import DoxaTagCard from "./DoxaTag";
import AddressBookCard from "./AddressBook";

const ProfileDetails = () => (
  <Fragment>
    <h5>PROFILE DETAILS</h5>
    <Suspense fallback={<Loading.Default />}>
      <PersonalInfoCard className="card-accent-primary my-4" />
    </Suspense>
    <Suspense fallback={<Loading.Default />}>
      <EmailCard className="card-accent-primary my-4" />
    </Suspense>
    <Suspense fallback={<Loading.Default />}>
      <PhoneNumberCard className="card-accent-primary my-4" />
    </Suspense>
    <Suspense fallback={<Loading.Default />}>
      <DoxaTagCard className="card-accent-primary my-4" />
    </Suspense>
    <Suspense fallback={<Loading.Default />}>
      <AddressBookCard className="card-accent-primary my-4" />
    </Suspense>
  </Fragment>
);

export default ProfileDetails;
