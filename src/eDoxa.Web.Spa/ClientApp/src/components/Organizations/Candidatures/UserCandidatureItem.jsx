import React, { Fragment, useEffect, useState } from "react";
import { Badge, Col } from "reactstrap";

const UserCandidatureItem = ({ candidature }) => {
  return (
    <Fragment>
      <Col xs="6" sm="6" md="6">
        <small className="text-muted">{candidature.clanId}</small>
      </Col>
    </Fragment>
  );
};

export default UserCandidatureItem;
