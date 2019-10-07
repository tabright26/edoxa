import React, { Fragment, useEffect, useState } from "react";
import { Col } from "reactstrap";

const ClanInvitationItem = ({ invitation, doxaTags }) => {
  const [doxaTag, setDoxaTag] = useState(null);

  useEffect(() => {
    if (doxaTags && invitation) {
      setDoxaTag(doxaTags.find(tag => tag.userId === invitation.userId));
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [doxaTags]);

  return (
    <Fragment>
      <Col>{doxaTag ? doxaTag.name : "Name"}</Col>
    </Fragment>
  );
};

export default ClanInvitationItem;
