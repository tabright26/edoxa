import React, { Fragment, useEffect } from "react";
import { Card, CardBody, CardHeader, Row, Col } from "reactstrap";

import { connectMembers } from "store/organizations/members/container";

import MemberItem from "./MemberItem";

const Members = ({ actions, members, clanId }) => {
  useEffect(() => {
    actions.loadMembers(clanId);
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <Card className="card-accent-primary">
      <CardHeader>Members</CardHeader>
      <CardBody className="p-1">
        <Col>
          {members
            ? members.map((member, index) => (
                <Fragment>
                  <Row className="mt-0 mb-1">
                    <MemberItem member={member} actions={actions} />
                  </Row>
                  <hr className="mt-1 mb-0" />
                </Fragment>
              ))
            : ""}
        </Col>
      </CardBody>
    </Card>
  );
};

export default connectMembers(Members);
