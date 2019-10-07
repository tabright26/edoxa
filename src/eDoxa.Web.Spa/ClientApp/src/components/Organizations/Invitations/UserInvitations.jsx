import React, { Fragment, useEffect } from "react";
import { Card, CardBody, CardHeader, Row, Col } from "reactstrap";

import { connectInvitations } from "store/organizations/invitations/container";
import UserInvitationItem from "./UserInvitationItem";

const UserInvitations = ({ actions, invitations, userId }) => {
  useEffect(() => {
    actions.loadInvitationsWithUserId(userId);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [userId]);

  return (
    <Card className="card-accent-primary">
      <CardHeader>Invitations</CardHeader>
      <CardBody className="p-1">
        <Col>
          {invitations.map((invitation, index) => (
            <Row key={index}>
              <UserInvitationItem actions={actions} invitation={invitation}></UserInvitationItem>
            </Row>
          ))}
        </Col>
      </CardBody>
    </Card>
  );
};

export default connectInvitations(UserInvitations);
