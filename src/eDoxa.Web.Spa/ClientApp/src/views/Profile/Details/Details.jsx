import React, { Fragment, Suspense } from "react";
import Loading from "../../../containers/Shared/Loading";

import PersonalInfoCard from "./PersonalInfo";
import EmailCard from "./Email";
import PhoneNumberCard from "./PhoneNumber";
import DoxaTagCard from "./DoxaTag";
import AddressBookCard from "./AddressBook";

import AddressModal from "../../../modals/Identity/Address";

const ProfileDetails = () => (
  <Fragment>
    <h5 className="my-4">PROFILE DETAILS</h5>
    <Suspense fallback={<Loading />}>
      <PersonalInfoCard className="card-accent-primary my-4" />
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
      <AddressModal.Create />
      <AddressModal.Update />
      <AddressModal.Delete />
    </Suspense>
  </Fragment>
);

export default ProfileDetails;
