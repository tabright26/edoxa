import React, { Fragment } from "react";
import { Badge, Col } from "reactstrap";

import MembersForm from "forms/Organizations/ClanMembers";

const MemberItem = ({ member, actions, clanId, withPermissions }) => {
  return (
    <Fragment>
      <Col>
        <small className="text-muted">{member.id}</small>
      </Col>
      <Col>{withPermissions ? <MembersForm.KickMember initialValues={{ memberId: member.id, clanId: clanId }} onSubmit={data => actions.kickMember(data.clanId, data.memberId)} /> : ""}</Col>
    </Fragment>
  );
};

export default MemberItem;
