import React, { Fragment, useState, useEffect } from "react";
import { Col } from "reactstrap";

import MembersForm from "forms/Organizations/ClanMembers";

const MemberItem = ({ member, actions, doxaTags, isOwner }) => {
  const [doxaTag, setDoxaTag] = useState(null);

  useEffect(() => {
    if (doxaTags && member) {
      setDoxaTag(doxaTags.find(tag => tag.userId === member.userId));
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [doxaTags]);

  return (
    <Fragment>
      <Col>{doxaTag ? doxaTag.name : ""}</Col>
      {isOwner ? (
        <Col>
          <MembersForm.KickMember initialValues={{ memberId: member.id, clanId: member.clanId }} onSubmit={data => actions.kickMember(data.clanId, data.memberId)} />
        </Col>
      ) : (
        ""
      )}
    </Fragment>
  );
};

export default MemberItem;
