import React, { Fragment, useEffect, useState } from "react";
import { Badge, Col } from "reactstrap";

import InvitationForm from "forms/Organizations/Invitations";

const UserInvitationItem = ({ invitation, actions }) => {
  return (
    <Fragment>
      <Col xs="4" sm="4" md="4">
        <small className="text-muted">{invitation ? invitation.clanId : ""}</small>
      </Col>
      <Col xs="4" sm="4" md="4">
        <InvitationForm.Accept initialValues={{ candidatureId: invitation.id }} onSubmit={data => actions.acceptInvitation(data.candidatureId)} /> : ""}
      </Col>
      <Col xs="4" sm="4" md="4">
        <InvitationForm.Decline initialValues={{ candidatureId: invitation.id }} onSubmit={data => actions.declineInvitation(data.candidatureId)} /> : ""}
      </Col>
    </Fragment>
  );
};

export default UserInvitationItem;
