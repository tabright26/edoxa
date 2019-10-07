import React, { useEffect } from "react";
import { Col } from "reactstrap";

import { connectInvitations } from "store/organizations/invitations/container";

import InvitationForm from "forms/Organizations/Invitations";

const InvitationWidget = ({ actions, invitations, clanId }) => {
  useEffect(() => {
    if (clanId) {
      actions.loadInvitationsWithClanId(clanId);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [clanId]);

  const handleAddInvitation = data => {
    if (actions && invitations) {
      var memberFound = false;

      invitations.forEach(invitation => {
        if (invitation.userId === data.userId) {
          memberFound = true;
        }
      });

      if (!memberFound) {
        actions.addInvitation(data);
      }
    }
  };

  return (
    <Col>
      <InvitationForm.Create initialValues={{ clanId: clanId }} invitations={invitations} onSubmit={data => handleAddInvitation(data)} />
    </Col>
  );
};

export default connectInvitations(InvitationWidget);
