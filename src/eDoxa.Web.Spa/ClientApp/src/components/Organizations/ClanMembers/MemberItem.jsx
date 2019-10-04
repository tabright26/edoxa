import React, { Fragment } from "react";
import { Badge, Col } from "reactstrap";

import MembersForm from "forms/Organizations/ClanMembers";

const MemberItem = ({ member, actions, clanId }) => {
  return (
    <Fragment>
      <Col xs="4" sm="4" md="4">
        <small className="text-muted">{member.id}</small>
      </Col>
      <Col xs="3" sm="3" md="3">
        <Badge color="light">Rank</Badge>
      </Col>
      <Col xs="3" sm="3" md="3">
        <Badge href="#" color="success" pill>
          Online
        </Badge>
      </Col>
      <Col xs="2" sm="2" md="2">
        <MembersForm.KickMember initialValues={{ memberId: member.id, clanId: clanId }} onSubmit={data => actions.kickMember(data)} />
      </Col>
    </Fragment>
  );
};

export default MemberItem;
