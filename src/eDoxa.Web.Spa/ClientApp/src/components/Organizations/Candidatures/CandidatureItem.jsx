import React, { Fragment } from "react";
import { Badge, Col } from "reactstrap";

const CandidatureItem = ({ candidature }) => {
  return (
    <Fragment>
      <Col xs="6" sm="6" md="6">
        <small className="text-muted">{candidature.doxaTag}</small>
      </Col>
      <Col xs="3" sm="3" md="3">
        <Badge color="success">Accept</Badge>
      </Col>
      <Col xs="3" sm="3" md="3">
        <Badge color="danger">Decline</Badge>
      </Col>
    </Fragment>
  );
};

export default CandidatureItem;
