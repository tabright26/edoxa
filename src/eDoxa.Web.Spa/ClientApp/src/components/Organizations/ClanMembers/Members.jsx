import React from "react";
import { Card, CardBody, CardHeader, Row, Col, Button } from "reactstrap";
import { toastr } from "react-redux-toastr";

import { connectMembers } from "store/organizations/members/container";

import MemberItem from "./MemberItem";

const Members = ({ actions, members, userId, clanId, isOwner }) => {
  return (
    <Card>
      <CardHeader>
        <Col>Members</Col>
        <Col>
          <Button
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
        </Col>
      </CardHeader>
      <CardBody>
        {members
          ? members.map((member, index) => (
              <Row key={index}>
                <MemberItem member={member} actions={actions} isOwner={isOwner && userId !== member.userId}></MemberItem>
              </Row>
            ))
          : null}
      </CardBody>
    </Card>
  );
};

export default connectMembers(Members);
