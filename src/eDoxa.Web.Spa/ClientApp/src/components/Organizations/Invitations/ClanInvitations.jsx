import React, { Fragment, useEffect } from "react";
import { Card, CardBody, CardHeader, Row, Col } from "reactstrap";

import { connectInvitations } from "store/organizations/invitations/container";

import ClanInvitationItem from "./ClanInvitationItem";

const ClanInvitations = ({ actions, invitations, clan, doxaTags }) => {
  useEffect(() => {
    if (clan) {
      actions.loadInvitationsWithClanId(clan.id);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [clan]);

  return (
    <Card>
      <CardHeader>Invitations</CardHeader>
      <CardBody>
        <Col>
          {invitations ? (
            invitations.map((invitation, index) => (
              <Row key={index}>
                <ClanInvitationItem invitation={invitation} doxaTags={doxaTags}></ClanInvitationItem>
              </Row>
            ))
          ) : (
            <Row></Row>
          )}
        </Col>
      </CardBody>
    </Card>
  );
};

export default connectInvitations(ClanInvitations);
