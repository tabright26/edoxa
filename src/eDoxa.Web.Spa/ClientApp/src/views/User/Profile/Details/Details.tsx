import React, { Fragment } from "react";
import Informations from "./Informations";
import Email from "./Email";
import AddressBook from "./AddressBook";

const ProfileDetails = () => (
  <Fragment>
    <h5 className="text-uppercase my-4">PROFILE DETAILS</h5>
    <Informations className="my-4" />
    <Email className="my-4" />
    <AddressBook className="my-4" />
  </Fragment>
);

export default ProfileDetails;
