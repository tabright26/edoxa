import React, { Fragment } from "react";
import { Card, CardBody, CardHeader, Row, Col } from "reactstrap";

import { connectInvitations } from "store/organizations/invitations/container";

import InvitationItem from "./InvitationItem";

const Invitations = ({ invitations }) => {
  return (
    <Card className="card-accent-primary">
      <CardHeader>Invitations</CardHeader>
      <CardBody className="p-1">
        <Col>
          {invitations.map((invitation, index) => (
            <Fragment>
              <Row className="mt-0 mb-1">
                <InvitationItem invitation={invitation}></InvitationItem>
              </Row>
              <hr className="mt-1 mb-0" />
            </Fragment>
          ))}
        </Col>
      </CardBody>
    </Card>
  );
};

export default connectInvitations(Invitations);
