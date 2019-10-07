import React, { useEffect } from "react";
import { Card, CardBody, CardHeader, Row, Col } from "reactstrap";

import { connectInvitations } from "store/organizations/invitations/container";

import ClanInvitationItem from "./ClanInvitationItem";

const ClanInvitations = ({ actions, invitations, clanId, doxaTags }) => {
  useEffect(() => {
    if (clanId) {
      actions.loadInvitationsWithClanId(clanId);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [clanId]);

  return (
    <Card>
      <CardHeader>Invitations</CardHeader>
      <CardBody>
        <Col>
          {invitations
            ? invitations.map((invitation, index) => (
                <Row key={index}>
                  <ClanInvitationItem invitation={invitation} doxaTags={doxaTags}></ClanInvitationItem>
                </Row>
              ))
            : ""}
        </Col>
      </CardBody>
    </Card>
  );
};

export default connectInvitations(ClanInvitations);
