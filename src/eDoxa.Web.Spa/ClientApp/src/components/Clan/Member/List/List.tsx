import React from "react";
import { Card, CardBody, CardHeader, Button } from "reactstrap";
import { toastr } from "react-redux-toastr";

import { withClanMembers } from "store/root/organizations/members/container";

import Item from "./Item/Item";

const Members = ({ actions, members: { data }, userId, clanId, isOwner }) => {
  return (
    <Card>
      <CardHeader className="d-flex">
        <h3>Members</h3>
        <Button
          className="ml-auto"
          color="danger"
          onClick={() =>
            actions
              .leaveClan(clanId)
              .then(toastr.success("SUCCESS", "Why so salty."))
              .catch(toastr.error("WARNINGAVERTISSEMENTAVECLELOGODUFBIQUIDECOLEPUAVANTLEFILM", "Member was not kicked in the butt."))
          }
        >
          Leave clan
        </Button>
      </CardHeader>
      <CardBody>{data && data.map((member, index: number) => <Item key={index} member={member} actions={actions} isOwner={isOwner && userId !== member.userId}></Item>)}</CardBody>
    </Card>
  );
};

export default withClanMembers(Members);
