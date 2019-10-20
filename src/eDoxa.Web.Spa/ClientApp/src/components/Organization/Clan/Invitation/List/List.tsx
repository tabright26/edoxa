import React, { FunctionComponent } from "react";
import { Card, CardBody, CardHeader } from "reactstrap";

import { withInvitations } from "store/root/organizations/invitations/container";

import Item from "./Item/Item";

const InvitationList: FunctionComponent<any> = ({ actions, invitations, type }) => (
  <Card>
    <CardHeader>Invitations</CardHeader>
    <CardBody>{invitations && invitations.map((invitation, index: number) => <Item key={index} actions={actions} invitation={invitation} type={type} />)}</CardBody>
  </Card>
);
export default withInvitations(InvitationList);
