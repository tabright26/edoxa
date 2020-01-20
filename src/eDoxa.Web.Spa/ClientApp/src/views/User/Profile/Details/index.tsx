import React from "react";
import ProfileCard from "components/User/Profile/Card";
import EmailCard from "components/User/Email/Card";
import AddressBookCard from "components/User/Address/List";

const ProfileDetails = () => (
  <>
    <h5 className="text-uppercase my-4">PROFILE DETAILS</h5>
    <ProfileCard className="my-4" />
    <EmailCard className="my-4" />
    <AddressBookCard className="my-4" />
  </>
);

export default ProfileDetails;
