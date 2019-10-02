import React, { Fragment } from "react";
import { Card, CardBody, CardHeader, Row, Col } from "reactstrap";

import MemberItem from "./MemberItem";

const Members = ({ members }) => {
  return (
    <Card className="card-accent-primary">
      <CardHeader>Members</CardHeader>
      <CardBody className="p-1">
        <Col>
          {members.map((member, index) => (
            <Fragment>
              <Row className="mt-0 mb-1">
                <MemberItem member={member}></MemberItem>
              </Row>
              <hr className="mt-1 mb-0" />
            </Fragment>
          ))}
        </Col>
      </CardBody>
    </Card>
  );
};

export default Members;
