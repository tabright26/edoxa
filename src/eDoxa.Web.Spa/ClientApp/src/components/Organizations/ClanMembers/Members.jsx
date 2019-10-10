import React, { useEffect } from "react";
import { Card, CardBody, CardHeader, Row, Col } from "reactstrap";

import { connectMembers } from "store/organizations/members/container";

import MemberItem from "./MemberItem";

const Members = ({ actions, members, clan, userId }) => {
  useEffect(() => {
    if (clan) {
      actions.loadMembers(clan.id);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [clan]);

  return (
    <Card>
      <CardHeader>Members</CardHeader>
      <CardBody>
        <Col>
          {members ? (
            members.map((member, index) => (
              <Row key={index}>
                <MemberItem member={member} actions={actions} clanId={clan ? clan.id : ""} withPermissions={clan ? clan.ownerId === userId : false}></MemberItem>
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

export default connectMembers(Members);
