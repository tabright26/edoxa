import React, { FunctionComponent } from "react";
import { Card, CardBody, CardHeader } from "reactstrap";
import { withInvitations } from "store/root/organization/invitation/container";
import Item from "./Item/Item";
import { Invitation } from "types";
import Loading from "components/Shared/Loading";

const InvitationList: FunctionComponent<any> = ({ actions, invitations: { data, loading }, type }) => (
  <Card>
    <CardHeader>Invitations</CardHeader>
    <CardBody>{loading ? <Loading /> : data && data.map((invitation: Invitation, index: number) => <Item key={index} actions={actions} invitation={invitation} type={type} />)}</CardBody>
  </Card>
);
export default withInvitations(InvitationList);
