import React from "react";
import { Col } from "reactstrap";

const UserCandidatureItem = ({ candidature }) => {
  return <Col>{candidature.clanId}</Col>;
};

export default UserCandidatureItem;
