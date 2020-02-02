import React, { FunctionComponent } from "react";
import ProfilePanel from "components/Profile/Panel";
import EmailPanel from "components/Email/Panel";
import AddressPanel from "components/Address/List";

const ProfileDetails: FunctionComponent = () => (
  <>
    <h5 className="text-uppercase my-4">PROFILE DETAILS</h5>
    <ProfilePanel className="my-4" />
    <EmailPanel className="my-4" />
    <AddressPanel className="my-4" />
  </>
);

export default ProfileDetails;
