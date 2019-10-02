import React, { Fragment } from "react";
import { Badge, Col } from "reactstrap";

const MemberItem = ({ member }) => {
  return (
    <Fragment>
      <Col xs="6" sm="6" md="6">
        <small className="text-muted">{member.doxaTag}</small>
      </Col>
      <Col xs="3" sm="3" md="3">
        <Badge color="light">Rank</Badge>
      </Col>
      <Col xs="3" sm="3" md="3">
        <Badge href="#" color="success" pill>
          Online
        </Badge>
      </Col>
    </Fragment>
  );
};

export default MemberItem;
