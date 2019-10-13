import React, { Fragment, Suspense } from "react";
import Loading from "components/Shared/Override/Loading";

import PersonalInfo from "./PersonalInfo";
import EmailCard from "./Email";
import PhoneNumberCard from "./PhoneNumber";
import DoxaTagCard from "./DoxaTag";
import AddressBookCard from "./AddressBook";

const ProfileDetails = () => (
  <Fragment>
    <h5>PROFILE DETAILS</h5>
    <Suspense fallback={<Loading />}>
      <PersonalInfo className="card-accent-primary my-4" />
    </Suspense>
    <Suspense fallback={<Loading />}>
      <EmailCard className="card-accent-primary my-4" />
    </Suspense>
    <Suspense fallback={<Loading />}>
      <PhoneNumberCard className="card-accent-primary my-4" />
    </Suspense>
    <Suspense fallback={<Loading />}>
      <DoxaTagCard className="card-accent-primary my-4" />
    </Suspense>
    <Suspense fallback={<Loading />}>
      <AddressBookCard className="card-accent-primary my-4" />
    </Suspense>
  </Fragment>
);

export default ProfileDetails;
