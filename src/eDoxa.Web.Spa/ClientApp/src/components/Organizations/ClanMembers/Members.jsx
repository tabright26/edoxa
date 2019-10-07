import React, { useEffect, useState } from "react";
import { Card, CardBody, CardHeader, Row, Col } from "reactstrap";

import { connectMembers } from "store/organizations/members/container";

import MembersForm from "forms/Organizations/ClanMembers";

import MemberItem from "./MemberItem";

const Members = ({ actions, members, clanId, doxaTags, isOwner }) => {
  useEffect(() => {
    if (clanId) {
      actions.loadMembers(clanId);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [clanId]);

  return (
    <Card>
      <CardHeader>
        <Col>Members</Col>
        <Col>
          <MembersForm.LeaveClan initialValues={{ clanId: clanId }} onSubmit={data => actions.leaveClan(data.clanId)} />
        </Col>
      </CardHeader>
      <CardBody>
        {members
          ? members.map((member, index) => (
              <Row key={index}>
                <MemberItem member={member} actions={actions} doxaTags={doxaTags} isOwner={isOwner}></MemberItem>
              </Row>
            ))
          : ""}
      </CardBody>
    </Card>
  );
};

export default connectMembers(Members);
