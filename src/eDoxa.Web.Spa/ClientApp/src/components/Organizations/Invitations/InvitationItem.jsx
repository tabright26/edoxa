import React, { Fragment } from "react";
import { Badge, Col } from "reactstrap";

const InvitationItem = ({ invitation }) => {
  return (
    <Fragment>
      <Col xs="6" sm="6" md="6">
        <small className="text-muted">{invitation.name}</small>
      </Col>
      <Col xs="3" sm="3" md="3">
        <Badge href="#" color="success" pill>
          Accept
        </Badge>
        <Badge href="#" color="success" pill>
          Decline
        </Badge>
      </Col>
    </Fragment>
  );
};

export default InvitationItem;
