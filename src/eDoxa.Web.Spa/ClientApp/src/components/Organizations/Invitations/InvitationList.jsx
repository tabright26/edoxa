import React from "react";
import { Card, CardBody, CardHeader, Row, Col } from "reactstrap";

import { connectInvitations } from "store/root/organizations/invitations/container";

import InvitationItem from "./InvitationItem";

const InvitationList = ({ actions, invitations, type }) => (
  <Card>
    <CardHeader>Invitations</CardHeader>
    <CardBody>
      <Col>
        {invitations
          ? invitations.map((invitation, index) => (
              <Row key={index}>
                <InvitationItem actions={actions} invitation={invitation} type={type} />
              </Row>
            ))
          : null}
      </Col>
    </CardBody>
  </Card>
);
export default connectInvitations(InvitationList);
